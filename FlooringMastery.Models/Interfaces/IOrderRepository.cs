using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        Order LoadOrderByONumber(DateTime orderDate, int orderNumber);
        List<Order> LoadOrdersByDate(DateTime orderDate);
        bool AddOrder(Order order);
        bool RemoveOrder(Order order, DateTime orderDate);
        bool EditOrder(Order order, Order editOrder, DateTime orderDate, int orderNumber);


    }
}
