using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Models
{
    public class UserInfoViewModel
    {
        public virtual string openid { get; set; }
        public virtual string nickname { get; set; }
        public virtual string sex { get; set; }
        public virtual string headimgurl { get; set; }
        public virtual string preferedLanguage { get; set; }
    }
}
