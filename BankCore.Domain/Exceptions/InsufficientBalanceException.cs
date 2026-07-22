using System;
using System.Collections.Generic;
using System.Text;

namespace BankCore.Domain.Exceptions;

public class InsufficientBalanceException: Exception //c# ın kendi exceptionunu türet
{
    public InsufficientBalanceException(string message) : base(message) //doğrudan üst sınıfa gider ve .message oto dolar.
    {

    }
}



