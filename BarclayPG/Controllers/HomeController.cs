using BarclayPG.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace BarclayPG.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
                PaymentSummaryVm paymentSummaryVm = new PaymentSummaryVm()
                {
                    //General parameters
                    Amount = Convert.ToInt64((orderInformationVM.Amount * 100)).ToString(), //"2000",
                    Currency = "£",
                    Language = "en_US",
                    OrderId = Guid.NewGuid().ToString(),
                    PSPID = orderInformationVM.GatewayPSPID,

                    //layout information
                    TITLE = "Payment Gateway Page",

                    // post payment redirection
                    //CancelURL = GetBaseUrl() + "/",
                    //AcceptURL = GetBaseUrl() + "/",
                    //ExceptionURL = GetBaseUrl() + "/", 
                    //DeclineURL = GetBaseUrl() + "/" ,
                    CATALOGURL = "",
                    HOMEURL = "",

                    //Extra info
                    PaymentGatewayURL = "",
                };
                return View(paymentSummaryVm);
            }
            else
            {
                return View(nameof(Index), orderInformationVM);
            }
        }

        //public IActionResult PaymentCancelled(Dictionary<string, string> transactionData)
        //{

        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
