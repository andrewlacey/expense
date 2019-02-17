using System;
using System.Threading.Tasks;
using DomainModels;
using Interfaces;
using Models;

namespace Services
{
    public class ExpenseService : IExpenseService 
    {

        private readonly IMessageParser _messageParser;
 

        public ExpenseService(IMessageParser messageParser)
        {
            _messageParser = messageParser;
        }
        public Task<ExpenseDetail> GetExpenseDetail(string expenseDetailText)
        {
            ExtractedExpenseDetail parsedData = _messageParser.ExtractExpenseData(expenseDetailText);

            if (!parsedData.Success)
            {
                throw new ApplicationException(parsedData.ValidationMessage);
            }

            //cost
            decimal total;
            if (!decimal.TryParse(parsedData.Total, out total))
            {
                throw new ApplicationException("Total is not a valid decimal amount");
            }

            //Get Details

            GstDetail gstDetail = Calculation.Calculator.CalculateGstDetailFromGstInclusiveAmount(total);

            return Task.FromResult(new ExpenseDetail
            {
                CostCentre = parsedData.CostCentre ?? ServiceConstants.DefaultCostCentre,
                Total = gstDetail.AmountIncludingGst,
                TotalExcludingGst = gstDetail.AmountExcludingGst,
                Gst = gstDetail.GstAmount,
                Description = parsedData.Description,
                Date = parsedData.Date,
                PaymentMethod = parsedData.PaymentMethod,
                Vendor = parsedData.Vendor
            });
        }
    }
}
