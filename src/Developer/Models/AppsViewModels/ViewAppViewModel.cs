using AiursoftBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Models.AppsViewModels
{
    public class ViewAppViewModel : AppLayoutModel
    {
        [Obsolete(message: "This method is only for framework", error: true)]
        public ViewAppViewModel() { }
        public ViewAppViewModel(DeveloperUser User,App ThisApp) : base(User, 1)
        {
            this.AppName = ThisApp.AppName;
            this.AppDescription = ThisApp.AppDescription;
            this.AppCategory = ThisApp.AppCategory;
            this.AppPlatform = ThisApp.AppPlatform;
        }
        [Display(Name = "App Name")]
        public virtual string AppName { get; set; }
        [Display(Name = "App Description")]
        public virtual string AppDescription { get; set; }
        [Display(Name = "App Category")]
        public virtual Category AppCategory { get; set; }
        [Display(Name = "App Platform")]
        public virtual Platform AppPlatform { get; set; }
    }
}
