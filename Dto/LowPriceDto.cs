using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System.Data;

namespace PriceManager
{
    public class LowPrice : BasePrice
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Goods { get; set; }
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Producer { get; set; }
        public string Outrate { get; set; }
        public string Clienttypegroup { get; set; }
        public string Clienttypegroupname { get; set; }
        public string Deliveryfeerate { get; set; }
        public string Ismodifyexec { get; set; }
        public string Ismodifyexecname { get; set; }
        public string Suggestexecprc { get; set; } //= "0.000000";//建议公开含税售价
        public string Suggestexecprice { get; set; } //= "0.000000";//建议公开无税售价
        //----------------------------------
        public string Iscover { get; set; }//是否修改已有渠道价格
        public string Iscreate { get; set; }//是否新建渠道价
        public LowPrice()
        {
        }
        public string GetString()
        {
            return string.Format("{0}|*|{1}|*|{2}|*|{3}|*|{4}|*|{5}|*|{6}|*|{7}|*|{8}|*|{9}|*|{10}|*|{11}|*|{12}|*|{13}|*|{14}|*|{15}",
           Compid, Ownerid, Goodid, Prc, Price, Stopflag, Audflag, Audstatus, Lastaudtime, Costprc, Costprice, Costrate, 
           Clienttypegroup, Deliveryfeerate, Ismodifyexec, SessionDto.Empdeptid);
        }

        public string GetExcelString()
        {
            return string.Format("{0}|*|{1}|*|{2}|*|{3}|*|{4}|*|{5}|*|{6}|*|{7}|*|{8}|*|{9}|*|{10}|*|{11}|*|{12}|*|{13}|*|{14}|*|{15}|*|{16}|*|{17}",
             Properties.Settings.Default.COMPID, Properties.Settings.Default.OWNERID, Goods,
             Prc, Price, Stopflag, Audflag, Audstatus, Lastaudtime, Costprc, Costprice, Costrate, Clienttypegroup,
             Deliveryfeerate, Ismodifyexec, SessionDto.Empdeptid, Iscover, Iscreate);
        }

        public LowPrice SetValue(DataTable dt)
        {
            Suggestexecprc = dt.Rows[0][0].ToString();
            Suggestexecprice = dt.Rows[0][1].ToString();
            Prc = dt.Rows[0][2].ToString();
            Price = dt.Rows[0][3].ToString();
            Costprc = dt.Rows[0][4].ToString();
            Costprice = dt.Rows[0][5].ToString();
            Costrate = dt.Rows[0][6].ToString();
            Ismodifyexecname = dt.Rows[0][7].ToString();
            return this;
        }
    }
}
