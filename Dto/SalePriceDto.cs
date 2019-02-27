 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
    public class SalePrice : RetailPrice
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }

        public string Offlevel { get; set; }
        public string Offlevelname { get; set; }
        public string Offtype { get; set; }
        public string Offtypename { get; set; }

        public SalePrice()
        {
        }

        public void AddValue(SalePrice salePrice)
        {
            Prc = StringUtils.IsNull(salePrice.Prc) ? Prc : salePrice.Prc;
            Price = StringUtils.IsNull(salePrice.Price) ? Prc : salePrice.Price;
            Costprc = StringUtils.IsNull(salePrice.Costprc) ? Costprc : salePrice.Costprc;
            Costprice = StringUtils.IsNull(salePrice.Costprice) ? Costprice : salePrice.Costprice;
            Offlevel = StringUtils.IsNull(salePrice.Offlevel) ? Offlevel : salePrice.Offlevel;
            Offtype = StringUtils.IsNull(salePrice.Offtype) ? Offtype : salePrice.Offtype;
            Costrate = StringUtils.IsNull(salePrice.Costrate) ? Costrate : salePrice.Costrate;
            Begindate = StringUtils.IsNull(salePrice.Begindate) ? Begindate : salePrice.Begindate;
            Enddate = StringUtils.IsNull(salePrice.Enddate) ? Enddate : salePrice.Enddate;
        }

        public string GetString(string flag)
        {
            return string.Format("{0}|*|{1}|*|{2}|*|{3}|*|{4}|*|{5}|*|{6}|*|{7}|*|{8}|*|{9}|*|{10}|*|{11}|*|{12}|*|{13}|*|{14}|*|{15}|*|{16}|*|{17}",
            "E".Equals(flag) ? Properties.Settings.Default.COMPID : Compid,
            "E".Equals(flag) ? Properties.Settings.Default.OWNERID : Ownerid,
            "E".Equals(flag) ? SessionDto.Empdeptid : Saledeptid,
            "E".Equals(flag) ? Cstcode : Cstid,
            "E".Equals(flag) ? Goods : Goodid,
            Prc, Price, Stopflag, Audflag, Audstatus, Lastaudtime, Offlevel, Offtype, Costprc, Costprice, Costrate, StringUtils.ToTimeStamp(Begindate), StringUtils.ToTimeStamp(Enddate));
        }
    }
}
