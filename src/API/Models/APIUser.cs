using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using API.Data;
using AiursoftBase.Models;

namespace API.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class APIUser : AiurUserBase
    {
        [InverseProperty(nameof(OAuthPack.User))]
        public virtual List<OAuthPack> Packs { get; set; }

        public async virtual Task<OAuthPack> GeneratePack(APIDbContext DbContext,string AppId)
        {
            var pack = new OAuthPack
            {
                AccessTokens = new List<AccessToken>(),
                Code = (Id + DateTime.Now.ToString()).GetHashCode(),
                UserId = this.Id,
                ApplyAppId = AppId
            };
            DbContext.OAuthPack.Add(pack);
            await DbContext.SaveChangesAsync();
            return pack;
        }
    }
}
