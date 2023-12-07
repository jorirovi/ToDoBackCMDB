namespace todoAPI.Comunes.Clases.Contracts
{
    public class TodoContract
    {
        public string? Id {get;set;}
        public string nombre {get;set;} = null!;
        public int completada {get;set;}
        public string? usuarioId { get; set; }
    }
}