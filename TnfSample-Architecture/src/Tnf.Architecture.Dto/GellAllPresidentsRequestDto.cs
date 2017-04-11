using Tnf.Dto;

namespace Tnf.Architecture.Dto
{
    public class GellAllPresidentsRequestDto : DtoRequestBase
    {
        public GellAllPresidentsRequestDto(int offset = 0, int pageSize = 10, string name = "", string zipCode = "")
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
