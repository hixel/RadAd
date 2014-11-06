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
    using FluentNHibernate.Cfg;
    using Kernel.App;
    using Kernel.Repository;
    using Migrator.Framework;
    using Models.Migration;

    public class MigrationController : ApiController
    {
        public MigrationModel Get()
        {
            var model = new MigrationModel();

            var migrationRepository = new BaseRepository<Entities.Migration.Migration>();

            model.ProducedMigrations = migrationRepository.GetAll()
                .Select(x => x.Version)
                .AsEnumerable();
            model.AvailableMigrations = AvailableMigrationList();

            return model;
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

        private IList<long> AvailableMigrationList()
        {
            List<long> list = new List<long>();

            var modules = AssemblyHelper.GetModules();
            foreach (var module in modules)
            {
                foreach (var type in module.GetType().Assembly.GetExportedTypes())
                {
                    var attribute = Attribute.GetCustomAttribute(type, typeof(MigrationAttribute)) as MigrationAttribute;
                    if (attribute != null && typeof(IMigration).IsAssignableFrom(type) && !attribute.Ignore)
                    {
                        list.Add(attribute.Version);
                    }
                }
            }

            return list;
        }
    }
}
