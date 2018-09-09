using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models
{
    public class ProdutoTipoViewModel
    {
        [Required(ErrorMessage = "É necessário informar o ID.")]
        [Display(Name = "ID")]
        [ReadOnly(true)]
        public int IdProdutoTipo { get; set; }

        [Required(ErrorMessage = "É necessário informar a descrição.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Prazo")]
        public int Prazo { get; set; }

        [Display(Name = "Multa")]
        public decimal VlMulta { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
    }
}
