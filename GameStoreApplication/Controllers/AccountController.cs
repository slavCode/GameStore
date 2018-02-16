namespace GameStoreApplication.Controllers
{
    using Server.Http.Contracts;
    using Services;
    using ViewModels.Account;

    public class AccountController : Controller
    {
       private readonly UserService users = new UserService();

        public IHttpResponse Register()
        {
            // this.ViewData["content"] = @"account\register";

            return this.FileViewResponse(@"account\register");
        }

        public IHttpResponse Register(RegisterUserViewModel userDetails)
        {
            this.users.Create(userDetails);

            return null;
        }
    }
}
