using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
    public class PubRole : Operator
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Roleid { get; set; }
        public string Rolename { get; set; }
        public string Mark { get; set; }
    }
}
