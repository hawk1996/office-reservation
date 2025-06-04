using OfficeReservation.Repository.Interfaces.User;
using OfficeReservation.Services.DTOs.Authentication;
using OfficeReservation.Services.Helpers;
using OfficeReservation.Services.Interfaces;

namespace OfficeReservation.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository userRepository;
        public AuthenticationService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var filter = new UserFilter { Email = loginRequest.Email };
            await foreach (var user in userRepository.RetrieveCollectionAsync(filter))
            {
                var password = SecurityHelper.HashPassword(loginRequest.Password);
                if (string.Equals(user.Password, password, StringComparison.OrdinalIgnoreCase))
                {
                    return new LoginResponse
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        Success = true
                    };
                }
            }

            return new LoginResponse { Success = false, ErrorMessage = "Invalid username or password." };
        }
    }
}
