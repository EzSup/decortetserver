using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecortetServer.Core.DTOs
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string? ClientName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Region { get; set; }
        public string? Town { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public decimal TotalSum { get; set; }
        public IEnumerable<string>? ProductsList { get; set; }
    }
}
