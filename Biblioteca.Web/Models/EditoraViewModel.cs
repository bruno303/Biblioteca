using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models
{
    public class EditoraViewModel
    {
        [Display(Name = "ID")]
        [ReadOnly(true)]
        public int? IdEditora { get; set; }

        [Required(ErrorMessage = "É necessário informar o nome da editora.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}
