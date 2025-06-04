using OfficeReservation.Model;
using OfficeReservation.Repository.Interfaces.Favorite;
using OfficeReservation.Repository.Interfaces.User;
using OfficeReservation.Repository.Interfaces.Workstation;
using OfficeReservation.Services.DTOs.Favorite;
using OfficeReservation.Services.Interfaces;

namespace OfficeReservation.Services.Implementations
{
    public class FavoriteService : IFavoriteService
    {
        private const int MaxFavoritesAllowed = 3;
        private readonly IFavoriteRepository favoriteRepository;
        private readonly IUserRepository userRepository;
        private readonly IWorkstationRepository workstationRepository;

        public FavoriteService(
            IFavoriteRepository favoriteRepository,
            IUserRepository userRepository,
            IWorkstationRepository workstationRepository)
        {
            this.favoriteRepository = favoriteRepository;
            this.userRepository = userRepository;
            this.workstationRepository = workstationRepository;
        }

        public async Task<AddToFavoritesResponse> AddToFavoritesAsync(AddToFavoritesRequest request)
        {
            var response = new AddToFavoritesResponse();
            var isFavoritedResponse = await IsFavoritedAsync(new IsFavoritedRequest { WorkstationId = request.WorkstationId, UserId = request.UserId });
            if (isFavoritedResponse.IsFavorited)
            {
                response.Success = false;
                response.ErrorMessage = "You've already favorited that workstation";
                return response;
            }

            var favoritesCountResponse = await GetFavoritesCountAsync(request.UserId);
            if (favoritesCountResponse.Count >= MaxFavoritesAllowed)
            {
                response.Success = false;
                response.ErrorMessage = "You're already capped on favorites - remove one first";
                return response;
            }

            response.FavoriteId = await favoriteRepository.CreateAsync(new Favorite
            {
                UserId = request.UserId,
                WorkstationId = request.WorkstationId
            });

            response.Success = true;
            return response;
        }

        public async Task<IsFavoritedResponse> IsFavoritedAsync(IsFavoritedRequest request)
        {
            var filter = new FavoriteFilter { UserId = request.UserId, WorkstationId = request.WorkstationId };
            var favorite = await favoriteRepository.RetrieveCollectionAsync(filter).SingleOrDefaultAsync();
            return new IsFavoritedResponse { IsFavorited = favorite != null, FavoriteId = favorite?.FavoriteId };
        }

        public async Task<GetUserFavoritesResponse> GetUserFavoritesAsync(int userId)
        {
            var filter = new FavoriteFilter { UserId = userId };
            var favorites = new List<FavoriteInfo>();

            await foreach (var reservation in favoriteRepository.RetrieveCollectionAsync(filter))
            {
                var dto = await MapToDtoAsync(reservation);
                favorites.Add(dto);
            }

            return new GetUserFavoritesResponse { Favorites = favorites };
        }

        public async Task<GetFavoritesCountResponse> GetFavoritesCountAsync(int userId)
        {
            var favorites = await GetUserFavoritesAsync(userId);
            return new GetFavoritesCountResponse { Count = favorites.Favorites.Count() };
        }

        public async Task<RemoveFromFavoritesResponse> RemoveFromFavoritesAsync(int favoriteId)
        {
            var response = new RemoveFromFavoritesResponse();
            response.Success = await favoriteRepository.DeleteAsync(favoriteId);
            if (!response.Success)
                response.ErrorMessage = "Nothing to remove";
            return response;
        }
        private async Task<FavoriteInfo> MapToDtoAsync(Favorite favorite)
        {
            var user = await userRepository.RetrieveByIdAsync(favorite.UserId);
            var workstation = await workstationRepository.RetrieveByIdAsync(favorite.WorkstationId);

            if (user == null || workstation == null)
            {
                throw new InvalidOperationException("User or Workstation not found for reservation.");
            }

            return new FavoriteInfo
            {
                UserId = favorite.UserId,
                WorkstationId = favorite.WorkstationId,
                UserName = user.Name,
                Floor = workstation.Floor,
                Zone = workstation.Zone
            };
        }
    }
}
