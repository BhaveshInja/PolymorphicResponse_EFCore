using Bogus;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

public static class SeedData
{
    public static void Initialize(PaymentDbContext context, int countPerType = 50)
    {
        // If you’ve already got any payments, bail out
        if (context.Set<Payment>().Any())
            return;

        // 1. Faker for CreditCardPayment
        var ccFaker = new Faker<CreditCardPayment>()
            .RuleFor(p => p.Amount, f => f.Finance.Amount(10, 1_000))
            .RuleFor(p => p.ProcessedAt, f => f.Date.Recent(30))
            .RuleFor(p => p.CardNumber, f => f.Finance.CreditCardNumber())
            .RuleFor(p => p.CardholderName, f => f.Name.FullName())
            .RuleFor(p => p.Expiry, f => DateTime.Now.AddMonths(f.Random.Int(1, 36)).ToString("MM/yy"));

        // 2. Faker for PaypalPayment
        var ppFaker = new Faker<PaypalPayment>()
            .RuleFor(p => p.Amount, f => f.Finance.Amount(5, 500))
            .RuleFor(p => p.ProcessedAt, f => f.Date.Recent(30))
            .RuleFor(p => p.PaypalTransactionId, f => f.Random.AlphaNumeric(10).ToUpper())
            .RuleFor(p => p.PayerEmail, f => f.Internet.Email());

        // 3. Faker for BankTransferPayment + its legs
        var legFaker = new Faker<BankTransferLeg>()
            .RuleFor(l => l.Beneficiary, f => f.Company.CompanyName())
            .RuleFor(l => l.Amount, f => Math.Round(f.Finance.Amount(20, 2_000), 2));

        var btFaker = new Faker<BankTransferPayment>()
            .RuleFor(p => p.Amount, f => f.Finance.Amount(100, 5_000))
            .RuleFor(p => p.ProcessedAt, f => f.Date.Recent(30))
            .RuleFor(p => p.BankAccount, f => f.Finance.Account())
            .RuleFor(p => p.RoutingNumber, f => f.Random.ReplaceNumbers("#########"))
            .FinishWith((f, p) =>
            {
                // generate 1–4 legs whose amounts sum up to p.Amount
                var legs = legFaker.Generate(f.Random.Int(1, 4));
                // optionally adjust leg amounts so total == p.Amount
                var total = legs.Sum(l => l.Amount);
                legs.ForEach(l => l.Amount = Math.Round(l.Amount / total * p.Amount, 2));
                p.TransferLegs = legs;
            });

        // 4. Generate and add to context
        var ccPayments = ccFaker.Generate(countPerType);
        var ppPayments = ppFaker.Generate(countPerType);
        var btPayments = btFaker.Generate(countPerType);

        context.AddRange(ccPayments);
        context.AddRange(ppPayments);
        context.AddRange(btPayments);
        // EF will cascade-add the TransferLegs thanks to the nav-prop
        context.SaveChanges();
    }
}

