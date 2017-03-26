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
    public class StateTaxFileRepository : IStateTaxRepository
    {
        private string _filepath = @".\Taxes.txt";

       public StateTax GetStateTax(string stateTax)
        {
            StateTax taxState = new StateTax();

            using (StreamReader sr = new StreamReader(_filepath))
            {
                sr.ReadLine();

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(',');

                    if (stateTax.Trim().ToUpper() == splitLine[0].Trim().ToUpper())
                    {
                        taxState.StateAbbreviation = splitLine[0];
                        taxState.StateName = splitLine[1];
                        taxState.TaxRate = decimal.Parse(splitLine[2]);

                        return taxState;
                    }

                }
                return null;
            }
        }

    }
}
