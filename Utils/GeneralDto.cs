using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
   
    public class SessionDto
    {
        public static string Empid { get; set; }
        public static string Empname { get; set; }
        public static string Empcode { get; set; }
        public static string Emproleid { get; set; }
        public static string Emprolename { get; set; }
        public static string Empdeptid { get; set; }
        public static string Empdeptname { get; set; }
        public static string Ownername { get; set; }
    }
    //货主规则类
    public class PubOwnerConfigureDto
    {
        public static string Compid { get; set; }//帐套
        public static string Ownerid { get; set; }//货主
        public static string Saledeptid { get; set; }//部门id
        public static string Saledepttype { get; set; }//客户组是否控制销售部门,0控制，1不控制
        public static string Detailtypemenu { get; set; }//定价客户类型是否可以选择无客户明细;0不可以，1可以
        public static string Grouptypeprior { get; set; }//定价客户组分类厂家覆盖公司自有是否抛出异常；0是直接覆盖，1抛出异常数据
        public static string Delivrate { get; set; }//加点率是否控制，0控制，1控制
        public static string Delivraterange { get; set; }//默认加点率最大值
        public static string Delivratedefault { get; set; }//默认加点率默认值
        public static string Allowprctype { get; set; }//是否允许修改客户组明细时自选更新对应价格，0不允许.直接默认复制价格；1允许,显示菜单，可以自选是否复制价格
        public static string BanOnSale { get; set; }//渠道限制类型，0黑名单，1白名单，2黑白名单共存
        public static string PrcDept { get; set; }//价格政策设置是否有多部门，0有，1无
        public static string Goodchoose { get; set; }//1默认全选品种；2可以自选商品；3默认全选商品,在已选界面有撤销功能

    }
    //部门规则类
    public class PubDeptConfigureDto
    {
        public static string Compid { get; set; }//帐套
        public static string Ownerid { get; set; }//货主
        public static string Saledeptid { get; set; }//部门id
        public static string Saledepttype { get; set; }//客户组是否控制销售部门,0控制，1不控制
        public static string Detailtypemenu { get; set; }//定价客户类型是否可以选择无客户明细;0不可以，1可以
        public static string Grouptypeprior { get; set; }//定价客户组分类厂家覆盖公司自有是否抛出异常；0是直接覆盖，1抛出异常数据
        public static string Delivrate { get; set; }//加点率是否控制，0控制，1控制
        public static string Delivraterange { get; set; }//默认加点率最大值
        public static string Delivratedefault { get; set; }//默认加点率默认值
        public static string Allowprctype { get; set; }//是否允许修改客户组明细时自选更新对应价格，0不允许.直接默认复制价格；1允许,显示菜单，可以自选是否复制价格
        public static string BanOnSale { get; set; }//渠道限制类型，0黑名单，1白名单，2黑白名单共存
        public static string PrcPri { get; set; }//定价优先属性，0价格部门属性优先，1客户组部门属性优先
        public static string WaitFlag { get; set; }//是否允许提前设价，0不允许,1允许
        public static string ValidDays { get; set; }//设置默认加多少天
        public static string DefaultEnddate { get; set; }//设置默认结束日期
    }

    //归属部门规则类
    public class DeptConfigureDto
    {
        public  string Compid { get; set; }//帐套
        public  string Ownerid { get; set; }//货主
        public  string Saledeptid { get; set; }//部门id
        public  string Saledepttype { get; set; }//客户组是否控制销售部门,0控制，1不控制
        public  string Detailtypemenu { get; set; }//定价客户类型是否可以选择无客户明细;0不可以，1可以
        public  string Grouptypeprior { get; set; }//定价客户组分类厂家覆盖公司自有是否抛出异常；0是直接覆盖，1抛出异常数据
        public  string Delivrate { get; set; }//加点率是否控制，0控制，1控制
        public  string Delivraterange { get; set; }//默认加点率最大值
        public  string Delivratedefault { get; set; }//默认加点率默认值
        public  string Allowprctype { get; set; }//是否允许修改客户组明细时自选更新对应价格，0不允许.直接默认复制价格；1允许,显示菜单，可以自选是否复制价格
        public  string BanOnSale { get; set; }//渠道限制类型，0黑名单，1白名单，2黑白名单共存
        public  string PrcPri { get; set; }//定价优先属性，0价格部门属性优先，1客户组部门属性优先
        public  string WaitFlag { get; set; }//是否允许提前设价，0不允许,1允许
        public  string ValidDays { get; set; }//设置默认加多少天
        public  string DefaultEnddate { get; set; }//设置默认结束日期
    }

    //默认下拉框
    public class NameValue
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }

        }
        public string Name { set; get; }
        public string Value { set; get; }
        public NameValue() { }
        public NameValue(string name, string value)
        {
            Value = value;
            Name = name;
        }
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }


    }

    //系统规则类
    public class SysCode
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Compid { get; set; }//账套
        public string Id { get; set; }//ID
        public string Typeid { get; set; }//规则id
        public string Typecode { get; set; }//规则代码
        public string Typename { get; set; }//规则名
        public string Code { get; set; }//状态代码
        public string Name { get; set; }//状态名称
        public string Mark { get; set; }//备注
        public string Stopflag { get; set; }//停用标识
        public string Createuser { get; set; }//创建人员
        public string Createdate { get; set; }//创建时间
        public string Modifyuser { get; set; }//修改人员
        public string Modifydate { get; set; }//修改时间
        public string Ownerid { get; set; }//货主
        public string Detailtypemenu { get; set; }//明细类型菜单
        public bool SelectFlag { get; set; }//勾选标识
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }


    //存储过程返回类
    public class SPRetInfo
    {
        public string num { set; get; }
        public string msg { set; get; }
        public string result { set; get; }
        public string count { set; get; }
        public string selflag { set; get; }
    }

    public class SqlStr
    {
        public static string PriceSql(string value, string sortableValue)
        {
            return string.Format("SELECT "
                                            + "`dfje`.`id` AS `id`, "
                                            + "`dfje`.`compid` AS `compid`, "
                                            + "`dfje`.`ownerid` AS `ownerid`, "
                                            + "`dfje`.`saledeptid` AS `saledeptid`, "
                                            + "`dept`.`deptname` AS `deptname`, "
                                            + "`dfje`.`cstid` AS `cstid`, "
                                            + "`pc`.`cstcode` AS `cstcode`, "
                                            + "`pc`.`cstname` AS `cstname`, "
                                            + " concat( `f_region_codetoname` (`pc`.`province`), '-', `f_region_codetoname` (`pc`.`city`), '-', `f_region_codetoname` (`pc`.`area`)) AS `region`,"
                                            + "`dfje`.`goodid` AS `goodid`, "
                                            + "`pw`.`goods` AS `goods`, "
                                            + "`pw`.`name` AS `name`, "
                                            + "`pw`.`spec` AS `spec`, "
                                            + "`pw`.`producer` AS `producer`, "
                                            + "`dfje`.`prc` AS `prc`, "
                                            + "`dfje`.`price` AS `price`, "
                                            + "`dfje`.`stopflag` AS `stopflag`, "
                                            + "(CASE "
                                            + "WHEN (`dfje`.`stopflag` = '00') THEN F_SYSCODE_TO_NAME(1, '00') "
                                            + "WHEN (`dfje`.`stopflag` = '99') THEN F_SYSCODE_TO_NAME(1, '99') "
                                            + "ELSE `dfje`.`stopflag` "
                                            + "END) AS `stopflagname`, "
                                            + "`dfje`.`createuser` AS `createuser`, "
                                            + "`pe1`.`empcode` AS `createusercode`, "
                                            + "`pe1`.`empname` AS `createusername`, "
                                            + "`dfje`.`createdate` AS `createdate`, "
                                            + "`dfje`.`modifyuser` AS `modifyuser`, "
                                            + "`pe2`.`empcode` AS `modifyusercode`, "
                                            + "`pe2`.`empname` AS `modifyusername`, "
                                            + "`dfje`.`modifydate` AS `modifydate`, "
                                            + "`dfje`.`audflag` AS `audflag`, "
                                            + "(CASE  "
                                            + "WHEN (`dfje`.`audflag` = '00') THEN F_SYSCODE_TO_NAME(4, '00') "
                                            + "WHEN (`dfje`.`audflag` = '20') THEN F_SYSCODE_TO_NAME(4, '20') "
                                            + "ELSE `dfje`.`audflag` "
                                            + "END) AS `audflagname`, "
                                            + "`dfje`.`audstatus` AS `audstatus`, "
                                            + "(CASE  "
                                            + "WHEN (`dfje`.`audstatus` = '00') THEN F_SYSCODE_TO_NAME(5, '00') "
                                            + "WHEN (`dfje`.`audstatus` = '10') THEN F_SYSCODE_TO_NAME(5, '10') "
                                            + "WHEN (`dfje`.`audstatus` = '20') THEN F_SYSCODE_TO_NAME(5, '20') "
                                            + "ELSE `dfje`.`audstatus` "
                                            + "END) AS `audstatusname`, "
                                            + "`dfje`.`lastaudtime` AS `lastaudtime`, "
                                            + "`dfje`.`costprc` AS `costprc`, "
                                            + "`dfje`.`costprice` AS `costprice`, "
                                            + "`dfje`.`bargain` AS `bargain`, "
                                            + "(CASE  "
                                            + "WHEN (`dfje`.`bargain` = '00') THEN F_SYSCODE_TO_NAME(2, '00') "
                                            + "WHEN (`dfje`.`bargain` = '10') THEN F_SYSCODE_TO_NAME(2, '10') "
                                            + "ELSE `dfje`.`bargain` "
                                            + "END) AS `bargainname`, "
                                            + "`dfje`.`iscredit` AS `iscredit`, "
                                            + "(CASE  "
                                            + "WHEN (`dfje`.`iscredit` = '00') THEN F_SYSCODE_TO_NAME(3, '00') "
                                            + "WHEN (`dfje`.`iscredit` = '10') THEN F_SYSCODE_TO_NAME(3, '10') "
                                            + "ELSE `dfje`.`iscredit` "
                                            + "END) AS `iscreditname`, "
                                            + "`dfje`.`costrate` AS `costrate`, "
                                            + "`dfje`.`begindate` AS `begindate`, "
                                            + "`dfje`.`enddate` AS `enddate`, "
                                            + "`dfje`.`bottomprc` AS `bottomprc`, "
                                            + "`dfje`.`bottomprice` AS `bottomprice`, "
                                            + "`dfje`.`suggestbottomprc` AS `suggestbottomprc`, "
                                            + "`dfje`.`suggestcostprc` AS `suggestcostprc`, "
                                            + "`dfje`.`suggestexecprc` AS `suggestexecprc`, "
                                            + "`dfje`.`oriprc` AS `oriprc`, "
                                            + "`dfje`.`lastprc` AS `lastprc`, "
                                            + "`dfje`.`expdate` AS `expdate`, "
                                            + "`dfje`.`exptype` AS `exptype`, "
                                            + "`dfje`.`origin` AS `origin`, "
                                            + "`dfje`.`b2bdisplay` AS `b2bdisplay`, "
                                            + " f_syscode_to_name(41, dfje.b2bdisplay) AS `b2bdisplayname`, "
                                            + "`pw`.`outrate` AS `outrate` "
                                            + "FROM "
                                            + "(SELECT  "
                                            + "`pd`.`id` AS `id`, "
                                            + "`pd`.`compid` AS `compid`, "
                                            + "`pd`.`ownerid` AS `ownerid`, "
                                            + "`pd`.`saledeptid` AS `saledeptid`, "
                                            + "`pd`.`cstid` AS `cstid`, "
                                            + "`pd`.`goodid` AS `goodid`, "
                                            + "`pd`.`prc` AS `prc`, "
                                            + "`pd`.`price` AS `price`, "
                                            + "`pd`.`stopflag` AS `stopflag`, "
                                            + "`pd`.`createuser` AS `createuser`, "
                                            + "`pd`.`createdate` AS `createdate`, "
                                            + "`pd`.`modifyuser` AS `modifyuser`, "
                                            + "`pd`.`modifydate` AS `modifydate`, "
                                            + "`pd`.`audflag` AS `audflag`, "
                                            + "`pd`.`audstatus` AS `audstatus`, "
                                            + "`pd`.`lastaudtime` AS `lastaudtime`, "
                                            + "`pd`.`costprc` AS `costprc`, "
                                            + "`pd`.`costprice` AS `costprice`, "
                                            + "`pd`.`bargain` AS `bargain`, "
                                            + "`pd`.`iscredit` AS `iscredit`, "
                                            + "`pd`.`costrate` AS `costrate`, "
                                            + "`pd`.`begindate` AS `begindate`, "
                                            + "`pd`.`enddate` AS `enddate`, "
                                            + "`pd`.`bottomprc` AS `bottomprc`, "
                                            + "`pd`.`bottomprice` AS `bottomprice`, "
                                            + "`pd`.`suggestbottomprc` AS `suggestbottomprc`, "
                                            + "`pd`.`suggestcostprc` AS `suggestcostprc`, "
                                            + "`pd`.`suggestexecprc` AS `suggestexecprc`, "
                                            + "`pd`.`oriprc` AS `oriprc`, "
                                            + "`pd`.`lastprc` AS `lastprc`, "
                                            + "`pd`.`expdate` AS `expdate`, "
                                            + "`pd`.`exptype` AS `exptype`, "
                                            + "`pd`.`b2bdisplay` AS `b2bdisplay`, "
                                            + "'draft' AS `origin` "
                                            + "FROM "
                                            + "`scm_price_draft` `pd`  "
                                            + "WHERE 1=1 {0} "
                                            + "union "
                                            + " SELECT   "
                                            + "`pe`.`id` AS `id`, "
                                            + "`pe`.`compid` AS `compid`, "
                                            + "`pe`.`ownerid` AS `ownerid`, "
                                            + "`pe`.`saledeptid` AS `saledeptid`, "
                                            + "`pe`.`cstid` AS `cstid`, "
                                            + "`pe`.`goodid` AS `goodid`, "
                                            + "`pe`.`prc` AS `prc`, "
                                            + "`pe`.`price` AS `price`, "
                                            + "`pe`.`stopflag` AS `stopflag`, "
                                            + "`pe`.`createuser` AS `createuser`, "
                                            + "`pe`.`createdate` AS `createdate`, "
                                            + "`pe`.`modifyuser` AS `modifyuser`, "
                                            + "`pe`.`modifydate` AS `modifydate`, "
                                            + "`pe`.`audflag` AS `audflag`, "
                                            + "`pe`.`audstatus` AS `audstatus`, "
                                            + "`pe`.`lastaudtime` AS `lastaudtime`, "
                                            + "`pe`.`costprc` AS `costprc`, "
                                            + "`pe`.`costprice` AS `costprice`, "
                                            + "`pe`.`bargain` AS `bargain`, "
                                            + "`pe`.`iscredit` AS `iscredit`, "
                                            + "`pe`.`costrate` AS `costrate`, "
                                            + "`pe`.`begindate` AS `begindate`, "
                                            + "`pe`.`enddate` AS `enddate`, "
                                            + "`pe`.`bottomprc` AS `bottomprc`, "
                                            + "`pe`.`bottomprice` AS `bottomprice`, "
                                            + "`pe`.`suggestbottomprc` AS `suggestbottomprc`, "
                                            + "`pe`.`suggestcostprc` AS `suggestcostprc`, "
                                            + "`pe`.`suggestexecprc` AS `suggestexecprc`, "
                                            + "`pe`.`oriprc` AS `oriprc`, "
                                            + "`pe`.`lastprc` AS `lastprc`, "
                                            + "NULL AS `expdate`, "
                                            + "NULL AS `exptype`, "
                                            + "`pe`.`b2bdisplay` AS `b2bdisplay`, "
                                            + "'executed' AS `origin` "
                                            + "FROM "
                                            + "`scm_price_executed` `pe` "
                                            + "WHERE 1=1 {1} "
                                            + ")  `dfje` "
                                            + "LEFT JOIN `pub_dept` `dept` ON ((`dfje`.`saledeptid` = `dept`.`saledeptid`)) "
                                            + "LEFT JOIN `pub_clients` `pc` ON ((`dfje`.`cstid` = `pc`.`cstid`)) "
                                            + "LEFT JOIN `pub_waredict` `pw` ON ((`dfje`.`goodid` = `pw`.`goodid`)) "
                                            + "LEFT JOIN `pub_emp` `pe1` ON ((`dfje`.`createuser` = `pe1`.`empid`)) "
                                            + "LEFT JOIN `pub_emp` `pe2` ON ((`dfje`.`modifyuser` = `pe2`.`empid`)) "
                                            + " where 1=1"
                                            + "  and f_judge_dept_cst(ifnull(dfje.compid, 0), ifnull(dfje.ownerid, 0), ifnull(dfje.saledeptid, 0), ifnull(dfje.cstid, 0)) "
                                            + " {2} ",
                                            value, value, sortableValue);
        }

        public static string ChangeConfirmSql(string saleValue, string value, string sortableValue)
        {
            return string.Format(
                                                " select "
                                            + " pc.cstid, "
                                            + " pc.cstcode, "
                                            + " pc.cstname, "
                                            + " pc.region, "
                                            + " d.id, "
                                            + " d.prc, "
                                            + " d.price, "
                                            + " d.costprc, "
                                            + " d.costprice, "
                                            + " d.bottomprc, "
                                            + " d.bottomprice, "
                                            + " d.costrate, "
                                            + " d.suggestexecprc, "
                                            + " d.suggestcostprc, "
                                            + " d.suggestbottomprc, "
                                            + " d.personal, "
                                            + "d.audflag,"
                                            + "d.audstatus,"
                                            + "d.begindate,"
                                            + "d.bargain,"
                                            + " f_syscode_to_name (2, d.bargain)as bargainname, "
                                            + "d.enddate,"
                                            + "d.compid,"
                                            + "d.ownerid,"
                                            + "d.goodid,"
                                            + "d.iscredit,"
                                            + "d.lastaudtime,"
                                            + "d.saledeptid,"
                                            + "d.stopflag,"
                                            + " f_syscode_to_name (1, d.stopflag)as stopflagname, "
                                            + " f_syscode_to_name (32, d.personal)as personalname, "
                                            + " 'draft' origin "
                                            + " from "
                                            + " pub_clients_sub cs, "
                                            + " v_pub_clients_all pc, "
                                            + " scm_price_draft d "
                                            + " where "
                                            + " pc.cstid = d.cstid "
                                            + " and pc.compid = d.compid "
                                            + " and d.ownerid = @ownerid "
                                            + " and d.goodid = @goodid "
                                            + "{0}"
                                            + " and pc.compid = cs.compid "
                                            + " and cs.ownerid = @ownerid "
                                            + " and cs.cstid = pc.cstid "
                                            + " and cs.subtype = 30 "
                                            + " and f_waredict_limit_judge (f_clientstype_to_group(pc.clienttype,'code'),pc.province,pc.city,pc.area,@ownerid,@goodid) "
                                            + " and f_judge_dept_cst(ifnull(d.compid, 0), ifnull(d.ownerid, 0), ifnull(d.saledeptid, 0), ifnull(d.cstid, 0)) "
                                            + " {1} "
                                            + " union "
                                            + " select "
                                            + " pc.cstid, "
                                            + " pc.cstcode, "
                                            + " pc.cstname, "
                                            + " pc.region, "
                                            + " d.id, "
                                            + " d.prc, "
                                            + " d.price, "
                                            + " d.costprc, "
                                            + " d.costprice, "
                                            + " d.bottomprc, "
                                            + " d.bottomprice, "
                                            + " d.costrate, "
                                            + " d.suggestexecprc, "
                                            + " d.suggestcostprc, "
                                            + " d.suggestbottomprc, "
                                            + " d.personal, "
                                            + "d.audflag,"
                                            + "d.audstatus,"
                                            + "d.begindate,"
                                            + "d.bargain,"
                                            + " f_syscode_to_name (2, d.bargain)as bargainname, "
                                            + "d.enddate,"
                                            + "d.compid,"
                                            + "d.ownerid,"
                                            + "d.goodid,"
                                            + "d.iscredit,"
                                            + "d.lastaudtime,"
                                            + "d.saledeptid,"
                                            + "d.stopflag,"
                                            + " f_syscode_to_name (1, d.stopflag)as stopflagname, "
                                            + " f_syscode_to_name (32, d.personal) as personalname, "
                                            + " 'executed' origin "
                                            + " from "
                                            + " pub_clients_sub cs, "
                                            + " v_pub_clients_all pc "
                                            + "left join scm_price_executed d on"
                                            + " pc.compid = d.compid "
                                            + " and pc.cstid = d.cstid "
                                            + " and d.ownerid = @ownerid "
                                            + " and d.goodid = @goodid "
                                            + "{2}"
                                            + " where "
                                            + " pc.compid = cs.compid "
                                            + " and cs.ownerid = @ownerid "
                                            + " and cs.cstid = pc.cstid "
                                            + " and cs.subtype = 30 "
                                            + " and f_waredict_limit_judge (f_clientstype_to_group(pc.clienttype,'code'),pc.province,pc.city,pc.area,@ownerid,@goodid) "
                                            + " and f_judge_dept_cst(ifnull(d.compid, 0), ifnull(d.ownerid, 0), ifnull(d.saledeptid, 0), ifnull(d.cstid, 0)) "
                                            + " {3} "
                                            + " {4} "
                                            , saleValue, value, saleValue, value, sortableValue);
        }

        public static string PriceExpandSql(string value, string sortableValue)
        {
            return string.Format(" SELECT "
                                            + " pb.compid, "
                                            + " pb.ownerid, "
                                            + " ( SELECT cs.subid FROM pub_clients_sub cs WHERE cs.compid = @compid AND ownerid = @ownerid AND subtype = 30 AND cstid = @cstid )  saledeptid, "
                                            + " @cstid cstid, "
                                            + " pw.goodid, "
                                            + " pw.goods, "
                                            + " pw.`name`, "
                                            + " pw.spec, "
                                            + " pw.producer, "
                                            + " pw.outrate, "
                                            + " pb.clienttypegroup, "
                                            + " pb.deliveryfeerate, "
                                            + " ROUND(pb.prc*(1+pb.deliveryfeerate),6) AS prc, "
                                            + " ROUND(pb.prc*(1+pb.deliveryfeerate)/(1+pw.outrate),6) AS price, "
                                            + " '00' AS stopflag, "
                                            + " f_syscode_to_name(1, '00') stopflagname, "
                                            + " '20' AS audflag, "
                                            + " '20' AS audstatus, "
                                            + " null AS lastaudtime, "
                                            + " pb.costprc, "
                                            + " pb.costprice, "
                                            + " pb.ismodifyexec AS bargain, "
                                            + " f_syscode_to_name(2, pb.ismodifyexec) bargainname, "
                                            + " '10' AS iscredit, "
                                            + " f_syscode_to_name(3, '10') iscreditname,"
                                            + " pb.costrate, "
                                            + " DATE_FORMAT(SYSDATE(),'%Y/%m/%d') begindate, "
                                            + " '2038/01/01' enddate, "
                                            + " pb.prc AS bottomprc, "
                                            + " pb.price AS bottomprice, "
                                            + " ROUND(pb.prc*(1+pb.deliveryfeerate),2) AS suggestexecprc, "
                                            + " pb.costprc AS suggestcostprc, "
                                            + " pb.prc AS suggestbottomprc, "
                                            + " '06' AS source "
                                            + " FROM "
                                            + " pub_clients pc, "
                                            + " scm_price_bottom pb, "
                                            + " pub_waredict pw "
                                            + " WHERE "
                                            + " pb.goodid = pw.goodid "
                                            + " AND pb.compid = pw.compid "
                                            + " AND pb.stopflag = '00' "
                                            + " AND pw.stopflag = '00' "
                                            + " AND pc.cstid = @cstid "
                                            + " AND pc.compid = @compid "
                                            + " AND f_clientstype_to_group ( f_get_b2b_clienttype(@ownerid,@cstid), 'code') = pb.clienttypegroup "
                                            + " AND pb.compid = @compid "
                                            + " AND pb.ownerid = @ownerid "
                                            + " AND f_waredict_limit_judge ( f_clientstype_to_group (f_get_b2b_clienttype(@ownerid,@cstid), 'code'), pc.province, pc.city, pc.area, @ownerid, pb.goodid ) "
                                            + "{0}{1}"
                                            , value, sortableValue);
        }
    }
}
