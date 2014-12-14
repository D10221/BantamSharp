using System;

namespace SimpleParser
{
    public static class ParserMapExtensions
    {
        public static bool Ok<T>(this Tuple<T, bool> result)
        {
            return result.Item2;
        }

        public static T Parselet<T>(this Tuple<T, bool> result)
        {
            return result.Item1;
        }

        public static Tuple<T, bool> OnSuccess<T>(this Tuple<T, bool> result, Action<T> action)
        {
            if (result.Ok()) action(result.Item1);
            return result;
        }
        
        public static T OkResult<T>(this Tuple<T, bool> result)
        {
            return result.Ok() ? result.Item1 : default(T);
        }

        public static Tuple<T, bool> OnError<T>(this Tuple<T, bool> result, Action action)
        {
            if (!result.Ok()) action();
            return result;
        }
        

    }
}