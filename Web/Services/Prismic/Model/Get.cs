using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services.Prismic.Model
{
    public class Get
    {
        public GetRef[] refs { get; set; }
    }

    public class GetRef
    {
        public string @ref { get; set; }
    }
}
