using Arqom.Extensions.Payment.Abstractions;
using ServiceReference;
using System.Data;
using System.Net.Http.Json;

namespace Arqom.Extensions.Payments.Mellat.Services;

public class MellatPaymentProvider : IPaymentProvider
{

    public string Name => "MELLAT";

    private readonly IPaymentGateway _paymentGateway;

    public MellatPaymentProvider(IPaymentGateway paymentGateway)
    {
        _paymentGateway = paymentGateway;
    }

    public async Task<PaymentResponse> CreatePaymentAsync(PaymentRequest request, PaymentGatewaySetting setting, CancellationToken cancellationToken = default)
    {
        var bpPayRequestBody = new bpPayRequestBody
        {
            terminalId = long.Parse(setting.MerchantId),
            userName = setting.ApiKey,
            userPassword = setting.SecretKey,
            localDate = DateTime.Now.ToString("yyyyMMdd"),
            localTime = DateTime.Now.ToString("HHmmss"),
            amount = long.Parse(request.Amount.ToString()),
            orderId = long.Parse(request.ReferenceId),
            callBackUrl = request.CallbackUrl,
            //mobileNo = paymentRequest.Mobile,
        };
        var result = await _paymentGateway.bpPayRequestAsync(new bpPayRequest(bpPayRequestBody));

        var response = result.Body.@return?.Split(",");

        //در صورتیکه پاسخی برگشت داده نشد
        if (response == null || !response.Any())
            return new PaymentResponse
            {
                Success = false,
                PaymentUrl = "",
                Message = "پاسخی از بانک دریافت نشد"
            };

        if (response.Length == 1)
        {
            return new PaymentResponse
            {
              Success = false,

                Message = ""
              
            };
        }

        //در صورت موفقیت آمیز بودن ثبت درخواست پرداخت در بانک ملت 
        return new PaymentResponse
        {
            TransactionId = response[1],
            Success = true,
            Message = "تراکنش با موفقیت انجام شد",
            PaymentUrl = $"https://bpm.shaparak.ir/pgwchannel/startpay.mellat?RefId={response[1]}"
        };
    }

    public async Task<PaymentStatusResponse> GetPaymentStatusAsync(string transactionId, PaymentGatewaySetting setting, CancellationToken cancellationToken = default)
    {
        using var client = new HttpClient();
        var request = new
        {
            terminalId = setting.MerchantId,
            userName = setting.ApiKey,
            userPassword = setting.SecretKey,
            orderId = transactionId,
            saleOrderId = transactionId,
            saleReferenceId = transactionId
        };

        var response = await client.PostAsJsonAsync("https://pgw.bpm.bankmellat.ir/pgwchannel/startpay.mellat/bpInquiryRequest", request, cancellationToken);
        var result = await response.Content.ReadAsStringAsync(cancellationToken);

        // ResCode = 0 یعنی موفق
        var parts = result.Split('|');
        if (parts[0] == "0")
        {
            return new PaymentStatusResponse
            {
                Status = PaymentStatus.Success,
                TransactionId= transactionId,

                //Amount = decimal.Parse(parts[2]) / 10, // برگشت به تومان
                Message = "پرداخت موفق"
            };
        }
        else
        {
            return new PaymentStatusResponse
            {
                Status = PaymentStatus.Failed,
                TransactionId = transactionId,
                Message = $"خطا در استعلام پرداخت: {parts[0]}"
            };
        }
    }

    public async Task<PaymentRefundResponse> RefundPaymentAsync(PaymentRefundRequest request, PaymentGatewaySetting setting, CancellationToken cancellationToken = default)
    {
        using var client = new HttpClient();
        var refundRequest = new
        {
            terminalId = setting.MerchantId,
            userName = setting.ApiKey,
            userPassword = setting.SecretKey,
            //orderId = request.,
            //saleOrderId = request.OrderId,
            saleReferenceId = request.TransactionId,
            refundAmount = (int)(request.Amount * 10) // تبدیل به ریال
        };

        var response = await client.PostAsJsonAsync("https://pgw.bpm.bankmellat.ir/pgwchannel/startpay.mellat/bpReversalRequest", refundRequest, cancellationToken);
        var result = await response.Content.ReadAsStringAsync(cancellationToken);

        var parts = result.Split('|');
        if (parts[0] == "0")
        {
            return new PaymentRefundResponse
            {
                Success = true,
                Message = "بازگشت وجه موفق",
                RefundId = request.TransactionId
            };
        }
        else
        {
            return new PaymentRefundResponse
            {
                Success = false,
                Message = $"خطا در بازگشت وجه: {parts[0]}",
                RefundId = request.TransactionId
            };
        }
    }

    public async Task<PaymentVerifyResponse> VerifyPaymentAsync(PaymentVerifyRequest request, PaymentGatewaySetting setting, CancellationToken cancellationToken = default)
    {
        var result = await _paymentGateway.bpVerifyRequestAsync(new bpVerifyRequest(new bpVerifyRequestBody
        {
            terminalId = long.Parse(setting.MerchantId),
            userName = setting.ApiKey,
            userPassword = setting.SecretKey,
            orderId = long.Parse(request.ReferenceId),
            saleOrderId = long.Parse(request.ReferenceId),
            saleReferenceId = long.Parse(request.ReferenceId),
        }));


        var response = result.Body.@return?.Split(",");

        //در صورتیکه پاسخی برگشت داده نشد
        if (response == null || !response.Any())
            return new PaymentVerifyResponse
            {
                Success = false,
                TrackingCode = string.Empty,
                Message = "پاسخی از بانک دریافت نشد"
                
            };

        var responseCode = response[0];

        var output = new PaymentVerifyResponse
        {
            Success = true,

           
            Message = "",
            TrackingCode = responseCode
        };

        //در صورت موفقیت آمیز بودن ثبت درخواست پرداخت در بانک ملت 
        return output;
    }
}
