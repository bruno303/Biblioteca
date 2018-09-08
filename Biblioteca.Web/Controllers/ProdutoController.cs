using Biblioteca.Dominio;
using Biblioteca.Web.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Biblioteca.Web.Controllers
{
    public class ProdutoController : Controller
    {
        #region Métodos para listagem dos cadastros
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

        public ActionResult EditarProdutoTipo(int id)
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            ViewData["idProdutoTipo"] = id;
            return View();
        }

        public ActionResult CadEditora()
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            List<Editora> editoras = new Repositorio.Repositorio().SelecionarEditoras();
            return View();
        }

        public ActionResult CadAutor()
        {
            ActionResult action = ValidarLogin(HttpContext);
            if (action != null)
            {
                return action;
            }
            List<Autor> autores = new Repositorio.Repositorio().SelecionarAutores();
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

        #endregion

        #region Exemplos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Produto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Produto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Produto/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #endregion
    }
}