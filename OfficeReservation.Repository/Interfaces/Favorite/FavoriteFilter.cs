using System.Data.SqlTypes;

namespace OfficeReservation.Repository.Interfaces.Favorite
{
    public class FavoriteFilter
    {
        public SqlInt32? UserId { get; set; }
        public SqlInt32? WorkstationId { get; set; }
    }
}
