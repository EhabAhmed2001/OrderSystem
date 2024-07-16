using OrderSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Service
{
    public interface ITokenService
    {
        public string CreateTokenAsync(User user);

    }
}
