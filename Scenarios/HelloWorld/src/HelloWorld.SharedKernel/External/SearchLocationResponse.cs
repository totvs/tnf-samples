using HelloWorld.SharedKernel.ValueObjects;

namespace HelloWorld.SharedKernel.External
{
    public class SearchLocationResponse
    {
        public ZipCode ZipCode { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
