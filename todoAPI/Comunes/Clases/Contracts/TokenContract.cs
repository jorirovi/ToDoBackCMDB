namespace todoAPI.Comunes.Clases.Contracts
{
    public class TokenContract
	{
        public string? id_usuario { get; set; }
        public string email { get; set; } = null!;
        public string token { get; set; } = null!;
    }
}

