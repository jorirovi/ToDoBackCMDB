using MongoDB.Bson.Serialization.Attributes;

namespace todoAPI.models
{
    public class TipoProductoEntity
    {
        [BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? idTP {get;set;}
        public string? TipoProducto {get; set; }
    }
}