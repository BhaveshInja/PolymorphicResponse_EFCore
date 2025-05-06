namespace Domain.Entities
{
    public class BankTransferLeg
    {
        public int Id { get; set; }
        public string Beneficiary { get; set; } = null!;
        public decimal Amount { get; set; }

        // foreign-key → parent payment
        public int BankTransferPaymentId { get; set; }
        public BankTransferPayment BankTransferPayment { get; set; } = null!;
    }
}
