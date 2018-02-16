using GameStoreApplication.ViewModels.Account;

namespace GameStoreApplication
{
    using Controllers;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Server.Contracts;
    using Server.Routing.Contracts;

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
            appRouteConfig.AnonymousPaths.Add("/account/register");

            appRouteConfig
                .Get("account/register",
                    req => new AccountController().Register());

            appRouteConfig
                .Post("account/register",
                    req => new AccountController().Register(new RegisterUserViewModel
                    {
                        FullName = req.FormData["full-name"],
                        Email = req.FormData["email"],
                        Password = req.FormData["password"],
                        ConfirmPassword = req.FormData["confirm-password"]

                    }));
        }
    }
}
