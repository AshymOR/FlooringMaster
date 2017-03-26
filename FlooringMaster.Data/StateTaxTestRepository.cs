using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMaster.Data
{
    public class StateTaxTestRepository : IStateTaxRepository
    {
        private static List<StateTax> _taxes;
        public StateTaxTestRepository()
        {
            if (_taxes == null)
            {
                _taxes = new List<StateTax>
                {
                    new StateTax()
                    {
                        StateName = "Indiana",
                        StateAbbreviation = "IN",
                        TaxRate = 6.00m
                    },
                    new StateTax()
                    {
                        StateName = "Pennsylvania",
                        StateAbbreviation = "PA",
                        TaxRate = 6.75m
                    },
                    new StateTax()
                    {
                        StateName = "Michigan",
                        StateAbbreviation = "MI",
                        TaxRate = 5.75m
                    },
                    new StateTax()
                    {
                        StateName = "Ohio",
                        StateAbbreviation = "OH",
                        TaxRate = 6.25m
                    }
                };
            }
        }


        public StateTax GetStateTax(string stateTax)
        {
            return _taxes.FirstOrDefault(o => o.StateAbbreviation == stateTax);
        }
    }
}
