using System.Collections.Generic;
using System.Linq;

namespace app.Extensions
{
    public static class EnumerableExtensions
    {
        public static T MinOrDefault<T>(this IEnumerable<T> sequence)
        {
            if (sequence.Any())
            {
                return sequence.Min();
            }
            else
            {
                return default(T);
            }
        }

        public static T MaxOrDefault<T>(this IEnumerable<T> sequence)
        {
            if (sequence.Any())
            {
                return sequence.Max();
            }
            else
            {
                return default(T);
            }
        }
    }
}