namespace OfficeReservation.Services.DTOs.Workstation
{
    public class GetAllAvailableResponse
    {
        public IEnumerable<WorkstationDto> Workstations { get; set; }
    }
}
