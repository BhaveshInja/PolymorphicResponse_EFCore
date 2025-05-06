using AutoMapper;
using Domain.DTO;
using Domain.Entities;

namespace PolymorphicResponse_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Base
            CreateMap<Payment, PaymentDto>()
                .Include<CreditCardPayment, CreditCardPaymentDto>()
                .Include<PaypalPayment, PaypalPaymentDto>()
                .Include<BankTransferPayment, BankTransferPaymentDto>()
                .ReverseMap();

            // Concrete payments
            CreateMap<CreditCardPayment, CreditCardPaymentDto>().ReverseMap();
            CreateMap<PaypalPayment, PaypalPaymentDto>().ReverseMap();
            CreateMap<BankTransferPayment, BankTransferPaymentDto>().ReverseMap();

            // Child entity
            CreateMap<BankTransferLeg, BankTransferLegDto>().ReverseMap();
        }
    }
}
