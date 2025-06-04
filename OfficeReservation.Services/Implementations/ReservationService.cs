using OfficeReservation.Model;
using OfficeReservation.Repository.Interfaces.Favorite;
using OfficeReservation.Repository.Interfaces.Reservation;
using OfficeReservation.Repository.Interfaces.User;
using OfficeReservation.Repository.Interfaces.Workstation;
using OfficeReservation.Services.DTOs.Reservation;
using OfficeReservation.Services.Interfaces;

namespace OfficeReservation.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private const int MaxReservationDaysAheadAllowed = 14;
        private readonly IReservationRepository reservationRepository;
        private readonly IFavoriteRepository favoriteRepository;
        private readonly IUserRepository userRepository;
        private readonly IWorkstationRepository workstationRepository;
        public ReservationService(
            IReservationRepository reservationRepository,
            IFavoriteRepository favoriteRepository,
            IUserRepository userRepository,
            IWorkstationRepository workstationRepository)
        {
            this.reservationRepository = reservationRepository;
            this.favoriteRepository = favoriteRepository;
            this.userRepository = userRepository;
            this.workstationRepository = workstationRepository;
        }
        public async Task<AddReservationResponse> AddAsync(AddReservationRequest request)
        {
            var response = new AddReservationResponse();
            var today = DateOnly.FromDateTime(DateTime.Today);
            if (request.ReservationDate < today || request.ReservationDate > today.AddDays(MaxReservationDaysAheadAllowed))
            {
                response.Success = false;
                response.ErrorMessage = "Invalid reservation date - must be between today and after 2 weeks";
                return response;
            }

            var hasUserReservatioOnDateResponse = await HasUserReservationOnDateAsync(new HasUserReservationOnDateRequest { ReservationDate = request.ReservationDate, UserId = request.UserId });
            if (hasUserReservatioOnDateResponse.HasUserReservationOnDate)
            {
                response.Success = false;
                response.ErrorMessage = "You already have a reservation for that date";
                return response;
            }
            
            var isWorkstationReservedOnDateResponse = await IsWorkstationReservedOnDateAsync(new IsWorkstationReservedOnDateRequest { WorkstationId = request.WorkstationId, ReservationDate = request.ReservationDate });
            if (isWorkstationReservedOnDateResponse.IsWorkstationReservedOnDate)
            {
                response.Success = false;
                response.ErrorMessage = "That workstation is already reserved for that date";
                return response;
            }

            response.ReservationId = await reservationRepository.CreateAsync(new Reservation
            {
                UserId = request.UserId,
                WorkstationId = request.WorkstationId,
                ReservationDate = request.ReservationDate
            });

            response.Success = true;
            return response;
        }

        public async Task<QuickAddReservationResponse> QuickAddAsync(QuickAddReservationRequest request)
        {
            var response = new QuickAddReservationResponse();
            var filter = new FavoriteFilter { UserId = request.UserId, WorkstationId = request.WorkstationId };
            var favorite = await favoriteRepository.RetrieveCollectionAsync(filter).SingleOrDefaultAsync();
            if (favorite == null)
            {
                response.Success = false;
                response.ErrorMessage = "You can only quick reserve your favorited workstations";
                return response;
            }

            var today = DateOnly.FromDateTime(DateTime.Today);
            var addRequest = new AddReservationRequest
            {
                UserId = request.UserId,
                WorkstationId = request.WorkstationId,
                ReservationDate = today.AddDays(1)
            };

            var addResponse = await AddAsync(addRequest);
            return new QuickAddReservationResponse
            {
                ReservationId = addResponse.ReservationId,
                Success = addResponse.Success,
                ErrorMessage = addResponse.ErrorMessage
            };
        }

        public async Task<HasUserReservationOnDateResponse> HasUserReservationOnDateAsync(HasUserReservationOnDateRequest request)
        {
            var filter = new ReservationFilter { UserId = request.UserId, ReservationDate = request.ReservationDate.ToDateTime(TimeOnly.MinValue) };
            var userReservationOnDate = await reservationRepository.RetrieveCollectionAsync(filter).SingleOrDefaultAsync();
            return new HasUserReservationOnDateResponse { HasUserReservationOnDate = userReservationOnDate != null };
        }

        public async Task<IsWorkstationReservedOnDateResponse> IsWorkstationReservedOnDateAsync(IsWorkstationReservedOnDateRequest request)
        {
            var filter = new ReservationFilter { WorkstationId = request.WorkstationId, ReservationDate = request.ReservationDate.ToDateTime(TimeOnly.MinValue) };
            var workstationReservationOnDate = await reservationRepository.RetrieveCollectionAsync(filter).SingleOrDefaultAsync();
            return new IsWorkstationReservedOnDateResponse { IsWorkstationReservedOnDate = workstationReservationOnDate != null };
        }

        public async Task<GetByUserResponse> GetByUserAsync(int userId)
        {
            var filter = new ReservationFilter { UserId = userId };
            var reservations = new List<ReservationInfo>();

            await foreach (var reservation in reservationRepository.RetrieveCollectionAsync(filter))
            {
                var dto = await MapToDtoAsync(reservation);
                reservations.Add(dto);
            }

            return new GetByUserResponse { Reservations = reservations };
        }

        public async Task<GetReservedWorkstationIdsResponse> GetReservedWorkstationIdsAsync(DateOnly date)
        {
            var reservations = await reservationRepository.RetrieveCollectionAsync(
                new ReservationFilter { ReservationDate = date.ToDateTime(TimeOnly.MinValue) }).ToListAsync();

            return new GetReservedWorkstationIdsResponse { ReservedWorkstationIds = reservations.Select(r => r.WorkstationId).Distinct() };
        }

        private async Task<ReservationInfo> MapToDtoAsync(Reservation reservation)
        {
            var user = await userRepository.RetrieveByIdAsync(reservation.UserId);
            var workstation = await workstationRepository.RetrieveByIdAsync(reservation.WorkstationId);

            if (user == null || workstation == null)
            {
                throw new InvalidOperationException("User or Workstation not found for reservation.");
            }

            return new ReservationInfo
            {
                UserId = reservation.UserId,
                WorkstationId = reservation.WorkstationId,
                ReservationDate = reservation.ReservationDate,
                UserName = user.Name,
                Floor = workstation.Floor,
                Zone = workstation.Zone
            };
        }

    }
}
