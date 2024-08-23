using System.ComponentModel.DataAnnotations;

namespace BarclayPG.Models
{
    public class PaymentSummaryVm
    {
        [Display(Name = "Amount")]
        [Required]
        public string Amount { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string OrderId { get; set; }
        [Required]
        public string PSPID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CN { get; set; }
        [Required]
        public string OwnerZIP { get; set; }
        [Required]
        public string OwnerAddress { get; set; }
        [Required]
        public string OwnerCity { get; set; }
        [Required]
        public string OwnerTown { get; set; }
        [Required]
        public string OwnerTelephoneNo { get; set; }

        //check before the payment
        [Required]
        public string ShaPass { get; set; }

        //layout information
        public string TITLE { get; set; }

        // post payment redirection
        public string AcceptURL { get; set; }
        public string DeclineURL { get; set; }
        public string ExceptionURL { get; set; }
        public string CancelURL { get; set; }

        public string CATALOGURL { get; set; }
        public string HOMEURL { get; set; }

        //Extra Info
        public string PaymentGatewayURL { get; set; }

        public string CurrencySign { get; set; }
    }
}
