using System.Runtime.Intrinsics.X86;
using System.Security.Principal;

namespace BarclayPG.Constants
{
    public class AppConstants
    {
        public static class GatewayConfigurations
        {
            //You have to create a test account to use the PSPID, Secret Key.
            //Ideally you would keep it encrypted in database or in your application and use it when needed.
            public const string GatewayPSPID = "RBMBarclayTest";
            public const string GatewaySHAInSecretKey = "26ddc278-0fe2-4538-a1d8-7aeb92a76ed9";
            public const string GatewaySHAOutSecretKey = "26ddc278-0fe2-4538-a1d8-7aeb92a76ed9";
            public const string CurrencyCode = "GBP";
            public const string GatewayPaymentURL = "https://mdepayments.epdq.co.uk/ncol/test/orderstandard_utf8.asp";
        }
    }
}
