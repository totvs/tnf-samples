using Tnf.Dto;

namespace Tnf.Architecture.Dto.Paging
{
    public class PagingRequestDto : RequestDtoBase
    {
        public PagingRequestDto()
        {
        }

        public PagingRequestDto(int offset = 0, int pageSize = 10)
        {
            Offset = offset;
            PageSize = pageSize;
        }

        public int Offset { get; set; }
    }
}
