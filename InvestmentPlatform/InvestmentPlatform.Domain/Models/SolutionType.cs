using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Domain.Models
{
    public class SolutionType : BaseType
    {
        public List<Solution> Solutions { get; set; }
    }
}
