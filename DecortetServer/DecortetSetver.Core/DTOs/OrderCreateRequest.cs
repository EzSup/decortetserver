using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecortetServer.Core.DTOs
{
    public record OrderCreateRequest(IEnumerable<ProductWithCount> ProductWithCounts,
        string Name,
        string Phone,
        string? Email,
        string? Region,
        string? City,
        string? Street,
        string? StreetNum,
        string? Description);
}
