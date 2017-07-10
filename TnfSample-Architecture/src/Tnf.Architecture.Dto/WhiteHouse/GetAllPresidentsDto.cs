using Tnf.App.Dto.Request;

namespace Tnf.Architecture.Dto.WhiteHouse
{
    public class GetAllPresidentsDto : RequestAllDto
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }
    }
}
