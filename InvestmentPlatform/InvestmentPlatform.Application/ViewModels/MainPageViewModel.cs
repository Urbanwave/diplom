using InvestmentPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.ViewModels
{
    public class MainPageViewModel
    {
        public int SolutionsAmount { get; set; }
        public int AuthorsAmount { get; set; }
        public int InvestorsAmount { get; set; }
        public int FavouriteSolutionsAmount { get; set; }

        public List<SolutionViewModel> SolutionViewModels { get; set; }
    }
}
