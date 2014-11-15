namespace WhiteBox.Kernel.DataResult
{
    using System.Net;
    using System.Net.Http;

    /// <summary>
    /// Результат ответа сервера
    /// </summary>
    public interface IDataResult
    {
        /// <summary>
        /// Данные в динамическом виде
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// Некоторое сообщение с дополнительной информацией
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Тип ответа
        /// </summary>
        HttpStatusCode StatusCode { get; set; }
    }
}