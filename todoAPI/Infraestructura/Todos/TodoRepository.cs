using todoAPI.models;
using MongoDB.Driver;
using todoAPI.Infraestructura.General;

namespace todoAPI.Infraestructura.Todos
{
    public class TodoRepository : ICrudRepository<TodoEntity>, ITodoRepository
    {
        private readonly IMongoCollection<TodoEntity> _collection;

        public TodoRepository(IConfiguration configuration)
        {
            var settings = configuration.GetSection("TodoDataBase").Get<TodoDBSettings>();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TodoEntity>("todo");
        }

        public async Task<TodoEntity> CreateAsync(TodoEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<TodoEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<TodoEntity> GetByID(string id)
        {
            return await _collection.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TodoEntity> GetByTodo(string todo)
        {
            return await _collection.Find(t => t.nombre == todo).FirstOrDefaultAsync();
        }

        public async Task<List<TodoEntity>> GetTodoByIdUser(string idU)
        {
            return await _collection.Find(t => t.usuarioId == idU).ToListAsync();
        }

        public async Task<TodoEntity> ModifyAsync(TodoEntity entity)
        {
            await _collection.ReplaceOneAsync(t => t.Id == entity.Id, entity);
            return entity;
        }

        public async Task RemoveAsync(TodoEntity entity)
        {
            await _collection.DeleteOneAsync(t => t.Id == entity.Id);
        }
    }
}