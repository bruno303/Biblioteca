﻿using System;
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
    }
}
