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
        public void ExecutarQuery(string query)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(MontarStringConexao()))
                {
                    conexao.Open();
                    using (SqlCommand command = new SqlCommand(query, conexao))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task ExecutarQueryAsync(string query)
        {
            return Task.Run(() =>
            {
                ExecutarQuery(query);
            });
        }
        #endregion

        #region Utils
        private string MontarStringConexao()
        {
            return string.Format("Server={0};Database={1};User Id={2};Password={3};", "192.168.56.135", "BIBLIOTECA", "biblioteca", "biblioteca@123");
        }
        #endregion

    }
}
