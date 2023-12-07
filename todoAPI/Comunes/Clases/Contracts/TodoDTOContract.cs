using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todoAPI.Comunes.Clases.Contracts
{
    public class TodoDTOContract
    {
        public string nombre {get;set;} = null!;
        public string? usuarioId { get; set; } 
    }
}