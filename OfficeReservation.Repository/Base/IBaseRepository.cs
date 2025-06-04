namespace OfficeReservation.Repository.Base
{
    public interface IBaseRepository<TObj, TFilter, TUpdate>
        where TObj : class
    {
        public Task<int> CreateAsync(TObj entity);
        public Task<TObj?> RetrieveByIdAsync(int id);
        IAsyncEnumerable<TObj> RetrieveCollectionAsync(TFilter? filter = default);
        public Task<bool> UpdateAsync(int id, TUpdate? update = default);
        public Task<bool> DeleteAsync(int id);
    }
}
