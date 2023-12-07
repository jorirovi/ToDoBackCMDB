using Microsoft.AspNetCore.Mvc;
using todoAPI.Comunes.Clases.Constantes;
using todoAPI.Comunes.Clases.Contracts;
using todoAPI.Dominio.services.General;
using todoAPI.Dominio.services.Usuarios;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ICrudService<UsuarioContract> _servicio;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(ICrudService<UsuarioContract> servicio, IUsuarioService usuarioService)
        {
            _servicio = servicio;
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(UsuarioTDOContract entity)
        {
            return Ok(await _usuarioService.CreateAsync(entity));
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerListadoUsuarios()
        {
            return Ok(await _servicio.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SeleccionarUsuario(string id)
        {
            return Ok(await _servicio.GetByIDAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> ModificarUsuario(UsuarioContract entity)
        {
            return Ok(await _servicio.UpdateAsync(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(string id)
        {
            await _servicio.RemoveAsync(id);
            return Ok(TodoConstantes.registroElimnado);
        }
    }
}

