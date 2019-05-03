using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ghumpo.common
{
    public static class Extensions
    {
        public static string RemoveNumberAndComma(this string text)
        {
            var output = Regex.Replace(text, @"[\d,-]", string.Empty);
            return output;
        }

        public static IList<Task> AddTask(this IList<Task> list, Task task)
        {
            if (task != null)
                list.Add(task);
            return list;
        }
    }
}