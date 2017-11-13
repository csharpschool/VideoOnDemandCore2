using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoOnDemand.Data.Services
{
    public interface IDbReadService
    {
        IQueryable<TEntity> Get<TEntity>() where TEntity : class;
        TEntity Get<TEntity>(int id, bool includeRelatedEntities = false) where TEntity : class;
        TEntity Get<TEntity>(string userId, int id) where TEntity : class;
    }
}
