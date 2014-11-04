namespace WhiteBox.RadAd.Map.Migration
{
    using Kernel.Map;

    public class MigrationMap : BaseMap<Entities.Migration.Migration>
    {
        public MigrationMap()
            : base("SCHEMAINFO")
        {
            Map(x => x.Version, "VERSION");
        }
    }
}