using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class StateTaxLookupResponse :Response
    {
        public StateTax StateTaxInfo { get; set; }
    }
}
