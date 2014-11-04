namespace WhiteBox.RadAd.Entities.Migration
{
    using Kernel.Entities;

    public class Migration : BaseEntity
    {
        public virtual long Version { get; set; }
    }
}