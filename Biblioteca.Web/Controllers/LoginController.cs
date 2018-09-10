using Biblioteca.Web.Classes;
using Biblioteca.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            else
            {
                int id = new Repositorio.Repositorio().RetornarIdUsuario(login.Usuario, login.Senha);
                if (id > 0)
                {
                    Sessao.Gravar(HttpContext, login.Usuario, id);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Usuário ou senha inválido(s)!");
                    return View(login);
                }
            }
        }

        public ActionResult Logoff(int id)
        {
            Sessao.Limpar(HttpContext);
            return RedirectToAction("Index", "Home");
        }
    }
}