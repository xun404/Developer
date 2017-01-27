using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Models.AppsViewModels
{
    public class IndexViewModel : AppLayoutModel
    {
        [Obsolete(message: "This method is only for framework", error: true)]

        public IndexViewModel() { }
        public IndexViewModel(DeveloperUser User) : base(User, 0)
        {
        }
    }
}
