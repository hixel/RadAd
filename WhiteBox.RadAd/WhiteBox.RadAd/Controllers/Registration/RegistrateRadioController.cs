namespace WhiteBox.RadAd.Controllers.Registration
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Entities.User;
    using Kernel.DataResult.Impl;
    using Kernel.Extensions;
    using Kernel.Repository;
    using Models.Registration;

    public class RegistrateRadioController : ApiController
    {
        public BaseDataResult Post([FromBody] RegistrateRadioModel model)
        {
            try
            {
                var userRepository = new BaseRepository<User>();

                if (model.Password != model.ConfirmPassword)
                {
                    return BaseDataResult.Success();
                }

                var newUser = new User
                {
                    Login = model.Login,
                    Name = model.Name,
                    TypeRegistration = TypeRegistration.Radio,
                    Password = model.Password.GetMd5(),
                    DateRegistration = DateTime.Now,
                    ConfirmationCode = Guid.NewGuid()
                };

                userRepository.Save(newUser);

                return BaseDataResult.Success();
            }
            catch (Exception e)
            {
                MvcApplication.Log.Error("Erro at radio registration", e);
                return BaseDataResult.Fail(HttpStatusCode.InternalServerError);
            }
        }
    }
}
