using MongoDB.Driver;
using todoAPI.Infraestructura.General;
using todoAPI.models;

namespace todoAPI.Infraestructura.TipoProducto
{
    public class TipoProductoRepository : ICrudRepository<TipoProductoEntity>, ITipoProductoRepository
    {
        private readonly IMongoCollection<TipoProductoEntity> _collection;
        public TipoProductoRepository(IConfiguration configuration)
        {
            var settings = configuration.GetSection("TodoDataBase").Get<TodoDBSettings>();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TipoProductoEntity>("TipoProducto");
        }

        public async Task<TipoProductoEntity> CreateAsync(TipoProductoEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<TipoProductoEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<TipoProductoEntity> GetByID(string id)
        {
            return await _collection.Find(tp => tp.idTP == id).FirstOrDefaultAsync();
        }

        public async Task<TipoProductoEntity> GetByTP(string TipoProducto)
        {
            return await _collection
                .Find(tp => tp.TipoProducto == TipoProducto).FirstOrDefaultAsync();
        }

        public async Task<TipoProductoEntity> ModifyAsync(TipoProductoEntity entity)
        {
            await _collection.ReplaceOneAsync(tp => tp.idTP == entity.idTP, entity);
            return entity;
        }

        public async Task RemoveAsync(TipoProductoEntity entity)
        {
            await _collection.DeleteOneAsync(tp => tp.idTP == entity.idTP);
        }
    }
}