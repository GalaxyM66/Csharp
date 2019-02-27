using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
    class APDto_ClientGroup : Operator
    {
        
    }
    public class PubConfigureInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        //a.,a.,a.,a.,a.
        public string Compid { get; set; }//仓库代码
        public string Ownerid { get; set; }//任务号
        public string Saledeptid { get; set; }//单据号
        public string Dname { get; set; }//客户名称
        public string Detailtypemenu { get; set; }//客户地址
        public string Grouptypeprior { get; set; }//赠样标识
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //定价客户组主表
    public class CstGroupHdr
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Ownerid { get; set; }//货主代码
        public string Deptname { get; set; }//货主名称
        public string Code { get; set; }//定价客户组代码
        public string Name { get; set; }//定价客户组名称
        public string Detailtypename { get; set; }//定价客户类型
        public string Grouptypename { get; set; }//定价客户组分类
        public string Synctypename { get; set; }//定价客户组同步分类
        public string Attachcode { get; set; }//默认归属代码
        public string Deliveryfeerate { get; set; }//默认加点率
        public string STOPNAME { get; set; }//停用表示
        public string Mark { get; set; }//备注
        public string Detailtype { get; set; }//定价客户类型代码
        public string Grouptype { get; set; }//定价客户组分类代码
        public string Synctype { get; set; }//定价客户组同步分类代码
        public string Stopflag { get; set; }//停用标识代码
        public string Createuser { get; set; }//创建人员代码
        public string Createusername { get; set; }//创建人员
        public string Createdate { get; set; }//创建时间
        public string Modifyuser { get; set; }//最后修改人员代码
        public string Modifyusername { get; set; }//最后修改人员
        public string Modifydate { get; set; }//最后修改时间
        public string Compid { get; set; }//账套ID
        public string Hdrid { get; set; }//客户组ID
        public string Saledeptid { get; set; }//部门ID
        public string Saledeptname { get; set; }//部门名称
        public bool SelectFlag { get; set; }//勾选标识

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //定价客户组明细
    public class CstGroupDtl
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Cstcode { get; set; }//客户代码
        public string CSTNAME { get; set; }//客户名称
        public string Region { get; set; }//区域
        public string STOPNAME { get; set; }//停用标识
        public string CREATENAME { get; set; }//创建人员
        public string MODIFYNAME { get; set; }//最后修改人
        public string Hdrid { get; set; }//客户组ID
        public string Dtlid { get; set; }//客户组明细ID
        public string Compid { get; set; }//账套ID
        public string Ownerid { get; set; }//货主代码
        public string Cstid { get; set; }//客户ID
        public string Modifyprctype { get; set; }//修改单价类型
        public string Modprcname { get; set; }//最后一次是否同步客户价格
        public string Modifyprcdate { get; set; }//修改单价日期
        public string Modifynum { get; set; }//修改数量
        public string Stopflag { get; set; }//
        public string Createuser { get; set; }//
        public string Createdate { get; set; }//
        public string Modifyuser { get; set; }//
        public string Modifydate { get; set; }//
        public string Batchid { get; set; }//批次ID
        public string Saledeptid { get; set; }//销售部门ID
        public string Saledeptname { get; set; }//销售部门名称
        public bool SelectFlag { get; set; }//勾选标识
        public string UseFlag { get; set; }//使用标识（程序前台使用）00:显示，99：不显示
        public string Goodtapid { get; set; }//Goodtapid

        public string Cmsclienttype { get; set; }//cms同步客户类型代码
        public string Cmsclienttypename { get; set; }//cms同步客户类型
        public string Clienttype { get; set; }//自维护客户类型代码
        public string Clienttypename { get; set; }//自维护客户类型
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }




    //客户组定价商品明细
    public class GoodPrc
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Saledeptid { get; set; }//部门ID
        public string Compid { get; set; }//账套ID
        public string Goodid { get; set; }//商品ID
        public string Goods { get; set; }//商品代码
        public string Name { get; set; }//品名
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//生产厂家
        public string Sprdug { get; set; }//是否特药
        public string Hdrid { get; set; }//客户组ID
        public string Ownerid { get; set; }//货主代码
        public string Prc { get; set; }//公开价格
        public string Price { get; set; }//公开无税销价
        public string Bottomprc { get; set; }//含税底价
        public string Bottomprice { get; set; }//无税底价
        public string Costprc { get; set; }//含税成本销价
        public string Costprice { get; set; }//无税成本销价
        public string Begindate { get; set; }//开始时间
        public string Enddate { get; set; }//结束时间
        public string Goodtapid { get; set; }//商品帽子价ID
        public string Costrate { get; set; }//考核毛利率
        public string Grouptype { get; set; }//定价客户组分类代码
        public string Synctype { get; set; }//定价客户组同步分类代码
        public bool SelectFlag { get; set; }//勾选标识


        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }


    //新增客户组临时表
    public class ScmClientsGroupTemp
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Batchid { get; set; }//批次号
        public string Hdrid { get; set; }//客户组ID
        public string Compid { get; set; }//界面compid
        public string Ownerid { get; set; }//界面ownerid
        public string Saledeptid { get; set; }//部门id，可空
        public string Code { get; set; }//填写的客户组代码
        public string Name { get; set; }//填写的客户组名称
        public string Detailtype { get; set; }//客户组类型
        public string Grouptype { get; set; }//客户组分类
        public string Synctype { get; set; }//客户组同步分类
        public string Attachcode { get; set; }//null空
        public string Deliveryfeerate { get; set; }//填写的加点率
        public string Mark { get; set; }//填写的备注
        public string Hdrstopflag { get; set; }//客户组停启用状态
        public string Dtlid { get; set; }//null空
        public string Cstid { get; set; }//客户ID
        public string Modifyprctype { get; set; }//是否同步价格
        public string Dtlstopflag { get; set; }//明细停启用状态
        public string Empid { get; set; }//操作人
        public string Operatype { get; set; }//操作类型;1新增客户组,2新增明细,3停用明细,4启用明细
        public string Createdate { get; set; }//系统当前时间
        public string HdrSaledeptid { get; set; }//部门id，可空
        public string DtlSaledeptid { get; set; }//部门id，可空
        public bool SelectFlag { get; set; }//勾选标识

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //-------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------
    //限禁销渠道维护(18-06-08)
    public class ScmPriceForbidsale
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Cstid { get; set; }//客户ID
        public string Cstcode { get; set; }//客户代码
        public string Cstname { get; set; }//客户名称
        public string Region { get; set; }//地区
        public string Goodid { get; set; }//商品ID
        public string Goods { get; set; }//商品代码
        public string Name { get; set; }//商品名称
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//生产厂家
        public string Outrate { get; set; }//利率
        public string Sprdug { get; set; }//是否特殊药品
        public string Id { get; set; }//ID
        public string Compid { get; set; }//账套ID
        public string Ownerid { get; set; }//货主ID
        public string Saleflag { get; set; }//限禁销标识代码
        public string Saleflagname { get; set; }//限禁销标识
        public string Begindate { get; set; }//开始时间
        public string Enddate { get; set; }//结束时间
        public string Hdrcode { get; set; }//客户组代码
        public string Hdrname { get; set; }//客户组名称
        public string Createuser { get; set; }//创建人员代码
        public string Createusername { get; set; }//创建人员名称
        public string Createdate { get; set; }//创建时间
        public string Modifyuser { get; set; }//最后修改人代码
        public string Modifyusername { get; set; }//最后修改人名称
        public string Modifydate { get; set; }//,//最后修改时间
        public bool SelectFlag { get; set; }//勾选标识

        public string Cmsclienttypename { get; set; }//CMS同步客户类型
        public string Clienttypename { get; set; }//自维护客户类型
        public string Cmsclienttype { get; set; }//CMS同步客户类型代码
        public string Clienttype { get; set; }//自维护客户类型代码
        public string Mark { get; set; }//备注

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //限禁销商品查询
    public class SelWaredict
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Goodid { get; set; }//商品id
        public string Goods { get; set; }//商品代码
        public string Name { get; set; }//商品名称
        public string Spec { get; set; }//商品规格
        public string Producer { get; set; }//生产厂家
        public string Stopflag { get; set; }//停用标识代码
        public string Stopflagname { get; set; }//停用标识
        public string Createuser { get; set; }//
        public string Createdate { get; set; }//
        public string Sprdug { get; set; }//,//
        public bool SelectFlag { get; set; }//勾选标识
        public string Islimit { get; set; }//
        public string Islimitname { get; set; }//
        public string Bargain { get; set; }//
        public string Bargainname { get; set; }//
        public string Outrate { get; set; }//
        public string Compid { get; set; }//账套id
        public string Wdrcode { get; set; }//商品组代码
        public string Wdrname { get; set; }//商品组名称

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //限禁销客户查询(18-05-31)2
    public class SelClientsGroup
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Cstid { get; set; }//客户ID
        public string Cstcode { get; set; }//客户代码
        public string Cstname { get; set; }//客户名称
        public string Region { get; set; }//地区
        public string Stopflag { get; set; }//
        public string Stopflagname { get; set; }//
        public string Hdrcode { get; set; }//客户组代码
        public string Hdrname { get; set; }//客户组名称
        public string Createuser { get; set; }//
        public string Createusercode { get; set; }//
        public string Createusername { get; set; }//
        public string Createdate { get; set; }//
        public string Modifyuser { get; set; }//
        public string Modifyusercode { get; set; }//
        public string Modifyusername { get; set; }//
        public string Modifydate { get; set; }//
        public bool SelectFlag { get; set; }//勾选标识
        public string Compid { get; set; }//
        public string Ownerid { get; set; }//
        public string Province { get; set; }//
        public string Provincename { get; set; }//
        public string City { get; set; }//
        public string Cityname { get; set; }//
        public string Area { get; set; }//
        public string Areaname { get; set; }//
        public string Paytype { get; set; }//
        public string Paytypename { get; set; }//

        public string Cmsclienttypename { get; set; }//CMS同步客户类型
        public string Clienttypename { get; set; }//自维护客户类型
        public string Cmsclienttype { get; set; }//CMS同步客户类型代码
        public string Clienttype { get; set; }//自维护客户类型代码

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    
    //限禁销新增临时表
    public class ScmPriceForbidsaleTemp
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Batchid { get; set; }//操作批次号
        public string Id { get; set; }//ID（可空）
        public string Compid { get; set; }//账套ID
        public string Ownerid { get; set; }//货主ID
        public string Cstid { get; set; }//客户ID
        public string Goodid { get; set; }//商品ID
        public string Saleflag { get; set; }//限禁销标识
        public string Operatetype { get; set; }//操作标识2：新增，1：停用/删除
        public string Audflag { get; set; }//审批标识（可空）
        public string Audstatus { get; set; }//审批状态（可空）
        public string Lastaudtime { get; set; }//最后审批时间（可空）
        public string Begindate { get; set; }//开时间
        public string Enddate { get; set; }//结束时间
        public string Source { get; set; }//数据来源：01
        public string Mark { get; set; }//备注
        public bool SelectFlag { get; set; }//勾选标识

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

}
