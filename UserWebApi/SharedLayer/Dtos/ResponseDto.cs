using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserWebApi.SharedLayer.Dtos
{
    public class ResponseDto
    {
        public string Token { get; set; }

        public bool SuccessStatus { get; set; }
    }
}
