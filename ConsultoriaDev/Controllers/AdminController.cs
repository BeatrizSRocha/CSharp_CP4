using Microsoft.AspNetCore.Mvc;

namespace ConsultoriaDev.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
