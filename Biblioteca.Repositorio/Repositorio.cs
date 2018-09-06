using System;
using System.Collections.Generic;
using System.Data;
using Biblioteca.Dominio;

namespace Biblioteca.Repositorio
{
    public class Repositorio : IRepositorio
    {
        public List<Autor> SelecionarAutores()
        {
            throw new NotImplementedException();
        }

        public List<Editora> SelecionarEditoras()
        {
            throw new NotImplementedException();
        }

        public List<Produto> SelecionarProdutos()
        {
            throw new NotImplementedException();
        }

        public List<ProdutoTipo> SelecionarTiposProdutos()
        {
            List<ProdutoTipo> retorno = new List<ProdutoTipo>();
            DataTable dados;

            try
            {
                string query = "SELECT ID_PRODUTO_TIPO, DESCRICAO, PRAZO, VL_MULTA, ATIVO FROM PRODUTO_TIPO WITH(NOLOCK)";
                dados = new Conexao().RetornarDados(query);

                if (dados.Rows.Count > 0)
                {
                    foreach (DataRow linha in dados.Rows)
                    {
                        ProdutoTipo produtoTipo = new ProdutoTipo
                        {
                            IdProdutoTipo = Convert.ToInt32(linha[0].ToString()),
                            Descricao = linha[1].ToString(),
                            Prazo = Convert.ToInt32(linha[2].ToString()),
                            VlMulta = Convert.ToDecimal(linha[3].ToString()),
                            Ativo = Convert.ToBoolean(linha[4].ToString())
                        };
                        retorno.Add(produtoTipo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }
    }
}
