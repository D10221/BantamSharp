using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public static class TokenExtensions
    {
        public static IEnumerable<T> Range<T>(this IEnumerable<T> source,int start, int end )
        {
            return source.Where((t, i) => i > start && i < end);
        }

        public static T With<T>(this T t, Action<T> action, Func<T,bool> condition=null)
        {
            condition = condition ?? (x => !Equals(x, default(T)));
            if (condition(t)) action(t);
            return default(T);
        }
        
        public static R Extract<T,R>(this T t, Func<T,R> action, Func<T,bool> condition=null)
        {
            condition = condition ?? (x => !Equals(x, default(T)));
            return condition(t) ? action(t) : default(R);
        }

        public static R Extract<R>(this Tuple<TokenType, char> tt, Func<Tuple<TokenType, char>, R> ttAction)
        {
            if (tt.HasValue())
                return  ttAction(tt);
            return default(R);
        }

        public static bool HasValue<T>(this Tuple<TokenType, T> tt)
        {
            return tt != null && tt.Item1 != TokenType.NONE;
        }
        public static TokenType TknType<T>(this Tuple<TokenType, T> tuple)
        {
            return tuple != null ? tuple.Item1 : TokenType.NONE;
        }

        public static T Value<T>(this Tuple<TokenType, T> tuple)
        {
            return tuple != null ? tuple.Item2 : default(T);
        }
       
        public static Tuple<TokenType, T> GetTokenType<T>(this IEnumerable<Tuple<TokenType, T>> tt, T c)
        {
            return tt.FirstOrDefault(t => Equals(t.Value(), c));
        }
    }
}