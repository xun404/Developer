using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AiursoftBase.Models
{
    public class AiurUserBase : IdentityUser
    {
        public virtual string nickname { get; set; }
        public virtual string sex { get; set; }
        public virtual string headimgurl { get; set; }
    }
}
