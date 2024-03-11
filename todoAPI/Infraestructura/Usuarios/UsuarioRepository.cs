using MongoDB.Driver;
using todoAPI.Infraestructura.General;
using todoAPI.models;

namespace todoAPI.Infraestructura.Usuarios
{
    public class UsuarioRepository : ICrudRepository<UsuarioEntity>, IUsuarioRepository
	{
        /// <summary>
        /// conexion con la bd de MongoDB
        /// </summary>
        private readonly IMongoCollection<UsuarioEntity> _collection;
        public UsuarioRepository(IConfiguration configuration)
		{
            var settings = configuration.GetSection("TodoDataBase").Get<TodoDBSettings>();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<UsuarioEntity>("usuario");
        }

        public async Task<UsuarioEntity> CreateAsync(UsuarioEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<UsuarioEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<UsuarioEntity> GetByID(string id)
        {
            return await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UsuarioEntity> GetUserByEmail(string email)
        {
            return await _collection.Find(u => u.email == email).FirstOrDefaultAsync();
        }

        public async Task<UsuarioEntity> GetUserByEmailPass(string email, string password)
        {
            return await _collection.Find(u => u.email == email && u.password == password).FirstOrDefaultAsync();
        }

        public async Task<UsuarioEntity> ModifyAsync(UsuarioEntity entity)
        {
            await _collection.ReplaceOneAsync(u => u.Id == entity.Id, entity);
            return entity;
        }

        public async Task RemoveAsync(UsuarioEntity entity)
        {
            await _collection.DeleteOneAsync(u => u.Id == entity.Id);
        }
    }
}

