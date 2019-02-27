using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
    public class PubWaredict : Operator
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Goodid { get; set; }
        public string Goods { get; set; }
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Producer { get; set; }
        public string Islimit { get; set; }
        public string Islimitname { get; set; }
        public string Outrate { get; set; }
        public string Area { get; set; }
        public string Forbitarea { get; set; }
        public string Limitcsttype { get; set; }
        public string Bargain { get; set; }
        public string Bargainname { get; set; }
        public string Sprdug { get; set; }//特药
        public string Ratifyflag { get; set; }//首营
    }

    public class GoodsSub : Sub
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Goodid { get; set; }
    }

    public class ImportGoodsSub 
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Goods { get; set; }
        public string Area { get; set; }
        public string Forbitarea { get; set; }
        public string Limitcsttype { get; set; }

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
}
