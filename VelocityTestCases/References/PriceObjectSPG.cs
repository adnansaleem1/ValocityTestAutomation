using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityTestCases.Entity;

namespace VelocityTestCases.BL
{
    class PriceObjectSPG
    {
        public PriceFill ProductPrice { get; set; }
        public PriceObjectSPG()
        {
            this.InIt();
            this.SetGridType();
            if (ProductPrice.GridType == PricegridType.MultipleGrid)
            {
                this.SetConfigrationCritaria();
            }
            this.SetInfoForGrids();
        }



        private void SetConfigrationCritaria()
        {
            ProductPrice.CritariaForMutiple = new MultiplePriceConfigrationCritaria("Size");
            // ProductPrice.CritariaForMutiple = new MultiplePriceConfigrationCritaria("Size","Color");

        }

        private void InIt()
        {
            ProductPrice = new PriceFill();
        }

        private void SetGridType()
        {
            ProductPrice.GridType = PricegridType.SingleGrid;
            ProductPrice.PriceType = PriceTypesForItem.List_Price;
        }

        private void SetInfoForGrids()
        {
            IList<Itemsprice> PricesList = null;
            IList<PriceGridCritaria> CritariaList = null;
            PriceGridFill grid = null;
            PricesList = new List<Itemsprice>() {new Itemsprice{Order=1,Quantity=2,ListPrice=10,PriceCode="N 60%"},
                                                                   new Itemsprice{Order=1,Quantity=5,ListPrice=9,PriceCode="N 60%"} };

             //CritariaList = new List<PriceGridCritaria>(){new PriceGridCritaria(){CriatriaName="S",ParentElementXpath="//*[@id=\"mpConfigStep2\"]/div[2]/div/div/div"},
             //                                             new PriceGridCritaria(){CriatriaName="M",ParentElementXpath="//*[@id=\"mpConfigStep2\"]/div[2]/div/div/div"},};
             grid = new PriceGridFill("S,M,L",0,PricesList,CritariaList);
             this.ProductPrice.SetPriceGrid(grid);
            // PricesList = new List<Itemsprice>() {new Itemsprice{Order=1,Quantity=2,ListPrice=10,PriceCode="N 60%"},
            //                                                       new Itemsprice{Order=1,Quantity=5,ListPrice=9,PriceCode="N 60%"} };

            //CritariaList = new List<PriceGridCritaria>(){new PriceGridCritaria(){CriatriaName="L",ParentElementXpath="//*[@id=\"mpConfigStep2\"]/div[2]/div/div/div"}};
            // grid = new PriceGrid("L", 0, PricesList, CritariaList);
            // this.ProductPrice.SetPriceGrid(grid);

            
            //throw new NotImplementedException();

        }
    }
}
