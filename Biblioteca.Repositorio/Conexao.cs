using Biblioteca.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Repositorio
{
    class Conexao
    {
        #region Retornar dados
        public DataTable RetornarDados(string query)
        {
            DataTable retorno = new DataTable();
            try
            {
                using (SqlConnection conexao = new SqlConnection(MontarStringConexao()))
                {
                    conexao.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conexao))
                    {
                        adapter.Fill(retorno);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        public Task<DataTable> RetornarDadosAsync(string query)
        {
            return Task.Run(() =>
            {
                return RetornarDados(query);
            });
        }
        #endregion

        #region Executar query
        public int ExecutarQuery(string query)
        {
            int linhasAfetadas = 0;
            try
            {
                using (SqlConnection conexao = new SqlConnection(MontarStringConexao()))
                {
                    conexao.Open();
                    using (SqlCommand command = new SqlCommand(query, conexao))
                    {
                        command.CommandType = CommandType.Text;
                        linhasAfetadas = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return linhasAfetadas;
        }

        public Task<int> ExecutarQueryAsync(string query)
        {
            return Task.Run(() =>
            {
                return ExecutarQuery(query);
            });
        }
        #endregion

        #region Utils
        private string MontarStringConexao()
        {
            JsonUtils<BancoDados> json = new JsonUtils<BancoDados>();
            ArquivoUtils arquivo = new ArquivoUtils();

            string texto = arquivo.LerArquivo("bd.json");

            BancoDados bancoDados = json.JsonToObject(texto);

            return bancoDados.ToString();
        }
        #endregion

    }
}
