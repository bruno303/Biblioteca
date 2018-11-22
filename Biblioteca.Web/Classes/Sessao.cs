using Microsoft.AspNetCore.Http;
using System;

namespace Biblioteca.Web.Classes
{
    public class Sessao
    {
        /// <summary>
        /// Grava dados do login na sessão.
        /// </summary>
        /// <param name="context">Context do controller que irá gravar a sessão.</param>
        /// <param name="usuario">Usuário que se conectou.</param>
        /// <param name="idUsuario">Id do usuário conectado.</param>
        /// <returns></returns>
        public static bool Gravar(HttpContext context, string usuario, int idUsuario)
        {
            bool retorno = false;
            try
            {
                if (context != null)
                {
                    context.Session.Remove("usuario");
                    context.Session.Remove("idUsuario");
                    context.Session.SetString("usuario", usuario);
                    context.Session.SetInt32("idUsuario", idUsuario);
                    retorno = true;
                }
            }
            catch (Exception)
            {
                retorno = false;
            }
            return retorno;
        }

        public static int GetIdUsuario(HttpContext httpContext)
        {
            int retorno = 0;

            try
            {
                if (httpContext != null)
                {
                    retorno = (int)httpContext.Session.GetInt32("idUsuario");
                }
            }
            catch(Exception)
            {
                retorno = 0;
            }

            return retorno;
        }

        /// <summary>
        /// Verifica se há sessão ativa.
        /// </summary>
        /// <param name="context">Context que possui a sessão a ser verificada</param>
        /// <returns>Retorna true quando há sessão ativa. False caso contrário.</returns>
        public static bool SessaoAtiva(HttpContext context)
        {
            bool retorno = false;
            int? idSessao = null;

            try
            {
                if (context != null)
                {
                    idSessao = context.Session.GetInt32("idUsuario");
                    retorno = (idSessao != null && idSessao > 0);
                }
            }
            catch (Exception)
            {
                retorno = false;
            }
            return retorno;
        }

        /// <summary>
        /// Limpa os dados de sessão do login.
        /// </summary>
        /// <param name="context">Context que possui a sessão a ser limpa.</param>
        public static void Limpar(HttpContext context)
        {
            if (context != null)
            {
                context.Session.Remove("usuario");
                context.Session.Remove("idUsuario");
            }
        }
    }
}
