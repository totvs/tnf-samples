using Tnf.Architecture.Dto.Paging;

namespace Tnf.Architecture.Dto.Registration
{
    public class GetAllProfessionalsDto : PagingRequestDto
    {
        public GetAllProfessionalsDto()
        {
        }

        public GetAllProfessionalsDto(int offset, int pageSize, string name)
            : base(offset, pageSize)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
