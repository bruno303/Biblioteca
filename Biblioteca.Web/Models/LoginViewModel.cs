using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "É necessário informar o usuário!")]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "É necessário informar a senha!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        //[Display(Name = "Lembrar-me")]
        //public bool Lembrar { get; set; }
    }
}
