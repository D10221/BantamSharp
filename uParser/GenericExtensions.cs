using System.Collections.Generic;
using System.Linq;

namespace uParser
{
    public static class GenericExtensions
    {
        public static IEnumerable<T> Range<T>(this IEnumerable<T> source, int start, int end)
        {
            return source.Where((t, i) => i > start && i < end);
        }
        public static IDictionary<TV, TK> Reverse<TK, TV>(this IDictionary<TK, TV> input ){
            return input.ToDictionary(x=> x.Value, x=>x.Key);
        }
    }
}