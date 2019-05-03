using System;
using ghumpo.common;

namespace ghumpo.model
{
    public class LocalBusiness : Common
    {
        public Guid LocalBusinessId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string CurrenciesAccepted { get; set; }
        public string PaymentAccepted { get; set; }
        public string OpeningHours { get; set; }
        public long FkBusinessGroupId { get; set; }
        public EnumHelper.ELocalBusinessType ELocalBusinessType { get; set; }
    }
}