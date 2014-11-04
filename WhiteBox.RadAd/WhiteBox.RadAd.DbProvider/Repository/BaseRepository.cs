namespace WhiteBox.RadAd.DbProvider.Repository
{
    using System;
    using System.Linq;
    using NHibernate;
    using NHibernate.Linq;
    using DbProvider;
    using Entity;

    public class BaseRepository<T>
        where T : IEntity
    {
        private readonly ISession session;

        public BaseRepository()
        {
            session = ApplicationCore.Instance.SessionFactory.OpenSession();
        }

        public T Get(long id)
        {
            return session.Get<T>(id);
        }

        public T Load(long id)
        {
            return session.Load<T>(id);
        }

        public IQueryable<T> GetAll()
        {
            return session.Query<T>();
        }

        public object Save(T value)
        {
            object newId = null;

            InTransaction(() => newId = session.Save(value));

            return newId;
        }

        public void Update(T value)
        {
            InTransaction(() => session.Update(value));
        }

        public void Delete(T value)
        {
            InTransaction(() => session.Delete(value));
        }

        public void InTransaction(Action action)
        {
            using (var transaction = session.BeginTransaction())
            {
                action();

                transaction.Commit();
            }
        }
    }
}