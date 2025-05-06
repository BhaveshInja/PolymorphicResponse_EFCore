using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IPaymentService
    {
        Task<ApiResponse<ICollection<PaymentDto>>> GetAllPayments();

        Task<ApiResponse<PaymentDto>> GetById(int id);
    }
}
