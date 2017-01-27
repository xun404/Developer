using Developer.Models.AppsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Models.AppsViewModels
{
    public class AllAppsViewModel : AppLayoutModel
    {
        [Obsolete(message: "This method is only for framework", error: true)]
        public AllAppsViewModel() { }
        public AllAppsViewModel(DeveloperUser User) : base(User, 1)
        {
        }
        public virtual IEnumerable<App> MyApps { get; set; }
    }
}
