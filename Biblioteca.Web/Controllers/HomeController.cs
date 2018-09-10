using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Web.Models;
using Biblioteca.Web.Classes;

namespace Biblioteca.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!Sessao.SessaoAtiva(HttpContext))
            {
                return RedirectToAction("", "Login");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
