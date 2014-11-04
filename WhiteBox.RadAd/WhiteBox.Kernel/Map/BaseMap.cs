namespace WhiteBox.Kernel.Map
{
    using Entities;
    using FluentNHibernate.Mapping;

    public class BaseMap<T> : ClassMap<T>
        where T : IEntity
    {
        public BaseMap(string tableName)
        {
            Table(tableName);

            Id(x => x.Id, "ID").CustomSqlType("Serial").GeneratedBy.Increment();
        }
    }
}
