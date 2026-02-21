namespace Arqom.Core.Domain.Exceptions;
/// <summary>
/// خطاهای لایه Domain مربوط به Entityها و ValueObjectها به کمک Extention برای لایه‌های بالاتر ارسال می‌شود
/// با توجه به اینکه هم Entity و هم ValueObject به یک شکل خطا را ارسال می‌کنند یک کلاس Exception طراحی و پیاده سازی شده است.
/// برای اینکه در لایه‌های بالاتر بتوان تفاوت خطای و محل رخداد آن را تشخیص داد از الگوی MicroType استفاده شده.
/// </summary>
public abstract class DomainStateException : Exception
{

    public IReadOnlyCollection<DomainError> Errors { get; }

    public DomainStateException(IEnumerable<DomainError> errors)
    {
        Errors = errors.ToList().AsReadOnly();
    }

    public DomainStateException(string code, params string?[] args)
    : this(new[] { new DomainError(code, args) })
    {
    }


}
