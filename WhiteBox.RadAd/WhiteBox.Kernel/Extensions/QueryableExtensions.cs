namespace WhiteBox.Kernel.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public static class QueryableExtensions
    {
        /// <summary>
        /// Условная сортировка последовательности.
        /// </summary>
        /// <param name="query">
        /// </param>
        /// <param name="condition">
        /// Проводить или нет сортировку. 
        /// </param>
        /// <param name="asc">
        /// Сортировать по возрастанию или убыванию. 
        /// </param>
        /// <param name="keySelector">
        /// Выражение для вычисления ключа. 
        /// </param>
        /// <typeparam name="T">
        /// Тип элементов последовательности.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// Тип ключей.
        /// </typeparam>
        /// <returns>
        /// </returns>
        public static IQueryable<T> OrderIf<T, TKey>(
            this IQueryable<T> query,
            bool condition,
            bool asc,
            Expression<Func<T, TKey>> keySelector)
        {
            if (!condition)
            {
                return query;
            }

            return asc ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);
        }

        /// <summary>
        /// Дополнительная условная сортировка последовательности.
        /// </summary>
        /// <param name="query">
        /// </param>
        /// <param name="condition">
        /// Проводить или нет сортировку. 
        /// </param>
        /// <param name="asc">
        /// Сортировать по возрастанию или убыванию. 
        /// </param>
        /// <param name="keySelector">
        /// Выражение для вычисления ключа. 
        /// </param>
        /// <typeparam name="T">
        /// Тип элементов последовательности.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// Тип ключей.
        /// </typeparam>
        /// <returns>
        /// </returns>
        public static IQueryable<T> OrderThenIf<T, TKey>(
            this IQueryable<T> query,
            bool condition,
            bool asc,
            Expression<Func<T, TKey>> keySelector)
        {
            if (!condition)
            {
                return query;
            }

            if (asc)
            {
                return ((IOrderedQueryable<T>)query).ThenBy(keySelector);
            }

            return ((IOrderedQueryable<T>)query).ThenByDescending(keySelector);
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition,
            Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
            {
                return source.Where(predicate);
            }

            return source;
        }
    }
}
