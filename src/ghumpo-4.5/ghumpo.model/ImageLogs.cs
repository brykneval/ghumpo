using System;
using ghumpo.common;

namespace ghumpo.model
{
    public class ImageLogs
    {
        public long ImageLogsId { get; set; }
        public Guid FkImageId { get; set; }
        public Guid FkProfileIdViewedBy { get; set; }
        public DateTime ViewedTs { get; set; }
        public EnumHelper.EApiCallBy EApiCallBy { get; set; }
    }
}