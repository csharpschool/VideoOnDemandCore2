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
    }
}
