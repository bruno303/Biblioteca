using Biblioteca.Web.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Web.Controllers
{
    public class LocacaoController : Controller
    {
        public IActionResult Index()
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            return View();
        }

        /// <summary>
        /// Verifica se o usuário está logado no sistema.
        /// </summary>
        /// <param name="context">Context a ser verificado.</param>
        /// <returns>ActionResult da tela de login, caso não esteja logado no sistema. Retorna null caso já esteja logado.</returns>
        private ActionResult ValidarLogin(HttpContext context)
        {
            if (!Sessao.SessaoAtiva(context))
            {
                return RedirectToAction("", "Login");
            }
            else
            {
                return null;
            }
        }
    }
}