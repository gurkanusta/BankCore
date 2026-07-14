using BankCore.Infrastructure;
using FluentValidation;
using BankCore.Infrastructure;
using Mapster;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

var mapsterConfig = TypeAdapterConfig.GlobalSettings;
mapsterConfig.Scan(typeof(BankCore.Application.Mappings.MappingConfig).Assembly);
builder.Services.AddSingleton(mapsterConfig);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(BankCore.Application.Features.Customers.Commands.CreateCustomer.CreateCustomerCommand).Assembly);
    cfg.AddOpenBehavior(typeof(BankCore.Application.Behaviors.ValidationBehavior<,>));
});



builder.Services.AddValidatorsFromAssembly(
    typeof(BankCore.Application.Features.Customers.Commands.CreateCustomer.CreateCustomerCommand).Assembly);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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