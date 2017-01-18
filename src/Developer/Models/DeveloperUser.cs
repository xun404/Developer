using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using AiursoftBase.Models;

namespace Developer.Models
{
    public class DeveloperUser : AiurUserBase
    {
        [InverseProperty(nameof(App.Creater))]
        public virtual List<App> MyApps { get; set; } = new List<App>();
    }
}
