namespace WhiteBox.Kernel.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public static class EnumerableExtensions
    {
        public static IList<SelectListItem> ToSelectListItem<TType, TKey>(this IEnumerable<TType> objectList, Func<TType, TKey> key, 
            Func<TType, string> text, string selectedValue = "")
            where TType : class 
        {
            if (objectList == null)
            {
                return new List<SelectListItem>();    
            }

            return objectList.Select(x => new SelectListItem
            {
                Value = key.Invoke(x).ToString(),
                Text = text.Invoke(x),
                Selected = key.Invoke(x).ToString() == selectedValue
            }).ToList();
        }

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition,
            Func<TSource, bool> predicate)
        {
            if (condition)
            {
                return source.Where(predicate);
            }

            return source;
        }

        public static IList<SelectListItem> AddEmptyItem(this IList<SelectListItem> objectList)
        {
            objectList.Insert(0, new SelectListItem { Value = "0", Text = "" });

            return objectList;
        }
    }
}
