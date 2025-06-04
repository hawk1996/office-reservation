using OfficeReservation.Services.DTOs.Reservation;

namespace OfficeReservation.Services.Interfaces
{
    public interface IReservationService
    {
        Task<AddReservationResponse> AddAsync(AddReservationRequest request);
        Task<QuickAddReservationResponse> QuickAddAsync(QuickAddReservationRequest request);
        Task<GetByUserResponse> GetByUserAsync(int userId);
        Task<HasUserReservationOnDateResponse> HasUserReservationOnDateAsync(HasUserReservationOnDateRequest request);
        Task<IsWorkstationReservedOnDateResponse> IsWorkstationReservedOnDateAsync(IsWorkstationReservedOnDateRequest request);
        Task<GetReservedWorkstationIdsResponse> GetReservedWorkstationIdsAsync(DateOnly date);
    }
}
