using AutoMapper;
using todoAPI.Comunes.Clases.Constantes;
using todoAPI.Comunes.Clases.Contracts;
using todoAPI.Dominio.services.General;
using todoAPI.Infraestructura.General;
using todoAPI.Infraestructura.TipoProducto;
using todoAPI.models;

namespace todoAPI.Dominio.services.TipoProducto
{
    public class TipoProductoService : ICrudService<TipoProductoContract>, ITipoProductoService
    {
        private readonly ICrudRepository<TipoProductoEntity> _tpRepository;
        private readonly ITipoProductoRepository _tipoProducto;
        private readonly IMapper _mapper;
        public TipoProductoService(ICrudRepository<TipoProductoEntity> tpRepository,
            IMapper mapper, ITipoProductoRepository tipoProducto)
            {
                _tpRepository = tpRepository;
                _mapper = mapper;
                _tipoProducto = tipoProducto;
            }

        public async Task<TipoProductoDTOContract> CreateTP(TipoProductoDTOContract entity)
        {
            TipoProductoDTOContract tipoProducto = _mapper.Map<TipoProductoDTOContract>(await _tipoProducto.GetByTP(entity.TipoProducto));
            if(tipoProducto == null)
            {
                await _tpRepository.CreateAsync(_mapper.Map<TipoProductoEntity>(entity));
                return entity;
            }
            else
            {
                return entity;
            }
        }

        public async Task<List<TipoProductoContract>> GetAllAsync()
        {
            List<TipoProductoContract> listaTP = _mapper.Map<List<TipoProductoContract>>(await _tpRepository.GetAllAsync());
            return listaTP;
        }

        public async Task<TipoProductoContract> GetByIDAsync(string id)
        {
            TipoProductoContract tipoProducto = _mapper.Map<TipoProductoContract>(await _tpRepository.GetByID(id));
            if (tipoProducto != null)
            {
                return tipoProducto;
            }
            else
            {
                throw new Exception(TodoConstantes.registroNoEncontrado);
            }
        }

        public async Task RemoveAsync(string id)
        {
            TipoProductoContract tipoProducto = _mapper.Map<TipoProductoContract>(await _tpRepository.GetByID(id));
            if (tipoProducto != null)
            {
                await _tpRepository.RemoveAsync(_mapper.Map<TipoProductoEntity>(tipoProducto));
            }
            else
            {
                throw new Exception(TodoConstantes.registroNoEncontrado);
            }
        }

        public async Task<TipoProductoContract> UpdateAsync(TipoProductoContract entity)
        {
            TipoProductoContract tipoProducto = _mapper.Map<TipoProductoContract>(await _tpRepository.GetByID(entity.idTP));
            if (tipoProducto != null)
            {
                TipoProductoContract modificarTP = new TipoProductoContract()
                {
                    idTP = tipoProducto.idTP,
                    TipoProducto = entity.TipoProducto
                };
                await _tpRepository.ModifyAsync(_mapper.Map<TipoProductoEntity>(modificarTP));
                return modificarTP;
            }
            else
            {
                throw new Exception(TodoConstantes.registroNoEncontrado);
            }
        }
    }
}