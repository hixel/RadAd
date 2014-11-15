namespace WhiteBox.Kernel.DataResult.Impl
{
    using System.Net;

    /// <summary>
    /// Базовая реализация результата ответа сервера
    /// </summary>
    public class BaseDataResult : IDataResult
    {
        public object Data { get; set; }

        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public BaseDataResult()
        {
            StatusCode = HttpStatusCode.OK;
            Message = string.Empty;
            Data = null;
        }

        public BaseDataResult(object data, string message, HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public BaseDataResult(object data, HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Message = string.Empty;
            Data = data;
        }

        public static BaseDataResult Success()
        {
            return new BaseDataResult();
        }

        public static BaseDataResult Success(object data, string message)
        {
            return new BaseDataResult(data, message, HttpStatusCode.OK);
        }

        public static BaseDataResult Fail(string message)
        {
            return new BaseDataResult(null, message, HttpStatusCode.BadRequest);
        }

        public static BaseDataResult Fail(string message, HttpStatusCode statusCode)
        {
            return new BaseDataResult(null, message, statusCode);
        }

        public static BaseDataResult Fail(HttpStatusCode statusCode)
        {
            return new BaseDataResult(null, string.Empty, statusCode);
        }
    }
}
