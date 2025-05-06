using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class PaypalPaymentDto : PaymentDto
    {
        public string PaypalTransactionId { get; set; } = string.Empty;
        public string PayerEmail { get; set; } = string.Empty;
    }
}
