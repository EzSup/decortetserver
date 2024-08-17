using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DecortetServer.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Underheader { get; set; }
        public string? Description { get; set; }
        public string[] PhotoLinks { get; set; }
        public bool Available { get; set; } = true;

        public List<ProductOrder>? ProductOrders { get; set; }

        public Product(int Id, string Name, decimal Price) : this(Id, Name, Price, null, null, new string[0], false) { }

        [JsonConstructor]
        public Product(int Id, string Name, decimal Price, string? Underheader, string? Description, string[] PhotoLinks, bool Available, List<ProductOrder>? productOrders = null)
        {
            this.Id = Id;
            this.Name = Name;
            this.Price = Price;
            this.Underheader = Underheader;
            this.Description = Description;
            this.PhotoLinks = PhotoLinks;
            this.Available = Available;
            this.ProductOrders = productOrders;
        }
    }
}
