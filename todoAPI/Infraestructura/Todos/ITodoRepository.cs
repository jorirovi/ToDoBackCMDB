using todoAPI.models;

namespace todoAPI.Infraestructura.Todos
{
    public interface ITodoRepository
    {
        Task<TodoEntity> GetByTodo(string todo);
    }
}