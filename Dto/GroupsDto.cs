using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
    class ClientsGroups : Operator
    {
        public Object TagPtr { get { return this; } }
        public string Ownerid { get; set; }
        public string Id { get; set; }
        public string Code { get; set; }
        public string Groupname { get; set; }
        public string Mark { get; set; }
        public string Type { get; set; }
        public string Typename { get; set; }

        //--客户
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

    class GoodsGroups : Operator
    {
        public Object TagPtr { get { return this; } }
        public string Ownerid { get; set; }
        public string Id { get; set; }
        public string Code { get; set; }
        public string Groupname { get; set; }
        public string Mark { get; set; }
        public string Type { get; set; }
        public string Typename { get; set; }

        //--商品
        public string Goodid { get; set; }
        public string Goods { get; set; }
       public string Name { get; set; }
        public string Spec { get; set; }
        public string Producer { get; set; }
        public string Outrate { get; set; }
        public string Area { get; set; }
        public string Bargain { get; set; }
        public string Bargainname { get; set; }
    }
}
