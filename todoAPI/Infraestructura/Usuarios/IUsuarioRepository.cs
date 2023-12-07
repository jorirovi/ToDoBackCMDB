using todoAPI.models;

namespace todoAPI.Infraestructura.Usuarios
{
	public interface IUsuarioRepository
	{
        Task<UsuarioEntity> GetUserByEmailPass(string email, string password);
        Task<UsuarioEntity> GetUserByEmail(string email);
    }
}

