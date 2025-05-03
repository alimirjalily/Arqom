using Arqom.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddArqomScalar(builder.Configuration, "Scalar");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseArqomScalar();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
