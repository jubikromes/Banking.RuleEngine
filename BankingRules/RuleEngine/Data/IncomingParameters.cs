using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingRules.RuleEngine.Data
{
    /// <summary>
    /// Provides a conntract for all typy of parameters coming into the database
    /// </summary>
    public interface IIncomingParameters
    {
        string Platform { get; set; }
    }
    public class TransactionParameters
    {
        public string AccountNumber { get; set; }
        public string Amount { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
