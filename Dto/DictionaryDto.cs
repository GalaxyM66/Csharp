using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
    public class Dictionary : Operator
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Id { get; set; }
        public string Typeid { get; set; }
        public string Typecode { get; set; }
        public string Typename { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Mark { get; set; }
    }

    public class DictionarySub
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Mark { get; set; }
        public string Type { get; set; }
        public string Typename { get; set; }
    }
}
