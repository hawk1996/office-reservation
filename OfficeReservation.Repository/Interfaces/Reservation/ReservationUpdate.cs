using System.Data.SqlTypes;

namespace OfficeReservation.Repository.Interfaces.Reservation
{
    public class ReservationUpdate
    {
        public SqlDateTime? ReservationDate { get; set; }
    }
}
