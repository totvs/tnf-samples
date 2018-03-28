using System.Collections.Generic;

namespace Messaging.Web2.Dto
{
    public class ResponseDto
    {
        public int? Total { get; set; }
        public List<string> Messages { get; set; }
    }
}
