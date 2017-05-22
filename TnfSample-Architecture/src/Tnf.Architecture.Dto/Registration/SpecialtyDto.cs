using Tnf.Dto.Response;

namespace Tnf.Architecture.Dto.Registration
{
    public class SpecialtyDto : SuccessResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
