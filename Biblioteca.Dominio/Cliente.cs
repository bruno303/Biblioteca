using System;

namespace Biblioteca.Dominio
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public DateTime DtNascimento { get; set; }
        public int IdSexo { get; set; }
        public bool Ativo { get; set; }
    }
}
