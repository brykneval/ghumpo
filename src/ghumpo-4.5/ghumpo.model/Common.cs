using System;
using ghumpo.common;

namespace ghumpo.model
{
    public abstract class Common
    {
        public string Ip { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime CreatedTs { get; set; }
        public DateTime? ModifiedTs { get; set; }
        public EnumHelper.EFlag EFlag { get; set; }
    }
}