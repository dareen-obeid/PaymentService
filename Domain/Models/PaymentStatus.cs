using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
	public class PaymentStatus
	{
        [Key]
        public int StatusId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentStatusName { get; set; }

        public ICollection<PaymentTransaction> PaymentTransactions { get; set; }
    }
}

