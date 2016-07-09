using System;
using System.Collections.Generic;

namespace Core.Linq
{
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                throw new InvalidOperationException("source cannot be null");
            }

            if (action == null)
            {
                throw new InvalidOperationException("action cannot be null");
            }

            foreach (T element in source)
            {
                action(element);
            }
        }
    }
}
