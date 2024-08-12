using System;
using Application.DTOs;
using Application.Services.Interface;
using Application.Validation;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Domain.RepositoriyInterfaces;

namespace Application.Services
{
    public class PaymentTransactionService : IPaymentTransactionService
    {
            private readonly IPaymentTransactionRepository _paymentTransactionRepository;
            private readonly IMapper _mapper;
            private readonly IValidator<PaymentTransactionDto> _paymentTransactionValidator;

            public PaymentTransactionService(IPaymentTransactionRepository paymentTransactionRepository, IMapper mapper, IValidator<PaymentTransactionDto> paymentTransactionValidator)
            {
                _paymentTransactionRepository = paymentTransactionRepository;
                _mapper = mapper;
                _paymentTransactionValidator = paymentTransactionValidator;
            }

        public async Task<PaymentTransactionDto> CreatePaymentTransactionAsync(PaymentTransactionDto paymentDto)
        {
            _paymentTransactionValidator.Validate(paymentDto);
            var paymentTransaction = _mapper.Map<PaymentTransaction>(paymentDto);
            paymentTransaction = await _paymentTransactionRepository.CreateAsync(paymentTransaction);
            return _mapper.Map<PaymentTransactionDto>(paymentTransaction);
        }

        public async Task<PaymentTransactionDto> GetTransactionByIdAsync(int transactionId)
        {
            var transaction = await _paymentTransactionRepository.GetByIdAsync(transactionId);

            if (transaction == null || !transaction.IsActive)
            {
                throw new NotFoundException($"Cart with CustomerID {transactionId} not found or is inactive.");
            }
            return _mapper.Map<PaymentTransactionDto>(transaction);
        }

        public async Task<IEnumerable<PaymentTransactionDto>> GetTransactionsByOrderIdAsync(int orderId)
        {
            var transactions = await _paymentTransactionRepository.GetByOrderIdAsync(orderId);
            if (transactions == null)
            {
                throw new NotFoundException($"Cart with CustomerID {orderId} not found or is inactive.");
            }
            return _mapper.Map<IEnumerable<PaymentTransactionDto>>(transactions);
        }

        public async Task UpdatePaymentTransactionAsync(PaymentTransactionDto paymentDto)
        {
            _paymentTransactionValidator.Validate(paymentDto);
            var transaction = await _paymentTransactionRepository.GetByIdAsync(paymentDto.PaymentTransactionId);
            if (transaction == null)
            {
                throw new InvalidOperationException("Payment transaction not found.");
            }

            _mapper.Map(paymentDto, transaction);
            transaction.LastUpdatedDate = DateTime.UtcNow;
            await _paymentTransactionRepository.UpdateAsync(transaction);
        }

        public async Task DeletePaymentTransactionAsync(int transactionId)
        {
            var transaction = await _paymentTransactionRepository.GetByIdAsync(transactionId);
            if (transaction == null)
            {
                throw new InvalidOperationException("Payment transaction not found.");
            }
            await _paymentTransactionRepository.DeleteAsync(transactionId);
        }
    }
}