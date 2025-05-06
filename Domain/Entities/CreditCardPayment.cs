using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public string CardNumber { get; set; } = null!;
        public string CardholderName { get; set; } = null!;
        public string Expiry { get; set; } = null!;
    }
}
