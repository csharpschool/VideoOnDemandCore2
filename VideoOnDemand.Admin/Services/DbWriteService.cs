using System.Threading.Tasks;
using VideoOnDemand.Data.Data;

namespace VideoOnDemand.Admin.Services
{
    public class DbWriteService : IDbWriteService
    {
        private VODContext _db;
        public DbWriteService(VODContext db)
        {
            _db = db;
        }

        public async Task<bool> Add<TEntity>(TEntity item) where TEntity : class
        {
            try
            {
                await _db.AddAsync<TEntity>(item);
                return await _db.SaveChangesAsync() >= 0;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete<TEntity>(TEntity item) where TEntity : class
        {
            try
            {
                _db.Set<TEntity>().Remove(item);
                return await _db.SaveChangesAsync() >= 0;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Update<TEntity>(TEntity item) where TEntity : class
        {
            try
            {
                _db.Set<TEntity>().Update(item);
                return await _db.SaveChangesAsync() >= 0;
            }
            catch
            {
                return false;
            }
        }

    }
}
