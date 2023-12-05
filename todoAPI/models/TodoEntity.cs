using MongoDB.Bson.Serialization.Attributes;

namespace todoAPI.models
{
    public class TodoEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id {get;set;}
        public string nombre {get;set;} = null!;
        public int completada {get;set;}
    }
}