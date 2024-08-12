using System;
using Domain.Models;

namespace Domain.RepositoriyInterfaces
{
    public interface IPaymentTransactionRepository
    {
        Task<PaymentTransaction> CreateAsync(PaymentTransaction paymentTransaction);
        Task<IEnumerable<PaymentTransaction>> GetByOrderIdAsync(int orderId);
        Task<PaymentTransaction> GetByIdAsync(int transactionId);
        Task UpdateAsync(PaymentTransaction paymentTransaction);
        Task DeleteAsync(int transactionId);
    }

}