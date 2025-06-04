using System.Data.SqlTypes;

namespace OfficeReservation.Repository.Interfaces.User
{
    public class UserFilter
    {
        public SqlString? Email { get; set; }
        public SqlString? Name { get; set; }
    }
}
