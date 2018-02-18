namespace GameStoreApplication.Controllers
{
    using Server.Http.Contracts;
    using Services;
    using ViewModels.Admin;

    public class AdminController : Controller
    {
        private const string AddGamePath = @"admin\add-game";

        private readonly IGameService games;

        public AdminController(IHttpRequest request)
            : base(request)
        {
            this.games = new GameService();
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

            this.games.Create(model);

            return this.RedirectResponse("/admin/games/list");
        }
    }
}
