using System;
using ghumpo.common;

namespace ghumpo.model
{
    public class MobileStats
    {
        public long MobileStatsId { get; set; }
        public EnumHelper.EApiCallBy EApiCallBy { get; set; }
        public EnumHelper.EMobileStatInteractionType EMobileStatInteractionType { get; set; }
        public string Parameters { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedTs { get; set; }
    }
}