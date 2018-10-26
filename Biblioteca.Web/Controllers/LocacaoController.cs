using Biblioteca.Dominio;
using Biblioteca.Web.Classes;
using Biblioteca.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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

            LocacaoViewModel viewModel = new LocacaoViewModel();

            return View(viewModel);
        }

        public IActionResult Gravar(LocacaoViewModel viewModel)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            var repo = new Repositorio.Repositorio();
            var tipoProduto = repo.SelecionarProdutoTipoPorId(repo.SelecionarProdutoPorId(viewModel.IdProduto).IdProdutoTipo);
            var dtLimiteDevolucao = DateTime.Now.AddDays(tipoProduto.Prazo);

            Locacao locacao = new Locacao()
            {
                IdUsuario = Sessao.GetIdUsuario(HttpContext),
                IdProduto = viewModel.IdProduto,
                IdCliente = viewModel.IdCliente,
                IdLocacao = viewModel.IdLocacao,
                DtDevolucao = viewModel.DtDevolucao,
                DtLimiteDevolucao = dtLimiteDevolucao,
                DtLocacao = DateTime.Now
            };

            var retorno = repo.GravarLocacao(locacao);

            if (retorno == string.Empty)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Json(new { Result = false, Message = retorno });
            }
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