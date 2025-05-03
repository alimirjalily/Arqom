using Arqom.Extensions.DependencyInjection;
using Arqom.Utilities.SerilogRegistration.Extensions;
using Arqom.Utilities.SerilogRegistration.Sample;
using Arqom.Utilities.SerilogRegistration.Sample.SampleEnrichers;
SerilogExtensions.RunWithSerilogExceptionHandling(() =>
{
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.AddArqomSerilog(c =>
    {
        c.ApplicationName = "SerilogRegistration";
        c.ServiceName = "SampleService";
        c.ServiceVersion = "1.0";
        c.ServiceId= Guid.NewGuid().ToString();
    },typeof(Sample01Enricher),typeof(Sample02Enricher)).ConfigureServices().ConfigurePipeline();
    app.Run();
});
