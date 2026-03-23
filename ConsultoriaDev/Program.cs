using Microsoft.AspNetCore.Authentication.Cookies;

// Garante wwwroot mesmo quando o diretório de trabalho é a pasta da solução (ex.: dotnet run a partir da raiz).
static string ResolveContentRoot()
{
    var baseDir = AppContext.BaseDirectory;
    if (Directory.Exists(Path.Combine(baseDir, "wwwroot")))
        return baseDir;

    for (var dir = new DirectoryInfo(baseDir); dir != null; dir = dir.Parent)
    {
        if (Directory.Exists(Path.Combine(dir.FullName, "wwwroot")))
            return dir.FullName;
    }

    return Directory.GetCurrentDirectory();
}

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = ResolveContentRoot()
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
