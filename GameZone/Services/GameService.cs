using GameZone.Data;
using GameZone.Models;
using GameZone.Settings;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace GameZone.Services
{
    public class GameService : IGameService
    {
        public GameService(AppDbContext context, IWebHostEnvironment webHost)
        {
            Context = context;
            WebHost = webHost;
            _imagePath = $"{webHost.WebRootPath}{FileSettings.ImagePath}";
        }

        public AppDbContext Context { get; }
        
        public IWebHostEnvironment WebHost { get; }
        public string _imagePath { get; }

        public async Task Create(CreatGameFormViewModel model)
        {
            //create path and the image name
            var coverName =await SaveCover(model.Cover);
            

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice{ DeviceId = d }).ToList(),

            };

            Context.Add(game);
            Context.SaveChanges();

            

            
        }

        public async Task<Game?> Edit(EditGameFormViewModel model)
        {
            var game = Context.Games.Include(d=>d.Devices).FirstOrDefault(f=> f.Id == model.Id);

            if (game is null)
                return null;

            var hasCover = model.Cover is not null;
            var oldCover = game.Cover;
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if (hasCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }
            

            var effectedRows= Context.SaveChanges();
            if (effectedRows > 0)
            {
                if (hasCover)
                {
                    var path = Path.Combine(_imagePath, oldCover);
                    File.Delete(path);
                }
                return game;
            }
            else
            {
                var path = Path.Combine(_imagePath, game.Cover);
                File.Delete(path);
                return null;

            }
            
            
        }
        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagePath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }
        public IEnumerable<Game> GetAll()
        {
            return Context.Games.Include(g=>g.Category).Include(f=> f.Devices).ThenInclude(d=>d.Device).ToList();
        }

        public Game? GetById(int id)
        {
            return Context.Games.Include(g => g.Category).Include(f => f.Devices).ThenInclude(d => d.Device).FirstOrDefault(g=>g.Id == id);
        }

        public bool Delete(int id)
        {
            var isDeleted = false;

            var game = Context.Games.Find(id);

            if (game is null)
                return isDeleted;

            Context.Remove(game);

            var effectRows = Context.SaveChanges();
            if(effectRows > 0)
            {
                isDeleted = true;

                var path = Path.Combine(_imagePath, game.Cover);
                File.Delete(path);
            }


            return isDeleted;
        }
    }
}
