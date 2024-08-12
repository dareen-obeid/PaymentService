using System;
namespace Domain.Models
{
    public enum PaymentMethodType
    {
        CreditCard = 0,
        DebitCard,
        PayPal,
        BankTransfer,
        Cash
    }
}

