using System;
using System.Collections.Generic;

namespace Manage_LAN.Library_Class
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> consumer)
        {
            foreach(T item in enumerable)
            {
                consumer( item );
                yield return item;
            }
        }
    }
}
