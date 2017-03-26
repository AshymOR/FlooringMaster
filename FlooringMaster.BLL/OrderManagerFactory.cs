using FlooringMaster.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMaster.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {


            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "OrderTest":
                    return new OrderManager(new OrderTestRepository(), new StateTaxTestRepository(), new ProductTestRepository());
                case "OrderProd":
                    return new OrderManager(new OrderRepository(), new StateTaxFileRepository(), new ProductFileRepository());       
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
