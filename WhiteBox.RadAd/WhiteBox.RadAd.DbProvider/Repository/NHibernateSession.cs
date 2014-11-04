namespace WhiteBox.RadAd.DbProvider.Repository
{
    using NHibernate;
    using DbProvider;

    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            return ApplicationCore.Instance.SessionFactory.OpenSession();
        }
    }
}
