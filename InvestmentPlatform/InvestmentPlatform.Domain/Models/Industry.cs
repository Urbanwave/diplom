using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Domain.Models
{
    public class Industry : BaseType
    {
        public List<Solution> Solutions { get; set; }

        public List<ApplicationUser> Investors { get; set; }
    }
}
