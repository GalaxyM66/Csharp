using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
    public class PubMenu : Operator
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Menuid { get; set; }
        public string Menucode { get; set; }
        public string Menuname { get; set; }
        public string Parentsid { get; set; }
        public string Parentsname { get; set; }
        public string Formname { get; set; }
        public string Level { get; set; }
        public string Levelname { get; set; }
    }
}
