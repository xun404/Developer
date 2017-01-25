using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Models.AppsViewModels
{
    public class IndexViewModel : AppLayoutModel
    {
        public IndexViewModel(DeveloperUser User) : base(User, 0)
        {
        }
    }
}
