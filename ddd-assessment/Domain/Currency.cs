using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Domain
{
    public class Currency
    {
        public string Name { get; protected set; }
        public decimal Ratio { get; protected set; }
        public int Id { get; set; }

        public Currency()
        {
        }

        public Currency(string currencyName, decimal ratio):this()
        {
            Name = currencyName;
            Ratio = ratio;
        }
    }
}
