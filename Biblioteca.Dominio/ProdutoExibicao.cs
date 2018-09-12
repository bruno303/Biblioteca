namespace Biblioteca.Dominio
{
    public class ProdutoExibicao
    {
        public int IdProduto { get; set; }
        public string Descricao { get; set; }
        public string ProdutoTipo { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public int Quantidade { get; set; }
        public bool Ativo { get; set; }
    }
}
