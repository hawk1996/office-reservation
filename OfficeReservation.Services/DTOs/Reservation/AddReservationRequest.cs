namespace OfficeReservation.Services.DTOs.Reservation
{
    public class AddReservationRequest
    {
        public int UserId { get; set; }
        public int WorkstationId { get; set; }
        public DateOnly ReservationDate { get; set; }
    }
}
