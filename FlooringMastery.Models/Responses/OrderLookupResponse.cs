using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.OrderLookupResponse
{
    public class OrderLookupResponse : Response
    {
        public Order Order { get; set; }
        public List<Order> OrderList { get; set; }
    }
}
