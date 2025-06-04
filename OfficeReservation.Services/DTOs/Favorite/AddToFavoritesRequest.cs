namespace OfficeReservation.Services.DTOs.Favorite
{
    public class AddToFavoritesRequest
    {
        public int UserId { get; set; }
        public int WorkstationId { get; set; }
    }
}
