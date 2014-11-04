using System.Web.Http;

namespace WhiteBox.RadAd.Controllers
{
    using System.Net;
    using System.Net.Http;
    using Models.Registration;

    public class RegistrationController : ApiController
    {
        public HttpResponseMessage Post([FromBody] RegistrationClientModel value)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
