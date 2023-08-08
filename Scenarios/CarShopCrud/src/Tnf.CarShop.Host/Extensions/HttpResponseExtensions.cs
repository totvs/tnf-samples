using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Tnf.CarShop.Host.Extensions;

internal static class HttpResponseExtensions
{
    public static void AddPaginationHeader(
        this HttpResponse response,
        int currentPage,
        int itemsPerPage,
        int totalItems)
    {
        var paginationHeader = new PaginationHeader(
            currentPage,
            itemsPerPage,
            totalItems,
            (int)Math.Ceiling(totalItems / (double)itemsPerPage));

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}

[ExcludeFromCodeCoverage]
internal sealed record PaginationHeader
{
    public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        TotalPages = totalPages;
    }

    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}
