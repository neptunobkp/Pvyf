using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services.Prismic.Model
{
    public class Response
    {
        public ResponseResult[] results { get; set; }
    }
    public class ResponseResult
    {
        public ResponseData data { get; set; }
    }

    public class ResponseData
    {
        public ResponseDataItem[] titulo { get; set; }
        public ResponseDataItem[] texto_hero { get; set; }
        public ResponseDataItem[] footer { get; set; }
    }

    public class ResponseDataItem
    {
        public string text { get; set; }
    }
}
