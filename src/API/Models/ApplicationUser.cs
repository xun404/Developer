using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class APIUser : IdentityUser
    {
        [InverseProperty(nameof(App.OwnerId))]
        public virtual List<App> MyApps { get; set; }
    }
}
