using System;

namespace Biblioteca.Web.Classes
{
    public class Paginacao
    {
        public readonly int ItensPorPagina;

        public Paginacao()
        {
            ItensPorPagina = 10;
        }

        public int QuantidadePaginas(int qtdItens)
        {
            return qtdItens % ItensPorPagina > 0 ? Convert.ToInt32((qtdItens / ItensPorPagina) + 1) : Convert.ToInt32(qtdItens / ItensPorPagina);
        }
    }
}
