using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValocityTestCases.Entity

{
    class PriceGridFill
    {
        public IList<Itemsprice> PricesList;
        public IList<PriceGridCritaria> CritariaList;

        public string PriceGridName { get; set; }
       // public IList<Itemsprice> PriceListByNumberOfItems { get; set; }
        public int GridIndex { get; set; }
        //public IList<PriceGridCritaria> CritariaForgridList { get; set; }


        public PriceGridFill(string Name, int Index, IList<Itemsprice> PricesList, IList<PriceGridCritaria> CritariaList)
        {
           // PriceListByNumberOfItems = new List<Itemsprice>();
           // CritariaForgridList = new List<PriceGridCritaria>();
            // TODO: Complete member initialization
            this.PriceGridName = Name;
            this.GridIndex = Index;
            this.PricesList = PricesList;
            this.CritariaList = CritariaList;
        }
    }
}
