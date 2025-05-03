using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MiniBlog.Endpoints.API.CustomDecorators;
using MiniBlog.Endpoints.API.Extentions.DependencyInjection.Swaggers.Extentions;
using MiniBlog.Infra.Data.Sql.Commands.Common;
using MiniBlog.Infra.Data.Sql.Queries.Common;
using Serilog;
using Arqom.Core.ApplicationServices.Commands;
using Arqom.Core.ApplicationServices.Events;
using Arqom.Core.ApplicationServices.Queries;
using Arqom.EndPoints.Web.Extensions.ModelBinding;
using Arqom.Extensions.DependencyInjection;
using Arqom.Extensions.Events.Outbox.Dal.EF.Interceptors;
using Arqom.Infra.Data.Sql.Commands.Interceptors;

namespace MiniBlog.Endpoints.API.Extentions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        IConfiguration configuration = builder.Configuration;

        builder.Services.AddSingleton<CommandDispatcherDecorator, CustomCommandDecorator>();
        builder.Services.AddSingleton<QueryDispatcherDecorator, CustomQueryDecorator>();
        builder.Services.AddSingleton<EventDispatcherDecorator, CustomEventDecorator>();

        //Arqom
        builder.Services.AddArqomApiCore("Arqom", "ArqomTemplate");

        //microsoft
        builder.Services.AddEndpointsApiExplorer();

        //Arqom
        builder.Services.AddArqomWebUserInfoService(configuration, "WebUserInfo", true);

        //Arqom
        builder.Services.AddArqomParrotTranslator(configuration, "ParrotTranslator");

        //Arqom
        //builder.Services.AddSoftwarePartDetector(configuration, "SoftwarePart");

        //Arqom
        builder.Services.AddNonValidatingValidator();

        //Arqom
        builder.Services.AddArqomMicrosoftSerializer();

        //Arqom
        builder.Services.AddArqomAutoMapperProfiles(configuration, "AutoMapper");

        //Arqom
        builder.Services.AddArqomInMemoryCaching();
        //builder.Services.AddArqomSqlDistributedCache(configuration, "SqlDistributedCache");

        //CommandDbContext
        builder.Services.AddDbContext<MiniblogCommandDbContext>(
            c => c.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
            .AddInterceptors(new SetPersianYeKeInterceptor(),
                             new AddAuditDataInterceptor()));

        //QueryDbContext
        builder.Services.AddDbContext<MiniblogQueryDbContext>(
            c => c.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString")));

        //PollingPublisher
        //builder.Services.AddArqomPollingPublisherDalSql(configuration, "PollingPublisherSqlStore");
        //builder.Services.AddArqomPollingPublisher(configuration, "PollingPublisher");

        //MessageInbox
        //builder.Services.AddArqomMessageInboxDalSql(configuration, "MessageInboxSqlStore");
        //builder.Services.AddArqomMessageInbox(configuration, "MessageInbox");

        //builder.Services.AddArqomRabbitMqMessageBus(configuration, "RabbitMq");

        //builder.Services.AddArqomTraceJeager(configuration, "OpenTeletmetry");

        //builder.Services.AddIdentityServer(configuration, "OAuth");

        builder.Services.AddSwagger(configuration, "Swagger");

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        //Arqom
        app.UseArqomApiExceptionHandler();

        //Serilog
        app.UseSerilogRequestLogging();

        app.UseSwaggerUI("Swagger");

        app.UseStatusCodePages();

        app.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });

        app.UseHttpsRedirection();

        //app.Services.ReceiveEventFromRabbitMqMessageBus(new KeyValuePair<string, string>("MiniBlog", "BlogCreated"));

        //var useIdentityServer = app.UseIdentityServer("OAuth");

        var controllerBuilder = app.MapControllers();

        //if (useIdentityServer)
        //    controllerBuilder.RequireAuthorization();

        //app.Services.GetService<SoftwarePartDetectorService>()?.Run();

        return app;
    }
}