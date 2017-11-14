using System.Threading.Tasks;

namespace VideoOnDemand.Admin.Services
{
    public interface IDbWriteService
    {
        Task<bool> Add<TEntity>(TEntity item) where TEntity : class;
    }
}
