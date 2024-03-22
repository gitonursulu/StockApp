using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.ValueObjects
{
    public record OrderAddress
    {
        public string City { get; set; }
        public string PostCode { get; set; }
    }
}
