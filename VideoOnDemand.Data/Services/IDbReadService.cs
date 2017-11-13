using Microsoft.AspNetCore.Mvc.Rendering;
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
        IEnumerable<TEntity> GetWithIncludes<TEntity>() where TEntity : class;
        SelectList GetSelectList<TEntity>(string valueField, string textField) where TEntity : class;
        (int courses, int downloads, int instructors, int modules, int videos, int users, int userCourses) Count();
    }
}
