using System.Collections.Generic;

namespace Biblioteca.Dominio
{
    public interface IRepositorio
    {
        #region Produto
        List<Produto> SelecionarProdutos();
        Produto SelecionarProdutoPorId(int id);
        int AtualizarProduto(Produto produto);
        #endregion

        #region Tipo de Produto
        List<ProdutoTipo> SelecionarTiposProdutos();
        ProdutoTipo SelecionarProdutoTipoPorId(int id);
        int AtualizarProdutoTipo(ProdutoTipo produtoTipo);
        string SelecionarDescricaoProdutoTipo(int id);
        #endregion

        #region Editora
        List<Editora> SelecionarEditoras();
        Editora SelecionarEditoraPorId(int id);
        int AtualizarEditora(Editora editora);
        string SelecionarNomeEditora(int id);
        #endregion

        #region Autor
        List<Autor> SelecionarAutores();
        Autor SelecionarAutorPorId(int id);
        int AtualizarAutor(Autor autor);
        string SelecionarNomeAutor(int id);
        #endregion

        #region Usuário
        int RetornarIdUsuario(string usuario, string senha);
        #endregion
    }
}
