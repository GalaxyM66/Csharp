using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
    public class PubEmp : Operator
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Ownerid { get; set; }
        public string Empid { get; set; }
        public string Empcode { get; set; }
        public string Empname { get; set; }
        public string Deptid { get; set; }
        public string Deptname { get; set; }
        public string Password { get; set; }
        public string Allowlogin { get; set; }
        public string Allowloginname { get; set; }
        public string Roleid { get; set; }
        public string Rolename { get; set; }
        public string Lastlogindate { get; set; }
        public string Logincount { get; set; }
    }
}
