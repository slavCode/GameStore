using GameStoreApplication.ViewModels.Admin;

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
            if (this.Authentication.IsAdmin)
            {
                return this.FileViewResponse(AddGamePath);
            }

            return this.RedirectResponse(HomePath);
        }

        public IHttpResponse Add(AdminAddGameViewModel model)
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(HomePath);
            }

            return null;
        }
    }
}
