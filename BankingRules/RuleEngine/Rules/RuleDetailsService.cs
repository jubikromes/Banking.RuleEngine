using BankingRules.Data.Message;
using BankingRules.Data.Repository;
using BankingRules.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingRules.RuleEngine.Rules
{
    public class RuleDetailsService
    {
        private readonly IRepository<BankingRuleDetails> _ruleDetailsRepository;

        public RuleDetailsService(IRepository<BankingRuleDetails> ruleDetailsRepository)
        {
            _ruleDetailsRepository = ruleDetailsRepository;
        }

        public RuleMessage<List<BankingRuleDetails>> GetRuleDetails(Guid guid)
        {
            var message = new RuleMessage<List<BankingRuleDetails>>
            {
                Result = new List<BankingRuleDetails> { },
                HasError = false,
                Message = ""
            };
            try
            {
                var ruledetails = _ruleDetailsRepository.Find(p => p.BankingRuleId == guid);
                message.HasError = false;
                message.Result = ruledetails.ToList();
                return message;
            }
            catch (Exception ex)
            {
                //log this message
                message.HasError = true;
                message.Message = ex.Message;
                return message;
            }
        }

        public BankingRuleDetails GetRuleDetail(Guid guid)
        {
            try
            {
                var ruledetail =_ruleDetailsRepository.GetById(guid);
                return ruledetail;
            }
            catch (Exception ex)
            {
                //log exception
                return null;
            }
        }
        public void AddRuleDetail(BankingRuleDetails ruleDetails)
        {
            try
            {
                var thisorder = ruleDetails.Order;
                _ruleDetailsRepository.Add(ruleDetails);
                _ruleDetailsRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                //log exception
            }
        }
        public void Update(BankingRuleDetails ruleDetails)
        {
            try
            {

                var thisorder = ruleDetails.Order;
                if (_ruleDetailsRepository.Find(p => p.Order > thisorder).Count() < 0)
                {
                    var oldLastRule = _ruleDetailsRepository.Find(p => p.IsLast == true && p.Id != ruleDetails.Id).FirstOrDefault();
                    if (oldLastRule != null)
                    {
                        ruleDetails.IsLast = true;
                        oldLastRule.IsLast = false;
                        _ruleDetailsRepository.Update(oldLastRule);
                        _ruleDetailsRepository.Update(ruleDetails);

                        _ruleDetailsRepository.SaveChanges();
                    }
                }
                if (_ruleDetailsRepository.Find(p => p.Order < thisorder).Count() < 0)
                {
                    var oldFirstRule = _ruleDetailsRepository.Find(p => p.IsFirst == true && p.Id != ruleDetails.Id).FirstOrDefault();
                    if (oldFirstRule != null)
                    {
                        ruleDetails.IsFirst = true;
                        oldFirstRule.IsFirst = false;
                        _ruleDetailsRepository.Update(oldFirstRule);
                        _ruleDetailsRepository.Update(ruleDetails);

                        _ruleDetailsRepository.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                //log exception
            }
            
        }
    }
}
