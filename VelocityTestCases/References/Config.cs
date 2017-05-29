using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocityTestCases.References
{
    static class Config
    {
        public  const Boolean LogException=true;
        public  const Boolean LogTestOutPut = true;
        public const int AddedDelay = 0;

        //Check Back if Element popup in Browser After waiting(in seconds)
        public const int LoopTimeOutToCheckElement = 2;
        public const int MaxTimeOutToCheckElement = 400;

        public static bool UploadImageForProduct = false;
        public static string ImagesFolderPath="D:\\Files\\";
        //Will append to string to make them unique for this test case
        //public static string TestIterationName_number = DateTime.Now.Ticks.ToString();
        public static string TestIterationName_number = new Random().Next(1, 100000).ToString();
        public static string DefaultFileDownloadPath=Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"..\\..\\"))+"Downloads";
        public static string DefaultProjectPath = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
        //Max Wait For File to Download (in seconds)
        public static int MaxTimeOutToCheckFile = 500;
    }
}

