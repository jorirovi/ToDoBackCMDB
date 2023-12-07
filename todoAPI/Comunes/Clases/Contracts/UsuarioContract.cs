using System;
namespace todoAPI.Comunes.Clases.Contracts
{
	public class UsuarioContract
	{
        public string? Id { get; set; }
        public string nombre { get; set; } = null!;
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
    }
}

