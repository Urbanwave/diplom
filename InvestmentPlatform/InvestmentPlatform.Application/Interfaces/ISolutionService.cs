using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.Interfaces
{
    public interface ISolutionService
    {
        List<Solution> GetAllSolutions(int page, int pageSize);

        Solution GetSolutionById(int id);

        void AddSolution(Solution solution);

        int GetSolutionsAmount();

        List<FavoriteSolution> GetAllFavoriteSolutionsByUserId(string id);

        void AddFavoriteSolution(FavoriteSolution favoriteSolution);

        void RemoveFavoriteSolution(FavoriteSolution favoriteSolution);

        List<int> GetFavoriteSolutionsByUserId(string id);
    }
}
