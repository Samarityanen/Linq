﻿namespace Bars.NuGet.Querying.Functionality
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal static class Apply
    {
        /// <summary>
        /// Функтор агрегатор коллекций в одну
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collections"></param>
        /// <returns></returns>
		internal static IEnumerable<T> Collection<T>(IEnumerable<IEnumerable<T>> collections)
        {
            var list = new List<T>();
            foreach (var collection in collections)
            {
                list.AddRange(collection);
            }

            return list.ToArray();
        }

        /// <summary>
        /// Функтор агрегатор предикатов в успех при хотя бы одном успешном результате
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        internal static bool OneIsSuccess(IEnumerable<bool> vs)
        {
            return vs.Any(x => x == true);
        }

        /// <summary>
        /// Функтор агрегатор берёт первый результат
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        internal static T First<T>(IEnumerable<T> ts)
        {
            return ts.FirstOrDefault();
        }
    }
}
