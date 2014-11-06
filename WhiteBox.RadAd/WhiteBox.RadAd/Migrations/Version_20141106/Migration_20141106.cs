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