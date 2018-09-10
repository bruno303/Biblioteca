using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models
{
    public class AutorViewModel
    {
        [Required(ErrorMessage = "É necessário informar o ID.")]
        [Display(Name = "ID")]
        public int IdAutor { get; set; }

        [Required(ErrorMessage = "É necessário informar o nome do autor.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Data Nascimento")]
        [DataType(DataType.DateTime)]
        public DateTime DtNascimento { get; set; }
    }
}
