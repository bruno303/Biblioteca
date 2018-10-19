using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Biblioteca.Utils
{
    public static class MapperUtils
    {
        /// <summary>
        /// Mapeia as propriedades que possuem o mesmo nome de um objeto Origem para um objeto Destino.
        /// </summary>
        /// <typeparam name="TOrigem">Tipo do objeto de Origem.</typeparam>
        /// <typeparam name="TDestino">Tipo do objeto de Destino.</typeparam>
        /// <param name="origem">Objeto de origem que contém as informações.</param>
        /// <param name="destino">Objeto de destino para as propriedades.</param>
        public static void Mapear<TOrigem, TDestino>(TOrigem origem, TDestino destino)
        {
            if (origem == null)
            {
                destino = default(TDestino);
                return;
            }

            if (destino == null)
            {
                destino = default(TDestino);
            }

            PropertyInfo[] propriedades = origem.GetType().GetProperties();
            foreach (var item in propriedades)
            {
                PropertyInfo prop = destino.GetType().GetProperties().Where(p => p.Name == item.Name).FirstOrDefault();
                if (prop != null)
                {
                    object valor = item.GetValue(origem);
                    prop.SetValue(destino, valor);
                }
            }
        }

        /// <summary>
        /// Mapeia as propriedades que possuem o mesmo nome de uma lista Origem para uma lista Destino.
        /// </summary>
        /// <typeparam name="TOrigem">Tipo do objeto de Origem.</typeparam>
        /// <typeparam name="TDestino">Tipo do objeto de Destino.</typeparam>
        /// <param name="origem">Lista de origem que contém as informações.</param>
        /// <param name="destino">Lista de destino para as propriedades.</param>
        public static void MapearLista<TOrigem, TDestino>(List<TOrigem> origem, List<TDestino> destino)
        {
            if (origem == null)
            {
                destino = null;
                return;
            }

            if (destino == null)
            {
                destino = new List<TDestino>();
            }

            foreach (var item in origem)
            {
                var itemDestino = Activator.CreateInstance<TDestino>();
                Mapear(item, itemDestino);
                destino.Add(itemDestino);
            }
        }
    }
}
