using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecortetServer.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? ClientName { get; set; }
        public string? Phone {  get; set; }
        public string? Email { get; set; }
        public string? Region { get; set; }
        public string? Town { get; set; }
        public string? Address { get; set; }
        public string? Desctiption { get; set; }
        public decimal totalSum { get; set; }

        public List<ProductOrder> ProductOrders { get; set; }



        public Order()
        {          
        }

        public Order(int id, string? clientName, string? phone, string? email, string? region, string? town, string? address, string? desctiption, decimal totalSum, List<ProductOrder> productOrders)
        {
            Id = id;
            ClientName = clientName;
            Phone = phone;
            Email = email;
            Region = region;
            Town = town;
            Address = address;
            Desctiption = desctiption;
            this.totalSum = totalSum;
            ProductOrders = productOrders;
        }
    }
}
