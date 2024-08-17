using DecortetServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecortetServer.Core.DTOs
{
    public class ProductWithCount
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
