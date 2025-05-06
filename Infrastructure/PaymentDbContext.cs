using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }

        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<CreditCardPayment> CreditCardPayments => Set<CreditCardPayment>();
        public DbSet<BankTransferPayment> BankTransferPayments => Set<BankTransferPayment>();
        public DbSet<PaypalPayment> PaypalPayments => Set<PaypalPayment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("PaymentSequence", schema: "dbo")
            .StartsAt(1)
            .IncrementsBy(1);

            // 2. Enable TPC and wire up Id to use the sequence
            modelBuilder.Entity<Payment>(b =>
            {
                b.UseTpcMappingStrategy();

                // Make Id default to NEXT VALUE FOR the sequence
                b.Property(p => p.Id)
                 .HasDefaultValueSql("NEXT VALUE FOR dbo.PaymentSequence")
                 .ValueGeneratedOnAdd();
            });

            // 3. Map each concrete type to its own table
            modelBuilder.Entity<CreditCardPayment>()
                .ToTable("CreditCardPayments");

            modelBuilder.Entity<PaypalPayment>()
                .ToTable("PaypalPayments");

            modelBuilder.Entity<BankTransferPayment>()
                .ToTable("BankTransferPayments");

            // 4. Configure the BankTransferLeg relationship
            modelBuilder.Entity<BankTransferLeg>(b =>
            {
                b.ToTable("BankTransferLegs");

                b.HasKey(l => l.Id);

                b.HasOne(l => l.BankTransferPayment)
                 .WithMany(p => p.TransferLegs)
                 .HasForeignKey(l => l.BankTransferPaymentId);
            });
        }

    }
}
