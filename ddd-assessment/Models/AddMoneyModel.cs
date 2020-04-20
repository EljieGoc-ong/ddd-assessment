using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Models
{
    public class AddMoneyModel
    {
        public decimal? Money { get; set; }
        public int? CurrencyId { get; set; }
        public int? UserId { get; set; }
    }
}
