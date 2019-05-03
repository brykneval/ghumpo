using ghumpo.data.Infrastructure;
using ghumpo.model;

namespace ghumpo.data.Repository
{
    public class ImageRatingCommandRepository : Repository<ImageRating>, IImageRatingCommandRepository
    {
        public ImageRatingCommandRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IImageRatingCommandRepository : IRepositoryCommand<ImageRating>
    {
    }
}