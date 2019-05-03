using ghumpo.data.Infrastructure;
using ghumpo.model;

namespace ghumpo.data.Repository
{
    public class ImageCommandRepository : Repository<Image>, IImageCommandRepository
    {
        public ImageCommandRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IImageCommandRepository : IRepositoryCommand<Image>
    {
    }
}