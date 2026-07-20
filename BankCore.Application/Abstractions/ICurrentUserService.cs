using System;
using System.Collections.Generic;
using System.Text;

namespace BankCore.Application.Abstractions;

public interface ICurrentUserService
{
    Guid CustomerId { get;  }
    bool IsAdmin { get; }
}
