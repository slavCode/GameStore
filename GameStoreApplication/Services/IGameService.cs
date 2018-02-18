namespace GameStoreApplication.Services
{
    using System.Collections.Generic;
    using ViewModels.Admin;

    public interface IGameService
    {
        void Create(AdminAddGameViewModel model);

        IEnumerable<AdminListGameViewModel> All();
    }
}
