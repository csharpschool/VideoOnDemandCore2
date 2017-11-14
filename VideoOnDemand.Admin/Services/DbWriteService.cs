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
    }
}
