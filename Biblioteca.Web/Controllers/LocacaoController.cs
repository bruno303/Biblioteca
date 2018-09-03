using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Web.Controllers
{
    public class LocacaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}