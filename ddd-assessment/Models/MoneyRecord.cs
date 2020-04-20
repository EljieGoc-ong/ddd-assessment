using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ddd_assessment.Models.BalanceModel;

namespace ddd_assessment.Models
{
    public class MoneyRecord
    {
        public CurrencyModel currency { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
    }
}
