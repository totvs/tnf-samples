using Tnf.Dto.Request;

namespace Tnf.Architecture.Dto.Registration
{
    public class GetAllProfessionalsDto : RequestAllDto
    {
        public GetAllProfessionalsDto()
            : base()
        {
        }

        public string Name { get; set; }
    }
}
