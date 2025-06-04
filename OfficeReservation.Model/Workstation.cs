using System.ComponentModel.DataAnnotations;

namespace OfficeReservation.Model
{
    public class Workstation
    {
        public int WorkstationId { get; set; }

        [Required(ErrorMessage = "Floor is required")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "Zone is required")]
        [MaxLength(50, ErrorMessage = "Zone can't be longer than 50 characters")]
        public string Zone { get; set; }

        [Required(ErrorMessage = "Monitor presence must be specified")]
        public bool HasMonitor { get; set; }

        [Required(ErrorMessage = "Docking station presence must be specified")]
        public bool HasDockingStation { get; set; }

        [Required(ErrorMessage = "Window proximity must be specified")]
        public bool NearWindow { get; set; }

        [Required(ErrorMessage = "Printer proximity must be specified")]
        public bool NearPrinter { get; set; }
    }
}
