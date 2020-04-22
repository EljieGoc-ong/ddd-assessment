using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Domain
{
    public interface IMoney
    {
        decimal? AddMoneyToBalance(decimal? balance, decimal? amount);
    }
}
