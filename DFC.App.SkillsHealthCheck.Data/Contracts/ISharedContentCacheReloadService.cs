using System.Threading;
using System.Threading.Tasks;

namespace DFC.App.SkillsHealthCheck.Data.Contracts
{
    public interface ISharedContentCacheReloadService
    {
        Task Reload(CancellationToken stoppingToken);
    }
}
