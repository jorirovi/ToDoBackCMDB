using AutoMapper;
using todoAPI.Comunes.Clases.Constantes;
using todoAPI.Comunes.Clases.Contracts;
using todoAPI.Comunes.Clases.Helper;
using todoAPI.Dominio.services.General;
using todoAPI.Infraestructura.General;
using todoAPI.Infraestructura.Usuarios;
using todoAPI.models;

namespace todoAPI.Dominio.services.Usuarios
{
    public class UsuarioService : ICrudService<UsuarioContract>, IUsuarioService
	{
		private readonly ICrudRepository<UsuarioEntity> _crudRepository;
		private readonly IUsuarioRepository _usuarioRepository;
		private readonly IMapper _mapper;
        private readonly ICifradoHelper _cifrado;

		public UsuarioService(ICrudRepository<UsuarioEntity> crudRepository, IUsuarioRepository usuarioRepository,
			IMapper mapper, ICifradoHelper cifrado)
		{
			_crudRepository = crudRepository;
			_usuarioRepository = usuarioRepository;
			_mapper = mapper;
            _cifrado = cifrado;
		}

        public async Task<UsuarioTDOContract> CreateAsync(UsuarioTDOContract entity)
        {
            UsuarioEntity usuario = await _usuarioRepository.GetUserByEmail(entity.email);
            if(usuario == null)
            {
                string pass = entity.password;
                entity.password = _cifrado.EncryptString(pass);
                await _crudRepository.CreateAsync(_mapper.Map<UsuarioEntity>(entity));
                return entity;
            }
            else
            {
                return _mapper.Map<UsuarioTDOContract>(usuario);
            }
        }

        public async Task<List<UsuarioContract>> GetAllAsync()
        {
            List<UsuarioEntity> listaUsuarios = await _crudRepository.GetAllAsync();
            return _mapper.Map<List<UsuarioContract>>(listaUsuarios);
        }

        public async Task<UsuarioContract> GetByIDAsync(string id)
        {
            UsuarioEntity usuario = await _crudRepository.GetByID(id);
            if (usuario != null)
            {
                return _mapper.Map<UsuarioContract>(usuario);
            }
            else
            {
                throw new Exception(TodoConstantes.registroNoEncontrado);
            }
        }

        public async Task RemoveAsync(string id)
        {
            UsuarioEntity usuarioEliminar = await _crudRepository.GetByID(id);
            if (usuarioEliminar != null)
            {
                await _crudRepository.RemoveAsync(usuarioEliminar);
            }
            else
            {
                throw new Exception(TodoConstantes.registroNoEncontrado);
            }
        }

        public async Task<UsuarioContract> UpdateAsync(UsuarioContract entity)
        {
            UsuarioEntity usuarioActual = await _crudRepository.GetByID(entity.Id);
            if (usuarioActual != null)
            {
                string pass = _cifrado.EncryptString(entity.password);
                if (pass != usuarioActual.password)
                {
                    entity.password = pass;
                }
                else
                {
                    entity.password = usuarioActual.password;
                }
                UsuarioEntity usuarioModificar = new UsuarioEntity()
                {
                    Id = usuarioActual.Id,
                    nombre = entity.nombre,
                    email = entity.email,
                    password = entity.password
                };
                await _crudRepository.ModifyAsync(usuarioModificar);
                return _mapper.Map<UsuarioContract>(usuarioModificar);
            }
            else
            {
                throw new Exception(TodoConstantes.registroNoEncontrado);
            }
        }
    }
}

