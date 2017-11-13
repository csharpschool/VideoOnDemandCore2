using VideoOnDemand.Data.Data;

namespace VideoOnDemand.Data.Services
{
    public class DbReadService : IDbReadService
    {
        private VODContext _db;
        public DbReadService(VODContext db)
        {
            _db = db;
        }

    }
}
