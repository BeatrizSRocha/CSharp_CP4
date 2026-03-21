using ConsultoriaDev.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConsultoriaDev.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string usuario, string senha)
        {
            if (usuario == "Admin" && senha == "123456")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Tech Lead")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));

                return RedirectToAction("Dashboard");
            }

            ViewBag.Erro = "Usuário ou senha inválidos!";
            return View();
        }

        private static List<Solicitacao> _solicitacoes = new List<Solicitacao>();

        [HttpPost]
        public IActionResult SalvarSolicitacao(string nome, string email, string problema, int tempoResposta)
        {
            var novaSolicitacao = new Solicitacao
            {
                NomeCliente = nome,
                EmailCliente = email,
                DescricaoProblema = problema,
                TempoResposta = tempoResposta,
                Status = "Pendente"
            };

            _solicitacoes.Add(novaSolicitacao);

            return Json(new { success = true });
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            return View(_solicitacoes);
        }

        [Authorize]
        public IActionResult Aprovar(int id)
        {
            if (id >= 0 && id < _solicitacoes.Count)
            {
                _solicitacoes[id].Status = "Aprovado";
            }

            return RedirectToAction("Dashboard");
        }

        [Authorize]
        public IActionResult Reprovar(int id)
        {
            if (id >= 0 && id < _solicitacoes.Count)
            {
                _solicitacoes[id].Status = "Reprovado";
            }

            return RedirectToAction("Dashboard");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}