using AiursoftBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Models.AppsViewModels
{
    public class CreateAppViewModel : AppLayoutModel
    {
        public CreateAppViewModel() { }
        public CreateAppViewModel(DeveloperUser User) : base(User, 1)
        {
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
