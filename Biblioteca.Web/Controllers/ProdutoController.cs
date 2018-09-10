using Biblioteca.Dominio;
using Biblioteca.Web.Classes;
using Biblioteca.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace Biblioteca.Web.Controllers
{
    public class ProdutoController : Controller
    {
        public ActionResult CadProduto()
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            List<Produto> produtos = new Repositorio.Repositorio().SelecionarProdutos();
            return View(produtos);
        }

        #region Tipo de Produto
        public ActionResult CadProdutoTipo(int? page = 0)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            if (page == null || page == 0)
            {
                page = 1;
            }
            IQueryable<ProdutoTipo> tiposProduto = new Repositorio.Repositorio().SelecionarTiposProdutosQuery();
            var umaPaginaDados = tiposProduto.ToPagedList((int)page, 10);

            ViewBag.Dados = umaPaginaDados;

            return View(tiposProduto);
        }

        [HttpPost]
        public ActionResult EditarProdutoTipo(ProdutoTipoViewModel produtoTipo)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            IRepositorio repositorio = new Repositorio.Repositorio();
            ProdutoTipo produtoAlterar = new ProdutoTipo()
            {
                IdProdutoTipo = produtoTipo.IdProdutoTipo,
                Descricao = produtoTipo.Descricao,
                Prazo = produtoTipo.Prazo,
                VlMulta = produtoTipo.VlMulta,
                Ativo = produtoTipo.Ativo
            };

            repositorio.AtualizarProdutoTipo(produtoAlterar);

            return RedirectToAction("CadProdutoTipo", "Produto");
        }

        public ActionResult EditarProdutoTipo(int id)
        {
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

            ProdutoTipo produtoTipo = new Repositorio.Repositorio().SelecionarProdutoTipoPorId(id);
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
        public ActionResult CadEditora(int? page)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            if (page == null || page == 0) { page = 1; }

            IQueryable<Editora> editoras = new Repositorio.Repositorio().SelecionarEditorasQuery();
            ViewBag.Dados = editoras.ToPagedList((int)page, 10);

            return View();
        }

        [HttpPost]
        public ActionResult EditarEditora(EditoraViewModel editoraViewModel)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            if (editoraViewModel != null )
            {
                IRepositorio repositorio = new Repositorio.Repositorio();

                Editora editora = new Editora()
                {
                    IdEditora = editoraViewModel.IdEditora,
                    Nome = editoraViewModel.Nome
                };

                repositorio.AtualizarEditora(editora);
            }

            return RedirectToAction("CadEditora", "Produto");
        }

        public ActionResult EditarEditora(int id)
        {
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

            Editora editora = new Repositorio.Repositorio().SelecionarEditoraPorId(id);
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
        public ActionResult CadAutor(int? page)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            if (page == null || page == 0)
            {
                page = 1;
            }

            IQueryable<Autor> autores = new Repositorio.Repositorio().SelecionarAutoresQuery();
            ViewBag.Dados = autores.ToPagedList((int)page, 10);

            return View();
        }

        public ActionResult EditarAutor(int id)
        {
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

            Autor autor = new Repositorio.Repositorio().SelecionarAutorPorId(id);
            AutorViewModel model = new AutorViewModel()
            {
                IdAutor = autor.IdAutor,
                Nome = autor.Nome,
                DtNascimento = autor.DtNascimento
            };

            ViewBag.Adicionar = false;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditarAutor(AutorViewModel autorViewModel)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            if (autorViewModel != null)
            {
                IRepositorio repositorio = new Repositorio.Repositorio();

                Autor autor = new Autor()
                {
                    IdAutor = autorViewModel.IdAutor,
                    Nome = autorViewModel.Nome,
                    DtNascimento = autorViewModel.DtNascimento
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