using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMaster.Data
{
    public class ProductTestRepository : IProductRepository
    {
        private Order order = new Order();
        private static List<Order> _product;

        public ProductTestRepository()
        {
            if (_product == null)
            {
                _product = new List<Order>
                {
                    new Order()
                    {
                        ProductType = "WOOD",
                        CostPerSquareFoot = 5.15m,
                        LaborCostPerSquareFoot = 4.75m,
                    },
                    new Order()
                    {
                        ProductType = "LAMINATE",
                        CostPerSquareFoot = 1.75m,
                        LaborCostPerSquareFoot = 2.10m,
                    },
                    new Order()
                    {
                        ProductType = "CARPET",
                        CostPerSquareFoot = 2.25m,
                        LaborCostPerSquareFoot = 2.10m,
                    },
                    new Order()
                    {
                        ProductType = "TILE",
                        CostPerSquareFoot = 3.50m,
                        LaborCostPerSquareFoot = 4.15m
                    }
                };
            }
        }

        Order IProductRepository.GetProductInfo(string productInformation)
        {
            return _product.FirstOrDefault(o => o.ProductType == productInformation);
        }
    }
}
