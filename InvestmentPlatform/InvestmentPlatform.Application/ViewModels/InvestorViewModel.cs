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
        [Required]
        public string CompanyName { get; set; }
        
        [Required]
        public int City { get; set; }

        [Required]
        public int InvestmentSize { get; set; }

    }
}
