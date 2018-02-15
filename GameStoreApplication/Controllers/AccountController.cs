namespace GameStoreApplication.Controllers
{
    using Server.Http.Contracts;

    public class AccountController : Controller
    {
        public IHttpResponse Register()
        {
            return this.FileViewResponse(@"account\register");
        }
    }
}
