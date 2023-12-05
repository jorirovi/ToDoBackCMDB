namespace todoAPI.Dominio.services.General
{
    public interface ICrudService<T>
    {
        Task<T> UpdateAsync(T entity);
        Task RemoveAsync(string id);
        Task<T> GetByIDAsync(string id);
        Task<List<T>> GetAllAsync();
    }
}