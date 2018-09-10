using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models
{
    public class EditoraViewModel
    {
        [Required(ErrorMessage = "É necessário informar o ID.")]
        [Display(Name = "ID")]
        public int IdEditora { get; set; }

        [Required(ErrorMessage = "É necessário informar o nome da editora.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}
