namespace OfficeReservation.Web.Models.Home
{
    public class FavoriteWorkstationViewModel
    {
        public int WorkstationId { get; set; }
        public int Floor { get; set; }
        public string Zone { get; set; }
        public bool IsAvailable { get; set; }
    }
}
