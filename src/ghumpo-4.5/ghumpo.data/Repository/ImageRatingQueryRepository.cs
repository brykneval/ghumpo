using ghumpo.data.Infrastructure;
using ghumpo.model;

namespace ghumpo.data.Repository
{
    public class ImageRatingQueryRepository : Repository<ImageRating>, IImageRatingQueryRepository
    {
        public ImageRatingQueryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IImageRatingQueryRepository : IRepositoryQuery<ImageRating>
    {
    }
}