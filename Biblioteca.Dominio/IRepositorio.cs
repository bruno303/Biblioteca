using System.Collections.Generic;

namespace Biblioteca.Dominio
{
    public interface IRepositorio
    {
        List<Produto> SelecionarProdutos();
        List<ProdutoTipo> SelecionarTiposProdutos();
        List<Editora> SelecionarEditoras();
        List<Autor> SelecionarAutores();

    }
}
