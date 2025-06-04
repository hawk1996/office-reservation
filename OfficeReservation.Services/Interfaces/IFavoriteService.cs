using OfficeReservation.Services.DTOs.Favorite;

namespace OfficeReservation.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<AddToFavoritesResponse> AddToFavoritesAsync(AddToFavoritesRequest request);
        Task<IsFavoritedResponse> IsFavoritedAsync(IsFavoritedRequest request);
        Task<GetFavoritesCountResponse> GetFavoritesCountAsync(int userId);
        Task<RemoveFromFavoritesResponse> RemoveFromFavoritesAsync(int favoriteId);
    }
}
