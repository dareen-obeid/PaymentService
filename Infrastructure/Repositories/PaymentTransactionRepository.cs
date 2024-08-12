using System;
using Domain.Models;
using Domain.RepositoriyInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{

    public class PaymentTransactionRepository : IPaymentTransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentTransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentTransaction> CreateAsync(PaymentTransaction paymentTransaction)
        {
            if (paymentTransaction.PaymentStatus != null && paymentTransaction.PaymentStatus.StatusId == 0)
            {
                // Adding a new PaymentStatus, ensure ID is not set
                _context.PaymentStatuses.Add(paymentTransaction.PaymentStatus);
            }
            else if (paymentTransaction.PaymentStatus != null)
            {
                // Link to an existing PaymentStatus, ensure it's not added as new
                _context.PaymentStatuses.Attach(paymentTransaction.PaymentStatus);
            }

            _context.PaymentTransactions.Add(paymentTransaction);
            await _context.SaveChangesAsync();
            return paymentTransaction;
        }


        public async Task<IEnumerable<PaymentTransaction>> GetByOrderIdAsync(int orderId)
        {
            return await _context.PaymentTransactions
                        .Where(pt => pt.OrderId == orderId && pt.IsActive)
                        .Include(pt => pt.PaymentStatus)
                        .ToListAsync();
        }

        public async Task<PaymentTransaction> GetByIdAsync(int transactionId)
        {
            return await _context.PaymentTransactions
                        .Include(pt => pt.PaymentStatus)
                        .FirstOrDefaultAsync(pt => pt.PaymentTransactionId == transactionId && pt.IsActive);
        }

        public async Task UpdateAsync(PaymentTransaction paymentTransaction)
        {
            _context.Entry(paymentTransaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int transactionId)
        {
            var transaction = await _context.PaymentTransactions.FindAsync(transactionId);
            if (transaction != null)
            {
                transaction.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}