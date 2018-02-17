namespace GameStoreApplication.Controllers
{
    using Server.Http.Contracts;
    using Server.Http;
    using Services;
    using ViewModels.Account;

    public class AccountController : Controller
    {
        private const string LoginPath = @"account\login";
        private const string RegisterPath = @"account\register";

        private readonly UserService users = new UserService();

        public IHttpResponse Register()
        {
            return this.FileViewResponse(RegisterPath);
        }

        public IHttpResponse Register(IHttpRequest req, RegisterUserViewModel model)
        {
            var error = this.ValidateModel(model);
            if (error != null)
            {
                ShowError();
                this.ViewData["error"] = error;

                return this.FileViewResponse(RegisterPath);
            }

            var success = this.users.Create(model);

            if (success)
            {
                req.Session.Add(SessionStore.CurrentUserKey, model.Email);

                return this.FileViewResponse(LoginPath);
            }

            else
            {
                ShowError();
                this.ViewData["error"] = "This e-mail is taken.";

                return this.FileViewResponse(RegisterPath);
            }
        }

        public IHttpResponse Login()
        {
            return this.FileViewResponse(LoginPath);
        }

        private void ShowError()
        {
            this.ViewData["showError"] = "block";
        }
    }
}
