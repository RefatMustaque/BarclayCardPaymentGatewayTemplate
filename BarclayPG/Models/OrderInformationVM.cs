using System.ComponentModel.DataAnnotations;

namespace BarclayPG.Models
{
    public class OrderInformationVM
    {
        [Display(Name = "Amount")]
        [Required]
        public decimal Amount { get; set; }


        //Billing Information
        [Display(Name = "Name")]
        [Required]
        public string CustomerName { get; set; }
        [Display(Name = "Email")]
        [Required]
        public string CustomerEmail { get; set; }
        [Display(Name = "ZIP")]
        [Required]
        public string CustomerZip { get; set; }
        [Display(Name = "Address")]
        [Required]
        public string CustomerAddress { get; set; }
        [Display(Name = "City")]
        [Required]
        public string CustomerCity { get; set; }
        [Display(Name = "Town")]
        [Required]
        public string CustomerTown { get; set; }
        [Display(Name = "Telephone/Mobile")]
        [Required]
        public string CustomerTelephoneNo { get; set; }

        //Gateway Information
        [Display(Name = "PSPID")]
        [Required]
        public string GatewayPSPID { get; set; }
        [Display(Name = "SHA-In Secret Key")]
        [Required]
        public string GatewaySHAInSecretKey { get; set; }
        [Display(Name = "SHA-Out Secret Key")]
        [Required]
        public string GatewaySHAOutSecretKey { get; set; }
    }
}
