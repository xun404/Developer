using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.OAuthViewModels
{
    public class UserInfoAddressModel
    {
        public virtual string access_token { get; set; }
        public virtual string openid { get; set; }
        public virtual string lang { get; set; }
    }
    public class UserInfoViewModel
    {
        public virtual string openid { get; set; }
        public virtual string nickname { get; set; }
        public virtual string sex { get; set; }
        public virtual string headimgurl { get; set; }
    }
}
