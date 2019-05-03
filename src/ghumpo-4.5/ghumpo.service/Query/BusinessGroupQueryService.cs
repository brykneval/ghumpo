using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ghumpo.model.Mobile;

namespace ghumpo.service.Query
{
    public interface IBusinessGroupQueryService
    {
        IList<BusinessGroupMobile> GetBusinessGroups();
    }

    public class BusinessGroupQueryService : IBusinessGroupQueryService
    {
        private readonly IDbConnection cnn;

        public BusinessGroupQueryService(IDbConnection cnn)
        {
            this.cnn = cnn;
        }

        public IList<BusinessGroupMobile> GetBusinessGroups()
        {
            IList<BusinessGroupMobile> groups =
                cnn.Query<BusinessGroupMobile>(
                    "SELECT BusinessGroupId AS [id], BusinessGroupName AS [name] FROM BusinessGroup", null,
                    commandType: CommandType.Text).ToList();
            return groups;
        }
    }
}