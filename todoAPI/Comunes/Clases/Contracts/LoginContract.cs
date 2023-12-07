using System;
namespace todoAPI.Comunes.Clases.Contracts
{
	public class LoginContract
	{
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
    }
}

