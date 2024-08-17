using DecortetServer.Core.Models;

namespace DecortetServer.Models
{
    public class ProductWithCount
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
