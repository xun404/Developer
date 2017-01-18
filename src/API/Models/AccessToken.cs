using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AccessToken
    {
        public virtual int AccessTokenId { get; set; }
        public virtual string Value { get; set; }

        [ForeignKey(nameof(PackId))]
        public virtual OAuthPack OAuthPack { get; set; }
        public virtual int PackId { get; set; }
    }
}
