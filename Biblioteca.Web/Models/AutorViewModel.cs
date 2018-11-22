using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models
{
    public class AutorViewModel
    {
        [Display(Name = "ID")]
        [ReadOnly(true)]
        public int? IdAutor { get; set; }

        [Required(ErrorMessage = "É necessário informar o nome do autor.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Data Nascimento")]
        public string DtNascimento { get; set; }
    }
}
