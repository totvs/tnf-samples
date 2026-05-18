using Tnf.Dto;

namespace TnfZero.Domain.Dtos;

/// <summary>Response DTO for Dog queries.</summary>
public record DogDto(Guid Id, string Name);

/// <summary>Paginated list request for Dog. Extends RequestAllDto for Page/PageSize/Order/Fields support.</summary>
public class DogRequestAllDto : RequestAllDto
{
    public string? Search { get; set; }
}