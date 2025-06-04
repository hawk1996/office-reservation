namespace OfficeReservation.Services.DTOs.Reservation
{
    public class HasUserReservationOnDateRequest
    {
        public int UserId { get; set; }
        public DateOnly ReservationDate { get; set; }
    }
}
