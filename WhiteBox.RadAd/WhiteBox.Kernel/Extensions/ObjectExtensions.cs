namespace WhiteBox.Kernel.Extensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Общие методы расширения, применимые к большинству типов.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Приводит переданный экземпляр к определенному типу.
        /// В случае, если экземпляр не приводится к этому типу, будет сгенерировано исключение
        /// </summary>
        /// <param name="instance">Экземпляр, который необходимо привести к целевому типу</param>
        /// <typeparam name="T">Целевой тип</typeparam>
        /// <returns></returns>
        public static T CastAs<T>(this object instance)
        {
            return (T)instance;
        }

        /// <summary>
        /// Приведение струкур к nullable-типу
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="TInput"></typeparam>
        /// <returns></returns>
        public static TInput? AsNullable<TInput>(this TInput instance)
            where TInput : struct
        {
            return (TInput?)instance;
        }

        /// <summary>
        /// Выполнение вычисления на объекте и возврат null в случае если объект равен null или возникла ошибка выполнения
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="evaluator"></param>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static TResult With<TInput, TResult>(this TInput instance, Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            if (instance == null)
            {
                return null;
            }

            TResult result;
            try
            {
                result = evaluator(instance);
            }
            catch
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Вычисление значения и его возврат из переданного экземпляра.        
        /// </summary>
        /// <param name="o">Экземпляр.</param>
        /// <param name="evaluator">Функция вычисления значения.</param>
        /// <param name="failureValue">Значение, которое будет возвращено если в качестве экземпляра передан Null.</param>
        /// <typeparam name="TInput">Тип экземпляра.</typeparam>
        /// <typeparam name="TResult">Тип результата.</typeparam>
        /// <returns></returns>
        public static TResult Return<TInput, TResult>(
            this TInput o,
            Func<TInput, TResult> evaluator,
            TResult failureValue = default(TResult))
        {
            if (typeof(TInput).IsValueType) return evaluator(o);

            return (object)o == null ? failureValue : evaluator(o);
        }

        /// <summary>
        /// Вычисление значения и его возврат из переданного экземпляра.        
        /// </summary>
        /// <param name="o">Экземпляр.</param>
        /// <param name="evaluator">Функция вычисления значения.</param>
        /// <param name="failureValue">Функция вычисления значения, которое будет возвращено если в качестве экземпляра передан null.</param>
        /// <typeparam name="TInput">Тип экземпляра.</typeparam>
        /// <typeparam name="TResult">Тип результата.</typeparam>
        /// <returns></returns>
        public static TResult ReturnFn<TInput, TResult>(
            this TInput o,
            Func<TInput, TResult> evaluator,
            Func<TResult> failureValue)
            where TInput : class
        {
            return o == null ? failureValue() : evaluator(o);
        }

        /// <summary>
        /// Аналог <see cref="Return{TInput,TResult}"/>, перехватывающий ошибки
        /// </summary>
        /// <param name="o">Экземпляр</param>
        /// <param name="evaluator">Функция вычисления значения</param>
        /// <typeparam name="TInput">Тип экземпляра</typeparam>
        /// <typeparam name="TResult">Тип результата</typeparam>
        /// <returns></returns>
        public static TResult ReturnSafe<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TInput : class
        {
            if (o == null)
                return default(TResult);

            TResult result;
            try
            {
                result = evaluator(o);
            }
            catch
            {
                result = default(TResult);
            }

            return result;
        }

        /// <summary>
        /// Выполняет проверку условия на экземляре и возвращает null, если проверка вернула false.
        /// </summary>
        /// <param name="o">
        /// Объект.
        /// </param>
        /// <param name="evaluator">
        /// Делегат, представляющий проверяемое условие.
        /// </param>
        /// <typeparam name="TInput">
        /// Тип объекта.
        /// </typeparam>
        /// <returns>
        /// Для ValueType если условие не выполняется, то возвращается default-значение.
        /// Для ReferenceType если ссылка null, то возвращается null.
        /// </returns>
        public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator)
        {
            if (typeof(TInput).IsValueType)
                return evaluator(o) ? o : default(TInput);

            if ((object)o == null)
            {
                return default(TInput);
            }

            return evaluator(o) ? o : default(TInput);
        }

        /// <summary>
        /// Выполнение действия на экземпляре, если экземлпяр не Null        
        /// </summary>
        /// <param name="o"></param>
        /// <param name="action"></param>
        /// <typeparam name="TInput"></typeparam>
        /// <returns></returns>
        public static TInput Do<TInput>(this TInput o, Action<TInput> action)
            where TInput : class
        {
            if (o == null)
            {
                return null;
            }

            action(o);

            return o;
        }

        /// <summary>
        /// Добавление элемента в список
        /// </summary>
        /// <param name="o">Экземпляр</param>
        /// <param name="container">Список</param>
        /// <typeparam name="TInput">Тип экземляра</typeparam>
        /// <returns></returns>
        public static TInput AddTo<TInput>(this TInput o, IList<TInput> container)
            where TInput : class
        {
            if (o != null)
                container.Add(o);

            return o;
        }

        /// <summary>
        /// Выполнение функции для экземпляра.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fn"></param>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public static TReturn ExprBind<TInput, TReturn>(this TInput value, Func<TInput, TReturn> fn)
        {
            return fn(value);
        }
    }
}
