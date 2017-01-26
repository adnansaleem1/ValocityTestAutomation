using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityTestCases.Entity;

namespace VelocityTestCases.BL
{
    class SizeObject
    {
        public Size  size { get; set; }
        public SizeObject() {
            size = new Size("Apparel-Standard & Numbered – (S,M,L,10,12)", "//*[@id=\"divStandardLookupValues\"]", new List<string>() { "S", "M", "L" });
        }
    }
}
