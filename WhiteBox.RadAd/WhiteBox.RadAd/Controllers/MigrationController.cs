namespace WhiteBox.RadAd.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;
    using Entities.Migration;
    using Kernel.Repository;
    using Models.Migration;

    public class MigrationController : ApiController
    {
        public IEnumerable<MigrationModel> Get()
        {
            var migrationRepository = new BaseRepository<Migration>();

            var result = migrationRepository.GetAll()
                .Select(x => new MigrationModel
                {
                    Version = x.Version
                });

            return result;
        }

        public HttpResponseMessage Post([FromBody] MigrationModel model)
        {
            try
            {
                var migrator = new Migrator.Migrator(
                                "PostgreSQL",
                                ConfigurationManager.ConnectionStrings["NHibernate.connectionString"].ToString(),
                                Assembly.GetExecutingAssembly());

                migrator.MigrateTo(model.Version);
            }
            catch (Exception e)
            {
                MvcApplication.Log.Error("", e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
