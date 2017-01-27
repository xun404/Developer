using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Models.AppsViewModels
{
    public class AppLayoutModel
    {
        [Obsolete(message: "This method is only for framework", error: true)]
        public AppLayoutModel() { }
        public AppLayoutModel(DeveloperUser User, int ActivePanel)
        {
            this.UserName = User.nickname;
            this.UserIconImageAddress = User.headimgurl;
            this.ActivePanel = ActivePanel;
            this.AppCount = User.MyApps.Count();
        }
        public string UserName { get; set; }
        public string UserIconImageAddress { get; set; }
        public int ActivePanel { get; set; }
        public int AppCount { get; set; } 
    }
}
