using Microsoft.Data.SqlClient;
using OfficeReservation.Model;
using OfficeReservation.Repository.Base;
using OfficeReservation.Repository.Interfaces.Favorite;

namespace OfficeReservation.Repository.Implementations
{
    public class FavoriteRepository : BaseRepository<Favorite, FavoriteFilter, FavoriteUpdate>
    {
        protected override string[] GetColumns() => new[]
{
            "UserId",
            "WorkstationId"
        };

        protected override string GetIdColumnName() => "FavoriteId";

        protected override string GetTableName() => "Favorites";

        protected override Favorite MapEntity(SqlDataReader reader)
        {
            return new Favorite
            {
                FavoriteId = Convert.ToInt32(reader["FavoriteId"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                WorkstationId = Convert.ToInt32(reader["WorkstationId"])
            };
        }
    }
}
