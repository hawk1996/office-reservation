namespace OfficeReservation.Services.DTOs.Favorite
{
    public class FavoriteInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int WorkstationId { get; set; }
        public string Zone { get; set; }
        public int Floor { get; set; }
    }
}
