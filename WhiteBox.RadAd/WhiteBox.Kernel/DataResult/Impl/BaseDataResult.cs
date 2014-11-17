namespace WhiteBox.Kernel.DataResult.Impl
{
    using System;
    using System.Data;
    using System.Net;
    using System.Net.Mime;
    using System.Text;
    using System.Web.Mvc;
    using Extensions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Базовая реализация результата ответа сервера
    /// </summary>
    public class BaseDataResult : ActionResult
    {
        public object Data { get; set; }

        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Получение или установка кодировки контента.
        /// </summary>
        public Encoding ContentEncoding { get; set; }

        /// <summary>
        /// Получение или установка типа контента.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Получение или установка формата.
        /// </summary>
        public Formatting Formatting { get; set; }

        /// <summary>
        /// Получение или установка настроек сериализатора.
        /// </summary>
        public JsonSerializerSettings SerializerSettings { get; set; }

        public BaseDataResult()
            : this(null, string.Empty, HttpStatusCode.OK)
        {
        }

        public BaseDataResult(object data, string message, HttpStatusCode statusCode)
        {
            SerializerSettings = new JsonSerializerSettings();

            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public BaseDataResult(object data, HttpStatusCode statusCode)
            : this(data, string.Empty, statusCode)
        {
        }

        public static BaseDataResult Success()
        {
            return new BaseDataResult();
        }

        public static BaseDataResult Success(object data)
        {
            return new BaseDataResult(data, string.Empty, HttpStatusCode.OK);
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

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;
            response.StatusCode = (int)StatusCode;
            response.ContentType = !ContentType.IsNullOrEmpty() ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (Data == null)
            {
                return;
            }

            response.StatusCode = (int)StatusCode;

            var writer = new JsonTextWriter(response.Output) { Formatting = Formatting };

            SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            if (Data is DataTable)
            {
                SerializerSettings.Converters.Add(new DataTableConverter());

                SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                SerializerSettings.ObjectCreationHandling = ObjectCreationHandling.Replace;
                SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }

            var serializer = JsonSerializer.Create(SerializerSettings);
            serializer.Serialize(writer, Data);
            writer.Flush();
        }
    }
}
