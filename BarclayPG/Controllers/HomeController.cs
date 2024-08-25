using BarclayPG.Models;
using BarclayPG.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace BarclayPG.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPaymentService _paymentService;

        public HomeController(ILogger<HomeController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        public IActionResult Index()
        {
            OrderInformationVM orderInformationVM = new OrderInformationVM
            {
                Amount = 20,
                CustomerName = "Robert Patkinson",
                CustomerEmail = "robert@gmail.com",
                CustomerZip = "M37GG",
                CustomerAddress = "12 Newland Avenue",
                CustomerCity = "Manchester",
                CustomerTown = "Manchester",
                CustomerTelephoneNo = "07498234098",
            };
            return View(orderInformationVM);
        }

        public IActionResult PaymentSummary(OrderInformationVM orderInformationVM)
        {
            if (ModelState.IsValid)
            {
                PaymentSummaryVm paymentSummaryVm = _paymentService.GetPaymentFormData(orderInformationVM);

                return View(paymentSummaryVm);
            }
            else
            {
                return View(nameof(Index), orderInformationVM);
            }
        }

        public IActionResult PaymentCancelled(Dictionary<string, string> transactionData)
        {
            bool isValid = _paymentService.TransactionFeedIsValid(transactionData);

            if (!isValid)
            {
                return BadRequest("Payment information is not valid.");
            }

            if (!transactionData.TryGetValue("orderID", out string orderId))
            {
                return BadRequest("Missing order ID");
            }

            PaymentTransactionFeedbackVm paymentTransactionFeedbackVm = new PaymentTransactionFeedbackVm();
            paymentTransactionFeedbackVm.Msg = "Really sorry that you had to cancel the payment. Please click below to go back to the campaign.";
            paymentTransactionFeedbackVm.OrderId = orderId;
            paymentTransactionFeedbackVm.Status = "CANCELLED";
            return View("PostPaymentTransaction", paymentTransactionFeedbackVm);
        }

        public IActionResult PaymentDeclined(Dictionary<string, string> transactionData)
        {
            bool isValid = _paymentService.TransactionFeedIsValid(transactionData);

            if (!isValid)
            {
                return BadRequest("Payment information is not valid.");
            }

            if (!transactionData.TryGetValue("orderID", out string orderId))
            {
                return BadRequest("Missing order ID");
            }

            PaymentTransactionFeedbackVm paymentTransactionFeedbackVm = new PaymentTransactionFeedbackVm();
            paymentTransactionFeedbackVm.Msg = "Sorry your payment is declined. Please check your card information is correct or there is enough balance. If you still persist the problem contact with the administrator.";
            paymentTransactionFeedbackVm.OrderId = orderId;
            paymentTransactionFeedbackVm.Status = "DECLINED";
            return View("PostPaymentTransaction", paymentTransactionFeedbackVm);
        }

        public IActionResult PaymentException(Dictionary<string, string> transactionData)
        {
            bool isValid = _paymentService.TransactionFeedIsValid(transactionData);

            if (!isValid)
            {
                return BadRequest("Payment information is not valid.");
            }

            if (!transactionData.TryGetValue("orderID", out string orderId))
            {
                return BadRequest("Missing order ID");
            }

            
            PaymentTransactionFeedbackVm paymentTransactionFeedbackVm = new PaymentTransactionFeedbackVm();
            paymentTransactionFeedbackVm.Msg = "Sorry, something went wrong. Please try again later. If you still persist the problem contact with the administrator.";
            paymentTransactionFeedbackVm.OrderId = orderId;
            paymentTransactionFeedbackVm.Status = "EXCEPTION";
            return View("PostPaymentTransaction", paymentTransactionFeedbackVm);
        }

        public IActionResult PaymentAccepted(Dictionary<string, string> transactionData)
        {
            bool isValid = _paymentService.TransactionFeedIsValid(transactionData);

            if (!isValid)
            {
                return BadRequest("Payment information is not valid.");
            }

            if (!transactionData.TryGetValue("orderID", out string orderId))
            {
                return BadRequest("Missing order ID");
            }

            
            PaymentTransactionFeedbackVm paymentTransactionFeedbackVm = new PaymentTransactionFeedbackVm();
            paymentTransactionFeedbackVm.Msg = "Congratulations! Payment was succesful.";
            paymentTransactionFeedbackVm.OrderId = orderId;
            paymentTransactionFeedbackVm.Status = "ACCEPTED";
            return View("PostPaymentTransaction", paymentTransactionFeedbackVm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
