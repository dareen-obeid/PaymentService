using System;
using Application.DTOs;
using Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentTransactionService _paymentTransactionService;

        public PaymentController(IPaymentTransactionService paymentService)
        {
            _paymentTransactionService = paymentService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePaymentTransaction([FromBody] PaymentTransactionDto paymentTransactionDto)
        {
            var createdTransaction = await _paymentTransactionService.CreatePaymentTransactionAsync(paymentTransactionDto);
            return CreatedAtAction(nameof(GetPaymentTransactionById), new { transactionId = createdTransaction.PaymentTransactionId }, createdTransaction);
        }

        [HttpGet("transaction/{transactionId}")]
        public async Task<IActionResult> GetPaymentTransactionById(int transactionId)
        {
            var transaction = await _paymentTransactionService.GetTransactionByIdAsync(transactionId);
            return Ok(transaction);
        }

        [HttpGet("transactions/{orderId}")]
        public async Task<IActionResult> GetTransactionsByOrderId(int orderId)
        {
            var transactions = await _paymentTransactionService.GetTransactionsByOrderIdAsync(orderId);
            return Ok(transactions);
        }

        [HttpPut("update/{transactionId}")]
        public async Task<IActionResult> UpdatePaymentTransaction(int transactionId, [FromBody] PaymentTransactionDto paymentTransactionDto)
        {
            if (transactionId != paymentTransactionDto.PaymentTransactionId)
            {
                return BadRequest("Mismatched Transaction ID");
            }

            await _paymentTransactionService.UpdatePaymentTransactionAsync(paymentTransactionDto);
            return NoContent();
        }

        [HttpDelete("delete/{transactionId}")]
        public async Task<IActionResult> DeletePaymentTransaction(int transactionId)
        {
            await _paymentTransactionService.DeletePaymentTransactionAsync(transactionId);
            return NoContent();
        }
    }
}
