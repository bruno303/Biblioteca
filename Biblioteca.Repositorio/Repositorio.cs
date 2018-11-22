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
        public List<Produto> SelecionarProdutos()
        {
            List<Produto> retorno = new List<Produto>();
            Conexao conexao = new Conexao();
            DataTable dados = null;

            try
            {
                string query = " EXEC PRC_SEL_PRODUTO 0 ";
                dados = conexao.RetornarDados(query);
                if (dados.Rows.Count > 0)
                {
                    foreach (DataRow linha in dados.Rows)
                    {
                        retorno.Add(new Produto()
                        {
                            IdProduto = Convert.ToInt32(linha[0].ToString()),
                            Descricao = linha[1].ToString(),
                            IdProdutoTipo = Convert.ToInt32(linha[2].ToString()),
                            IdAutor = Convert.ToInt32(linha[3].ToString()),
                            IdEditora = Convert.ToInt32(linha[4].ToString()),
                            Quantidade = Convert.ToInt32(linha[5].ToString()),
                            Ativo = Convert.ToBoolean(linha[6].ToString()),
                            AnoPublicacao = Convert.ToInt32(linha[7].ToString()),
                            Isbn = linha[8].ToString()
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
                string query = string.Format("EXEC PRC_IU_PRODUTO {0}, '{1}', {2}, {3}, {4}, {5}, {6}, {7}, '{8}'",
                    produto.IdProduto, produto.Descricao, produto.IdProdutoTipo, produto.IdAutor, produto.IdEditora,
                    produto.Quantidade, produto.Ativo ? 1 : 0, produto.AnoPublicacao, produto.Isbn);
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
                string query = string.Format(" EXEC PRC_SEL_PRODUTO {0}", id);
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
                    retorno.AnoPublicacao = Convert.ToInt32(dados.Rows[0][7].ToString());
                    retorno.Isbn = dados.Rows[0][8].ToString();
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

        /// <summary>
        /// Seleciona a descrição do tipo de produto.
        /// </summary>
        /// <param name="id">Id do tipo de produto.</param>
        /// <returns>Retorna a descrição do tipo de produto.</returns>
        public string SelecionarDescricaoProdutoTipo(int id)
        {
            ProdutoTipo produtoTipo = SelecionarProdutoTipoPorId(id);
            return produtoTipo.Descricao;
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

        /// <summary>
        /// Consulta o nome da editora.
        /// </summary>
        /// <param name="id">Id da editora.</param>
        /// <returns>Retorna o nome da editora.</returns>
        public string SelecionarNomeEditora(int id)
        {
            Editora editora = SelecionarEditoraPorId(id);
            return editora.Nome;
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

        /// <summary>
        /// Consulta o nome do autor.
        /// </summary>
        /// <param name="id">Id do autor.</param>
        /// <returns>Retorna o nome do autor</returns>
        public string SelecionarNomeAutor(int id)
        {
            Autor autor = SelecionarAutorPorId(id);
            return autor.Nome;
        }
        #endregion

        public List<Cliente> SelecionarClientes()
        {
            var query = " EXEC PRC_SEL_CLIENTE 0 ";
            List<Cliente> clientes = new List<Cliente>();
            DataTable dados = null;
            Conexao conexao = new Conexao();

            try
            {
                dados = conexao.RetornarDados(query);
                if (dados.Rows.Count > 0)
                {
                    foreach (DataRow linha in dados.Rows)
                    {
                        clientes.Add(new Cliente()
                        {
                            IdCliente = Convert.ToInt32(linha[0].ToString()),
                            Nome = linha[1].ToString(),
                            DtNascimento = Convert.ToDateTime(linha[2].ToString()),
                            IdSexo = Convert.ToInt32(linha[3].ToString()),
                            Ativo = Convert.ToBoolean(linha[4].ToString())
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                clientes = null;
            }
            return clientes;
        }

        public string GravarLocacao(Locacao locacao)
        {
            var retorno = string.Empty;
            Conexao conexao = new Conexao();

            try
            {
                var query = string.Format(" EXEC PRC_IU_LOCACAO {0}, {1}, {2}, '{3}', '{4}', {5}, {6}",
                    locacao.IdLocacao.HasValue ? locacao.IdLocacao : 0,
                    locacao.IdProduto,
                    locacao.IdCliente,
                    locacao.DtLocacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    locacao.DtLimiteDevolucao.ToString("yyyy-MM-dd HH:mm:ss"),
                    locacao.DtDevolucao.HasValue ? "'" + locacao.DtDevolucao.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" : "NULL",
                    locacao.IdUsuario);

                conexao.ExecutarQuery(query);
            }
            catch(Exception ex)
            {
                retorno = ex.Message;
            }

            return retorno;
        }

        public string DevolverLocacao(Locacao locacao)
        {
            var retorno = string.Empty;
            Conexao conexao = new Conexao();

            try
            {
                if ((!locacao.DtDevolucao.HasValue) || (locacao.DtDevolucao.Value == new DateTime()))
                {
                    locacao.DtDevolucao = DateTime.Now;
                }
                return GravarLocacao(locacao);
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }

            return retorno;
        }

        public List<Locacao> SelecionarLocacoes()
        {
            var locacoes = new List<Locacao>();
            var conexao = new Conexao();
            DataTable dados = null;

            try
            {
                dados = conexao.RetornarDados(" EXEC PRC_SEL_LOCACAO 0 ");
                if ((dados?.Rows?.Count ?? 0) > 0)
                {
                    for (int cont = 0; cont < dados.Rows.Count; cont++)
                    {
                        DateTime dataDevolucao;
                        var locacao = new Locacao()
                        {
                            IdLocacao = Convert.ToInt32(dados.Rows[cont][0].ToString()),
                            IdProduto = Convert.ToInt32(dados.Rows[cont][1].ToString()),
                            IdCliente = Convert.ToInt32(dados.Rows[cont][2].ToString()),
                            DtLocacao = Convert.ToDateTime(dados.Rows[cont][3].ToString()),
                            DtLimiteDevolucao = Convert.ToDateTime(dados.Rows[cont][4].ToString()),
                            IdUsuario = Convert.ToInt32(dados.Rows[cont][6].ToString())
                        };
                        DateTime.TryParse(dados.Rows[cont][5].ToString(), out dataDevolucao);
                        locacao.DtDevolucao = dataDevolucao;
                        locacoes.Add(locacao);
                    }
                }

            }
            catch(Exception)
            {
                throw;
            }
            return locacoes;
        }

        public string SelecionarNomeProduto(int idProduto)
        {
            var produto = SelecionarProdutoPorId(idProduto);
            return produto?.Descricao ?? string.Empty;
        }

        public string SelecionarNomeCliente(int id)
        {
            var retorno = string.Empty;
            var conexao = new Conexao();
            DataTable dados = null;

            try
            {
                dados = conexao.RetornarDados(" SELECT NOME FROM CLIENTE WITH(NOLOCK) WHERE ID_CLIENTE = " + id.ToString());
                if ((dados?.Rows?.Count ?? 0) > 0)
                {
                    retorno = dados.Rows[0][0].ToString();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        public string SelecionarNomeUsuario(int id)
        {
            var retorno = string.Empty;
            var conexao = new Conexao();
            DataTable dados = null;

            try
            {
                dados = conexao.RetornarDados(" SELECT NOME FROM USUARIO WITH(NOLOCK) WHERE ID_USUARIO = " + id.ToString());
                if ((dados?.Rows?.Count ?? 0) > 0)
                {
                    retorno = dados.Rows[0][0].ToString();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        public Locacao SelecionarLocacaoPorId(int id)
        {
            var locacao = new Locacao();
            var conexao = new Conexao();
            DataTable dados = null;

            try
            {
                dados = conexao.RetornarDados(string.Format(" EXEC PRC_SEL_LOCACAO {0} ", id));
                if ((dados?.Rows?.Count ?? 0) > 0)
                {
                    DateTime dataDevolucao;
                    locacao.IdLocacao = Convert.ToInt32(dados.Rows[0][0].ToString());
                    locacao.IdProduto = Convert.ToInt32(dados.Rows[0][1].ToString());
                    locacao.IdCliente = Convert.ToInt32(dados.Rows[0][2].ToString());
                    locacao.DtLocacao = Convert.ToDateTime(dados.Rows[0][3].ToString());
                    locacao.DtLimiteDevolucao = Convert.ToDateTime(dados.Rows[0][4].ToString());
                    DateTime.TryParse(dados.Rows[0][5].ToString(), out dataDevolucao);
                    locacao.DtDevolucao = dataDevolucao;
                    locacao.IdUsuario = Convert.ToInt32(dados.Rows[0][6].ToString());
                }

            }
            catch (Exception)
            {
                throw;
            }
            return locacao;
        }
    }
}
