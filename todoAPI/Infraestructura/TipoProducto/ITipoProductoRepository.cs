using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todoAPI.models;

namespace todoAPI.Infraestructura.TipoProducto
{
    public interface ITipoProductoRepository
    {
        Task<TipoProductoEntity> GetByTP(string TipoProducto);
    }
}