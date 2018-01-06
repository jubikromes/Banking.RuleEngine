using BankingRules.Data.Message;
using BankingRules.Models;
using BankingRules.RuleEngine.Data.Enum;
using BankingRules.RuleEngine.Rules;
using BankingRules.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BankingRules.Web.Controllers
{
    public class RuleController : Controller
    {
        private readonly RuleService _ruleService;
        private readonly RuleDetailsService _ruleDetailsService;
        public RuleController(RuleService ruleService, RuleDetailsService ruleDetailsService)
        {
            _ruleService = ruleService;
            _ruleDetailsService = ruleDetailsService;
        }
        // GET: Rule
        public ActionResult Index()
        {
            //get all rules and display them
            var models = _ruleService.GetRules();
            return View(models);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateRuleViewModel model)
        {
            if (model == null)
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Edit(EditRuleViewModel model)
        {
            return View();
        }

        public ActionResult ConfigureRule(Guid id)
        {
            TempData["RuleId"] = id;
            var models = _ruleDetailsService.GetRuleDetails(id);
            return View(models);
        }
        [HttpGet]
        public ActionResult AddRuleDetail(Guid bankingRuleId)
        {
            var model = new RuleMessage<AddRuleDetailViewModel>
            {
                Result = new AddRuleDetailViewModel { BankingRuleId = bankingRuleId}
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult AddRuleDetail(AddRuleDetailViewModel model)
        {
            var response = new RuleMessage<AddRuleDetailViewModel>
            {
                Result = model
            };
            ViewBag.OperandList = new List<SelectListItem> {
                new SelectListItem{ Text = ">", Value= "greaterthan"},
                new SelectListItem{ Text = ">=", Value= "greaterthanorequalto"},
                new SelectListItem{ Text = "+", Value= "add"},
                new SelectListItem{ Text = "-", Value= "subtract"},
                new SelectListItem{ Text = "!=", Value= "notequal"},
                new SelectListItem{ Text = "&", Value= "and"},
                new SelectListItem{ Text = "=", Value= "equals"},
                new SelectListItem{ Text = "<", Value= "lessthan"},
                new SelectListItem{ Text = "<=", Value= "lessthanorequalto"},
                new SelectListItem{ Text = "!", Value= "or"},
            };
            if (model == null)
            {
                response.Message = "";
                response.HasError = true;
                return View(response);
            }
            try
            {
                var ruleDetail = new BankingRuleDetails
                {
                    BankingRuleId = model.BankingRuleId,
                    LeftOperator = model.LeftOperator,
                    RightOperator = model.RightOperator,
                    ExpectedResult = model.ExpectedResult,
                    ExpectedResultType = model.ExpectedResultType,
                    IsChained = model.IsChained,
                    LeftParameterString = model.LeftParameterString,
                    Operand = model.Operand,
                    Order = model.Order,
                    RuleType = model.RuleType,
                    RightParamererString = model.RightParamererString,
                    Id = Guid.NewGuid(),
                };
                _ruleDetailsService.AddRuleDetail(ruleDetail);
                return RedirectToAction("ConfigureRule", new { id = model.BankingRuleId });
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.HasError = true;
                //log exception
                return View(response);
            }
        }
        [HttpGet]
        public ActionResult EditRuleDetail(Guid id)
        {
            ViewBag.OperandList = new List<SelectListItem> {
                new SelectListItem{ Text = ">", Value= "greaterthan"},
                new SelectListItem{ Text = ">=", Value= "greaterthanorequalto"},
                new SelectListItem{ Text = "+", Value= "add"},
                new SelectListItem{ Text = "-", Value= "subtract"},
                new SelectListItem{ Text = "!=", Value= "notequal"},
                new SelectListItem{ Text = "&", Value= "and"},
                new SelectListItem{ Text = "=", Value= "equals"},
                new SelectListItem{ Text = "<", Value= "lessthan"},
                new SelectListItem{ Text = "<=", Value= "lessthanorequalto"},
                new SelectListItem{ Text = "!", Value= "or"},
            };
            var responseModel = new RuleMessage<EditRuleDetailViewModel>
            {
                Message = string.Empty,
                HasError = false
            };
            try
            {
                var ruleDetail = _ruleDetailsService.GetRuleDetail(id);
                if (ruleDetail == null)
                {
                }
                var model = new EditRuleDetailViewModel
                {
                    Order = ruleDetail.Order,
                    ExpectedResult = ruleDetail.ExpectedResult,
                    ExpectedResultType = ruleDetail.ExpectedResultType,
                    IsChained = ruleDetail.IsChained,
                    LeftOperator = ruleDetail.LeftOperator,
                    LeftParameterString = ruleDetail.LeftParameterString,
                    OperationType = Enum.GetName(typeof(OperationType), ruleDetail.OperationType) ?? "",
                    Operand = ruleDetail.Operand,
                    RightOperator = ruleDetail.RightOperator,
                    RightParamererString = ruleDetail.RightParamererString,
                    RuleType = ruleDetail.RuleType, Id = ruleDetail.Id
                };
                responseModel.Result = model;
            }
            catch(Exception exc)
            {
                responseModel.Message = exc.Message;
                responseModel.HasError = true;
            }
            return View(responseModel);
        }
        [HttpPost]
        public ActionResult EditRuleDetail(EditRuleDetailViewModel model)
        {
            var response = new RuleMessage<EditRuleDetailViewModel>
            {

            };
            if (model == null)
            {
                response.HasError = true;
                response.Message = "Model cannot be null or doesnt exist";
                return View(response);
            }
            try
            {
                var ruleDetail = _ruleDetailsService.GetRuleDetail(model.Id);
                if (ruleDetail == null)
                {
                    response.HasError = true;
                    response.Message = "Rule does not exist or has been deleted.";
                    return View(response);
                }
                ruleDetail.ExpectedResult = model.ExpectedResult;
                ruleDetail.ExpectedResultType = model.ExpectedResultType;
                ruleDetail.IsChained = model.IsChained;
                ruleDetail.LeftOperator = model.LeftOperator;
                ruleDetail.LeftParameterString = model.LeftParameterString;
                ruleDetail.ModifiedDate = DateTime.Now;
                ruleDetail.Order = model.Order;
                ruleDetail.RightOperator = model.RightOperator;
                ruleDetail.RightParamererString = model.RightParamererString;
                ruleDetail.RuleType = model.RuleType;
                ruleDetail.Operand = model.Operand;

                _ruleDetailsService.Update(ruleDetail);
                return RedirectToAction("ConfigureRule", new { id = ruleDetail.BankingRuleId });
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.HasError = true;
                //log exception
                return View(response);

            }
        }
    }
}