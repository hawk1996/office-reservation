namespace OfficeReservation.Web.Models.Home
{
    public class HomeViewModel
    {
        public DateOnly SelectedDate { get; set; }
        public List<FavoriteWorkstationViewModel> FavoriteWorkstations { get; set; } = new();
    }
}
