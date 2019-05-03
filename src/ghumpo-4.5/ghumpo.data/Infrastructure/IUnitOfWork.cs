using System.Threading.Tasks;
using ghumpo.common;

namespace ghumpo.data.Infrastructure
{
    public interface IUnitOfWork
    {
        EnumHelper.EOpStatus Commit();
        Task<EnumHelper.EOpStatus> CommitAsync();
    }
}