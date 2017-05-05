namespace Tnf.Architecture.Dto.Paging
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
