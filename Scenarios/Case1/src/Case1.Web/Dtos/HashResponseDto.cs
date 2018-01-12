using Tnf.Dto;

namespace Case1.Web.Dtos
{
    public class HashResponseDto : DtoBase
    {
        public HashResponseDto()
        {
        }

        public HashResponseDto(string result)
        {
            Result = result;
        }

        public string Result { get; set; }
    }
}
