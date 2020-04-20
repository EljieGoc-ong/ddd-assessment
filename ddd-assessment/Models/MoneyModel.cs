using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static ddd_assessment.Models.BalanceModel;

namespace ddd_assessment.Models
{
    public class MoneyModel
    {
        public decimal? Amount { get; set; }
        public CurrencyModel Currency { get; internal set; }
        public int UserId { get; set; }
    }
}
