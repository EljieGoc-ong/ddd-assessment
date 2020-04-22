using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Models
{
    public class SendMoney
    {
        public int ToUserId { get; set; }
        public int FromUserId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
    }
}
