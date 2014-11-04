namespace WhiteBox.Kernel.Repository
{
    using Kernel;
    using NHibernate;

    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            return ApplicationCore.Instance.SessionFactory.OpenSession();
        }
    }
}
