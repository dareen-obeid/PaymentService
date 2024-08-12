using System;
using Application.DTOs;
using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.Validation
{
    public class PaymentTransactionValidator : IValidator<PaymentTransactionDto>
    {
        public void Validate(PaymentTransactionDto paymentTransaction)
        {
            if (paymentTransaction.OrderId <= 0)
                throw new ValidationException("Order ID must be positive.");

            if (paymentTransaction.Amount < 0)
                throw new ValidationException("Amount must be greater than zero.");

            if (!Enum.TryParse<PaymentMethodType>(paymentTransaction.PaymentMethod, out _))
                throw new ValidationException("Invalid payment method.");

            if (paymentTransaction.PaymentStatus == null || paymentTransaction.PaymentStatus.StatusId <= 0)
                throw new ValidationException("Invalid payment status.");


        }
    }

}