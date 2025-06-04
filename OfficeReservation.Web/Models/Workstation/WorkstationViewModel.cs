namespace OfficeReservation.Web.Models.Workstation
{
    public class WorkstationViewModel
    {
        public int WorkstationId { get; set; }
        public int Floor { get; set; }
        public string Zone { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasDockingStation { get; set; }
        public bool NearWindow { get; set; }
        public bool NearPrinter { get; set; }
        public bool IsFavorite { get; set; }
    }
}
