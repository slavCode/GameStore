using System.Linq;

namespace GameStoreApplication.Services
{
    using System.Collections.Generic;
    using Data;
    using Data.Models;
    using ViewModels.Admin;

    public class GameService : IGameService
    {
        public void Create(AdminAddGameViewModel model)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = new Game
                {
                    Title = model.Title,
                    Description = model.Description,
                    Image = model.Image,
                    ReleaseDate = model.ReleaseDate,
                    Size = model.Size,
                    Trailer = model.Trailer,
                    Price = model.Price,
                };

                db.Games.Add(game);
                db.SaveChanges();
            }
        }

        public IEnumerable<AdminListGameViewModel> All()
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Games.Select(g => new AdminListGameViewModel
                    {
                        Id = g.Id,
                        Title = g.Title,
                        Price = g.Price,
                        Size = g.Size
                    })
                    .ToList();
            }
        }
    }
}
