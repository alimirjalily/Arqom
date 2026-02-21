using Arqom.Core.Domain.Repositories;
using Arqom.Infra.Data.Sql.Commands;

namespace Arqom.Extensions.DependencyInjection;
/// <summary>
/// توابع کمکی جهت ثبت نیازمندی‌های لایه داده
/// </summary>
public static class AddDataAccessExtentsions
{
    public static IServiceCollection AddArqomDataAccess(
        this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch) =>
        services.AddRepositories(assembliesForSearch);

    public static IServiceCollection AddRepositories(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch) =>
        services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandRepository<,>), typeof(IQueryRepository));

}
