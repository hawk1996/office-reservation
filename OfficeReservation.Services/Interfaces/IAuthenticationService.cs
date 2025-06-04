using OfficeReservation.Services.DTOs.Authentication;

namespace OfficeReservation.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}
