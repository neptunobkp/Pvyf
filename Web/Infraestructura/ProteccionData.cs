using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infraestructura
{
    public class ProteccionData
    {
        private readonly IDataProtector protector;
        public ProteccionData(IDataProtectionProvider dataProtectionProvider, UniqueCode uniqueCode)
        {
            protector = dataProtectionProvider.CreateProtector(uniqueCode.CodigoProteccion);
        }
        public string Decode(string data)
        {
            return protector.Protect(data);
        }
        public string Encode(string data)
        {
            return protector.Unprotect(data);
        }
    }

    public class UniqueCode
    {
        public readonly string CodigoProteccion = "recuperosbci";
    }
   
}
