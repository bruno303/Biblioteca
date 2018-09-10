using System;
using System.IO;

namespace Biblioteca.Utils
{
    public class ArquivoUtils
    {
        /// <summary>
        /// Faz a leitura de todo o conteúdo de um arquivo.
        /// </summary>
        /// <param name="path">Caminho completo, incluindo nome do arquivo e extensão, do arquivo a ser lido.</param>
        /// <returns>Retorna o texto do arquivo caso tenha sucesso na leitura. String.Empty caso contrário.</returns>
        public string LerArquivo(string path)
        {
            string retorno = string.Empty;
            if (File.Exists(path))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        retorno = reader.ReadToEnd();
                    }
                }
                catch(Exception)
                {
                    retorno = string.Empty;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Escreve texto em arquivo.
        /// </summary>
        /// <param name="texto">Texto a ser escrito.</param>
        /// <param name="path">Caminho completo do arquivo a ser escrito, incluindo nome e extensão.</param>
        /// <param name="append">False se deve sobreescrever o arquivo, caso já exista. True se deve adicionar o texto ao arquivo.</param>
        /// <returns>True caso a escrita tenha sido com sucesso. False caso contrário.</returns>
        public bool EscreverArquivo(string texto, string path, bool append = false)
        {
            bool retorno = false;

            try
            {
                using (StreamWriter writer = new StreamWriter(path, append))
                {
                    writer.Write(texto);
                }
            }
            catch(Exception)
            {
                retorno = false;
            }

            return retorno;
        }
    }
}
