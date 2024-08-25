
using BarclayPG.Models;

namespace BarclayPG.Services
{
    public interface IPaymentService
    {
        public PaymentSummaryVm GetPaymentFormData(OrderInformationVM orderInformationVM);
        public bool TransactionFeedIsValid(Dictionary<string, string> transactionData);
    }
}
