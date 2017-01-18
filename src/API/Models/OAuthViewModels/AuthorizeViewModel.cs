using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.OAuthViewModels
{
    public class AuthorizeAddressModel
    {
        [Required]
        public string appid { get; set; }
        [Required]
        [Url]
        public string redirect_uri { get; set; }
        [Required]
        public string response_type { get; set; }
        [Required]
        public string scope { get; set; }
        [Required]
        public string state { get; set; }
    }

    public class AuthorizeViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Url]
        public string ToRedirect { get; set; }
        public string State { get; set; }
    }
}
