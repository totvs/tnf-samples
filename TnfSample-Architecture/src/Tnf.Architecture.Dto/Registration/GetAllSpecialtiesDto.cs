using Tnf.Architecture.Dto.Paging;

namespace Tnf.Architecture.Dto.Registration
{
    public class GetAllSpecialtiesDto : PagingRequestDto
    {
        public GetAllSpecialtiesDto()
        {
        }

        public GetAllSpecialtiesDto(int offset, int pageSize, string description)
            : base(offset, pageSize)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}
