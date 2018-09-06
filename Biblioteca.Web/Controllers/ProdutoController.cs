using Biblioteca.Dominio;
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
            List<Produto> produtos = new Repositorio.Repositorio().SelecionarProdutos();
            return View(produtos);
        }

        public ActionResult CadProdutoTipo()
        {
            List < ProdutoTipo> tiposProduto = new Repositorio.Repositorio().SelecionarTiposProdutos();
            return View(tiposProduto);
        }

        public ActionResult EditarProdutoTipo(int id)
        {
            ViewData["idProdutoTipo"] = id;
            return View();
        }

        public ActionResult CadEditora()
        {
            List<Editora> editoras = new Repositorio.Repositorio().SelecionarEditoras();
            return View();
        }

        public ActionResult CadAutor()
        {
            List<Autor> autores = new Repositorio.Repositorio().SelecionarAutores();
            return View();
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