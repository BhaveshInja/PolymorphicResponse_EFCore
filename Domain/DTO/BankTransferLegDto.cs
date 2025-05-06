using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class BankTransferLegDto
    {
        public int Id { get; set; }
        public int BankTransferPaymentId { get; set; }
        public string Beneficiary { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
