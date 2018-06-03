using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.ViewModels
{
    public class InvestorViewModel
    {
        public string Id { get; set; }

        public string CompanyName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyDescription { get; set; }

        public City City { get; set; }

        public Currency Currency { get; set; }

        public int InvestmentSize { get; set; }

        public string FileName { get; set; }

        public List<Industry> Industries { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }
    }
}
