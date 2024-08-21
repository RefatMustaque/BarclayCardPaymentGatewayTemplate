using System.ComponentModel.DataAnnotations;

namespace BarclayPG.Models
{
    public class OrderInformationVM
    {
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }


        //Billing Information
        [Display(Name = "Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Email")]
        public string CustomerEmail { get; set; }
        [Display(Name = "ZIP")]
        public string CustomerZip { get; set; }
        [Display(Name = "Address")]
        public string CustomerAddress { get; set; }
        [Display(Name = "City")]
        public string CustomerCity { get; set; }
        [Display(Name = "Town")]
        public string CustomerTown { get; set; }
        [Display(Name = "Telephone/Mobile")]
        public string CustomerTelephoneNo { get; set; }

        //Gateway Information
        [Display(Name = "PSPID")]
        public string GatewayPSPID { get; set; }
        [Display(Name = "SHA-In Secret Key")]
        public string GatewaySHAInSecretKey { get; set; }
        [Display(Name = "SHA-Out Secret Key")]
        public string GatewaySHAOutSecretKey { get; set; }
    }
}
