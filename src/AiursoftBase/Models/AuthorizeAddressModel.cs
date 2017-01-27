using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Models
{
    public class AuthorizeAddressModel
    {
        [Required]
        public string appid { get; set; }
        [Required]
        [Url]
        public string redirect_uri { get; set; }
        public string state { get; set; }
        public string scope { get; set; }
        public string response_type { get; set; }
    }
}
