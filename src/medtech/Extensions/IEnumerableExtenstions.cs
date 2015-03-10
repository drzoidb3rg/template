using System;
using System.Collections.Generic;
using System.Linq;

namespace medtech.Extensions
{
    public static class IEnumerableExtenstions
    {
        public static bool NullSafeAny<T>(this IEnumerable<T> list)
        {
            if (list == null)
                return false;
            return list.Any();
        }


        public static bool NullSafeAny<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {   
            if (list == null)
                return false;
            return list.Any(predicate);
        }
    }
}