using System;
using MongoDB.Bson.Serialization.Attributes;

namespace todoAPI.models
{
	public class UsuarioEntity
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public string? Id { get; set; }
		public string nombre { get; set; } = null!;
		public string email { get; set; } = null!;
		public string password { get; set; } = null!;
	}
}

