using Microsoft.Data.SqlClient;
using OfficeReservation.Model;
using OfficeReservation.Repository.Base;
using OfficeReservation.Repository.Interfaces.Workstation;

namespace OfficeReservation.Repository.Implementations
{
    public class WorkstationRepository : BaseRepository<Workstation, WorkstationFilter, WorkstationUpdate>, IWorkstationRepository
    {
        protected override string[] GetColumns() => new[]
       {
            "Floor",
            "Zone",
            "HasMonitor",
            "HasDockingStation",
            "NearWindow",
            "NearPrinter"
        };

        protected override string GetIdColumnName() => "WorkstationId";

        protected override string GetTableName() => "Workstations";

        protected override Workstation MapEntity(SqlDataReader reader)
        {
            return new Workstation
            {
                WorkstationId = Convert.ToInt32(reader["WorkstationId"]),
                Floor = Convert.ToInt32(reader["Floor"]),
                Zone = Convert.ToString(reader["Zone"]),
                HasMonitor = Convert.ToBoolean(reader["HasMonitor"]),
                HasDockingStation = Convert.ToBoolean(reader["HasDockingStation"]),
                NearWindow = Convert.ToBoolean(reader["NearWindow"]),
                NearPrinter = Convert.ToBoolean(reader["NearPrinter"])
            };
        }
    }
}
