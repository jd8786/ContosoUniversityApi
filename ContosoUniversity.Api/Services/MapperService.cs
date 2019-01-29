using System.Collections.Generic;
using AutoMapper;

namespace ContosoUniversity.Api.Services
{
    public class MapperService<TEntity, TModel> where TEntity: class where TModel: class
    {
        private readonly IMapper _mapper;

        public MapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TModel MapToModel(TEntity entity)
        {
            return _mapper.Map<TModel>(entity);
        }

        public TEntity MapToEntity(TModel model)
        {
            return _mapper.Map<TEntity>(model);
        }

        public IEnumerable<TModel> MapToModels(IEnumerable<TEntity> entities)
        {
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }

        public IEnumerable<TEntity> MapToEntities(IEnumerable<TModel> models)
        {
            return _mapper.Map<IEnumerable<TEntity>>(models);
        }
    }
}
