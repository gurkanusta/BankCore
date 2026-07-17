using BankCore.Application.Abstractions;
using BankCore.Application.Identity;
using BankCore.Application.Settings;
using BankCore.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BankCore.Infrastructure.Persistence;
using BankCore.Infrastructure.Services;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Serilog;

Log.Logger=new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Uygulama başlatılıyor...");
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    

    var mapsterConfig = TypeAdapterConfig.GlobalSettings;
    mapsterConfig.Scan(typeof(BankCore.Application.Mappings.MappingConfig).Assembly);
    builder.Services.AddSingleton(mapsterConfig);
    builder.Services.AddScoped<IMapper, ServiceMapper>();

    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(BankCore.Application.Features.Customers.Commands.CreateCustomer.CreateCustomerCommand).Assembly);
        cfg.AddOpenBehavior(typeof(BankCore.Application.Behaviors.AuditLoggingBehavior<,>));
        cfg.AddOpenBehavior(typeof(BankCore.Application.Behaviors.ValidationBehavior<,>));
    });


    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<BankCoreDbContext>()
    .AddDefaultTokenProviders();




    var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()!;

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

    

    builder.Services.AddAuthorization();

    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
    builder.Services.AddScoped<ITokenService, TokenService>();

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


    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = { BankCore.Domain.Constants.RoleNames.Customer, BankCore.Domain.Constants.RoleNames.Admin };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Uygulama başlatılamadı!");
}
finally
{
    Log.CloseAndFlush();
}