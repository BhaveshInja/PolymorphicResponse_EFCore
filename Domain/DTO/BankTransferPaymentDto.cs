using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class BankTransferPaymentDto : PaymentDto
    {
        public string BankAccount { get; set; } = string.Empty;
        public string RoutingNumber { get; set; } = string.Empty;

        // mirror the collection navigation
        public List<BankTransferLegDto> TransferLegs { get; set; }
            = new List<BankTransferLegDto>();
    }
}
