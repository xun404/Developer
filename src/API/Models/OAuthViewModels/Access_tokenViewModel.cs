using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.OAuthViewModels
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

    public class Access_tokenViewModel
    {
        public virtual string access_token { get; set; }
        public virtual int expires_in { get; set; }
        public virtual string refresh_token { get; set; }
        public virtual string openid { get; set; }
        public virtual string scope { get; set; }
    }
}
