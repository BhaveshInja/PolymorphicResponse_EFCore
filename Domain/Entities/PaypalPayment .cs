using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PaypalPayment : Payment
    {
        public string PaypalTransactionId { get; set; } = null!;
        public string PayerEmail { get; set; } = null!;
    }
}
