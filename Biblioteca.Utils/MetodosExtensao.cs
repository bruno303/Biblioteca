using System.Data;

namespace Biblioteca.Utils
{
    public static class MetodosExtensao
    {
        public static bool TemRegistros(this DataTable dataTable)
        {
            return dataTable?.Rows?.Count > 0;
        }
    }
}
