using System.Data.SqlTypes;

namespace OfficeReservation.Repository.Interfaces.Workstation
{
    public class WorkstationUpdate
    {
        public SqlInt32? Floor { get; set; }
        public SqlString? Zone { get; set; }
        public SqlBoolean? HasMonitor { get; set; }
        public SqlBoolean? HasDockingStation { get; set; }
        public SqlBoolean? NearWindow { get; set; }
        public SqlBoolean? NearPrinter { get; set; }
    }
}
