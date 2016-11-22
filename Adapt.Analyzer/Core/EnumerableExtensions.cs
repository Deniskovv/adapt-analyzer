using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adapt.Analyzer.Core
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T element)
        {
            return source.Concat(Enumerable.Repeat(element, 1));
        }

        public static async Task<IEnumerable<TSource>> Flatten<TSource>(this IEnumerable<Task<TSource[]>> source)
        {
            var tasks = source.ToArray();
            var flat = await Task.WhenAll(tasks);
            return flat.SelectMany(s => s);
        }
    }
}
