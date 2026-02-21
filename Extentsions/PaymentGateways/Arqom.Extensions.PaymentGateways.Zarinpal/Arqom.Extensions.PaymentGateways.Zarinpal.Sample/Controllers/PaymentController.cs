using Arqom.Extensions.PaymentGateways.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arqom.Extensions.PaymentGateways.Zarinpal.Sample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentGatewayAdapter _paymentGateway;

    public PaymentController(IPaymentGatewayAdapter paymentGateway)
    {
        _paymentGateway = paymentGateway;
    }

    // ایجاد پرداخت
    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentCreateRequest request)
    {
        var result = await _paymentGateway.CreatePaymentAsync(request);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    // تایید پرداخت
    [HttpGet("verify")]
    public async Task<IActionResult> VerifyPayment([FromQuery] Dictionary<string , string> query)
    {
        if (query["Status"] != "OK")
        {
            return BadRequest("Payment was canceled or failed by user.");
        }

        var verifyRequest = new PaymentVerifyRequest
        {
            TrackingCode = query["Authority"],
            Amount = 100000, // همان مبلغ سفارش
        };

        var result = await _paymentGateway.VerifyPaymentAsync(verifyRequest);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}
