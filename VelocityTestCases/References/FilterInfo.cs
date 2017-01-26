using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityTestCases.Entity;

namespace VelocityTestCases.References
{
    class FilterInfo
    {
        public static IEnumerable<string> SearchKeywordsList = new List<string>() { "chin up", "Fitness", "Home Fitness" };
        public static IEnumerable<string> DefaultFilterList = new List<string>() { "Standard", "Active", "With Images" };
        public static IEnumerable<MoreFilter> MoreFilterList = new List<MoreFilter>{new MoreFilter(){filter="E-commerce ready",heading="Delivery Options"},
                                                                                 new MoreFilter(){filter="Weak Score Products",heading="Score Range"}};

        public static string ProductName_SearchForNavigation = "chin up bar";
    }
}
