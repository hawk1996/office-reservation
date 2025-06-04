using System.Data.SqlTypes;

namespace OfficeReservation.Repository.Interfaces.Reservation
{
    public class ReservationFilter
    {
        public SqlInt32? UserId { get; set; }
        public SqlInt32? WorkstationId { get; set; }
        public SqlDateTime? ReservationDate { get; set; }
    }
}
