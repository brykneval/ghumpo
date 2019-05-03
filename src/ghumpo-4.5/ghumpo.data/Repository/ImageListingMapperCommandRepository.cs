using ghumpo.data.Infrastructure;
using ghumpo.model;

namespace ghumpo.data.Repository
{
    public class ImageListingMapperCommandRepository : Repository<ImageListingMapper>,
        IImageListingMapperCommandRepository
    {
        public ImageListingMapperCommandRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IImageListingMapperCommandRepository : IRepositoryCommand<ImageListingMapper>
    {
    }
}