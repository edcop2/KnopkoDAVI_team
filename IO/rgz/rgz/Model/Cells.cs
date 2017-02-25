using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rgz.Model
{
    
    public interface ICell
    {

    }

    public class InnerCell : ICell
    {
        public string NW { get; set; }

        public string NE { get; set; }

        public string SW { get; set; }

        public string SE { get; set; }


        public InnerCell()
        {
            NW = "";
            NE = "";
            SW = "";
            SE = "";

        }

        public InnerCell(string ne, string sw, string nw="", string se="")
        {
            NW = nw;
            NE = ne;
            SW = sw;
            SE = se;
        }
    }

    public class OuterCell : ICell
    {
        public string Content { get; set; }


        public OuterCell()
        { }

        public OuterCell(string content)
        {
            Content = content;
        }

    }
}
