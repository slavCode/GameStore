namespace GameStoreApplication.Controllers
{
    using Server.Http.Contracts;

    public class AdminController : Controller
    {
        private const string AddGamePath = @"admin\add-game";

        public AdminController(IHttpRequest request) 
            : base(request)
        {
        }

        public IHttpResponse Add()
        {
            return this.FileViewResponse(AddGamePath);
        }
    }
}
