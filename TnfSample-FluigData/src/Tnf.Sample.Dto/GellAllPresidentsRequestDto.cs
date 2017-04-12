using Tnf.Dto;

namespace Tnf.Sample.Dto
{
    public class GellAllPresidentsRequestDto : DtoRequestBase
    {
        public GellAllPresidentsRequestDto(int offset, int pageSize, string name, string zipCode)
        {
            Offset = offset;
            PageSize = pageSize;
            Name = name;
            ZipCode = zipCode;
        }

        public int Offset { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
    }
}
