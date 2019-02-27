using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
    public class PubClients : Operator
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Cstid { get; set; }
        public string Cstcode { get; set; }
        public string Cstname { get; set; }
        public string Province { get; set; }
        public string Provincename { get; set; }
        public string City { get; set; }
        public string Cityname { get; set; }
        public string Area { get; set; }
        public string Areaname { get; set; }
        public string Clienttype { get; set; }
        public string Clienttypename { get; set; }
        public string Clienttypegroup { get; set; }
        public string Clienttypegroupname { get; set; }
        public string Region { get; set; }
        public string Dept { get; set; }
        public string Paytype { get; set; }
        public string Paytypename { get; set; }
    }

    public class ClientsSub : Sub
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Cstid { get; set; }
    }
}
