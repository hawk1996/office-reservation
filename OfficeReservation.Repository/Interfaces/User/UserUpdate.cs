﻿using System.Data.SqlTypes;

namespace OfficeReservation.Repository.Interfaces.User
{
    public class UserUpdate
    {
        public SqlString? Name { get; set; }
        public SqlString? Email { get; set; }
        public SqlString? Password { get; set; }
    }
}
