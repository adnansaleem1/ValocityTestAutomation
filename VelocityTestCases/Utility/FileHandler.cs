using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityTestCases.References;

namespace VelocityTestCases.Utility
{
    class FileHandler
    {
        public static void ClearDefaultFolder()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(Config.DefaultFileDownloadPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
        public static bool FileDownloaded()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(Config.DefaultFileDownloadPath);

            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Extension.ToString() != ".crdownload")
                {
                    return true;
                }
            }
            return false;
        }
        public static string FindExcelFilePathForReport()
        {
            try
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(Config.DefaultFileDownloadPath);

                foreach (FileInfo file in di.GetFiles())
                {
                    if (file.Extension == ".xlsx" || file.Extension == ".xls" || file.Extension == ".csv")
                    {
                        return file.FullName;
                    }
                }
                throw new Exception("No excel fileFound.");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool CheckIfExcelFileContainRecords(string filename)
        {
            ExcelReader reader = new ExcelReader(filename);
            DataSet FileTable = reader.ReadExcelFile();
            if (FileTable.Tables[0].Rows.Count > 0 && FileTable.Tables[0].Columns.Count > 0)
            {
                return true;
            }
            else
            {

                throw new Exception("Excel File does not contain any records.");
            }

        }
        public static DataSet GetDataGridForExcelFile(string filename)
        {
            ExcelReader reader = new ExcelReader(filename);
            DataSet FileTable = reader.ReadExcelFile();
            if (FileTable.Tables[0].Rows.Count > 0 && FileTable.Tables[0].Columns.Count > 0)
            {
                return FileTable;
            }
            else
            {

                throw new Exception("Excel File does not contain any records.");
            }

        }

        internal static bool CompareTwoFiles(string p1, string p2)
        {
            ExcelReader reader = new ExcelReader(p1);
            DataSet FileTable = reader.ReadExcelFile();
            ExcelReader reader2 = new ExcelReader(p2);
            DataSet FileTable2 = reader.ReadExcelFile();
            return FileHandler.AreTablesTheSame(FileTable.Tables[0], FileTable2.Tables[0]);
        }
        public static bool AreTablesTheSame(DataTable tbl1, DataTable tbl2)
        {
            if (tbl1.Rows.Count != tbl2.Rows.Count || tbl1.Columns.Count != tbl2.Columns.Count)
                return false;


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                for (int c = 0; c < tbl1.Columns.Count; c++)
                {
                    if (!Equals(tbl1.Rows[i][c], tbl2.Rows[i][c]))
                        return false;
                }
            }
            return true;
        }


        internal static JObject GetJsonFileContents(string p)
        {
            try
            {
                JObject o2 = null;
                using (StreamReader file = File.OpenText(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + p))

                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    o2 = (JObject)JToken.ReadFrom(reader);
                }
                return o2;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
