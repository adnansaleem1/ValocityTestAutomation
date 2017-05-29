using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocityTestCases.Utility
{
    static class Common
    {
        public static bool Compare(string a, string b)
        {
            try
            {
                a = a.ToLower().Trim();
                b = b.ToLower().Trim();
                if (a == b)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }

        public static bool CompareContatin(string a, string b)
        {
            try
            {
                a = a.ToLower().Trim();
                b = b.ToLower().Trim();
                if (a == b||a.Contains(b))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
