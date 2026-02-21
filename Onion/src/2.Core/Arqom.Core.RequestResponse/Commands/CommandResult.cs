using Arqom.Core.RequestResponse.Common;

namespace Arqom.Core.RequestResponse.Commands;
/// <summary>
/// نتیجه انجام هر عملیات به کمک این کلاس بازگشت داده می‌شود.
/// دلایل استفاده از این الگو و پیاده سازی کاملی از این الگو را در لینک زیر می‌توانید مشاهده کنید
/// https://github.com/vkhorikov/CqrsInPractice
/// </summary>
public class CommandResult : ApplicationServiceResult
{

    public static CommandResult ValidationFailed()
    {
        return new CommandResult
        {
            Status = ApplicationServiceStatus.ValidationError,
        };
    }

}
/// <summary>
/// نتیجه انجام هر عملیات به کمک این کلاس بازگشت داده می‌شود.
/// دلایل استفاده از این الگو و پیاده سازی کاملی از این الگو را در لینک زیر می‌توانید مشاهده کنید
/// این ساختار در صورتی استفاده میشود که برای عملیات مقدار خروجی نیاز باشد
/// https://github.com/vkhorikov/CqrsInPractice
/// </summary>
/// <typeparam name="TData">نوع داده‌ای که بازگشت داده می‌شود</typeparam>
public class CommandResult<TData> : CommandResult
{
    public TData? Data { get; private set; }

    private CommandResult() { 

    }

    private CommandResult(TData data)
    {
        Data = data;
    }

    public static CommandResult<TData> From(CommandResult source)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        var result = new CommandResult<TData>
        {
            Status = source.Status
        };

        result.AddMessages(source.Messages);

        return result;
    }

    public static CommandResult<TData> Ok(TData data)
    {
        var result = new CommandResult<TData>
        {
            Status = ApplicationServiceStatus.Ok,
            Data = data
        };

        return result;
    }

    public static CommandResult<TData> Result(TData data, ApplicationServiceStatus status)
    {
        var result = new CommandResult<TData>
        {
            Status = status,
            Data = data
        };

        return result;
    }

    public static CommandResult<TData> InvalidDomainState()
    {
        var result = new CommandResult<TData>
        {
            Status = ApplicationServiceStatus.InvalidDomainState
        };

        return result;
    }


}

