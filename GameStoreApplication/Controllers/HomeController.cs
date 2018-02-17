namespace GameStoreApplication.Controllers
{
    using Server.Http.Contracts;

    public class HomeController : Controller
    {
        public HomeController(IHttpRequest request) 
            : base(request)
        {
        }

        public IHttpResponse Index()
        {
            return this.FileViewResponse(@"home\index");
        }
    }
}
