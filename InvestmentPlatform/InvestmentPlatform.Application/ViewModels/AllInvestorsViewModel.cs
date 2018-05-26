using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.ViewModels
{
    public class AllInvestorsViewModel
    {        
        public List<InvestorViewModel> InvestorViewModels { get; set; }

        public int ProjectAmount { get; set; }

        public AllInvestorsViewModel()
        {
            InvestorViewModels = new List<InvestorViewModel>();
        }
    }
}
