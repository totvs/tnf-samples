using Tnf.Dto.Request;

namespace Tnf.Architecture.Dto.WhiteHouse
{
    public class GetAllPresidentsDto : RequestAllDto
    {
        public GetAllPresidentsDto()
            :base()
        {
        }

        public string Name { get; set; }
        public string ZipCode { get; set; }
    }
}
