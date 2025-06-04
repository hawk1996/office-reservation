using Microsoft.AspNetCore.Mvc;
using OfficeReservation.Services.DTOs.Reservation;
using OfficeReservation.Services.Interfaces;
using OfficeReservation.Web.Controllers;
using OfficeReservation.Web.Models.Home;

public class HomeController : BaseController
{
    private readonly IFavoriteService favoriteService;
    private readonly IWorkstationService workstationService;
    private readonly IReservationService reservationService;

    public HomeController(
        IFavoriteService favoriteService,
        IWorkstationService workstationService,
        IReservationService reservationService)
    {
        this.favoriteService = favoriteService;
        this.workstationService = workstationService;
        this.reservationService = reservationService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = CurrentUserId;
        var today = DateOnly.FromDateTime(DateTime.Today);
        var favorites = await favoriteService.GetUserFavoritesAsync(userId);
        var favoriteWorkstations = new List<FavoriteWorkstationViewModel>();

        foreach (var favorite in favorites.Favorites)
        {
            var isReserved = await reservationService.IsWorkstationReservedOnDateAsync(new IsWorkstationReservedOnDateRequest
            {
                WorkstationId = favorite.WorkstationId,
                ReservationDate = today.AddDays(1)
            });

            favoriteWorkstations.Add(new FavoriteWorkstationViewModel
            {
                WorkstationId = favorite.WorkstationId,
                Floor = favorite.Floor,
                Zone = favorite.Zone,
                IsAvailable = !isReserved.IsWorkstationReservedOnDate
            });
        }

        var model = new HomeViewModel
        {
            SelectedDate = DateOnly.FromDateTime(DateTime.Today),
            FavoriteWorkstations = favoriteWorkstations
        };

        return View(model);
    }
}
