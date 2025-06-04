namespace OfficeReservation.Services.DTOs.Authentication
{
    public class LoginResponse : ResponseBase
    {
        public int? UserId { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
