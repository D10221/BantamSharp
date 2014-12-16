using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public static class GenericExtensions
    {
        public static IEnumerable<T> Range<T>(this IEnumerable<T> source, int start, int end)
        {
            return source.Where((t, i) => i > start && i < end);
        }

        public static T With<T>(this T t, Action<T> action, Func<T, bool> condition = null)
        {
            condition = condition ?? (x => !Equals(x, default(T)));
            if (condition(t)) action(t);
            return default(T);
        }

        public static R Extract<T, R>(this T t, Func<T, R> action, Func<T, bool> condition = null)
        {
            condition = condition ?? (x => !Equals(x, default(T)));
            return condition(t) ? action(t) : default(R);
        }
    }
}
