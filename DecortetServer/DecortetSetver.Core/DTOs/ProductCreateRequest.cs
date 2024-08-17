using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DecortetServer.Core.DTOs
{
    public record ProductCreateRequest(
        string Name,
        decimal Price ,
        string? Underheader ,
        string? Description ,
        ICollection<IFormFile> Photos,
        bool Available  = true
    );
}
