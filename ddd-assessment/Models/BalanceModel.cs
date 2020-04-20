using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Models
{
    public class BalanceModel
    {
        public int BalanceId { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public CurrencyModel Currencies { get; private set; }
        public string CurrencyName { get; internal set; }
        public decimal Ratio { get; internal set; }
    }
}
