using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OfficeReservation.Web.Models.Reservation
{
    public class CreateReservationViewModel
    {

        public DateOnly ReservationDate { get; set; }

        [Required(ErrorMessage = "Please select a workstation.")]
        public int SelectedWorkstationId { get; set; }

        [ValidateNever]
        public List<WorkstationReservationOptionViewModel> Workstations { get; set; }
    }

}
