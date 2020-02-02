using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTOne.Services
{
    public interface IUser
    {
        string GenerateToken();

    }
}
