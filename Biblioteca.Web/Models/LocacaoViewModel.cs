using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Biblioteca.Dominio;

namespace Biblioteca.Web.Models
{
    public class LocacaoViewModel
    {
        public int? IdLocacao { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "O cliente é obrigatório")]
        public int IdCliente { get; set; }

        [Display(Name = "Produto")]
        [Required(ErrorMessage = "O produto é obrigatório")]
        public int IdProduto { get; set; }

        [Display(Name = "Data de Devolução")]
        public DateTime? DtDevolucao { get; set; }

        public List<Cliente> Clientes { get; set; }
        public List<Produto> Produtos { get; set; }

        public LocacaoViewModel()
        {
            var repositorio = new Repositorio.Repositorio();

            Clientes = repositorio.SelecionarClientes();
            Produtos = repositorio.SelecionarProdutos();
        }
    }
}
