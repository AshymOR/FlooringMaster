using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using System.IO;

namespace FlooringMaster.Data
{
    public class ProductFileRepository : IProductRepository
    {
        private string _filePath = @".\Products.txt";

        Order IProductRepository.GetProductInfo(string productInformation)
        {
            Order productType = new Order();

            using (StreamReader sr = new StreamReader(_filePath))
            {
                sr.ReadLine();

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(',');

                    if (productInformation.Trim().ToUpper() == splitLine[0].Trim().ToUpper())
                    {
                        productType.ProductType = splitLine[0];
                        productType.CostPerSquareFoot = decimal.Parse(splitLine[1]);
                        productType.LaborCostPerSquareFoot = decimal.Parse(splitLine[2]);

                        return productType;
                    }
                }
                return null;
            }
        }
    }
}
