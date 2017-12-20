using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class TwoColumnView
    {
        public string First { get; set; }

        public string Second { get; set; }
        
        
        public static TwoColumnView Create(string first, string second)
        {
            return new TwoColumnView
            {
                First = first,
                Second = second
            };
        }
    }

    public class ThreeColumnView : TwoColumnView
    {
        public string Third { get; set; }

        public static ThreeColumnView Create(string first, string second, string third)
        {
            return new ThreeColumnView
            {
                First = first,
                Second = second,
                Third = third
            };
        }
    }
}
