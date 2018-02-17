namespace GameStoreApplication.Services
{
    using Data;
    using Data.Models;
    using System.Linq;
    using ViewModels.Account;

    public class UserService : IUserService
    {
        public void Create(RegisterUserViewModel userDetails)
        {
            using (var db = new GameStoreDbContext())
            {
                var isFirstUser = !db.Users.Any();

                var user = new User
                {
                    Name = userDetails.FullName,
                    Email = userDetails.Email,
                    Password = userDetails.Password
                };

                if (isFirstUser) user.IsAdmin = true;

                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public bool Find(LoginUserViewModel loginUserDetails)
        {
            using (var db = new GameStoreDbContext())
            {
                var user = db
                    .Users
                    .FirstOrDefault(u => u.Email == loginUserDetails.Email);

                if (user != null) return true;

                return false;
            }
        }
    }
}
