using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class ListExtensions
    {
        public static bool MoveTo<TItem>(this IList<TItem> list, TItem item, int index)
        {
            if (!list.Contains(item))
                return false;

            if (index < 0 || index >= list.Count)
                throw new ArgumentOutOfRangeException("index", "Index does not represent a location within the list.");

            var currentIndex = list.IndexOf(item);

            if (currentIndex == index)
                return true; // No move necessary.

            list.Remove(item);
            list.Insert(index, item);

            return true;
        }
    }
}
