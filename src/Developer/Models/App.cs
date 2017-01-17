using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Models
{
    public class App
    {
        public virtual string AppId { get; set; }
        public virtual string AppSecret { get; set; }

        public virtual string AppName { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual DeveloperUser Owner { get; set; }
        public virtual string OwnerId { get; set; }
    }
}
