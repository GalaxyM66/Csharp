using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
    public class PubDept : Operator
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }

        }
        public string Ownerid { get; set; }
        public string Saledeptid { get; set; }
        public string Deptname { get; set; }
    }
}
