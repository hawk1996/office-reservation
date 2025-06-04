using OfficeReservation.Model;
using OfficeReservation.Repository.Interfaces.Favorite;
using OfficeReservation.Services.DTOs.Favorite;
using OfficeReservation.Services.Interfaces;

namespace OfficeReservation.Services.Implementations
{
    public class FavoriteService : IFavoriteService
    {
        private const int MaxFavoritesAllowed = 3;
        private readonly IFavoriteRepository favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            this.favoriteRepository = favoriteRepository;
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
            return new IsFavoritedResponse { IsFavorited = favorite != null };
        }

        public async Task<GetFavoritesCountResponse> GetFavoritesCountAsync(int userId)
        {
            var filter = new FavoriteFilter { UserId = userId };
            var favorites = await favoriteRepository.RetrieveCollectionAsync(filter).ToListAsync();
            return new GetFavoritesCountResponse { Count = favorites.Count() };
        }

        public async Task<RemoveFromFavoritesResponse> RemoveFromFavoritesAsync(int favoriteId)
        {
            var response = new RemoveFromFavoritesResponse();
            response.Success = await favoriteRepository.DeleteAsync(favoriteId);
            if (!response.Success)
                response.ErrorMessage = "Nothing to remove";
            return response;    
        }
    }
}
