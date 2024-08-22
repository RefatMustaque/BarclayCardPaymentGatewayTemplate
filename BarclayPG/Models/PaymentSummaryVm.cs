using System.ComponentModel.DataAnnotations;

namespace BarclayPG.Models
{
    public class PaymentSummaryVm
    {
        [Display(Name = "Amount")]
        [Required]
        public decimal Amount { get; set; }
    }
}
