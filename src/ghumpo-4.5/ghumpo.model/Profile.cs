using System;
using ghumpo.common;

namespace ghumpo.model
{
    public class Profile : Common
    {
        public Guid ProfileId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public EnumHelper.ELoginType ELoginType { get; set; }
        public string UserId { get; set; }
        public string Interest { get; set; }
    }
}