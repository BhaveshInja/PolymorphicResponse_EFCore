using System.Text.Json.Serialization;

namespace Domain.DTO
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(CreditCardPaymentDto), "credit-card")]
    [JsonDerivedType(typeof(PaypalPaymentDto), "paypal")]
    [JsonDerivedType(typeof(BankTransferPaymentDto), "bank-transfer")]
    public abstract class PaymentDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
