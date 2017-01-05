using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Xml.Serialization;
using System.IO;

namespace ValocityTestCases.Utility
{
    class ExcelReader
    {
        private Dictionary<string, string> props = new Dictionary<string, string>();
        private String FileName = "";
        private string Range="";
        public ExcelReader(string FileName){
            this.FileName = FileName;
        }

        public ExcelReader(string filename, string range)
        {
            // TODO: Complete member initialization
            this.FileName = filename;
            this.Range = range;
        }

    
        public DataSet ReadExcelFile()
        {
            var filename = this.FileName;
            var connString = string.Format(
                @"Provider=Microsoft.Jet.OleDb.4.0; Data Source={0};Extended Properties=""Text;HDR=YES;FMT=Delimited""",
                Path.GetDirectoryName(filename)
            );
            using (var conn = new OleDbConnection(connString))
            {
                string query;
                conn.Open();
                if (this.Range != "")
                {
                    query = "SELECT * FROM [" + Path.GetFileName(filename) + "$" + Range + "]";

                }
                else {
                     query = "SELECT * FROM [" + Path.GetFileName(filename) + "]";
                
                }
                using (var adapter = new OleDbDataAdapter(query, conn))
                {
                    var ds = new DataSet("CSV File");
                    adapter.Fill(ds);
                    return ds;
                }
            }
        }
    }
}
