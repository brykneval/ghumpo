using System;
using ghumpo.common;

namespace ghumpo.model
{
    public class ImageReport : Common
    {
        public Guid ImageReportId { get; set; }
        public EnumHelper.EReportType EReportType { get; set; }
        public EnumHelper.EReportStatus EReportStatus { get; set; }
        public string Message { get; set; }
        public Guid? FkImageId { get; set; }
    }
}