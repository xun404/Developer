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
        [InverseProperty(nameof(App.Owner))]
        public virtual List<App> MyApps { get; set; } = new List<App>();
    }
}
