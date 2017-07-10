using Tnf.App.Dto.Request;

namespace Tnf.Architecture.Dto.Registration
{
    public class GetAllProfessionalsDto : RequestAllDto
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }
    }
}
