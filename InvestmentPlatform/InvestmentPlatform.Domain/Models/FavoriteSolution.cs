using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InvestmentPlatform.Domain.Models
{
    public class FavoriteSolution
    {
        [Key, Column(Order = 0)]
        public string FollowedUserId { get; set; }

        [Key, Column(Order = 1)]
        public int FollowedSolutionId { get; set; }
    }
}