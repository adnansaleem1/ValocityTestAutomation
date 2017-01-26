using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocityTestCases.Entity
{
    class Size
    {
        public string SizeType { get; set; }
        public string Parent_Container_Xapth { get; set; }
        public IList<string> SelectionList;

        public Size(string p1, string p2, List<string> list)
        {
            // TODO: Complete member initialization
            this.SizeType = p1;
            this.Parent_Container_Xapth = p2;
            this.SelectionList = list;
        }
    }
}
