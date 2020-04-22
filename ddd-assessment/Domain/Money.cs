using ddd_assessment.DataManager;
using ddd_assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Domain
{
    public class Money : IMoney
    {
        public decimal? AddMoneyToBalance(decimal? balance, decimal? amount)
        {
            decimal? result = 0;
            if (balance > 0)
                result = balance + amount;
            else
                result = amount;
            return result;
        }
    }
}
