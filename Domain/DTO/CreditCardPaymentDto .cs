using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class CreditCardPaymentDto : PaymentDto
    {
        public string CardNumber { get; set; } = string.Empty;
        public string CardholderName { get; set; } = string.Empty;
        public string Expiry { get; set; } = string.Empty;
    }
}
