using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Services.Interface;
using System.Collections.Generic;

namespace Services.Implementation
{
    public class PaymentService(PaymentDbContext _db, IMapper mapper) : IPaymentService
    {
        public async Task<ApiResponse<ICollection<PaymentDto>>> GetAllPayments()
        {
            var payments = await _db.Payments
                                .Include(p => (p as BankTransferPayment)!.TransferLegs)
                                .ToListAsync();

            var result = mapper.Map<ICollection<PaymentDto>>(payments);

            if (result == null) return ApiResponse<ICollection<PaymentDto>>.Fail("Error");

            return ApiResponse<ICollection<PaymentDto>>.Ok(result);
        }

        public async Task<ApiResponse<PaymentDto>> GetById(int id)
        {
            var payment = await _db.Payments
                    .Include(p => (p as BankTransferPayment)!.TransferLegs)
                    .FirstOrDefaultAsync(p => p.Id == id);

            if (payment is null)
                return ApiResponse<PaymentDto>
                       .Fail($"Payment with ID {id} not found.", 404);

            var dto = mapper.Map<PaymentDto>(payment);

            return ApiResponse<PaymentDto>.Ok(dto);
        }
    }
}
