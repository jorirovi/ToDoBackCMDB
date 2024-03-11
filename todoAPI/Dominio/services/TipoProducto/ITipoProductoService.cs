using todoAPI.Comunes.Clases.Contracts;

namespace todoAPI.Dominio.services.TipoProducto
{
    public interface ITipoProductoService
    {
        Task<TipoProductoDTOContract> CreateTP(TipoProductoDTOContract entity);
    }
}