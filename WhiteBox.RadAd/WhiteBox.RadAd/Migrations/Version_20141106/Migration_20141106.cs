namespace WhiteBox.RadAd.Migrations.Version_20141106
{
    using System.Data;
    using Migrator.Framework;

    [Migration(20141106)]
    public class Migration_20141106 : Migration
    {
        public override void Up()
        {
            Database.AddTable("KERNEL_USER",
                new Column("ID", DbType.Int64, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("LOGIN", DbType.String, 255, ColumnProperty.NotNull),
                new Column("PASSWORD", DbType.String, 1024, ColumnProperty.NotNull),
                new Column("NAME", DbType.String, 255, ColumnProperty.NotNull),
                new Column("SURNAME", DbType.String, 255),
                new Column("TYPE_REGISTRATION", DbType.Int32, 4, ColumnProperty.NotNull),
                new Column("DATE_REGISTRATION", DbType.DateTime, ColumnProperty.NotNull),
                new Column("CONFIRMATION_CODE", DbType.Guid));
        }

        public override void Down()
        {
            Database.RemoveTable("KERNEL_USER");
        }
    }
}