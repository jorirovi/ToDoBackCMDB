using todoAPI.Comunes.Clases.Constantes;
using todoAPI.Comunes.Clases.Contracts;
using todoAPI.Comunes.Clases.Helper;
using todoAPI.Infraestructura.Usuarios;
using todoAPI.models;

namespace todoAPI.Dominio.services.Login
{
	public class LoginService : ILoginService
	{
		private readonly IUsuarioRepository _usuario;
		private readonly ICifradoHelper _cifrado;
		private readonly IConfiguration _configutration;

		public LoginService(IUsuarioRepository usuario, ICifradoHelper cifrado,
			IConfiguration configuration)
		{
			_usuario = usuario;
			_cifrado = cifrado;
			_configutration = configuration;
		}

        public async Task<TokenContract> loginAsync(LoginContract entity)
        {
			string pass = _cifrado.EncryptString(entity.password);
			UsuarioEntity usuarioLogin = await _usuario.GetUserByEmailPass(entity.email, pass);
			if(usuarioLogin != null)
			{
				TokenContract userToken = new TokenContract()
				{
					id_usuario = usuarioLogin.Id,
					email = usuarioLogin.email,
					token = JWTHelper.GenerarToken(usuarioLogin.nombre, _configutration)
				};
				return userToken;
			}
			else
			{
				throw new Exception(TodoConstantes.registroNoEncontrado);
			}
        }
    }
}

