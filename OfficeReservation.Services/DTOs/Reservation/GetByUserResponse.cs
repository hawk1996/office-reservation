namespace OfficeReservation.Services.DTOs.Reservation
{
    public class GetByUserResponse
    {
        public IEnumerable<ReservationInfo> Reservations { get; set; }
    }
}
