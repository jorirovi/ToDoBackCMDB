using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todoAPI.Comunes.Clases.Constantes;
using todoAPI.Comunes.Clases.Contracts;
using todoAPI.Dominio.services.General;
using todoAPI.Dominio.services.Todo;

namespace todoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ICrudService<TodoContract> _servicio;
        private readonly ITodoService _todoService;
        public TodoController(ICrudService<TodoContract> servicio, ITodoService todoService)
        {
            _servicio = servicio;
            _todoService = todoService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTodo(TodoDTOContract entity){
            return Ok(await _todoService.CreateAsync(entity));
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(await _servicio.GetAllAsync());
        }

        [HttpPut]
        public async Task<IActionResult> ModificarTodo(TodoContract entity)
        {
            return Ok(await _servicio.UpdateAsync(entity));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTodo(string id)
        {
            return Ok(await _servicio.GetByIDAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTodo(string id)
        {
            await _servicio.RemoveAsync(id);
            return Ok(TodoConstantes.registroElimnado);
        }

        [HttpGet("userid/{idU}")]
        public async Task<IActionResult> obtenerTodoPorIdUsusrio(string idU)
        {
            return Ok(await _todoService.GetTodoByIDUser(idU));
        }
    }
}