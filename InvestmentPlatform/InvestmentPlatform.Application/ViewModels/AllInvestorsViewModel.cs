using InvestmentPlatform.Domain.Models;
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

        public List<int> SelectedCountries { get; set; }

        public List<int> SelectedCities { get; set; }

        public List<int> SelectedIndustries { get; set; }

        public int ProjectAmount { get; set; }

        public List<Country> Countries { get; set; }

        public List<City> Cities { get; set; }

        public List<Industry> Industries { get; set; }

        public AllInvestorsViewModel()
        {
            InvestorViewModels = new List<InvestorViewModel>();
            Countries = new List<Country>();
            Cities = new List<City>();
            Industries = new List<Industry>();
            SelectedCountries = new List<int>();
            SelectedCities = new List<int>();
            SelectedIndustries = new List<int>();
        }
    }
}
