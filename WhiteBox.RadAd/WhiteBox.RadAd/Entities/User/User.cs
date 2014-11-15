namespace WhiteBox.RadAd.Entities.User
{
    using System;
    using Kernel.Entities;
    using Models.Registration;

    public class User : BaseEntity
    {
        public virtual string Login { get; set; }

        public virtual string Name { get; set; }
                
        public virtual string Surname { get; set; }
                
        public virtual TypeRegistration TypeRegistration { get; set; }
                
        public virtual string Password { get; set; }
                
        public virtual DateTime DateRegistration { get; set; }
                
        public virtual Guid? ConfirmationCode { get; set; }
    }
}