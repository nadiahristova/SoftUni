namespace CustomLINQExtension.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class Extensions
    {
        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> collection,
            Func<T, bool> predicate)
        {
            IList<T> result = new List<T>();

            foreach (var item in collection)
                if (!predicate(item))
                    result.Add(item);

            return result;
        }

        public static TSelector Max<TSource, TSelector>(this IEnumerable<TSource> collection,
            Func<TSource, TSelector> SelectFunc)
            where TSelector : IComparable
        {
            TSelector output = default(TSelector);

            foreach (var item in collection)
            {
                TSelector sel = SelectFunc(item);
                if (sel.CompareTo(output) > 0)
                    output = sel;
            }

            return output;
        }
    }
}
