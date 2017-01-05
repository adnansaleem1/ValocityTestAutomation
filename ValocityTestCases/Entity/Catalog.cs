using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValocityTestCases.Entity
{
    class Catalog
    {
        public string Name { get; set; }
        public string StartMonth { get; set; }
        public string EndMonth { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string Url { get; set; }
    }
    class Document {
        public string DocumentName { get; set; }
        public string DocumentLink { get; set; }
    }
}
