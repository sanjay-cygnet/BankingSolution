using BuildingBlocks.Shared.Middleware;
using Customer.Api.Extensions;

#region Service Config
var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddRouting(r => r.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddServices(configuration);

#endregion

#region App Services
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

#endregion
