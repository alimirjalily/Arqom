using Arqom.Extensions.Payment.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Arqom.Extensions.Payments.Zarinpal.Sample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentProvider _paymentProvider;

    public PaymentController(IPaymentProvider paymentProvider)
    {
        _paymentProvider = paymentProvider;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)
    {
        // setting رو از دیتابیس یا ثابت برای تست میگیری
        var setting = new PaymentGatewaySetting
        {
            ProviderName = "ZARINPAL",
            MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
            ApiKey = "YOUR_API_KEY",
            SecretKey = "YOUR_SECRET_KEY",
            CallbackBaseUrl = request.CallbackUrl,
            CallbackParamName = "referenceId"
        };

        var response = await _paymentProvider.CreatePaymentAsync(request, setting);

        return Ok(response);
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string referenceId, [FromQuery] string authority, [FromQuery] string status)
    {
        // setting رو از دیتابیس یا ثابت برای تست میگیری
        var setting = new PaymentGatewaySetting
        {
            ProviderName = "ZARINPAL",
            MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
            ApiKey = "YOUR_API_KEY",
            SecretKey = "YOUR_SECRET_KEY",
            CallbackBaseUrl = "https://localhost:7153/swagger/index.html",
            CallbackParamName = "referenceId"
        };

        var verifyRequest = new PaymentVerifyRequest
        {
            ReferenceId = referenceId,
            TransactionId = authority

        };

        var response = await _paymentProvider.VerifyPaymentAsync(verifyRequest, setting);


        return Ok(response);
    }

}
