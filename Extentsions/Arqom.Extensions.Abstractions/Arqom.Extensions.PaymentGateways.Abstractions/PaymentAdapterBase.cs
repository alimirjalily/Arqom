using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Arqom.Extensions.PaymentGateways.Abstractions;

public abstract class PaymentAdapterBase<TOptions> : IPaymentGatewayAdapter
    where TOptions : class, new()
{
    protected readonly TOptions Options;
    protected readonly ILogger Logger;

    public abstract string Name { get; }

    protected PaymentAdapterBase(IOptions<TOptions> options, ILogger logger)
    {
        Options = options.Value;
        Logger = logger;
    }

    public async Task<PaymentResult> CreatePaymentAsync(PaymentCreateRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            Logger.LogInformation("[{Gateway}] Starting payment creation for Order {OrderId}", Name, request.OrderId);
            ValidateRequest(request);

            var result = await OnCreatePaymentAsync(request, cancellationToken);

            Logger.LogInformation("[{Gateway}] Payment creation result: {Result}", Name, result.IsSuccess);
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "[{Gateway}] Error while creating payment", Name);
            return new PaymentResult
            {
                IsSuccess = false,
                ErrorKey = ex.Message
            };
        }
    }

    public async Task<PaymentVerificationResult> VerifyPaymentAsync(PaymentVerifyRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            Logger.LogInformation("[{Gateway}] Verifying payment {TrackingCode}", Name, request.TrackingCode);

            var result = await OnVerifyPaymentAsync(request, cancellationToken);

            Logger.LogInformation("[{Gateway}] Verification result: {Result}", Name, result.IsSuccess);
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "[{Gateway}] Error while verifying payment", Name);
            return new PaymentVerificationResult
            {
                IsSuccess = false,
                ErrorKey = ex.Message
            };
        }
    }

    // برای اعتبارسنجی ساده
    protected virtual void ValidateRequest(PaymentCreateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.OrderId))
            throw new ArgumentException("OrderId is required.");

        if (request.Amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        if (string.IsNullOrWhiteSpace(request.CallbackUrl))
            throw new ArgumentException("CallbackUrl is required.");
    }

    // متدهایی که هر درگاه باید خودش پیاده کند
    protected abstract Task<PaymentResult> OnCreatePaymentAsync(PaymentCreateRequest request, CancellationToken cancellationToken);
    protected abstract Task<PaymentVerificationResult> OnVerifyPaymentAsync(PaymentVerifyRequest request, CancellationToken cancellationToken);

    public Task<PaymentRefundResult> RefundPaymentAsync(PaymentRefundRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentStatusResult> GetPaymentStatusAsync(string transactionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

