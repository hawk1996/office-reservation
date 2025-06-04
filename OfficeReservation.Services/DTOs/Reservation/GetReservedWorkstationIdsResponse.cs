namespace OfficeReservation.Services.DTOs.Reservation
{
    public class GetReservedWorkstationIdsResponse
    {
        public IEnumerable<int> ReservedWorkstationIds { get; set; } = new List<int>();
    }
}
