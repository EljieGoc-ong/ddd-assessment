using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Models
{
    public class MoneyExchange
    {
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
    }
}
