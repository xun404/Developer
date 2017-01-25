using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Models.AppsViewModels
{
    public class AppLayoutModel
    {
        public AppLayoutModel(string UserName,int ActivePanel)
        {
            this.UserName = UserName;
            this.ActivePanel = ActivePanel;
        }
        public string UserName { get; set; }
        public int ActivePanel { get; set; }
    }
}
