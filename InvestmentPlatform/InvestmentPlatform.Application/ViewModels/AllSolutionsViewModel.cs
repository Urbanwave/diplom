using InvestmentPlatform.Domain.Models;
using InvestmentPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.ViewModels
{
    public class AllSolutionsViewModel
    {
        public List<SolutionViewModel> SolutionViewModels { get; set; }

        public List<Country> Countries { get; set; }

        public List<City> Cities { get; set; }

        public List<Industry> Industries { get; set; }

        public List<SolutionType> SolutionTypes { get; set; }

        public List<ImplementationStatus> ImplementationStatuses { get; set; }

        public List<int> SelectedCountries { get; set; }

        public List<int> SelectedCities { get; set; }

        public List<int> SelectedIndustries { get; set; }

        public List<int> SelectedSolutionStatuses { get; set; }

        public List<int> SelectedSolutionTypes { get; set; }

        public int ProjectAmount { get; set; }
        
        public int AllProjectsCount { get; set; }

        public int FromInvestmentSize { get; set; }

        public int ToInvestmentSize { get; set; }

        public AllSolutionsViewModel()
        {
            SolutionViewModels = new List<SolutionViewModel>();
            Countries = new List<Country>();
            Cities = new List<City>();
            Industries = new List<Industry>();
            ImplementationStatuses = new List<ImplementationStatus>();
            SolutionTypes = new List<SolutionType>();
            SelectedCountries = new List<int>();
            SelectedCities = new List<int>();
            SelectedIndustries = new List<int>();
            SelectedSolutionStatuses = new List<int>();
            SelectedSolutionTypes = new List<int>();
        }
    }
}
