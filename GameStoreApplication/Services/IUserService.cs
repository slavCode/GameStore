namespace GameStoreApplication.Services
{
    using ViewModels.Account;
        
    public interface IUserService
    {
        bool Create(RegisterUserViewModel model);

    }
}
