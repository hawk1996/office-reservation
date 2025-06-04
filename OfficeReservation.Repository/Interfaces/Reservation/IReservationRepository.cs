using OfficeReservation.Repository.Base;

namespace OfficeReservation.Repository.Interfaces.Reservation
{
    public interface IReservationRepository : IBaseRepository<Model.Reservation, ReservationFilter, ReservationUpdate>
    {
    }
}
