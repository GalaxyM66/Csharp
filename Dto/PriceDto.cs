using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace PriceManager
{
    public class ChannelPrice : RetailPrice
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Serialno { get; set; }
        public string Bargain { get; set; }
        public string Bargainname { get; set; }
        public string Iscredit { get; set; }
        public string Iscreditname { get; set; }
        public string Origin { get; set; }
        public string Bottomprc { get; set; }
        public string Bottomprice { get; set; }
        public string Suggestbottomprc { get; set; }
        public string Suggestcostprc { get; set; }
        public string Suggestexecprc { get; set; }
        public string Source { get; set; }
        public string Personal { get; set; }
        public string Personalname { get; set; }
        public string B2bdisplay { get; set; }
        public string B2bdisplayname { get; set; }
        public ChannelPrice()
        {
        }

        public void AddValue(ChannelPrice price)
        {
            Prc = StringUtils.IsNull(price.Prc) ? Prc : price.Prc;
            Price = StringUtils.IsNull(price.Price) ? Prc : price.Price;
            Costprc = StringUtils.IsNull(price.Costprc) ? Costprc : price.Costprc;
            Costprice = StringUtils.IsNull(price.Costprice) ? Costprice : price.Costprice;
            Bargain = StringUtils.IsNull(price.Bargain) ? Bargain : price.Bargain;
            Iscredit = StringUtils.IsNull(price.Iscredit) ? Iscredit : price.Iscredit;
            Costrate = StringUtils.IsNull(price.Costrate) ? Costrate : price.Costrate;
            //Begindate = StringUtils.IsNull(price.Begindate) ? Begindate : price.Begindate;
            Enddate = StringUtils.IsNull(price.Enddate) ? Enddate : price.Enddate;
            Bottomprc = StringUtils.IsNull(price.Bottomprc) ? Bottomprc : price.Bottomprc;
            Bottomprice = StringUtils.IsNull(price.Bottomprice) ? Bottomprice : price.Bottomprice;
        }

        public string GetString(string flag)
        {
            return string.Format("{0}|*|{1}|*|{2}|*|{3}|*|{4}|*|{5}|*|{6}|*|{7}|*|{8}|*|{9}|*|{10}|*|{11}|*|{12}|*|{13}|*|{14}|*|{15}|*|{16}|*|{17}|*|{18}|*|{19}|*|{20}|*|{21}|*|{22}|*|{23}",
            "E".Equals(flag) ? Properties.Settings.Default.COMPID : Compid,
            "E".Equals(flag) ? Properties.Settings.Default.OWNERID : Ownerid,
            "E".Equals(flag) ? SessionDto.Empdeptid : Saledeptid,
            "E".Equals(flag) ? Cstcode : Cstid,
            "E".Equals(flag) ? Goods : Goodid,
            Prc, Price, Stopflag, Audflag, Audstatus, Lastaudtime, Costprc, Costprice, Bargain, Iscredit, Costrate,
            StringUtils.ToTimeStamp(Begindate), StringUtils.ToTimeStamp(Enddate), Bottomprc, Bottomprice,
            Suggestexecprc, Suggestcostprc, Suggestbottomprc, Source);
        }
    }
    public class ExceptionPrice : ChannelPrice
    {
        public string Outlierid { get; set; }
        public string Priceid { get; set; }
        public string Outliertype { get; set; }
        public string Outliertypename { get; set; }
        public string Outlierorigin { get; set; }
        public string Originname { get; set; }
        public string Outliercreatedate { get; set; }
        public string Handleflag { get; set; }
        public string Handleflagname { get; set; }
        public string Outliermodifyuser { get; set; }
        public string Outliermodifyusercode { get; set; }
        public string Outliermodifyusername { get; set; }
        public string Outliermodifydate { get; set; }
    }
}
