using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
    public class Company
    {
        public string Compid { get; set; }
    }

    public class Operator : Company
    {
        public string Stopflag { get; set; }
        public string Stopflagname { get; set; }
      //  public string Createuser { get; set; }
        public string Createusercode { get; set; }
        public string Createusername { get; set; }
        public string Createdate { get; set; }
       // public string Modifyuser { get; set; }
        public string Modifyusercode { get; set; }
        public string Modifyusername { get; set; }
        public string Modifydate { get; set; }
    }

    public class BasePrice : Operator
    {
        public string Id { get; set; }
        public string Ownerid { get; set; }
        public string Goodid { get; set; }
        public string Prc { get; set; } //= "0.000000";
        public string Price { get; set; } //= "0.000000";
        public string Audflag { get; set; }
        public string Audflagname { get; set; }
        public string Audstatus { get; set; }
        public string Audstatusname { get; set; }
        public string Lastaudtime { get; set; }
        public string Costprc { get; set; } //= "0.0000000";
        public string Costprice { get; set; } //= "0.000000";
        public string Costrate { get; set; } //= "0.000000";
        public string Oriprc { get; set; }
        public string Lastprc { get; set; }
        public string Buyercode { get; set; }
        public string Buyername { get; set; }

        public string Needcheck { get; set; }
        public string Checktext { get; set; }
        public string CheckName
        {
            get
            {
                switch (Needcheck)
                {
                    case "0":
                        return "数据错误";
                    case "1":
                        return "可写入";
                    case "2":
                        return "存储过程错误";
                    default:
                        return "待校验";
                }
            }
        }
    }

    public class RetailPrice : BasePrice
    {
        public string Saledeptid { get; set; }
        public string Deptname { get; set; }

        public string Cstid { get; set; }
        public string Cstcode { get; set; }
        public string Cstname { get; set; }
        public string Region { get; set; }

        public string Goods { get; set; }
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Producer { get; set; }
        public string Outrate { get; set; }

        public string Begindate { get; set; }
        public string Enddate { get; set; }
        //------------------------------
    }

    public class ResultInfo
    {
        public string ResultMsg { get; set; }
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }

        public void InitResult()
        {
            ResultMsg = "";
            SuccessCount = 0;
            ErrorCount = 0;
        }
    }

    public class Sub : Operator
    {
        public string Subtype { get; set; }
        public string Subid { get; set; }
        public string Subname { get; set; }
        public string Mark { get; set; }

    }
}
