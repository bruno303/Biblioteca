using System;

namespace Biblioteca.Dominio
{
    public class Locacao
    {
        public int? IdLocacao { get; set; }
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public DateTime DtLocacao { get; set; }
        public DateTime DtLimiteDevolucao { get; set; }
        public DateTime? DtDevolucao { get; set; }
        public int IdUsuario { get; set; }
    }
}