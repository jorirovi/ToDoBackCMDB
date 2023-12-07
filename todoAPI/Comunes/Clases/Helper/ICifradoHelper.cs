namespace todoAPI.Comunes.Clases.Helper
{
	public interface ICifradoHelper
	{
        public string EncryptString(string plainText);
        public string DecryptString(string cipherText);
    }
}

