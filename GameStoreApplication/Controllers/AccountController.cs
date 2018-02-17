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
            SetDefaultViewData();

            return this.FileViewResponse(RegisterPath);
        }

        public IHttpResponse Register(IHttpRequest req, RegisterUserViewModel model)
        {
            var error = this.ValidateModel(model);
            if (error != null)
            {
                ShowError(error);

                return this.FileViewResponse(RegisterPath);
            }

            var success = this.users.Create(model);

            if (success)
            {
                LoginUser(req, model.Email);

                return this.FileViewResponse(LoginPath);
            }

            ShowError("This e-mail is taken.");

            SetDefaultViewData();
            
            return this.FileViewResponse(RegisterPath);
        }

        public IHttpResponse Login()
        {
            SetDefaultViewData();

            return this.FileViewResponse(LoginPath);
        }

        public IHttpResponse Login(IHttpRequest req, LoginUserViewModel model)
        {
            var success = this.users.Find(model);

            if (success)
            {
                LoginUser(req, model.Email);

                return this.FileViewResponse("/");
            }

            ShowError("Invalid name or password.");

            SetDefaultViewData();
            
            return this.FileViewResponse(LoginPath);
        }

        private void ShowError(string errorMessage)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["error"] = errorMessage;
        }

        private static void LoginUser(IHttpRequest req, string email)
        {
            req.Session.Add(SessionStore.CurrentUserKey, email);
        }

        public void SetDefaultViewData()
        {
            this.ViewData["authenticatedDisplay"] = "none";
            this.ViewData["anonymousDisplay"] = "flex";
        }
    }
}
