using Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExpenseCalculatorApi.Controllers
{
    [RoutePrefix("api/ExpenseService")]
    public class ExpenseController : ApiController
    {
        private readonly IExpenseService _expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        [Route("SubmitExpense")]
        public async Task<IHttpActionResult> SubmitExpense([FromBody]string expenseDetail)
        {
            try
            {
                var expense = await _expenseService.GetExpenseDetail(expenseDetail);
                return Ok(expense);
            }

            catch (ApplicationException appEx)
            {
                //Error in validation let client know issue
                return Content(HttpStatusCode.BadRequest, appEx.Message);
            }
            catch (Exception ex)
            {
                //Genuine exception this would be logged to appopriate place.
                //Log(ex)

                // return friendly message to client
                return Content(HttpStatusCode.InternalServerError, "An unexpected error has occurred, please contact support");
            }
        }
    }
}
