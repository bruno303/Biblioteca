using System;
using System.Text;

namespace Biblioteca.Utils
{
    public class Criptografia
    {
        public string Criptografar(string texto)
        {
            return Convert.ToBase64String(new UTF32Encoding().GetBytes(texto));
        }

        public string Descriptografar(string texto)
        {
            return new UTF32Encoding().GetString(Convert.FromBase64String(texto));
        }
    }
}
