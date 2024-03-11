using Microsoft.AspNetCore.Mvc;
using todoAPI.Comunes.Clases.Contracts;
using todoAPI.Dominio.services.General;
using todoAPI.Dominio.services.TipoProducto;

namespace todoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoProductoController : ControllerBase
    {
        private readonly ICrudService<TipoProductoContract> _servicio;
        private readonly ITipoProductoService _tpServicio;
        public TipoProductoController(ICrudService<TipoProductoContract> servicio,
            ITipoProductoService tpServicio)
        {
            _servicio = servicio;
            _tpServicio = tpServicio;
        }
        [HttpPost]
        public async Task<IActionResult> CrearTipoProducto(TipoProductoDTOContract entity)
        {
            return Ok(await _tpServicio.CreateTP(entity));
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTipoProducto()
        {
            return Ok(await _servicio.GetAllAsync());
        }
        [HttpGet("{idTP}")]
        public async Task<IActionResult> Obtener1TP(string idTP)
        {
            return Ok(await _servicio.GetByIDAsync(idTP));
        }
        [HttpDelete("{idTP}")]
        public async Task<IActionResult> EliminarTP(string idTP)
        {
            await _servicio.RemoveAsync(idTP);
            return Ok($"el Tipo de Producto con id " + idTP + " fue Eliminado");
        }
        [HttpPut]
        public async Task<IActionResult> ActualizarTP(TipoProductoContract entity)
        {
            return Ok(await _servicio.UpdateAsync(entity));
        }
    }
}