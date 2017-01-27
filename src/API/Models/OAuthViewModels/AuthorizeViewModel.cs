using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.OAuthViewModels
{
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

        public bool RememberMe { get; set; }
        public string State { get; set; }
        public string AppId { get; set; }
        public string ResponseType { get; set; }
        public string Scope { get; set; }
    }
}
