namespace WhiteBox.RadAd.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Web.Mvc;
    using Kernel.App;
    using Kernel.DataResult.Impl;
    using Kernel.Repository;
    using Migrator.Framework;
    using Models.Migration;

    public class MigrationController : Controller
    {
        public BaseDataResult GetMigrations()
        {
            var model = new MigrationModel();

            var migrationRepository = new BaseRepository<Entities.Migration.Migration>();

            model.ProducedMigrations = migrationRepository.GetAll()
                .Select(x => x.Version)
                .AsEnumerable();
            model.AvailableMigrations = AvailableMigrationList();

            return BaseDataResult.Success(model);
        }

        public BaseDataResult ProvideMigration(MigrationModel model)
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
                return BaseDataResult.Fail(HttpStatusCode.InternalServerError);
            }

            return BaseDataResult.Success();
        }

        private IEnumerable<long> AvailableMigrationList()
        {
            var list = new List<long>();

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
