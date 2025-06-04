using System.ComponentModel.DataAnnotations;

namespace OfficeReservation.Model
{
    public class Favorite
    {
        public int FavoriteId { get; set; }

        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Workstation is required")]
        public int WorkstationId { get; set; }
    }
}
