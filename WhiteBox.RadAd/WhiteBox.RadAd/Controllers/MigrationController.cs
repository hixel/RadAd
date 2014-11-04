namespace WhiteBox.RadAd.Controllers
{
    using System;
    using System.Configuration;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;

    public class MigrationController : ApiController
    {
        public HttpResponseMessage Get()
        {
            Assembly a = System.Reflection.Assembly.Load("WhiteBox.RadAd");

            Migrator.Migrator m = new Migrator.Migrator("PostgreSQL", ConfigurationManager.ConnectionStrings["NHibernate.connectionString"].ToString(), a);
            m.MigrateTo(0);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Post()
        {
            //var migrator = new Migrator(
            //    "ECM7.Migrator.Providers.PostgreSQL.PostgreSQLDialect, ECM7.Migrator.Providers.PostgreSQL",
            //    Convert.ToString(ConfigurationManager.ConnectionStrings["NHibernate.connectionString"]),
            //    Assembly.GetExecutingAssembly());
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
