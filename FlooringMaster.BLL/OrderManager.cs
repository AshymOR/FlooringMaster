using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.OrderLookupResponse;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMaster.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IStateTaxRepository _stateTaxRespository;

        public OrderManager(IOrderRepository orderRepository, IStateTaxRepository stateTaxRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _stateTaxRespository = stateTaxRepository;
        }

        public OrderLookupResponse LookupOrder(DateTime orderdate)
        {
            OrderLookupResponse response = new OrderLookupResponse();
            var order = _orderRepository.LoadOrdersByDate(orderdate);
            int orderCount = 1;
            if (order != null)
            {
                 orderCount = order.Count;
            }

            if (orderCount == 0 || order == null)
            {
                response.Success = false;
                response.Message = $"{orderdate} is not a valid date.";
            }
            else
            {
                response.Success = true;
                response.OrderList = order;
                
            }
            return response;
        }

        public StateTaxLookupResponse LookupState(string stateInput)
        {
            StateTaxLookupResponse response = new StateTaxLookupResponse();
            var state = _stateTaxRespository.GetStateTax(stateInput);

            if (state == null)
            {
                response.Success = false;
                response.Message = "State not found";
            }
            else
            {
                response.Success = true;
                response.StateTaxInfo = state;
            }
            return response;


        }

        public ProductLookupResponse LookupProduct(string productInput)
        {
            ProductLookupResponse response = new ProductLookupResponse();
            Order product = _productRepository.GetProductInfo(productInput);

            if(product == null)
            {
                response.Success = false;
                response.Message = "Product not found";
            }
            else
            {
                response.Success = true;
                response.ProductInfo = product;
            }
            return response;
        }

        public OrderLookupResponse AddOrder(Order order)
        {
            OrderLookupResponse response = new OrderLookupResponse();
            var repo = _orderRepository;

            if (repo.AddOrder(order))
            {
                response.Success = true;
                response.Order = order;
            }
            else
            {
                response.Success = false;
                response.Message = "An error has occurred. The order was not added.";
            }
            return response;
        }

        public OrderLookupResponse EditOrder(Order order, Order editOrder,DateTime orderDate, int orderNumber)
        {
            OrderLookupResponse response = new OrderLookupResponse();
            var repo = _orderRepository;

            if (repo.EditOrder(order, editOrder, orderDate, orderNumber))
            {
                response.Success = true;
                response.Order = order;
            }
            else
            {
                response.Success = false;
                response.Message = "An error has occurred. The order was not edited";
            }
            return response;
        }

        public OrderLookupResponse GetOrderByONumber(DateTime orderDate, int order)
        {
            OrderLookupResponse response = new OrderLookupResponse();

            var orders = _orderRepository.LoadOrderByONumber(orderDate, order);

            if (orders != null)
            {
                response.Success = true;
                response.Order = orders;
            }
            else
            {
                response.Success = false;
                response.Message = "An error has occured.";
            }
            return response;
        }

        public OrderLookupResponse RemoveOrder(Order order, DateTime orderDate)
        {
            OrderLookupResponse response = new OrderLookupResponse();
            var repo = _orderRepository;

            if (repo.RemoveOrder(order,orderDate))
            {
                response.Success = true;
                response.Order = order;
            }
            else
            {
                response.Success = false;
                response.Message = "An error has occured. The order was not removed";
            }
            return response;
        }

        public OrderLookupResponse LookupAddOrder(DateTime orderdate)
        {
            OrderLookupResponse response = new OrderLookupResponse();
            var order = _orderRepository.LoadOrdersByDate(orderdate);
            while (true)
            {
                if (order == null)
                {
                    response.Success = false;
                    response.Message = $"{orderdate} is not a valid date.";
                }
                else
                {
                    response.Success = true;
                    response.OrderList = order;

                }
                return response;
            }

        }


        public static int AssignOrderNumber(OrderLookupResponse getOrders)
        {
            int count = 1000;
            Order newOrder = new Order();
            if (getOrders.OrderList != null)
            {
                count = getOrders.OrderList.Count;
            }


            if (count == 0 || getOrders.OrderList == null)
            {
                newOrder.OrderNumber = 1;
            }
            else
            {
                newOrder.OrderNumber = getOrders.OrderList.Max(o => o.OrderNumber);
                newOrder.OrderNumber += 1;
            }

            return newOrder.OrderNumber;
        }
    }
}
