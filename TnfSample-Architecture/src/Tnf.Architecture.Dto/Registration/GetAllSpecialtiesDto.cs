using Tnf.App.Dto.Request;

namespace Tnf.Architecture.Dto.Registration
{
    public class GetAllSpecialtiesDto : RequestAllDto
    {
        public GetAllSpecialtiesDto()
            : base()
        {
        }

        public string Description { get; set; }
    }
}
