namespace DecortetServer.Models
{
    public record BuyRequest(IEnumerable<ProductWithCount> ProductWithCounts, 
        string Name, 
        string Phone, 
        string? Email,
        string? Region,
        string? City,
        string? Street,
        string? StreetNum,
        string? Description);
}
