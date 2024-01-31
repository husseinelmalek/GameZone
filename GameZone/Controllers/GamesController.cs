using GameZone.Data;
using GameZone.Models;
using GameZone.Services;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        public IDevicesService DevicesService { get; }
        public ICategoriesService CategoriesService { get; }
        public IGameService GameService { get; }

        public GamesController(IDevicesService devicesService,ICategoriesService categoriesService,IGameService gameService)
        {
            DevicesService = devicesService;
            CategoriesService = categoriesService;
            GameService = gameService;
        }
        public IActionResult Index()
        {
            var games = GameService.GetAll();
            return View(games);
        }
        public IActionResult Create()
        {
            CreatGameFormViewModel ModelVm = new()
            {
                Categories = CategoriesService.GetSelectedList(),
                Devices = DevicesService.GetSelectedList(),


            };
            return View(ModelVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = CategoriesService.GetSelectedList();
                model.Devices = DevicesService.GetSelectedList();

                return View(model);
            }
            await GameService.Create(model);
            

            return RedirectToAction(nameof(Index));

        }
        public IActionResult Details(int id)
        {
            var gameItem = GameService.GetById(id);
            if (gameItem is null)
                return NotFound();
            return View(gameItem);
        }

        public IActionResult Edit(int id)
        {
            var gameItem = GameService.GetById(id);
            if (gameItem is null)
                return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id = gameItem.Id,
                Name = gameItem.Name,
                Description = gameItem.Description,
                CategoryId = gameItem.CategoryId,
                SelectedDevices = gameItem.Devices.Select(d => d.DeviceId).ToList(),
                Categories = CategoriesService.GetSelectedList(),
                Devices = DevicesService.GetSelectedList(),
                CurrentCover = gameItem.Cover,
            };
            
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = CategoriesService.GetSelectedList();
                model.Devices = DevicesService.GetSelectedList();
                return View(model);
            }
            var game=await GameService.Edit(model);
            if (game is null)
                return BadRequest();
           return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var isDeleted = GameService.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }
    }
}
