using Arqom.Extensions.PaymentGateways.Abstractions;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Arqom.Extensions.PaymentGateways.Zarinpal;

public class ZarinpalPaymentAdapter : IPaymentGatewayAdapter
{
    private readonly HttpClient _httpClient;
    private readonly ZarinpalOptions _options;

    public ZarinpalPaymentAdapter(HttpClient httpClient, IOptions<ZarinpalOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public string Name => "Zarinpal";

    public async Task<PaymentResult> CreatePaymentAsync(PaymentCreateRequest request, CancellationToken cancellationToken = default)
    {
        var apiUrl = _options.IsSandbox
            ? "https://sandbox.zarinpal.com/pg/v4/payment/request.json"
            : "https://api.zarinpal.com/pg/v4/payment/request.json";

        var payload = new
        {
            merchant_id = _options.MerchantId,
            amount = (int)request.Amount,
            description = request.Description,
            callback_url = _options.CallBackUrl,
            metadata = new
            {
                email = request.PayerEmail,
                mobile = request.PayerPhone
            }
        };

        var response = await _httpClient.PostAsJsonAsync(apiUrl, payload, cancellationToken);
        var content = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: cancellationToken);

        // بررسی data
        if (content.TryGetProperty("data", out var data))
        {
            if (data.TryGetProperty("code", out var codeProp) && data.TryGetProperty("authority", out var authorityProp))
            {
                var code = codeProp.GetInt32();
                var authority = authorityProp.GetString();
                var errorKey = GetErrorKey(code);


                if ((code == 100 || code == 101) && authority != null)
                {
                    string gatewayUrl = _options.IsSandbox
                        ? $"https://sandbox.zarinpal.com/pg/StartPay/{authority}"
                        : $"https://www.zarinpal.com/pg/StartPay/{authority}";

                    return new PaymentResult
                    {
                        IsSuccess = true,
                        RedirectUrl = gatewayUrl,
                        TrackingCode = authority,
                        ErrorKey = errorKey
                    };
                }
                else
                {
                    return new PaymentResult
                    {
                        IsSuccess = false,
                        ErrorKey = errorKey
                    };
                }
            }
        }

        // اگر data وجود ندارد
        return new PaymentResult
        {
            IsSuccess = false,
            ErrorKey = "ZARINPAL_UnknownError"
        };
    }

    public Task<PaymentStatusResult> GetPaymentStatusAsync(string transactionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentRefundResult> RefundPaymentAsync(PaymentRefundRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<PaymentVerificationResult> VerifyPaymentAsync(PaymentVerifyRequest request, CancellationToken cancellationToken = default)
    {
        var apiUrl = _options.IsSandbox
            ? "https://sandbox.zarinpal.com/pg/v4/payment/verify.json"
            : "https://api.zarinpal.com/pg/v4/payment/verify.json";

        var payload = new
        {
            merchant_id = _options.MerchantId,
            amount = (int)request.Amount,
            authority = request.TrackingCode
        };

        var response = await _httpClient.PostAsJsonAsync(apiUrl, payload, cancellationToken);
        var content = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: cancellationToken);

        if (content.TryGetProperty("errors", out var errors) &&
    !(errors.ValueKind == JsonValueKind.Array && errors.GetArrayLength() == 0))
        {
            int? errorCode = null;

            if (errors.ValueKind == JsonValueKind.Object && errors.TryGetProperty("code", out var codeProp))
                errorCode = codeProp.GetInt32();

            return new PaymentVerificationResult
            {
                IsSuccess = false,
                ReferenceId = null,
                ErrorKey = GetErrorKey(errorCode ?? -999)
            };
        }

        // بررسی data
        if (content.TryGetProperty("data", out var data))
        {
            var code = data.GetProperty("code").GetInt32();

            if (code == 100 || code == 101)
            {
                return new PaymentVerificationResult
                {
                    IsSuccess = true,
                    ReferenceId = data.GetProperty("ref_id").GetInt64().ToString(),
                    ErrorKey = "Paid"
                };
            }
            else
            {
                return new PaymentVerificationResult
                {
                    IsSuccess = false,
                    ErrorKey = GetErrorKey(code)
                };
            }
        }

        // اگر هیچ data یا errors وجود نداشت
        return new PaymentVerificationResult
        {
            IsSuccess = false,
            ErrorKey = "ZARINPAL_UnknownError"
        };
    }

    public string GetErrorKey(int code)
    {
        return code switch
        {
             -9 => "ValidationError",
             -10 => "TerminalNotValid",
             -11 => "TerminalNotActive",
             -12 => "TooManyAttempts",
             -13 => "TerminalLimitReached",
             -14 => "CallbackUrlMismatch",
             -15 => "TerminalSuspended",
             -16 => "TerminalLevelSilver",
             -17 => "TerminalLevelBlue",
             -18 => "ReferrerMismatch",
             -19 => "TransactionsBanned",
             100 => "Success",

             -30 => "FloatingWagesNotAllowed",
             -31 => "BankAccountMissing",
             -32 => "WagesExceedTotal",
             -33 => "InvalidWagesPercentage",
             -34 => "FixedWagesExceedTotal",
             -35 => "FloatingWagesLimitExceeded",
             -36 => "MinimumWagesAmount",
             -37 => "InactiveIban",
             -38 => "IbanNotSetInShaparak",
             -39 => "WagesError",
             -40 => "InvalidExtraParams",
             -41 => "MaximumAmountExceeded",

             -50 => "AmountMismatch",
             -51 => "PaymentFailed",
             -52 => "UnexpectedError",
             -53 => "WrongMerchantId",
             -54 => "InvalidAuthority",
             -55 => "TransactionNotFound",

             -60 => "CannotReverse",
             -61 => "NotSuccessfulOrAlreadyReversed",
             -62 => "IpNotSet",
             -63 => "ReverseTimeExpired",

             101 => "Verified",
             _ => "ZARINPAL_UnknownError"
        };
    }
}

