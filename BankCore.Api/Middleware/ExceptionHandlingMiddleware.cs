using System.Text.Json;
using BankCore.Application.Exceptions;
using BankCore.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BankCore.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next; //sıradaki middleareleri getirir
    private readonly ILogger<ExceptionHandlingMiddleware> _logger; //middleware mı değil mi bakması için

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;

        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); //sıradaki middleware ı çalıştır
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title) = exception switch //istisnanın türüne göre bir int,string üretiliyor
        {
            ValidationException => (StatusCodes.Status400BadRequest, exception.Message),
            InsufficientBalanceException => (StatusCodes.Status400BadRequest, exception.Message),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, exception.Message),
            ForbiddenAccessException => (StatusCodes.Status403Forbidden, exception.Message),
            InvalidOperationException => (StatusCodes.Status404NotFound, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "Beklenmeyen bir hata oluştu.")//hatanın ne olduğu bilinmediği için sabit bir string
        };

        if (statusCode == StatusCodes.Status500InternalServerError) //beklenmeyen 500 hataları için ayrı düz 400 ler hataları için ayrı 
        {
            _logger.LogError(exception, "Beklenmeyen bir hata oluştu. Path: {Path}", context.Request.Path);
        }
        else
        {
            _logger.LogWarning("İşlenen istisna: {ExceptionType} - {Message}", exception.GetType().Name, exception.Message);
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = $"https://httpstatuses.com/{statusCode}",
            Instance = context.Request.Path  //hatanın hangş endpointte olduğunu söyler
        };

        context.Response.ContentType = "application/problem+json";//yanıtın içerik türünü ve http durum kodunu elle ayarlama htppcontext i manipüle etme
        context.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(problemDetails);//problemdetailsi jsona çevirme ve doğrudan yanıt gövdesine yazma
        await context.Response.WriteAsync(json);
    }
}