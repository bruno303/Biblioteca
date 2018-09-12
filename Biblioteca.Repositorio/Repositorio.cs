using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Biblioteca.Dominio;

namespace Biblioteca.Repositorio
{
    public class Repositorio : IRepositorio
    {
        public int RetornarIdUsuario(string usuario, string senha)
        {
            int retorno = 0;
            try
            {
                string query = string.Format("EXEC PRC_SEL_USUARIO '{0}', '{1}'", usuario, senha);
                DataTable dados = new Conexao().RetornarDados(query);
                if (dados.Rows.Count > 0)
                {
                    retorno = Convert.ToInt32(dados.Rows[0][0].ToString());
                }
            }
            catch (Exception)
            {
                retorno = 0;
            }

            return retorno;
        }

        #region Produto
        /// <summary>
        /// Consulta todos os produtos cadastrados.
        /// </summary>
        /// <returns>Lista com todos os produtos.</returns>
        public List<ProdutoExibicao> SelecionarProdutosExibicao()
        {
            List<ProdutoExibicao> retorno = new List<ProdutoExibicao>();
            Conexao conexao = new Conexao();
            DataTable dados = null;

            try
            {
                string query = " EXEC PRC_SEL_PRODUTO ";
                dados = conexao.RetornarDados(query);
                if (dados.Rows.Count > 0)
                {
                    foreach (DataRow linha in dados.Rows)
                    {
                        retorno.Add(new ProdutoExibicao()
                        {
                            IdProduto = Convert.ToInt32(linha[0].ToString()),
                            Descricao = linha[1].ToString(),
                            ProdutoTipo = linha[2].ToString(),
                            Autor = linha[3].ToString(),
                            Editora = linha[4].ToString(),
                            Quantidade = Convert.ToInt32(linha[5].ToString()),
                            Ativo = Convert.ToBoolean(linha[6].ToString())
                        });
                    }
                }
            }
            catch (Exception)
            {
                retorno = null;
            }

            return retorno;
        }

        /// <summary>
        /// Retorna um IQueryable com todos os produtos cadastrados.
        /// </summary>
        /// <returns>IQueryable com produtos cadastrados.</returns>
        public IQueryable<ProdutoExibicao> SelecionarProdutosExibicaoQuery()
        {
            return SelecionarProdutos().AsQueryable();
        }

        /// <summary>
        /// Atualiza o produto na base de dados.
        /// </summary>
        /// <param name="produto">Produto com os dados para ser alterado.</param>
        /// <returns>Int: Quantidade de erros durante a execução.</returns>
        public int AtualizarProduto(Produto produto)
        {
            int retorno = 0;
            Conexao conexao = new Conexao();

            try
            {
                string query = string.Format("EXEC PRC_IU_PRODUTO {0}, '{1}', {2}, {3}, {4}, {5}, {6}",
                    produto.IdProduto, produto.Descricao, produto.IdProdutoTipo, produto.IdAutor, produto.IdEditora,
                    produto.Quantidade, produto.Ativo ? 1 : 0);
                retorno = conexao.ExecutarQuery(query);
            }
            catch (Exception)
            {
                retorno = 0;
            }

            return retorno;
        }

        /// <summary>
        /// Seleciona um produto através do ID.
        /// </summary>
        /// <param name="id">ID do produto a ser pesquisado.</param>
        /// <returns>Um objeto do tipo Produto com os dados retornados.</returns>
        public Produto SelecionarProdutoPorId(int id)
        {
            Produto retorno = new Produto();
            Conexao conexao = new Conexao();
            DataTable dados = null;

            try
            {
                string query = string.Format(" EXEC PRC_SEL_PRODUTO_ID {0}", id);
                dados = conexao.RetornarDados(query);
                if (dados.Rows.Count > 0)
                {
                    retorno.IdProduto = Convert.ToInt32(dados.Rows[0][0].ToString());
                    retorno.Descricao = dados.Rows[0][1].ToString();
                    retorno.IdProdutoTipo = Convert.ToInt32(dados.Rows[0][2].ToString());
                    retorno.IdAutor = Convert.ToInt32(dados.Rows[0][3].ToString());
                    retorno.IdEditora = Convert.ToInt32(dados.Rows[0][4].ToString());
                    retorno.Quantidade = Convert.ToInt32(dados.Rows[0][5].ToString());
                    retorno.Ativo = Convert.ToBoolean(dados.Rows[0][6].ToString());
                }
            }
            catch (Exception)
            {
                retorno = null;
            }

            return retorno;
        }
        #endregion

