namespace OfficeReservation.Web.Models.Reservation
{
    public class UserReservationViewModel
    {
        public DateOnly ReservationDate { get; set; }

        public int Floor { get; set; }

        public string Zone { get; set; }
    }
}
