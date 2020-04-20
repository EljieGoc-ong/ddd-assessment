using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Models
{
    public class ChargeMoney
    {
        public decimal Money { get; set; }
        public string CurrencyName { get; set; }
        public int UserId { get; set; }
    }
}
