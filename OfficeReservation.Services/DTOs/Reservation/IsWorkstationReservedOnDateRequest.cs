namespace OfficeReservation.Services.DTOs.Reservation
{
    public class IsWorkstationReservedOnDateRequest
    {
        public int WorkstationId { get; set; }
        public DateOnly ReservationDate { get; set; }
    }
}
