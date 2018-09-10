namespace Biblioteca.Dominio
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Descricao { get; set; }
        public ProdutoTipo ProdutoTipo { get; set; }
        public Autor Autor { get; set; }
        public Editora Editora { get; set; }
        public int Quantidade { get; set; }
        public bool Ativo { get; set; }
    }
}
