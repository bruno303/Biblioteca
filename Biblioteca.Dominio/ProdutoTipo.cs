namespace Biblioteca.Dominio
{
    public class ProdutoTipo
    {
        public int IdProdutoTipo { get; set; }
        public string Descricao { get; set; }
        public int Prazo { get; set; }
        public decimal VlMulta { get; set; }
        public bool Ativo { get; set; }
    }
}
