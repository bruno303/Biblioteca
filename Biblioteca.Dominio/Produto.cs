namespace Biblioteca.Dominio
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Descricao { get; set; }
        public int IdProdutoTipo { get; set; }
        public int IdAutor { get; set; }
        public int IdEditora { get; set; }
        public int Quantidade { get; set; }
        public bool Ativo { get; set; }
    }
}
