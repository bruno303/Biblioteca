using Biblioteca.Dominio;
using Biblioteca.Web.Classes;
using Biblioteca.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Biblioteca.Web.Controllers
{
    public class ProdutoController : Controller
    {
        #region Produto
        public ActionResult CadProduto()
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            List<ProdutoExibicao> produtos = new Repositorio.Repositorio().SelecionarProdutos();


            return View(produtos);
        }

        public ActionResult EditarProduto(int id)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            Produto produto = new Repositorio.Repositorio().SelecionarProdutoPorId(id);
            ProdutoViewModel produtoViewModel = new ProdutoViewModel()
            {
                IdProdutoTipo = produto.IdProdutoTipo,
                Descricao = produto.Descricao,
                IdAutor = produto.IdAutor,
                IdProduto = produto.IdProduto,
                IdEditora = produto.IdEditora,
                Quantidade = produto.Quantidade,
                Ativo = produto.Ativo
            };

            ViewBag.Adicionar = id == 0;

            return View(produtoViewModel);
        }

        [HttpPost]
        public ActionResult EditarProduto(ProdutoViewModel produtoViewModel)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            if (produtoViewModel != null)
            {
                IRepositorio repositorio = new Repositorio.Repositorio();

                Produto produto = new Produto()
                {
                    IdProduto = produtoViewModel.IdProduto,
                    Descricao = produtoViewModel.Descricao,
                    IdProdutoTipo = produtoViewModel.IdProdutoTipo,
                    IdAutor = produtoViewModel.IdAutor,
                    IdEditora = produtoViewModel.IdEditora,
                    Quantidade = produtoViewModel.Quantidade,
                    Ativo = produtoViewModel.Ativo
                };

                repositorio.AtualizarProduto(produto);

            }

            return RedirectToAction("CadProduto", "Produto");
        }
        #endregion

        #region Tipo de Produto
        public ActionResult CadProdutoTipo()
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            List<ProdutoTipo> tiposProduto = new Repositorio.Repositorio().SelecionarTiposProdutos();

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
        public ActionResult CadEditora()
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            List<Editora> editoras = new Repositorio.Repositorio().SelecionarEditoras();

            return View(editoras);
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
        public ActionResult CadAutor()
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }

            List<Autor> autores = new Repositorio.Repositorio().SelecionarAutores();

            return View(autores);
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