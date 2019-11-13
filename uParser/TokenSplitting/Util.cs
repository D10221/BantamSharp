using System;
using System.Collections.Generic;
using System.Linq;

namespace TokenSplitting
{
    internal static class Util
    {
        public static string SliceToEnd(string input, int start)
        {
            return input.Substring(start, input.Length - start);
        }
        public static IEnumerable<T> SliceToEnd<T>(IEnumerable<T> input, int start)
        {
            var array = ToArray(input);
            if (start > array.Length - 1 || start < 0) throw new ArgumentOutOfRangeException(nameof(start));
            IEnumerable<T> ret = new T[0];
            for (var i = 0; i < array.Length; i++)
            {
                if (i >= start) ret = ret.Concat(new T[] { array[i] });
            }
            return ret;
        }
        public static T[] ToArray<T>(IEnumerable<T> input)
        {
            return input is T[]? (T[])input : input.ToArray();
        }
        public static IEnumerable<T> Exclude<T>(IEnumerable<T> input, int index)
        {
            var array = ToArray(input);
            if (index > array.Length - 1 || index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            IEnumerable<T> ret = new T[0];
            for (var i = 0; i < array.Length; i++)
            {
                if (i != index) ret = ret.Concat(new T[] { array[i] });
            }
            return ret;
        }    
    }
}