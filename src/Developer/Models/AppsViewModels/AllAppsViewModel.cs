using Developer.Models.AppsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Models.AppsViewModels
{
    public class AllAppsViewModel : AppLayoutModel
    {
        public AllAppsViewModel(string UserName) : base(UserName, 1)
        {
        }
    }
}
