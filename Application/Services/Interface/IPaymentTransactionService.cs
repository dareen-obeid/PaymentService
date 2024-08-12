using Application.DTOs;

namespace Application.Services.Interface
{
	public interface IPaymentTransactionService
	{
        Task<PaymentTransactionDto> CreatePaymentTransactionAsync(PaymentTransactionDto paymentDto);
        Task<IEnumerable<PaymentTransactionDto>> GetTransactionsByOrderIdAsync(int orderId);
        Task<PaymentTransactionDto> GetTransactionByIdAsync(int transactionId);
        Task UpdatePaymentTransactionAsync(PaymentTransactionDto paymentDto);
        Task DeletePaymentTransactionAsync(int transactionId);
    }
}


