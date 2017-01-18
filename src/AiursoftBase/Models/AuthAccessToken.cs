using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Models
{
    public class AuthAccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string openid { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
    }
}