        #region Tipo de Produto
        /// <summary>
        /// Prepara IQueryable com os tipos de produtos cadastrados.
        /// </summary>
        /// <returns>Retorna IQueryable com os tipos de produtos cadastrados.</returns>
        public IQueryable<ProdutoTipo> SelecionarTiposProdutosQuery()
        {
            return SelecionarTiposProdutos().AsQueryable();
        }

        /// <summary>
        /// Seleciona todos os tipos de produto cadastrados.
        /// </summary>
        /// <returns>Lista com todos os tipos de produto cadastrados.</returns>
        public List<ProdutoTipo> SelecionarTiposProdutos()
        {
            List<ProdutoTipo> retorno = new List<ProdutoTipo>();
            DataTable dados;

            try
            {
                string query = "EXEC PRC_SEL_PRODUTOTIPO 0";
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

        /// <summary>
        /// Atualiza um tipo de produto.
        /// </summary>
        /// <param name="produtoTipo">ProdutoTipo com os dados a serem atualizados.</param>
        /// <returns>Int: Total de erros durante o processo.</returns>
        public int AtualizarProdutoTipo(ProdutoTipo produtoTipo)
        {
            int retorno = 0;
            try
            {
                string query = string.Format("EXEC PRC_IU_PRODUTOTIPO {0}, '{1}', {2}, {3}, {4}", produtoTipo.IdProdutoTipo, produtoTipo.Descricao,
                    produtoTipo.Prazo, produtoTipo.VlMulta.ToString().Replace(",", "."), (produtoTipo.Ativo ? "1" : "0"));
                retorno = new Conexao().ExecutarQuery(query);
            }
            catch (Exception)
            {
                retorno = 0;
            }

            return retorno;
        }

        /// <summary>
        /// Seleciona tipo de produto por ID.
        /// </summary>
        /// <param name="id">ID do tipo de produto.</param>
        /// <returns>Objeto do tipo ProdutoTipo.</returns>
        public ProdutoTipo SelecionarProdutoTipoPorId(int id)
        {
            ProdutoTipo retorno = new ProdutoTipo();
            try
            {
                string query = string.Format("EXEC PRC_SEL_PRODUTOTIPO {0}", id);
                DataTable dados = new Conexao().RetornarDados(query);
                if (dados.Rows.Count > 0)
                {
                    retorno.IdProdutoTipo = Convert.ToInt32(dados.Rows[0][0].ToString());
                    retorno.Descricao = dados.Rows[0][1].ToString();
                    retorno.Prazo = Convert.ToInt32(dados.Rows[0][2].ToString());
                    retorno.VlMulta = Convert.ToDecimal(dados.Rows[0][3].ToString());
                    retorno.Ativo = Convert.ToBoolean(dados.Rows[0][4].ToString());
                }
            }
            catch (Exception)
            {
                retorno = null;
            }

            return retorno;
        }
        #endregion

        #region Editora
        /// <summary>
        /// Seleciona as editoras cadastradas.
        /// </summary>
        /// <returns>Uma lista de Editoras.</returns>
        public List<Editora> SelecionarEditoras()
        {
            List<Editora> retorno = new List<Editora>();
            DataTable dados;
            Conexao conexao = new Conexao();

            try
            {
                string query = "EXEC PRC_SEL_EDITORA 0";
                dados = conexao.RetornarDados(query);
                if (dados.Rows.Count > 0)
                {
                    foreach (DataRow linha in dados.Rows)
                    {
                        retorno.Add(new Editora()
                        {
                            IdEditora = Convert.ToInt32(linha[0].ToString()),
                            Nome = linha[1].ToString()
                        });
                    }
                }
            }
            catch (Exception)
            {
                retorno = null;
            }

            return retorno;
        }

        /// <summary>
        /// Prepara IQueryable com as editoras cadastradas.
        /// </summary>
        /// <returns>Retorna IQueryable com as editoras cadastradas.</returns>
        public IQueryable<Editora> SelecionarEditorasQuery()
        {
            return SelecionarEditoras()?.AsQueryable();
        }

        /// <summary>
        /// Seleciona editora por ID.
        /// </summary>
        /// <param name="id">ID da editora desejada.</param>
        /// <returns>Retorna um objeto do tipo Editora.</returns>
        public Editora SelecionarEditoraPorId(int id)
        {
            Editora retorno = new Editora();
            DataTable dados = new DataTable();
            Conexao conexao = new Conexao();

            try
            {
                string query = string.Format("EXEC PRC_SEL_EDITORA {0}", id);
                dados = conexao.RetornarDados(query);
                if (dados.Rows.Count > 0)
                {
                    retorno.IdEditora = Convert.ToInt32(dados.Rows[0][0].ToString());
                    retorno.Nome = dados.Rows[0][1].ToString();
                }
            }
            catch (Exception)
            {
                retorno = null;
            }

            return retorno;
        }

        /// <summary>
        /// Atualiza editora.
        /// </summary>
        /// <param name="editora">Objeto do tipo Editora com os dados a serem alterados.</param>
        /// <returns>Int: Total de erros durante o processo.</returns>
        public int AtualizarEditora(Editora editora)
        {
            int retorno = 0;
            Conexao conexao = new Conexao();

            try
            {
                if (editora != null)
                {
                    string query = string.Format("EXEC PRC_IU_EDITORA {0}, '{1}'", editora.IdEditora, editora.Nome);
                    retorno = conexao.ExecutarQuery(query);
                }
            }
            catch (Exception)
            {
                retorno = 0;
            }

            return retorno;
        }
        #endregion

        #region Autor
        /// <summary>
        /// Seleciona todos os autores cadastrados.
        /// </summary>
        /// <returns>Uma lista de autores contendo todos os autores cadastrados.</returns>
        public List<Autor> SelecionarAutores()
        {
            List<Autor> retorno = new List<Autor>();
            Conexao conexao = new Conexao();
            DataTable dados;

            try
            {
                string query = "EXEC PRC_SEL_AUTOR 0";
                dados = conexao.RetornarDados(query);
                if (dados.Rows.Count > 0)
                {
                    foreach (DataRow linha in dados.Rows)
                    {
                        retorno.Add(new Autor()
                        {
                            IdAutor = Convert.ToInt32(linha[0].ToString()),
                            Nome = linha[1].ToString(),
                            DtNascimento = Convert.ToDateTime(linha[2].ToString())
                        });
                    }
                }
            }
            catch (Exception)
            {
                retorno = null;
            }

            return retorno;
        }

        /// <summary>
        /// Prepara um IQueryable com todos os autores cadastrados.
        /// </summary>
        /// <returns>Retorna um IQueryable com os autores cadastrados.</returns>
        public IQueryable<Autor> SelecionarAutoresQuery()
        {
            return SelecionarAutores().AsQueryable();
        }

        /// <summary>
        /// Seleciona autor através do ID.
        /// </summary>
        /// <param name="id">ID do autor.</param>
        /// <returns>Retorna um objeto do tipo Autor com os dados desejados.</returns>
        public Autor SelecionarAutorPorId(int id)
        {
            Autor retorno = new Autor();
            Conexao conexao = new Conexao();
            DataTable dados;

            try
            {
                string query = string.Format("EXEC PRC_SEL_AUTOR {0}", id);
                dados = conexao.RetornarDados(query);
                if (dados.Rows.Count > 0)
                {
                    retorno.IdAutor = Convert.ToInt32(dados.Rows[0][0].ToString());
                    retorno.Nome = dados.Rows[0][1].ToString();
                    retorno.DtNascimento = Convert.ToDateTime(dados.Rows[0][2].ToString());
                }
            }
            catch (Exception)
            {
                retorno = null;
            }

            return retorno;
        }

        /// <summary>
        /// Atualiza o autor.
        /// </summary>
        /// <param name="autor">Objeto do tipo Autor com os dados a serem alterados.</param>
        /// <returns>Int: Número de erros durante o processo.</returns>
        public int AtualizarAutor(Autor autor)
        {
            int retorno = 0;
            Conexao conexao = new Conexao();

            try
            {
                if (autor != null)
                {
                    string query = string.Format("EXEC PRC_IU_AUTOR {0}, '{1}', '{2}'", autor.IdAutor, autor.Nome, autor.DtNascimento.ToString("yyyy-MM-dd HH:mm:ss"));
                    retorno = conexao.ExecutarQuery(query);
                }
            }
            catch (Exception)
            {
                retorno = 0;
            }

            return retorno;
        }
        #endregion
    }
}
