using Microsoft.Data.SqlClient;
using OfficeReservation.Model;
using OfficeReservation.Repository.Base;
using OfficeReservation.Repository.Interfaces.User;

namespace OfficeReservation.Repository.Implementations
{
    public class UserRepository : BaseRepository<User, UserFilter, UserUpdate>, IUserRepository
    {
        protected override string[] GetColumns() => new[]
        {
            "Name",
            "Email",
            "Password"
        };

        protected override string GetIdColumnName() => "UserId";

        protected override string GetTableName() => "Users";

        protected override User MapEntity(SqlDataReader reader)
        {
            return new User
            {
                UserId = Convert.ToInt32(reader["UserId"]),
                Name = Convert.ToString(reader["Name"]),
                Email = Convert.ToString(reader["Email"]),
                Password = Convert.ToString(reader["Password"])
            };
        }
    }
}
