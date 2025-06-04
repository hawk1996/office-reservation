using Microsoft.Data.SqlClient;
using OfficeReservation.Model;
using OfficeReservation.Repository.Base;
using OfficeReservation.Repository.Interfaces.Reservation;

namespace OfficeReservation.Repository.Implementations
{
    public class ReservationRepository : BaseRepository<Reservation, ReservationFilter, ReservationUpdate>
    {
        protected override string[] GetColumns() => new[]
{
            "UserId",
            "WorkstationId",
            "ReservationDate"
        };

        protected override string GetIdColumnName() => "ReservationId";

        protected override string GetTableName() => "Reservations";

        protected override Reservation MapEntity(SqlDataReader reader)
        {
            return new Reservation
            {
                ReservationId = Convert.ToInt32(reader["ReservationId"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                WorkstationId = Convert.ToInt32(reader["WorkstationId"]),
                ReservationDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["ReservationDate"]))
            };
        }
    }
}
