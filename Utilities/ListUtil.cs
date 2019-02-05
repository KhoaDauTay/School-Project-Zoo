using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    /// <summary>
    /// A class that contains helper code to support enhanced functionality for lists.
    /// </summary>
    public static class ListUtil
    {
        /// <summary>
        /// Flattens a list of values to a delimited string.
        /// </summary>
        /// <param name="list">The list of values to flatten.</param>
        /// <param name="separator">A string to insert as a delimiter between each of the values.</param>
        /// <returns>A flattened string.</returns>
        public static string Flatten(this IEnumerable<string> list, string separator)
        {
            string result = null;

            list.ToList().ForEach(s => result += result == null ? s : separator + s);

            return result;
        }
    }
}