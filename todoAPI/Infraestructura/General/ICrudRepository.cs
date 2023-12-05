namespace todoAPI.Infraestructura.General
{
    public interface ICrudRepository<T>
    {
        Task<T> CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> ModifyAsync(T entity);
        Task RemoveAsync(T entity);
        Task<T> GetByID(string id);
    }
}