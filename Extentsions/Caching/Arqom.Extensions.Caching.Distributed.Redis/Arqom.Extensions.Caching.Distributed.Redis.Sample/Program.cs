using Arqom.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddArqomRedisDistributedCache(option =>
{
    option.Configuration = "localhost:9191,password=123456";
    option.InstanceName = "Arqom.Sample.";
});

builder.Services.AddArqomNewtonSoftSerializer();

//Middlewares

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
