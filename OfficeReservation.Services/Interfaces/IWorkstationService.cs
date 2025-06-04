using OfficeReservation.Services.DTOs.Workstation;

namespace OfficeReservation.Services.Interfaces
{
    public interface IWorkstationService
    {
        Task<GetWorkstationsResponse> GetAllAsync();
        Task<GetWorkstationsResponse> GetAllAvailableAsync(DateOnly date);
    }
}
