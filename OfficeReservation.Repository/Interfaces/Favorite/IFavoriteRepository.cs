using OfficeReservation.Repository.Base;

namespace OfficeReservation.Repository.Interfaces.Favorite
{
    public interface IFavoriteRepository : IBaseRepository<Model.Favorite, FavoriteFilter, FavoriteUpdate>
    {
    }
}
