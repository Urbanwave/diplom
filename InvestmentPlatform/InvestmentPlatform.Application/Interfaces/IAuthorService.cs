using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.Interfaces
{
    public interface IAuthorService
    {
        List<ApplicationUser> GetAllAuthors(int page, int pageSize);

        ApplicationUser GetAuthorById(string id);

        void DeleteAuthorById(string id);
    }
}
