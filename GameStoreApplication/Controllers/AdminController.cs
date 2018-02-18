namespace GameStoreApplication.Controllers
{
    using Server.Http.Contracts;
    using Services;
    using System;
    using System.Linq;
    using ViewModels.Admin;

    public class AdminController : Controller
    {
        private const string AddGamePath = @"admin\add-game";
        private const string ListGamePath = @"admin\list-games";


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

            return this.RedirectResponse(ListGamePath);
        }

        public IHttpResponse List()
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(HomePath);
            }

            var result = this.games
                .All()
                .Select(
                    g => $@"<tr>
                                <td>{g.Id}</td>
                                <td>{g.Title}</td>
                                <td>{g.Size:F2} GB</td>
                                <td>{g.Price} &euro;</td>
                                <td>
                                   <a class=""btn btn-warning"" href=""/admin/games/edit/{g.Id}"">Edit</a>
                                   <a class=""btn btn-danger"" href=""/admin/games/delete/{g.Id}"">Delete</a>
                                </td>
                            </tr>");

            this.ViewData["results"] = string.Join(Environment.NewLine, result);

            return this.FileViewResponse(ListGamePath);
        }
    }
}
