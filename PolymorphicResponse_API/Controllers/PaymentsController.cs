using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace PolymorphicResponse_API.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController(IPaymentService paymentService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await paymentService.GetAllPayments();
            return Ok(payments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await paymentService.GetById(id);

            return Ok(payment);
        }
    }
}
