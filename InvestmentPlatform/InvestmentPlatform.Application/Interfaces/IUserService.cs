using InvestmentPlatform.Application.ViewModels;
using InvestmentPlatform.Domain.Models;
using InvestmentPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.Interfaces
{
    public interface IUserService
    {
        ApplicationUser GetUserById(string id);

        void UpdateUser(InvestorEditViewModel model, ApplicationUser user, string pictureName);

        void UpdateUser(AuthorEditViewModel model, ApplicationUser user, string pictureName);
    }
}
