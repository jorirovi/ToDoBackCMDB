using todoAPI.Comunes.Clases.Contracts;

namespace todoAPI.Dominio.services.Login
{
	public interface ILoginService
	{
		Task<TokenContract> loginAsync(LoginContract entity);
	}
}

