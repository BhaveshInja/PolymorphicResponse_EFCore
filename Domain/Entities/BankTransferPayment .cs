using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BankTransferPayment : Payment
    {
        public string BankAccount { get; set; } = null!;
        public string RoutingNumber { get; set; } = null!;

        // ← Collection navigation for one-to-many
        public ICollection<BankTransferLeg> TransferLegs { get; set; }
            = new List<BankTransferLeg>();
    }
}
