using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Models
{
    public class Access_tokenAddressModel
    {
        [Required]
        public virtual string AppId { get; set; }
        [Required]
        public virtual string AppSecret { get; set; }
        [Required]
        public virtual int Code { get; set; }
        [Required]
        public virtual string grant_type { get; set; }
    }
}
