using todoAPI.Comunes.Clases.Contracts;

namespace todoAPI.Dominio.services.Usuarios
{
	public interface IUsuarioService
	{
		Task<UsuarioTDOContract> CreateAsync(UsuarioTDOContract entity);
	}
}

