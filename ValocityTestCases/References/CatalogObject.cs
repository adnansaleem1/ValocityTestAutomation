using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValocityTestCases.Entity;

namespace ValocityTestCases.BL
{
    class CatalogObject
    {
        public static Catalog Newcatalog = new Catalog() { Name = "Organizer, bags" ,
                                                           StartMonth = "June",
                                          StartYear=2017,
                                          EndMonth="July",
                                          EndYear=2017,
                                          Url = "http://google.com/"
        };
        public static Document doc = new Document()
        {
            DocumentName = "abc",
            DocumentLink = "http://www.google.com.pk/"
        };
        public static string CatNameToUpdate = "CAT on 8 sept";
        public static Catalog Updatecatalog = new Catalog()
        {
            Name = "Organizer, bags",
            StartMonth = "June",
            StartYear = 2018,
            EndMonth = "July",
            EndYear = 2018,
            Url = "http://google.com/"
        };
    }
}
