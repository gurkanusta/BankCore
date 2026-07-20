using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Application.Abstractions;
using BankCore.Domain.Constants;
using Microsoft.AspNetCore.Http;


namespace BankCore.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;


    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid CustomerId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User?.FindFirst(c => c.Type == "customerId")?.Value;
            return Guid.TryParse(value, out var customerId) ? customerId : Guid.Empty;
        }
    }

    public bool IsAdmin =>
        _httpContextAccessor.HttpContext?.User.IsInRole(RoleNames.Admin) ?? false;
}
