namespace WhiteBox.RadAd.Map.User
{
    using Kernel.Map;

    public class UserMap 
        : BaseMap<Entities.User.User>
    {
        public UserMap() : base("KERNEL_USER")
        {
            LazyLoad();

            Map(x => x.ConfirmationCode, "CONFIRMATION_CODE");
            Map(x => x.DateRegistration, "DATE_REGISTRATION").Length(255).Not.Nullable();
            Map(x => x.Login, "LOGIN").Length(255).Not.Nullable();
            Map(x => x.Name, "NAME").Length(255).Not.Nullable();
            Map(x => x.Password, "PASSWORD").Length(1024).Not.Nullable();
            Map(x => x.Surname, "SURNAME").Length(255);
            Map(x => x.TypeRegistration, "TYPE_REGISTRATION").Not.Nullable();
        }
    }
}