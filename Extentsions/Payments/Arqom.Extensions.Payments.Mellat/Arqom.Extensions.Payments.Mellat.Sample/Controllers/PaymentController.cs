using Arqom.Extensions.Payment.Abstractions;
using Arqom.Extensions.Payments.Mellat.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arqom.Extensions.Payments.Mellat.Sample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentProvider _provider;

    public PaymentController(IPaymentProvider provider)
    {
        _provider = provider;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)
    {
        var setting = new PaymentGatewaySetting
        {
            MerchantId = "123",
            ApiKey = "123",
            SecretKey = "YourPassword"
        };

        var result = await _provider.CreatePaymentAsync(request, setting);
        return Ok(result);
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string SaleOrderId, [FromQuery] string SaleReferenceId, [FromQuery] string ResCode)
    {
        // اگر ResCode == "0" یعنی موفق
        if (ResCode == "0")
        {
            var setting = new PaymentGatewaySetting
            {
                MerchantId = "YourTerminalId",
                ApiKey = "YourUsername",
                SecretKey = "YourPassword"
            };

            var verifyResult = await _provider.VerifyPaymentAsync(new PaymentVerifyRequest
            {
                ReferenceId = SaleOrderId
            }, setting);

            return Ok(verifyResult);
        }
        return BadRequest("پرداخت ناموفق");
    }
}
