using Microsoft.AspNetCore.Mvc;
using OfficeReservation.Services.DTOs.Favorite;
using OfficeReservation.Services.Interfaces;
using OfficeReservation.Web.Models.Workstation;

namespace OfficeReservation.Web.Controllers
{
    public class WorkstationController : BaseController
    {
        private readonly IWorkstationService workstationService;
        private readonly IFavoriteService favoriteService;
        public WorkstationController(IWorkstationService workstationService, IFavoriteService favoriteService)
        {
            this.workstationService = workstationService;
            this.favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var all = await workstationService.GetAllAsync();
            var favorites = await favoriteService.GetUserFavoritesAsync(CurrentUserId);
            var favoriteIds = favorites.Favorites.Select(f => f.WorkstationId).ToHashSet();

            var model = all.Workstations.Select(ws => new WorkstationViewModel
            {
                WorkstationId = ws.WorkstationId,
                Floor = ws.Floor,
                Zone = ws.Zone,
                HasMonitor = ws.HasMonitor,
                HasDockingStation = ws.HasDockingStation,
                NearWindow = ws.NearWindow,
                NearPrinter = ws.NearPrinter,
                IsFavorite = favoriteIds.Contains(ws.WorkstationId)
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(int workstationId)
        {
            var isFavoriteResponse = await favoriteService.IsFavoritedAsync(new IsFavoritedRequest { WorkstationId = workstationId, UserId = CurrentUserId });
            var isFavorite = isFavoriteResponse.IsFavorited;
            if (isFavorite && isFavoriteResponse.FavoriteId != null)
            {
                var response = await favoriteService.RemoveFromFavoritesAsync(isFavoriteResponse.FavoriteId.Value);
                if (!response.Success)
                    TempData["ErrorMessage"] = response?.ErrorMessage;
            }
                
            else
            {
                var response = await favoriteService.AddToFavoritesAsync(new AddToFavoritesRequest { WorkstationId = workstationId, UserId = CurrentUserId });
                if (!response.Success)
                    TempData["ErrorMessage"] = response?.ErrorMessage;
            }
                

            return RedirectToAction(nameof(Index));
        }

    }
}
