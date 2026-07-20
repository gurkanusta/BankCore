using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Application.Exceptions;

namespace BankCore.Application.Exceptions;

public class ForbiddenAccessException: Exception
{
    public ForbiddenAccessException(string message): base(message) { }
}
