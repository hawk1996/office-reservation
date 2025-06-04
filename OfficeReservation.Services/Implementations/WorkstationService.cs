using OfficeReservation.Model;
using OfficeReservation.Repository.Interfaces.Workstation;
using OfficeReservation.Services.DTOs.Workstation;
using OfficeReservation.Services.Interfaces;

namespace OfficeReservation.Services.Implementations
{
    public class WorkstationService : IWorkstationService
    {
        private readonly IWorkstationRepository workstationRepository;
        private readonly IReservationService reservationService;
        public WorkstationService(IWorkstationRepository workstationRepository, IReservationService reservationService)
        {
            this.workstationRepository = workstationRepository;
            this.reservationService = reservationService;
        }
        public async Task<GetAllAvailableResponse> GetAllAvailableAsync(DateOnly date)
        {
            var allWorkstations = await workstationRepository.RetrieveCollectionAsync().ToListAsync();
            var reservedIds = await reservationService.GetReservedWorkstationIdsAsync(date);

            var available = allWorkstations
                .Where(ws => !reservedIds.ReservedWorkstationIds.Contains(ws.WorkstationId));

            return new GetAllAvailableResponse { Workstations = available.Select(MapToDto) };
        }

        private static WorkstationDto MapToDto(Workstation workstation) => new WorkstationDto
        {
            Floor = workstation.Floor,
            Zone = workstation.Zone,
            HasMonitor = workstation.HasMonitor,
            HasDockingStation = workstation.HasDockingStation,
            NearWindow = workstation.NearWindow,
            NearPrinter = workstation.NearPrinter
        };
    }
}
