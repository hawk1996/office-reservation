using System.ComponentModel.DataAnnotations;

namespace OfficeReservation.Model
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Workstation is required")]
        public int WorkstationId { get; set; }

        [Required(ErrorMessage = "Reservation date is required")]
        [DataType(DataType.Date)]
        public DateOnly ReservationDate { get; set; }
    }
}
