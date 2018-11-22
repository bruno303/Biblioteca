using Biblioteca.Dominio;
using Biblioteca.Web.Classes;
using Biblioteca.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Repo = Biblioteca.Repositorio.Repositorio;

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

            var viewModel = new List<LocacaoExibicaoViewModel>();
            var repositorio = new Repo();

            var locacoes = repositorio.SelecionarLocacoes();
            foreach (var locacao in locacoes)
            {
                LocacaoExibicaoViewModel item = new LocacaoExibicaoViewModel
                {
                    IdLocacao = locacao.IdLocacao.Value,
                    DtLocacao = locacao.DtLocacao.ToString("dd/MM/yyyy HH:mm:ss"),
                    DtDevolucao = ((!locacao.DtDevolucao.HasValue) || (locacao.DtDevolucao.Value == new DateTime())) ? "" : locacao.DtDevolucao.Value.ToString("dd/MM/yyyy"),
                    DtLimiteDevolucao = locacao.DtLimiteDevolucao.ToString("dd/MM/yyyy"),
                    Cliente = repositorio.SelecionarNomeCliente(locacao.IdCliente),
                    Usuario = repositorio.SelecionarNomeUsuario(locacao.IdUsuario),
                    Produto = repositorio.SelecionarNomeProduto(locacao.IdProduto),
                    Atrasado = locacao.DtLimiteDevolucao < DateTime.Now && (locacao.DtDevolucao == null || locacao.DtDevolucao == new DateTime())
                };
                viewModel.Add(item);
            }

            return View(viewModel);
        }

        public IActionResult NovaLocacao(int id)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            LocacaoViewModel viewModel = new LocacaoViewModel();
            ViewBag.Adicionar = true;

            return View(viewModel);
        }

        public IActionResult Editar(int id)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            Repo repositorio = new Repo();

            LocacaoViewModel viewModel = new LocacaoViewModel();
            Locacao locacao = repositorio.SelecionarLocacaoPorId(id);

            viewModel.IdCliente = locacao.IdCliente;
            viewModel.IdLocacao = locacao.IdLocacao;
            viewModel.IdProduto = locacao.IdProduto;
            viewModel.DtDevolucao = (locacao.DtDevolucao.HasValue && (locacao.DtDevolucao.Value != new DateTime()))? locacao.DtDevolucao.Value.ToString("yyyy-MM-dd") : "";
            viewModel.DtLimiteDevolucao = locacao.DtLimiteDevolucao.ToString("yyyy-MM-dd");

            ViewBag.Adicionar = false;

            return View("NovaLocacao", viewModel);
        }

        public IActionResult Gravar(LocacaoViewModel viewModel)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            var repo = new Repo();
            var tipoProduto = repo.SelecionarProdutoTipoPorId(repo.SelecionarProdutoPorId(viewModel.IdProduto).IdProdutoTipo);
            var dtLimiteDevolucao = !string.IsNullOrEmpty(viewModel.DtLimiteDevolucao) ? Convert.ToDateTime(viewModel.DtLimiteDevolucao) : DateTime.Now.AddDays(tipoProduto.Prazo);

            Locacao locacao = new Locacao()
            {
                IdUsuario = Sessao.GetIdUsuario(HttpContext),
                IdProduto = viewModel.IdProduto,
                IdCliente = viewModel.IdCliente,
                IdLocacao = viewModel.IdLocacao,
                DtDevolucao = string.IsNullOrEmpty(viewModel.DtDevolucao) ? null : (DateTime?)(Convert.ToDateTime(viewModel.DtDevolucao)),
                DtLimiteDevolucao = dtLimiteDevolucao,
                DtLocacao = DateTime.Now
            };

            var retorno = repo.GravarLocacao(locacao);

            if (retorno == string.Empty)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Json(new { Result = false, Message = retorno });
            }
        }

        public IActionResult Devolver(int id)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            var repo = new Repo();

            Locacao locacao = repo.SelecionarLocacaoPorId(id);
            var retorno = repo.DevolverLocacao(locacao);

            if (retorno == string.Empty)
            {
                return RedirectToAction("Index");
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