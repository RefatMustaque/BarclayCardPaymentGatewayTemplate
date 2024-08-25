
using BarclayPG.Constants;
using BarclayPG.Controllers;
using BarclayPG.Helpers;
using BarclayPG.Models;
using System.Xml.Linq;

namespace BarclayPG.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PaymentService(IHttpContextAccessor httpContextAccessor) { 
            _httpContextAccessor = httpContextAccessor;
        }
        public PaymentSummaryVm GetPaymentFormData(OrderInformationVM orderInformationVM)
        {
            PaymentSummaryVm paymentSummaryVm = new PaymentSummaryVm()
            {
                //General parameters
                Amount = Convert.ToInt64((orderInformationVM.Amount * 100)).ToString(), //"2000",
                Currency = AppConstants.GatewayConfigurations.CurrencyCode,
                Language = "en_US",
                OrderId = Guid.NewGuid().ToString(),

                //layout information
                TITLE = "Payment Gateway Page",

                // post payment redirection
                CancelURL = GetBaseUrl() + "/" + "Home" + "/" + nameof(HomeController.PaymentCancelled),
                AcceptURL = GetBaseUrl() + "/" + "Home" + "/" + nameof(HomeController.PaymentAccepted),
                ExceptionURL = GetBaseUrl() + "/" + "Home" + "/" + nameof(HomeController.PaymentException),
                DeclineURL = GetBaseUrl() + "/" + "Home" + "/" + nameof(HomeController.PaymentDeclined),
                CATALOGURL = GetBaseUrl() + "/" + "Home" + "/" + nameof(HomeController.Index),
                HOMEURL = GetBaseUrl() + "/" + "Home" + "/" + nameof(HomeController.Index),

                //Gateway Information
                PaymentGatewayURL = AppConstants.GatewayConfigurations.GatewayPaymentURL,
                PSPID = AppConstants.GatewayConfigurations.GatewayPSPID,

                //Billing Information
                Email = orderInformationVM.CustomerEmail,
                CN = orderInformationVM.CustomerName,
                OwnerZIP = orderInformationVM.CustomerZip,
                OwnerAddress = orderInformationVM.CustomerAddress,
                OwnerCity = orderInformationVM.CustomerCity,
                OwnerTown = orderInformationVM.CustomerTown,
                OwnerTelephoneNo = orderInformationVM.CustomerTelephoneNo,
            };

            string plainKey =
                        "ACCEPTURL=" + paymentSummaryVm.AcceptURL + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "AMOUNT=" + paymentSummaryVm.Amount + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "CANCELURL=" + paymentSummaryVm.CancelURL + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "CATALOGURL=" + paymentSummaryVm.CATALOGURL + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "CN=" + paymentSummaryVm.CN + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "CURRENCY=" + paymentSummaryVm.Currency + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "DECLINEURL=" + paymentSummaryVm.DeclineURL + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "EMAIL=" + paymentSummaryVm.Email + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "EXCEPTIONURL=" + paymentSummaryVm.ExceptionURL + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "HOMEURL=" + paymentSummaryVm.HOMEURL + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "LANGUAGE=" + paymentSummaryVm.Language + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "ORDERID=" + paymentSummaryVm.OrderId + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "OWNERADDRESS=" + paymentSummaryVm.OwnerAddress + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "OWNERCTY=" + paymentSummaryVm.OwnerCity + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "OWNERTELNO=" + paymentSummaryVm.OwnerTelephoneNo + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "OWNERTOWN=" + paymentSummaryVm.OwnerTown + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "OWNERZIP=" + paymentSummaryVm.OwnerZIP + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "PSPID=" + paymentSummaryVm.PSPID + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey +
                        "TITLE=" + paymentSummaryVm.TITLE + AppConstants.GatewayConfigurations.GatewaySHAOutSecretKey
                        ;

            paymentSummaryVm.ShaPass = SHAHelper.ConvertToSha256Hash(plainKey);

            return paymentSummaryVm;
        }
        public bool TransactionFeedIsValid(Dictionary<string, string> transactionData)
        {
            bool isValid = false;

            string ShaInSecretKey = AppConstants.GatewayConfigurations.GatewaySHAInSecretKey;

            // Remove SHASIGN and store its value
            string RecievedShaSign = transactionData.ContainsKey("SHASIGN") ? transactionData["SHASIGN"] : string.Empty;
            transactionData.Remove("SHASIGN");

            var sortedKeys = transactionData.Keys.OrderBy(k => k).ToList();
            var plainKey = string.Empty;

            foreach (var key in sortedKeys)
            {
                //Important
                //All sent parameters(that appear in the SHA-OUT Parameter list), will be included in the string to hash.
                //All parameters need to be sorted alphabetically
                //Parameters that do not have a value should NOT be included in the string to hash
                //Even though some parameters are(partially) returned in lower case by our system, for the SHA-OUT calculation each parameter must be put in upper case.
                //When you choose to transfer your test account to production via the link in the back-office menu, a random SHA - OUT passphrase will be automatically configured in your production account.
                //For extra safety, we request that you use different SHA passphrases for TEST and PROD.Please note that if they are found to be identical, your TEST passphrase will be changed by our system(you will of course be notified).
                var thisKey = key;
                var thisValue = transactionData[key];
                if (!string.IsNullOrEmpty(thisValue))
                {
                    plainKey += thisKey.ToUpper() + "=" + thisValue + ShaInSecretKey; 
                }
            }

            string GeneratedShaPass = SHAHelper.ConvertToSha256Hash(plainKey);

            if (RecievedShaSign.Trim().ToUpper() == GeneratedShaPass.Trim().ToUpper())
            {
                isValid = true;
            }

            return isValid;
        }

        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            return baseUrl;
        }
    }
}
