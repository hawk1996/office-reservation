namespace OfficeReservation.Services.DTOs.Favorite
{
    public class IsFavoritedRequest
    {
        public int UserId { get; set; }
        public int WorkstationId { get; set; }
    }
}
