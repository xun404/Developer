using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Developer.Models
{
    public class DeveloperUser : IdentityUser
    {
        [InverseProperty(nameof(App.Creater))]
        public virtual List<App> MyApps { get; set; } = new List<App>();

        public virtual string nickname { get; set; }
        public virtual string sex { get; set; }
        public virtual string headimgurl { get; set; }
    }
}
