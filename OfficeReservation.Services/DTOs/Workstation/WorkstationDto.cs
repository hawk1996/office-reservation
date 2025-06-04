namespace OfficeReservation.Services.DTOs.Workstation
{
    public class WorkstationDto
    {
        public int Floor { get; set; }
        public string Zone { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasDockingStation { get; set; }
        public bool NearWindow { get; set; }
        public bool NearPrinter { get; set; }
    }
}
