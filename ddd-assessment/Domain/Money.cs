using ddd_assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Domain
{
    public class Money
    {
        public Money()
        {
        }

        private Currency currency;
        private decimal amount;
        private Dictionary<Currency, decimal> Currencies = new Dictionary<Currency, decimal>();

        public Money(Currency currency, decimal amount)
        {
            this.currency = currency;
            this.amount = amount;
        }
    }
}
