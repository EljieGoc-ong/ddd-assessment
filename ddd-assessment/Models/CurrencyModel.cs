using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Models
{
    public class CurrencyModel
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public decimal? Ratio { get; set; }
        public int Id { get; internal set; }
    }
}
