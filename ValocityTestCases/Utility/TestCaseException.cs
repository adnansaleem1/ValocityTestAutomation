using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValocityTestCases.Utility
{
    [Serializable]
    class TestCaseException : Exception
    {
        public TestCaseException() : base() { }
        public TestCaseException(string message) : base(message) { }
    }
}
