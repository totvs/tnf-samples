using Tnf.Architecture.Dto.Paging;

namespace Tnf.Architecture.Dto.WhiteHouse
{
    public class GetAllPresidentsDto : PagingRequestDto
    {
        public GetAllPresidentsDto()
            :base()
        {
        }

        public GetAllPresidentsDto(int offset, int pageSize)
            : base(offset, pageSize)
        {
        }

        public GetAllPresidentsDto(int offset, int pageSize, string name, string zipCode)
            : base(offset, pageSize)
        {
            Name = name;
            ZipCode = zipCode;
        }

        public string Name { get; set; }
        public string ZipCode { get; set; }
    }
}
