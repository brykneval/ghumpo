using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using Dapper;
using ghumpo.model.Mobile;

namespace ghumpo.service.Query
{
    public interface IProfileStatsQueryService
    {
        ProfileStatsGetMobile ProfileStats(string id);
    }

    public class ProfileStatsQueryService : IProfileStatsQueryService
    {
        private readonly IDbConnection cnn;

        public ProfileStatsQueryService(IDbConnection cnn)
        {
            this.cnn = cnn;
        }

        public ProfileStatsGetMobile ProfileStats(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                id = new JavaScriptSerializer().Deserialize(id, null).ToString();
            }
            var ProfileStats =
                cnn.Query<ProfileStatsGetMobile>("spGetProfileStats", new {id}, commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            return ProfileStats;
        }
    }
}