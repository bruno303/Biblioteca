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

        public List<Produto> SelecionarProdutos()
        {
            throw new NotImplementedException();
        }

        #region Tipo de Produto
        public IQueryable<ProdutoTipo> SelecionarTiposProdutosQuery()
        {
            return SelecionarTiposProdutos().AsQueryable();
        }

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

        public IQueryable<Editora> SelecionarEditorasQuery()
        {
            return SelecionarEditoras()?.AsQueryable();
        }

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

        public IQueryable<Autor> SelecionarAutoresQuery()
        {
            return SelecionarAutores().AsQueryable();
        }

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
