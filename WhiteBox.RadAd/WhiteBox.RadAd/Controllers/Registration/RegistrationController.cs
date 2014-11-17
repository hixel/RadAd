namespace WhiteBox.RadAd.Controllers.Registration
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using CaptchaMvc.Infrastructure;
    using Entities.User;
    using Kernel.DataResult.Impl;
    using Kernel.Extensions;
    using Kernel.Repository;
    using Models.Registration;

    public class RegistrationController : Controller
    {
        public BaseDataResult RegistrateRadio(RegistrateRadioModel model)
        {
            try
            {
                var userRepository = new BaseRepository<User>();

                if (model.Password != model.ConfirmPassword)
                {
                    return BaseDataResult.Success();
                }

                var captchaValue = CaptchaUtils.CaptchaManager.StorageProvider
                             .GetValue(model.CaptchaToken, CaptchaMvc.Interface.TokenType.Validation);

                if (captchaValue == null || !captchaValue.IsEqual(model.Captcha))
                {
                    return BaseDataResult.Fail("Некорректная капча");
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