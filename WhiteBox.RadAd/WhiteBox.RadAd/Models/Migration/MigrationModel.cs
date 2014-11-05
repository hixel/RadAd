namespace WhiteBox.RadAd.Models.Migration
{
    using System.Collections.Generic;

    public class MigrationModel
    {
        public long Version { get; set; }

        public IEnumerable<long> ProducedMigrations { get; set; }

        public IEnumerable<long> AvailableMigrations { get; set; }
    }
}