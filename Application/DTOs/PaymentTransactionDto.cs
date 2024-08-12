using System;
using Domain.Models;

namespace Application.DTOs
{
    public class PaymentTransactionDto
    {
        public int PaymentTransactionId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public PaymentStatusDto PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsActive { get; set; }
    }

}