namespace GameStoreApplication.Services
{
    using Data;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;
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

        public AdminAddGameViewModel FindById(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Games
                    .Where(g => g.Id == id)
                    .Select(g => new AdminAddGameViewModel
                    {
                        Id = g.Id,
                        Description = g.Description,
                        Size = g.Size,
                        Price = g.Price,
                        ReleaseDate = g.ReleaseDate,
                        Trailer = g.Trailer,
                        Image = g.Image,
                        Title = g.Title
                    })
                    .FirstOrDefault();
            }
        }

        public void Edit(AdminAddGameViewModel model)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db
                    .Games
                    .FirstOrDefault(g => g.Id == model.Id);

                if (game == null)
                {
                    return;
                }

                game.Description = model.Description;
                game.Image = model.Image;
                game.Price = model.Price;
                game.ReleaseDate = model.ReleaseDate;
                game.Size = model.Size;
                game.Title = model.Title;
                game.Trailer = model.Trailer;

                db.SaveChanges();
            }
        }
    }
}
