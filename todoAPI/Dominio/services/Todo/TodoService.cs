using AutoMapper;
using todoAPI.Comunes.Clases.Constantes;
using todoAPI.Comunes.Clases.Contracts;
using todoAPI.Comunes.Clases.Enumerables;
using todoAPI.Dominio.services.General;
using todoAPI.Infraestructura.General;
using todoAPI.Infraestructura.Todos;
using todoAPI.models;

namespace todoAPI.Dominio.services.Todo
{
    public class TodoService : ICrudService<TodoContract>, ITodoService
    {
        private readonly ICrudRepository<TodoEntity> _crudService;
        private readonly ITodoRepository _todo;
        private readonly IMapper _mapper;

        public TodoService(ICrudRepository<TodoEntity> crudService, IMapper mapper,
            ITodoRepository todo)
        {
            _crudService = crudService;
            _todo = todo;
            _mapper = mapper;
        }

        public async Task<TodoDTOContract> CreateAsync(TodoDTOContract entity)
        {
            TodoEntity todo = await _todo.GetByTodo(entity.nombre);
            if (todo == null){
                TodoEntity creaTodo = new TodoEntity
                {
                    nombre = entity.nombre,
                    completada = ((int)(EstadoTodo.no_completado))
                };
                await _crudService.CreateAsync(creaTodo);
                return entity;
            }
            else{
                return _mapper.Map<TodoDTOContract>(todo);
            }
        }

        public async Task<List<TodoContract>> GetAllAsync()
        {
            List<TodoEntity> listaTodos = await _crudService.GetAllAsync();
            return _mapper.Map<List<TodoContract>>(listaTodos);
        }

        public async Task<TodoContract> GetByIDAsync(string id)
        {
            TodoEntity todoById = await _crudService.GetByID(id);
            if (todoById != null)
            {
                return _mapper.Map<TodoContract>(todoById);
            }
            else
            {
                throw new Exception(TodoConstantes.registroNoEncontrado);
            }
        }

        public async Task RemoveAsync(string id)
        {
            TodoEntity todoEliminar = await _crudService.GetByID(id);
            if (todoEliminar != null)
            {
                await _crudService.RemoveAsync(todoEliminar);
            }
            else
            {
                throw new Exception(TodoConstantes.registroNoEncontrado);
            }
        }

        public async Task<TodoContract> UpdateAsync(TodoContract entity)
        {
            TodoEntity todoActual = await _crudService.GetByID(entity.Id);
            if (todoActual != null)
            {
                TodoEntity todoModificado = new TodoEntity
                {
                    Id = todoActual.Id,
                    nombre = entity.nombre,
                    completada = entity.completada,
                    usuarioId = entity.usuarioId
                };
                await _crudService.ModifyAsync(todoModificado);
                return _mapper.Map<TodoContract>(todoModificado);
            }
            else
            {
                throw new Exception(TodoConstantes.registroNoEncontrado);
            }
        }
    }
}