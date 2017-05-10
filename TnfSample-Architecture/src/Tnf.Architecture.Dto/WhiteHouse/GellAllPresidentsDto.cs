using Tnf.Architecture.Dto.Paging;
using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Dto.WhiteHouse
{
    public class GellAllPresidentsDto : PagingRequestDto
    {
        public GellAllPresidentsDto()
        {
        }

        public GellAllPresidentsDto(int offset, int pageSize)
            : base(offset, pageSize)
        {
        }

        public GellAllPresidentsDto(int offset, int pageSize, string name, string zipCode)
            : base(offset, pageSize)
        {
            Name = name;
            ZipCode = zipCode;
        }

        public string Name { get; set; }
        public string ZipCode { get; set; }
    }
}
