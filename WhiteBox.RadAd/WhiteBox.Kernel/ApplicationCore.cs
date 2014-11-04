namespace WhiteBox.Kernel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using App;
    using FluentNHibernate.Cfg;
    using NHibernate;
    using NHibernate.Cfg;

    public sealed class ApplicationCore
    {
        private static readonly ApplicationCore instance = new ApplicationCore();
        private static ISessionFactory sessionFactory;

        public static ApplicationCore Instance
        {
            get { return instance; }
        }

        public ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();

                    var modules = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(x => x.IsClass
                                    && !x.IsAbstract
                                    && typeof (IModule).IsAssignableFrom(x))
                        .Select(x => Activator.CreateInstance(x) as IModule);
                    foreach (var module in modules)
                    {
                        var loadedAssembly = Assembly.Load(module.GetAssembly().GetName().Name);

                        var cfg = Fluently.Configure(configuration)
                            .Mappings(c => c.FluentMappings.AddFromAssembly(loadedAssembly));

                        sessionFactory = cfg.BuildConfiguration().BuildSessionFactory();
                    }
                }

                return sessionFactory;
            }
        }

        public ISession GetSession()
        {
            return sessionFactory.OpenSession();
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(x => !x.IsDynamic);
        }
    }
}
