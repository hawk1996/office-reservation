using Microsoft.AspNetCore.Mvc;
using OfficeReservation.Services.DTOs.Reservation;
using OfficeReservation.Services.Interfaces;
using OfficeReservation.Web.Models.Reservation;

namespace OfficeReservation.Web.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly IWorkstationService workstationService;
        private readonly IFavoriteService favoriteService;
        private readonly IReservationService reservationService;

        public ReservationController(
            IWorkstationService workstationService,
            IFavoriteService favoriteService,
            IReservationService reservationService)
        {
            this.workstationService = workstationService;
            this.favoriteService = favoriteService;
            this.reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(DateOnly reservationDate)
        {
            var availableWorkstationsResponse = await workstationService.GetAllAvailableAsync(reservationDate);
            var availableWorkstations = availableWorkstationsResponse.Workstations;
            var favoritesResponse = await favoriteService.GetUserFavoritesAsync(CurrentUserId);
            var favoriteWorkstationIds = favoritesResponse.Favorites.Select(f => f.WorkstationId);

            var model = new CreateReservationViewModel
            {
                ReservationDate = reservationDate,
                Workstations = availableWorkstations.Select(ws => new WorkstationReservationOptionViewModel
                {
                    WorkstationId = ws.WorkstationId,
                    Floor = ws.Floor,
                    Zone = ws.Zone,
                    HasMonitor = ws.HasMonitor,
                    HasDockingStation = ws.HasDockingStation,
                    NearWindow = ws.NearWindow,
                    NearPrinter = ws.NearPrinter,
                    IsFavorite = favoriteWorkstationIds.Contains(ws.WorkstationId)
                })
                .OrderByDescending(ws => ws.IsFavorite) // favorites on top
                .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationViewModel model)
        {
            if (!ModelState.IsValid)
                return await RedisplayCreateFormWithErrors(model);

            var reservedIdsResponse = await reservationService.GetReservedWorkstationIdsAsync(model.ReservationDate);
            var reservedIds = reservedIdsResponse.ReservedWorkstationIds;
            if (reservedIds.Contains(model.SelectedWorkstationId))
            {
                ModelState.AddModelError("", "This workstation is no longer available.");
                return await RedisplayCreateFormWithErrors(model);
            }

            var addResponse = await reservationService.AddAsync(new AddReservationRequest
            {
                UserId = CurrentUserId,
                WorkstationId = model.SelectedWorkstationId,
                ReservationDate = model.ReservationDate
            });

            if (!addResponse.Success)
            {
                ModelState.AddModelError("", "Could not complete reservation. Please try again.");
                return await RedisplayCreateFormWithErrors(model);
            }

            return RedirectToAction("MyReservations", "Reservation");
        }

        [HttpPost]
        public async Task<IActionResult> QuickCreate(QuickCreateReservationViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(HomeController.Index), "Home");

            var result = await reservationService.QuickAddAsync(new QuickAddReservationRequest
            {
                UserId = CurrentUserId,
                WorkstationId = model.WorkstationId,
            });

            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            TempData["SuccessMessage"] = "Reservation created!";
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> MyReservations()
        {
            var response = await reservationService.GetByUserAsync(CurrentUserId);

            var viewModel = response.Reservations.Select(r => new UserReservationViewModel
            {
                ReservationDate = r.ReservationDate,
                Floor = r.Floor,
                Zone = r.Zone
            }).ToList();

            return View(viewModel);
        }


        private async Task<IActionResult> RedisplayCreateFormWithErrors(CreateReservationViewModel model)
        {
            var availableWorkstationsResponse = await workstationService.GetAllAvailableAsync(model.ReservationDate);
            var availableWorkstations = availableWorkstationsResponse.Workstations;
            var favoritesResponse = await favoriteService.GetUserFavoritesAsync(CurrentUserId);
            var favoriteWorkstationIds = favoritesResponse.Favorites.Select(f => f.WorkstationId);

            model.Workstations = availableWorkstations.Select(ws => new WorkstationReservationOptionViewModel
            {
                WorkstationId = ws.WorkstationId,
                Floor = ws.Floor,
                Zone = ws.Zone,
                HasMonitor = ws.HasMonitor,
                HasDockingStation = ws.HasDockingStation,
                NearWindow = ws.NearWindow,
                NearPrinter = ws.NearPrinter,
                IsFavorite = favoriteWorkstationIds.Contains(ws.WorkstationId)
            })
            .OrderByDescending(ws => ws.IsFavorite)
            .ToList();

            return View(nameof(Create), model);
        }


    }
}
