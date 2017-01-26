using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class OAuthPack
    {
        public virtual int OAuthPackId { get; set; }
        public virtual int Code { get; set; }
        public virtual string ApplyAppId { get; set; }

        [InverseProperty(nameof(AccessToken.OAuthPack))]
        public virtual List<AccessToken> AccessTokens { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual APIUser User { get; set; }
        public virtual string UserId { get; set; }
    }
}
