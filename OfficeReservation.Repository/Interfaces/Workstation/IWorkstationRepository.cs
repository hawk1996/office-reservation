using OfficeReservation.Repository.Base;

namespace OfficeReservation.Repository.Interfaces.Workstation
{
    public interface IWorkstationRepository : IBaseRepository<Model.Workstation, WorkstationFilter, WorkstationUpdate>
    {
    }
}
