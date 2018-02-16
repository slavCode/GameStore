namespace GameStoreApplication.Services
{
    using ViewModels.Account;
        
    public interface IUserService
    {
        void Create(RegisterUserViewModel userDetails);
    }
}
