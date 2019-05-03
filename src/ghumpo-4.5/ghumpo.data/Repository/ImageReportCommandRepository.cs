using ghumpo.data.Infrastructure;
using ghumpo.model;

namespace ghumpo.data.Repository
{
    public class ImageReportCommandRepository : Repository<ImageReport>, IImageReportCommandRepository
    {
        public ImageReportCommandRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IImageReportCommandRepository : IRepositoryCommand<ImageReport>
    {
    }
}