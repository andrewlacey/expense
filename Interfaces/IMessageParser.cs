using Models;

namespace Interfaces
{
    public interface IMessageParser
    {
        ExtractedExpenseDetail ExtractExpenseData(string text);
    }
}
