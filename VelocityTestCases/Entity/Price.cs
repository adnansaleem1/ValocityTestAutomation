using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocityTestCases.Entity
{
    class PriceFill
    {
        public PricegridType GridType { get; set; }
        public MultiplePriceConfigrationCritaria CritariaForMutiple { get; set; }
        private IList<PriceGridFill> PriceGridList = new List<PriceGridFill>();
        public PriceTypesForItem PriceType;

        public void SetPriceGrid(PriceGridFill PG) {
            if (this.GridType == PricegridType.SingleGrid && this.PriceGridList.Count == 1)
            {
                throw new Exception("Grid type is configured as Single Grid Multiple Grid not Allow...");
            }
            else {
                PriceGridList.Add(PG);
            
            }
        }


        internal IList<PriceGridFill> GetPriceGridList()
        {
            return PriceGridList;
        }
    }
}
