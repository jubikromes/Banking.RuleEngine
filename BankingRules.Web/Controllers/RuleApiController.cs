using BankingRules.RuleEngine.Data;
using BankingRules.RuleEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankingRules.Web.Controllers
{
    public class RuleApiController : ApiController
    {
        private readonly RuleService _ruleService;
        public RuleApiController(RuleService ruleService)
        {
            _ruleService = ruleService;
        }
        /// <summary>
        /// Entry point for running all rules 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        [Route("api/ruleapi/executetransactions")]
        public HttpResponseMessage ExecuteTransactions(TransactionParameters parameters)
        {
            var result = _ruleService.RunRules(parameters);
            return Request.CreateResponse(result);
        }
    }
}
