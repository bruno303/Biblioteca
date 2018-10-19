using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models
{
    public class ProdutoViewModel
    {
        [Display(Name = "ID")]
        [ReadOnly(true)]
        public int? IdProduto { get; set; }

        [Required(ErrorMessage = "Descrição obrigatória.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Tipo de produto obrigatório.")]
        [Display(Name = "Tipo de Produto")]
        public int IdProdutoTipo { get; set; }

        [Required(ErrorMessage = "Autor obrigatório.")]
        [Display(Name = "Autor")]
        public int IdAutor { get; set; }

        [Required(ErrorMessage = "Editora obrigatória.")]
        [Display(Name = "Editora")]
        public int IdEditora { get; set; }

        [Required(ErrorMessage = "Quantidade obrigatória.")]
        [Display(Name = "Quantidade Estoque")]
        public int Quantidade { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Ano Publicação")]
        public int AnoPublicacao { get; set; }

        [Display(Name = "ISBN")]
        public string Isbn { get; set; }

        public List<Dominio.ProdutoTipo> ProdutosTipo { get; set; }
        public List<Dominio.Autor> Autores { get; set; }
        public List<Dominio.Editora> Editoras { get; set; }

        public ProdutoViewModel()
        {
            var repositorio = new Repositorio.Repositorio();

            ProdutosTipo = repositorio.SelecionarTiposProdutos();
            Autores = repositorio.SelecionarAutores();
            Editoras = repositorio.SelecionarEditoras();
        }

    }
}
