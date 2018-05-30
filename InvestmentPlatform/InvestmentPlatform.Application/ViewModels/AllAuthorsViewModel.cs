using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.ViewModels
{
    public class AllAuthorsViewModel
    {
        public List<AuthorEditViewModel> AuthorViewModels { get; set; }

        public AllAuthorsViewModel()
        {
            AuthorViewModels = new List<AuthorEditViewModel>();
        }
    }
}
