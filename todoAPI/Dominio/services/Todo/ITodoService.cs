using todoAPI.Comunes.Clases.Contracts;

namespace todoAPI.Dominio.services.Todo
{
    public interface ITodoService
    {
        Task<TodoDTOContract> CreateAsync(TodoDTOContract entity);
        Task<List<TodoContract>> GetTodoByIDUser(string idU);
    }
}