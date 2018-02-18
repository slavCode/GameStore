namespace GameStoreApplication.Services
{
    using ViewModels.Admin;

    public interface IGameService
    {
        void Create(AdminAddGameViewModel model);
    }
}
