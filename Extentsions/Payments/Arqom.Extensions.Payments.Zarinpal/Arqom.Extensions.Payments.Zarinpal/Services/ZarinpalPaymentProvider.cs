using Arqom.Extensions.Payment.Abstractions;
using Arqom.Extensions.Payments.Zarinpal.Dtos;
using System.Net.Http;
using System.Net.Http.Json;

namespace Arqom.Extensions.Payments.Zarinpal.Services;

public class ZarinpalPaymentProvider : IPaymentProvider
{
    public string Name => "ZARINPAL";

    private readonly HttpClient _httpClient;

    public ZarinpalPaymentProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaymentResponse> CreatePaymentAsync(PaymentRequest request,PaymentGatewaySetting setting, CancellationToken cancellationToken = default)
    {
        using var http = new HttpClient();

        var payload = new
        {
            merchant_id = setting.MerchantId,
            amount = request.Amount,
            callback_url = $"{setting.CallbackBaseUrl}?{setting.CallbackParamName}={request.ReferenceId}",
            description = request.Description,
            metadata = new { email = request}
        };

        var response = await http.PostAsJsonAsync("https://api.zarinpal.com/pg/v4/payment/request.json", payload, cancellationToken);
        var content = await response.Content.ReadFromJsonAsync<ZarinpalResponse>(cancellationToken: cancellationToken);

        if (content?.Data?.Code == 100)
        {
            return new PaymentResponse
            {
                Success = true,
                PaymentUrl = "https://api.zarinpal.com/pg/StartPay/" + content.Data.Authority,
                TransactionId = content.Data.RefID.ToString()
            };
        }

        return new PaymentResponse
        {
            Success = false,
            Message = content?.Data?.Message ?? ""
        };
    }

    public async Task<PaymentStatusResponse> GetPaymentStatusAsync(string transactionId, PaymentGatewaySetting setting, CancellationToken cancellationToken = default)
    {
        var verifyRequest = new PaymentVerifyRequest
        {
            TransactionId = transactionId,
            ReferenceId = Guid.Empty.ToString()
        };

        var verify = await VerifyPaymentAsync(verifyRequest, setting, cancellationToken);

        return new PaymentStatusResponse
        {
            Status = verify.Success ? PaymentStatus.Success : PaymentStatus.Failed,
            TrackingCode = verify.TrackingCode,
            Message = verify.Message
        };
    }

    public Task<PaymentRefundResponse> RefundPaymentAsync(PaymentRefundRequest request, PaymentGatewaySetting setting, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<PaymentVerifyResponse> VerifyPaymentAsync(PaymentVerifyRequest request, PaymentGatewaySetting setting, CancellationToken cancellationToken = default)
    {
        if (setting == null)
            throw new ArgumentNullException(nameof(setting));

        var payload = new
        {
            merchant_id = setting.MerchantId,
            amount=request.Amount,
            authority = request.TransactionId
        };

        var response = await _httpClient.PostAsJsonAsync(
            "https://api.zarinpal.com/pg/v4/payment/verify.json",
            payload,
            cancellationToken);

        var content = await response.Content.ReadFromJsonAsync<ZarinpalResponse>(cancellationToken: cancellationToken);

        if (content?.Data?.Code == 100)
        {
            return new PaymentVerifyResponse
            {
                Success = true,
                TrackingCode = content.Data.RefID.ToString()
            };
        }

        return new PaymentVerifyResponse
        {
            Success = false,
            TrackingCode = null,
            Message = content?.Data?.Message ?? "Verification failed"
        };
}
}


