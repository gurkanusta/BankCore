using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Application.Identity;


namespace BankCore.Application.Abstractions;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user, IList<string> roles);
}
