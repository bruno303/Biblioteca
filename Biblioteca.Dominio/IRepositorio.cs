using System.Collections.Generic;

namespace Biblioteca.Dominio
{
    public interface IRepositorio
    {
        #region Produto
        List<ProdutoExibicao> SelecionarProdutosExibicao();
        System.Linq.IQueryable<ProdutoExibicao> SelecionarProdutosExibicaoQuery();
        Produto SelecionarProdutoPorId(int id);
        int AtualizarProduto(Produto produto);
        #endregion

        #region Tipo de Produto
        List<ProdutoTipo> SelecionarTiposProdutos();
        System.Linq.IQueryable<ProdutoTipo> SelecionarTiposProdutosQuery();
        ProdutoTipo SelecionarProdutoTipoPorId(int id);
        int AtualizarProdutoTipo(ProdutoTipo produtoTipo);
        #endregion

        #region Editora
        List<Editora> SelecionarEditoras();
        System.Linq.IQueryable<Editora> SelecionarEditorasQuery();
        Editora SelecionarEditoraPorId(int id);
        int AtualizarEditora(Editora editora);
        #endregion

        #region Autor
        List<Autor> SelecionarAutores();
        System.Linq.IQueryable<Autor> SelecionarAutoresQuery();
        Autor SelecionarAutorPorId(int id);
        int AtualizarAutor(Autor autor);
        #endregion

        #region Usuário
        int RetornarIdUsuario(string usuario, string senha);
        #endregion
    }
}
