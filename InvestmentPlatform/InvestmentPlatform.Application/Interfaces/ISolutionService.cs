using InvestmentPlatform.Domain.Models;
using InvestmentPlatform.Models;
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

        List<Solution> GetAllUserSolutions(int page, int pageSize, string userId);

        Solution GetSolutionById(int id);

        void AddSolution(Solution solution);

        int GetSolutionsAmount();

        int GetFavouriteSolutionsAmount();

        List<FavoriteSolution> GetAllFavoriteSolutionsByUserId(string id);

        void AddFavoriteSolution(FavoriteSolution favoriteSolution);

        void RemoveFavoriteSolution(FavoriteSolution favoriteSolution);

        List<int> GetFavoriteSolutionsByUserId(string id);

        void DeleteSolutionById(int id, string userId, bool isAdmin);

        void UpdateSolution(SolutionViewModel solutionViewModel,Solution solution, string pictureName);
    }
}
