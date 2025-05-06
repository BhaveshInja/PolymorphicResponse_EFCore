
namespace Domain.Entities
{
    public abstract class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
