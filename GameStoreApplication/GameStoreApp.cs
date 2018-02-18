namespace GameStoreApplication
{
    using Controllers;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Server.Contracts;
    using Server.Routing.Contracts;
    using ViewModels.Account;

    public class GameStoreApp : IApplication
    {
        public void InitializeDatabase()
        {
            using (var db = new GameStoreDbContext())
            {
                db.Database.Migrate();
            }
        }

        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AnonymousPaths.Add("/");
            appRouteConfig.AnonymousPaths.Add("/account/login");
            appRouteConfig.AnonymousPaths.Add("/account/register");
            appRouteConfig.AnonymousPaths.Add("/account/logout");
            appRouteConfig.AnonymousPaths.Add("/admin/add-game");


            appRouteConfig
                .Get("account/register",
                    req => new AccountController(req).Register());

            appRouteConfig
                .Post("account/register",
                    req => new AccountController(req).Register(new RegisterUserViewModel
                    {
                        FullName = req.FormData["full-name"],
                        Email = req.FormData["email"],
                        Password = req.FormData["password"],
                        ConfirmPassword = req.FormData["confirm-password"]
                    }));

            appRouteConfig
                .Get("account/login", req => new AccountController(req).Login());

            appRouteConfig
                .Post("account/login",
                    req => new AccountController(req).Login(new LoginUserViewModel
                    {
                        Email = req.FormData["email"],
                        Password = req.FormData["password"]
                    }));

            appRouteConfig
                .Get("account/logout", req => new AccountController(req).Logout());

            appRouteConfig
                .Get("/", req => new HomeController(req).Index());

            appRouteConfig
                .Get("/admin/games/add", req => new AdminController(req).Add());

        }
    }
}
