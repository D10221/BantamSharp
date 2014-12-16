using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public static class TokenExtensions
    {
       
        public static R Extract<T,R>(this Tuple<T, char> tt, Func<Tuple<T, char>, R> ttAction)
        {
            if (tt.HasValue())
                return  ttAction(tt);
            return default(R);
        }

        public static bool HasValue<T,R>(this Tuple<R, T> tt)
        {
            return tt != null && !Equals(tt.Item1, default(R));
        }
        public static R TknType<T,R>(this Tuple<R, T> tuple)
        {
            return tuple != null ? tuple.Item1 : default(R);
        }

        public static T Value<T,R>(this Tuple<R, T> tuple)
        {
            return tuple != null ? tuple.Item2 : default(T);
        }
       
        public static Tuple<R, T> GetTokenType<T,R>(this IEnumerable<Tuple<R, T>> tt, T c)
        {
            return tt.FirstOrDefault(t => Equals(t.Value(), c));
        }
    }
}