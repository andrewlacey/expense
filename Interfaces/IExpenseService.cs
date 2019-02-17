using System.Threading.Tasks;
using DomainModels;

namespace Interfaces
{
    public interface IExpenseService
    {
        Task<ExpenseDetail> GetExpenseDetail(string expenseDetailText);
    }
}
