using Biblioteca.Dominio;
using Biblioteca.Utils;
using Biblioteca.Web.Classes;
using Biblioteca.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Web.Controllers
{
    public class ProdutoController : Controller
    {
        #region Produto
        public ActionResult CadProduto()
        {
            IRepositorio repositorio = new Repositorio.Repositorio();

            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            List<Produto> produtos = repositorio.SelecionarProdutos();
            List<ProdutoExibicaoViewModel> produtosExibicao = new List<ProdutoExibicaoViewModel>();
            foreach (Produto prod in produtos)
            {
                produtosExibicao.Add(new ProdutoExibicaoViewModel()
                {
                    IdProduto = prod.IdProduto,
                    Descricao = prod.Descricao,
                    Ativo = prod.Ativo,
                    Quantidade = prod.Quantidade,
                    Autor = repositorio.SelecionarNomeAutor(prod.IdAutor),
                    Editora = repositorio.SelecionarNomeEditora(prod.IdEditora),
                    ProdutoTipo = repositorio.SelecionarDescricaoProdutoTipo(prod.IdProdutoTipo),
                    AnoPublicacao = prod.AnoPublicacao,
                    Isbn = prod.Isbn
                });
            }


            return View(produtosExibicao);
        }

        public ActionResult EditarProduto(int id)
        {
            IRepositorio repositorio = new Repositorio.Repositorio();
            ProdutoViewModel produtoViewModel = new ProdutoViewModel();

            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            if (id == 0)
            {
                ViewBag.Adicionar = true;
                return View(produtoViewModel);
            }
            else
            {
                ViewBag.Adicionar = false;
            }

            Produto produto = repositorio.SelecionarProdutoPorId(id);

            MapperUtils.Mapear(produto, produtoViewModel);

            return View(produtoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProduto(ProdutoViewModel produtoViewModel)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            if (produtoViewModel != null && ModelState.IsValid)
            {
                IRepositorio repositorio = new Repositorio.Repositorio();

                Produto produto = new Produto();
                MapperUtils.Mapear(produtoViewModel, produto);

                repositorio.AtualizarProduto(produto);
            }
            else
            {
                ModelState.AddModelError("erro_binding", "Erro nos parâmetros informados");
                return RedirectToAction("EditarProduto", "Produto");
            }
            return RedirectToAction("CadProduto", "Produto");
        }

        public JsonResult ListarProdutos()
        {
            IRepositorio repositorio = new Repositorio.Repositorio();
            List<Produto> produtos = repositorio.SelecionarProdutos();
            List<ProdutoExibicaoViewModel> viewModel = new List<ProdutoExibicaoViewModel>();

            MapperUtils.MapearLista(produtos, viewModel);
            for (int i = 0; i < viewModel.Count; i++)
            {
                viewModel[i].Autor = repositorio.SelecionarNomeAutor(produtos[i].IdAutor);
                viewModel[i].Editora = repositorio.SelecionarNomeEditora(produtos[i].IdEditora);
                viewModel[i].ProdutoTipo = repositorio.SelecionarDescricaoProdutoTipo(produtos[i].IdProdutoTipo);
            }

            return Json(new { data = viewModel } );
        }
        #endregion

        #region Tipo de Produto
        public Task<ActionResult> CadProdutoTipo()
        {
            return Task.Run(() =>
            {
                IRepositorio repositorio = new Repositorio.Repositorio();

                ActionResult action = ValidarLogin(HttpContext);
                if (action != null)
                {
                    return action;
                }

                List<ProdutoTipo> tiposProduto = repositorio.SelecionarTiposProdutos();

                return View(tiposProduto);
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProdutoTipo(ProdutoTipoViewModel produtoTipo)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            if (produtoTipo != null && ModelState.IsValid)
            {
                IRepositorio repositorio = new Repositorio.Repositorio();

                ProdutoTipo produtoAlterar = new ProdutoTipo()
                {
                    IdProdutoTipo = produtoTipo.IdProdutoTipo ?? 0,
                    Descricao = produtoTipo.Descricao,
                    Prazo = produtoTipo.Prazo,
                    VlMulta = produtoTipo.VlMulta,
                    Ativo = produtoTipo.Ativo
                };

                repositorio.AtualizarProdutoTipo(produtoAlterar);
            }

            return RedirectToAction("CadProdutoTipo", "Produto");
        }

        public ActionResult EditarProdutoTipo(int id)
        {
            IRepositorio repositorio = new Repositorio.Repositorio();

            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            if (id == 0)
            {
                ViewBag.Adicionar = true;
                return View();
            }

            ProdutoTipo produtoTipo = repositorio.SelecionarProdutoTipoPorId(id);
            ProdutoTipoViewModel model = new ProdutoTipoViewModel()
            {
                IdProdutoTipo = produtoTipo.IdProdutoTipo,
                Descricao = produtoTipo.Descricao,
                Prazo = produtoTipo.Prazo,
                VlMulta = produtoTipo.VlMulta,
                Ativo = produtoTipo.Ativo
            };

            ViewBag.Adicionar = false;

            return View(model);
        }
        #endregion

        #region Editora
        public ActionResult CadEditora()
        {
            IRepositorio repositorio = new Repositorio.Repositorio();

            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            List<Editora> editoras = repositorio.SelecionarEditoras();

            return View(editoras);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarEditora(EditoraViewModel editoraViewModel)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            if (editoraViewModel != null && ModelState.IsValid)
            {
                IRepositorio repositorio = new Repositorio.Repositorio();

                Editora editora = new Editora()
                {
                    IdEditora = editoraViewModel.IdEditora ?? 0,
                    Nome = editoraViewModel.Nome
                };

                repositorio.AtualizarEditora(editora);
            }

            return RedirectToAction("CadEditora", "Produto");
        }

        public ActionResult EditarEditora(int id)
        {
            IRepositorio repositorio = new Repositorio.Repositorio();

            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            if (id == 0)
            {
                ViewBag.Adicionar = true;
                return View();
            }

            Editora editora = repositorio.SelecionarEditoraPorId(id);
            EditoraViewModel model = new EditoraViewModel()
            {
                IdEditora = editora.IdEditora,
                Nome = editora.Nome
            };

            ViewBag.Adicionar = false;

            return View(model);
        }
        #endregion

        #region Autor
        public ActionResult CadAutor()
        {
            IRepositorio repositorio = new Repositorio.Repositorio();

            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            List<Autor> autores = repositorio.SelecionarAutores();
            List<AutorViewModel> viewModel = new List<AutorViewModel>();
            foreach (var autor in autores)
            {
                viewModel.Add(new AutorViewModel()
                {
                    IdAutor = autor.IdAutor,
                    Nome = autor.Nome,
                    DtNascimento = autor.DtNascimento.HasValue ? autor.DtNascimento.Value.ToString("dd/MM/yyyy") : ""
                });
            }

            return View(viewModel);
        }

        public ActionResult EditarAutor(int id)
        {
            IRepositorio repositorio = new Repositorio.Repositorio();

            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            if (id == 0)
            {
                ViewBag.Adicionar = true;
                return View();
            }

            Autor autor = repositorio.SelecionarAutorPorId(id);

            AutorViewModel model = new AutorViewModel()
            {
                IdAutor = autor.IdAutor,
                Nome = autor.Nome,
                DtNascimento = autor.DtNascimento.HasValue ? autor.DtNascimento.Value.ToString("yyyy-MM-dd") : ""
            };

            ViewBag.Adicionar = false;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarAutor(AutorViewModel autorViewModel)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            if (autorViewModel != null && ModelState.IsValid)
            {
                IRepositorio repositorio = new Repositorio.Repositorio();

                Autor autor = new Autor()
                {
                    IdAutor = autorViewModel.IdAutor ?? 0,
                    Nome = autorViewModel.Nome,
                    DtNascimento = string.IsNullOrEmpty(autorViewModel.DtNascimento) ? null : (DateTime?)Convert.ToDateTime(autorViewModel.DtNascimento)
                };

                repositorio.AtualizarAutor(autor);
            }

            return RedirectToAction("CadAutor", "Produto");
        }
        #endregion

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