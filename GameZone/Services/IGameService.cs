using GameZone.Models;
using GameZone.ViewModels;

namespace GameZone.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetAll();
        Task Create(CreatGameFormViewModel model);
        Game? GetById(int id);
        Task<Game?> Edit(EditGameFormViewModel model);

        bool Delete(int id);
    }
}
