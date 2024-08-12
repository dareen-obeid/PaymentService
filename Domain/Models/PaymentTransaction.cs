using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class PaymentTransaction
	{
        [Key]
        public int PaymentTransactionId { get; set; }

        [Required]
        public int OrderId { get; set; }  

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        [ForeignKey("PaymentStatus")] 
        public int PaymentStatusId { get; set; }

        [Required]
        public PaymentMethodType PaymentMethod { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }
}

