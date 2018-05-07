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
       
        public string CompanyName { get; set; }
            
        public City City { get; set; }
        
        public int InvestmentSize { get; set; }

        public string FileName { get; set; }

    }
}
