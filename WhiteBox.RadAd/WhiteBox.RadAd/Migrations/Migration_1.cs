namespace WhiteBox.RadAd.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(1)]
    public class Migration_1 : Migration
    {
        public override void Up()
        {
            Database.AddTable("KERNEL_USER",
                new Column("ID", DbType.Int64, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("LOGIN", DbType.String, ColumnProperty.NotNull),
                new Column("NAME", DbType.String),
                new Column("SURNAME", DbType.String));
        }

        public override void Down()
        {
            Database.RemoveTable("KERNEL_USER");
        }
    }
}