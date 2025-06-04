using OfficeReservation.Repository.Base;

namespace OfficeReservation.Repository.Interfaces.User
{
    public interface IUserRepository : IBaseRepository<Model.User, UserFilter, UserUpdate>
    {
    }
}
