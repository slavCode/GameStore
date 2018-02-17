namespace GameStoreApplication.Controllers
{
    using Server.Http.Contracts;
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

        public IHttpResponse Register(RegisterUserViewModel model)
        {
            var error = this.ValidateModel(model);
            if (error != null)
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = error;
                
                return this.FileViewResponse(RegisterPath);
            }

            this.users.Create(model);

            return this.FileViewResponse(LoginPath);
        }

        public IHttpResponse Login()
        {
            return this.FileViewResponse(LoginPath);
        }

        public IHttpResponse Login(LoginUserViewModel loginUser)
        {
            var success = this.users.Find(loginUser);

            if (success)
            {
                
            }



            return this.FileViewResponse(@"account\login");
        }
    }
}
