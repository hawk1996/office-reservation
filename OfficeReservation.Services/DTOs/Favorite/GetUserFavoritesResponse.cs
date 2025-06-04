namespace OfficeReservation.Services.DTOs.Favorite
{
    public class GetUserFavoritesResponse
    {
        public IEnumerable<FavoriteInfo> Favorites { get; set; }
    }
}
