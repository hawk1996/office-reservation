using OfficeReservation.Services.DTOs.Workstation;

namespace OfficeReservation.Services.Interfaces
{
    public interface IWorkstationService
    {
        Task<GetAllAvailableResponse> GetAllAvailableAsync(DateOnly date);
    }
}
