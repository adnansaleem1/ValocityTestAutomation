using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValocityTestCases.Entity
{
    class MultiplePriceConfigrationCritaria
    {
        public string CritariaA { get; set; }
        public string CritariaB { get; set; }

        public MultiplePriceConfigrationCritaria(string p)
        {
            // TODO: Complete member initialization
            this.CritariaA = p;
        }

        public MultiplePriceConfigrationCritaria(string p1, string p2)
        {
            // TODO: Complete member initialization
            this.CritariaA = p1;
            this.CritariaB = p2;
        }

        public MultiplePriceConfigrationCritaria()
        {
            // TODO: Complete member initialization
        }
     
    }
}
