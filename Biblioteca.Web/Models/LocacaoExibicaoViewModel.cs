using System;

namespace Biblioteca.Web.Models
{
    public class LocacaoExibicaoViewModel
    {
        public int IdLocacao { get; set; }
        public string Cliente { get; set; }
        public string Produto { get; set; }
        public DateTime DtLocacao { get; set; }
        public string DtDevolucao { get; set; }
        public string DtLimiteDevolucao { get; set; }
        public string Usuario { get; set; }
        public bool Atrasado { get; set; }
    }
}
