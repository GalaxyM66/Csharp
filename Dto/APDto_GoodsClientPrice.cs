using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
    class APDto_GoodsClientPrice : Operator
    {
    }
    //商品信息
    public class SelPubWaredict
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Compid { get; set; }//帐套
        public string Goodid { get; set; }//商品id
        public string Goods { get; set; }//商品代码
        public string Name { get; set; }//商品名称
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//厂家
        public string Islimit { get; set; }//是否限销代码 00非限销，10限销，限销区域、定价区域要挂从表
        public string Islimitname { get; set; }//是否限销名称 00非限销，10限销，限销区域、定价区域要挂从表
        public string Bargain { get; set; }//是否可议价代码 00否，10是
        public string Bargainname { get; set; }//是否可议价名称 00否，10是
        public string Outrate { get; set; }//税率
        public string Stopflag { get; set; }//停用标识代码
        public string Stopflagname { get; set; }//停用标识
        public string Createuser { get; set; }//创建人员id
        public string Createdate { get; set; }//创建时间
        public string Sprdug { get; set; }//特殊药品
        public bool SelectFlag { get; set; }//勾选标识

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }
    //商品信息
    public class SelPubWaredicts
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Compid { get; set; }//帐套
        public string Goodid { get; set; }//商品id
        public string Goods { get; set; }//商品代码
        public string Name { get; set; }//商品名称
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//厂家
        public string Islimit { get; set; }//是否限销代码 00非限销，10限销，限销区域、定价区域要挂从表
        public string Islimitname { get; set; }//是否限销名称 00非限销，10限销，限销区域、定价区域要挂从表
        public string Bargain { get; set; }//是否可议价代码 00否，10是
        public string Bargainname { get; set; }//是否可议价名称 00否，10是
        public string Outrate { get; set; }//税率
        public string Stopflag { get; set; }//停用标识代码
        public string Stopflagname { get; set; }//停用标识
        public string Createuser { get; set; }//创建人员id
        public string Createdate { get; set; }//创建时间
        public string Sprdug { get; set; }//特殊药品
        public string BuyerName { get; set; }//采购员姓名
        public string Buyercode { get; set; }//采购员工号
        public bool SelectFlag { get; set; }//勾选标识

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //商品统一价
    public class ScmPriceGoodunify
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Id { get; set; }//ID
        public string Compid { get; set; }//帐套
        public string Ownerid { get; set; }//货主id
        public string Saledeptid { get; set; }//部门id
        //public string Defaultdeptid { get; set; }//默认部门id
        public string Goodid { get; set; }//商品id
        public string Prc { get; set; }//公开含税销价
        public string Price { get; set; }//公开无税销价
        public string Stopflag { get; set; }//停用标识,00正常，99停用
        public string Stopflagname { get; set; }//停用标识名称
        public string Createuser { get; set; }//创建人员id
        public string Createusername { get; set; }//创建人员
        public string Createdate { get; set; }//创建时间
        public string Modifyuser { get; set; }//修改人员id
        public string Modifyusername { get; set; }//修改人员
        public string Modifydate { get; set; }//修改时间
        public string Audflag { get; set; }//审批标识
        public string Audflagname { get; set; }//审批标识名称
        public string Audstatus { get; set; }//审批状态
        public string Audstatusname { get; set; }//审批状态名称
        public string Lastaudtime { get; set; }//最后审批时间
        public string Costprc { get; set; }//含税成本销价,与考核毛利率2选1
        public string Costprice { get; set; }//无税成本销价
        public string Bargain { get; set; }//是否可议价,00否，10是
        public string Iscredit { get; set; }//价格类型,00现款价，10赊销价
        public string Costrate { get; set; }//考核毛利率,与含税成本价2选1
        public string Begindate { get; set; }//开始时间
        public string Enddate { get; set; }//结束时间
        public string Bottomprc { get; set; }//含税底价
        public string Bottomprice { get; set; }//无税底价
        public string Oriprc { get; set; }//初始维护含税价
        public string Lastprc { get; set; }//上个版本含税价
        public string Source { get; set; }//数据来源
        public string B2bdisplay { get; set; }//B2B是否显示价格
        public string B2bdisplayname { get; set; }//B2B是否显示价格名称
        public string Goods { get; set; }//商品代码
        public string Name { get; set; }//商品名称
        public string Outrate { get; set; }//税率
        public string Producer { get; set; }//生产厂家
        public string Spec { get; set; }//商品规格
        public string Sprdug { get; set; }//药品分类
        public bool SelectFlag { get; set; }//勾选标识

        public string Lastpurprc { get; set; }// 最新采购价
        public string Lastpurdate { get; set; }//最新采购时间

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }


    public class ScmPriceGoodtap
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Bacthid { get; set; }//批次号
        public string Goodtapid { get; set; }//记录id
        public string Compid { get; set; }//帐套
        public string Ownerid { get; set; }//货主id
        public string Saledeptid { get; set; }//部门id
        public string Goodid { get; set; }//商品id,来源于pub_waredict.goodid
        public string Hdrid { get; set; }//帽子ID
        public string Ismodifyexec { get; set; }//是否可以修改公开销价（是否可议价）
        public string Ismodifyexecname { get; set; }//是否可以修改公开销价（是否可议价）名称
        public string Pgdeliveryfeerate { get; set; }//加点率
        public string Prc { get; set; }//含税公开销价
        public string Price { get; set; }//无税公开销价
        public string Stopflag { get; set; }//停用标识,00正常，99停用
        public string Stopflagname { get; set; }//停用标识名称
        public string Createuser { get; set; }//创建人员
        public string Createusername { get; set; }//创建人名称
        public string Createdate { get; set; }//创建时间
        public string Modifyuser { get; set; }//修改人员
        public string Modifyusername { get; set; }//修改人员名称
        public string Modifydate { get; set; }//修改时间
        public string Audflag { get; set; }//审批标识
        public string Audflagname { get; set; }//审批标识名称
        public string Audstatus { get; set; }//审批状态
        public string Audstatusname { get; set; }//审批状态名称
        public string Lastaudtime { get; set; }//最后审批时间
        public string Costprctype { get; set; }//成本类型
        public string Costprctypename { get; set; }//成本类型名称
        public string Costprc { get; set; }//含税成本价,与考核毛利率2选1(建议含税考核成本价)
        public string Costprice { get; set; }//无税成本价(建议无税考核成本价)
        public string Costrate { get; set; }//建议考核毛利率,与含税成本价2选1
        public string Oriprc { get; set; }//初始维护价
        public string Lastprc { get; set; }//上个版本价
        public string Bottomprc { get; set; }//含税底价
        public string Bottomprice { get; set; }//无税底价
        public string Begindate { get; set; }//开始时间
        public string Enddate { get; set; }//结束时间
        public string Code { get; set; }//类型代码
        public string Name { get; set; }//类型名称
        public string Ghdeliveryfeerate { get; set; }//默认加点率
        public string Detailtype { get; set; }//定价客户类型
        public string Detailtypename { get; set; }//定价客户类型名称
        public string Grouptype { get; set; }//定价客户组分类
        public string Grouptypename { get; set; }//定价客户组分类名称
        public string Synctype { get; set; }//定价客户组同步分类
        public string Synctypename { get; set; }//,//定价客户组同步分类名称
        public bool SelectFlag { get; set; }//勾选标识
        public string Operatype { get; set; }//操作类型;1新增商品客户组定价,2修改商品客户组定价
        public string PreGoodtapid { get; set; }//原客户组id
        public string Goods { get; set; }//商品编码

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //----------------------------2018-8-22----------------
    public class ScmPriceExe
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Code { get; set; }//类型代码
        public string Name { get; set; }//类型名称
        public string Province { get; set; }//省代码
        public string Provincename { get; set; }//省
        public string City { get; set; }//城市代码
        public string Cityname { get; set; }//城市
        public string Area { get; set; }//区代码
        public string Areaname { get; set; }//区名称
        public string Region { get; set; }//省市区
        public string Cstcode { get; set; }//客户代码
        public string Cstname { get; set; }//客户名称
        public string Id { get; set; }//记录id
        public string Compid { get; set; }//帐套
        public string Ownerid { get; set; }//货主id
        public string Saledeptid { get; set; }//部门id
        public string Cstid { get; set; }//客户id
        public string Hdrid { get; set; }//客户组id
        public string Goodid { get; set; }//商品id
        public string Prc { get; set; }//含税公开销价
        public string Price { get; set; }//无税公开销价
        public string Bottomprc { get; set; }//含税底价
        public string Bottomprice { get; set; }//无税底价
        public string Costprc { get; set; }//含税成本销价,与考核毛利率2选1
        public string Costprice { get; set; }//无税成本销价
        public string Costrate { get; set; }//考核毛利率,与含税成本价2选1
        public string Begindate { get; set; }//开始时间
        public string Enddate { get; set; }//结束时间
        public string Goodtapid { get; set; }//商品帽子价ID
        public string Source { get; set; }//数据来源
        public string Type { get; set; }//前台处理类型，0新增价格，1修改价格，2移除价格
        public string Synctype { get; set; }//客户组同步类型
        public string Stopflag { get; set; }//停用标识,00正常，99停用
        public string Stopflagname { get; set; }//停用标识
        public string Createuser { get; set; }//创建人员id
        public string Createusername { get; set; }//创建人员
        public string Createdate { get; set; }//创建时间
        public string Modifyuser { get; set; }//修改人员id
        public string Modifyusername { get; set; }//修改人员
        public string Modifydate { get; set; }//修改时间
        public string Audflag { get; set; }//审批标识
        public string Audflagname { get; set; }//审批标识名称
        public string Audstatus { get; set; }//审批状态
        public string Audstatusname { get; set; }//审批状态名称
        public string Lastaudtime { get; set; }//审批时间
        public string Bargain { get; set; }//是否可议价,00否，10是
        public string Iscredit { get; set; }//价格类型,00现款价，10赊销价
        public string Oriprc { get; set; }//初始维护含税价
        public string Lastprc { get; set; }//上个版本含税价
        public string B2bdisplay { get; set; }//B2B是否显示价格
        public string B2bdisplayname { get; set; }//B2B是否显示价格名称
        public string SynDate { get; set; }//,//同步时间
        public bool SelectFlag { get; set; }//勾选标识
        public string GoodsCode { get; set; }//商品代码
        public string GoodsName { get; set; }//商品名称
        public string PrcPri { get; set; }//客户组分类优先级
        public string Outrate { get; set; }//税率
        public string Producer { get; set; }//生产厂家
        public string Spec { get; set; }//规格
        public string Sprdug { get; set; }//药品分类
        public string PreGoodtapid { get; set; }//原来的Goodtapid
        public string Detailtype { get; set; }
        public string Detailtypename { get; set; }
        public string Grouptype { get; set; }
        public string Grouptypename { get; set; }
        public string Ghsynctype { get; set; }
        public string Ghsynctypename { get; set; }
        public string Cmsclienttype { get; set; }//cms客户类型
        public string Cmsclienttypename { get; set; }//自维护客户类型

       


        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //部门下拉框
    public class GoodunifyDeptmenu
    {

        public string Compid { get; set; }//帐套
        public string Ownerid { get; set; }//货主id
        public string Saledeptid { get; set; }//部门id
        public string Deptname { get; set; }//部门名称

    }

    //异常视图
    public class ScmPriceExeWait
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Hdrcode { get; set; }//客户组代码
        public string Hdrname { get; set; }//客户组名称
        public string Province { get; set; }//省代码
        public string Provincename { get; set; }//省
        public string City { get; set; }//市代码
        public string Cityname { get; set; }//市
        public string Area { get; set; }//区代码
        public string Areaname { get; set; }//区
        public string Region { get; set; }//区域
        public string Cstcode { get; set; }//客户代码
        public string Cstname { get; set; }//客户名称
        public string Goods { get; set; }//商品代码
        public string Goodsname { get; set; }//商品名称
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//生产厂家
        public string Outrate { get; set; }//点率
        public string Sprdug { get; set; }//药品分类
        public string Id { get; set; }//ID
        public string Compid { get; set; }//帐套
        public string Ownerid { get; set; }//货主id
        public string Saledeptid { get; set; }//部门id
        public string Cstid { get; set; }//客户id
        public string Hdrid { get; set; }//客户组id
        public string Goodid { get; set; }//商品id
        public string Prc { get; set; }//公开含税销价
        public string Price { get; set; }//公开无税销价
        public string Bottomprc { get; set; }//含税底价
        public string Bottomprice { get; set; }//无税底价
        public string Costprc { get; set; }//含税成本销价,与考核毛利率2选1
        public string Costprice { get; set; }//无税成本销价
        public string Costrate { get; set; }//考核毛利率,与含税成本价2选1
        public string Begindate { get; set; }//开始时间
        public string Enddate { get; set; }//结束时间
        public string Goodtapid { get; set; }//商品客户组价ID
        public string Source { get; set; }//数据来源
        public string Type { get; set; }//前台处理类型，0新增价格，1修改价格，2移除价格
        public string Synctype { get; set; }//同步类型
        public string Stopflag { get; set; }//停用标识,00正常，99停用
        public string Stopflagname { get; set; }//停用标识,00正常，99停用
        public string Createuser { get; set; }//创建人
        public string Createusername { get; set; }//创建人
        public string Createdate { get; set; }//创建时间
        public string Modifyuser { get; set; }//修改人
        public string Modifyusername { get; set; }//修改人
        public string Modifydate { get; set; }//修改时间
        public string Audflag { get; set; }//审批标识
        public string Audflagname { get; set; }//审批标识
        public string Audstatus { get; set; }//审批状态
        public string Audstatusname { get; set; }//审批状态
        public string Lastaudtime { get; set; }//最后审批时间
        public string Bargain { get; set; }//是否可议价,00否，10是
        public string Iscredit { get; set; }//价格类型,00现款价，10赊销价
        public string Oriprc { get; set; }//初始维护含税价
        public string Lastprc { get; set; }//上个版本含税价
        public string B2bdisplay { get; set; }//B2B是否显示价格
        public string B2bdisplayname { get; set; }//B2B是否显示价格
        public string SynDate { get; set; }//同步时间
        public string TypeFlag { get; set; }//操作分类,0确认，1待同步
        public string TypeFlagname { get; set; }//操作分类,0确认，1待同步
        public string Origin { get; set; }//来源分类，0临时表，1正式表
        public string Originname { get; set; }//来源分类，0临时表，1正式表
        public string Ownchgid { get; set; }//
        public string Beneficiate { get; set; }//
        public string AffirmFlag { get; set; }//确认结论，0待确认，1启用，2弃用
        public string AffirmFlagname { get; set; }//确认结论，0待确认，1启用，2弃用
        public string AffirmBatchid { get; set; }//,//确认操作序号,与BATCHID共用序号
        public bool SelectFlag { get; set; }//勾选标识
        public string UnFlag { get; set; }//前台界面显示标识（00显示，99不显示）
        public string Cmsclienttype { get; set; }
        public string Cmsclienttypename { get; set; }
        public string Buyercode { get; set; }//责任采购员工号
        public string Buyername { get; set; }//责任采购员名称

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }


    //责任采购员下拉框
    public class PubEmpBuyer
    {

        public string Compid { get; set; }//帐套
        public string Ownerid { get; set; }//货主id
        public string Empid { get; set; }//人员id
        public string Empcode { get; set; }//人员工号
        public string Empname { get; set; }//人员名称
        public string Roleid { get; set; }//人员角色id

    }

    //----------------------------2018-8-21---------------
    //销售部门 价格查询
    public class SalePriceEx
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Code { get; set; }//类型代码
        public string Name { get; set; }//类型名称
        public string Province { get; set; }//省代码
        public string Provincename { get; set; }//省
        public string City { get; set; }//城市代码
        public string Cityname { get; set; }//城市
        public string Area { get; set; }//区代码
        public string Areaname { get; set; }//区名称
        public string Region { get; set; }//省市区
        public string Cstcode { get; set; }//客户代码
        public string Cstname { get; set; }//客户名称
        public string Id { get; set; }//记录id
        public string Compid { get; set; }//帐套
        public string Ownerid { get; set; }//货主id
        public string Saledeptid { get; set; }//部门id
        public string Cstid { get; set; }//客户id
        public string Hdrid { get; set; }//客户组id
        public string Goodid { get; set; }//商品id
        public string Prc { get; set; }//含税公开销价
        public string Price { get; set; }//无税公开销价
        public string Bottomprc { get; set; }//含税底价
        public string Bottomprice { get; set; }//无税底价
        public string Costprc { get; set; }//含税成本销价,与考核毛利率2选1
        public string Costprice { get; set; }//无税成本销价
        public string Costrate { get; set; }//考核毛利率,与含税成本价2选1
        public bool SelectFlag { get; set; }//勾选标识
        public string GoodsName { get; set; }//商品名称
        public string PrcPri { get; set; }//客户组分类优先级
        public string Outrate { get; set; }//税率
        public string Producer { get; set; }//生产厂家
        public string Spec { get; set; }//规格
        public string Sprdug { get; set; }//药品分类
        public string PreGoodtapid { get; set; }//原来的Goodtapid
        public string Detailtype { get; set; }
        public string Detailtypename { get; set; }
        public string Grouptype { get; set; }
        public string Grouptypename { get; set; }
        public string Ghsynctype { get; set; }
        public string Ghsynctypename { get; set; }
        public string Cmsclienttype { get; set; }//cms客户类型
        public string Cmsclienttypename { get; set; }//自维护客户类型
        public string Packnum { get; set; }//件包装
        public string Ratifier { get; set; }//国药准字
        public string Goods { get; set; }//商品代码
        public string Lastsoprc { get; set; }//最近合同价
        public string Buyer { get; set; }//采购员
        public string Planbuyer { get; set; }//计划员
        public string Msg { get; set; }//价格信息
        public string Prcresultcode { get; set; }//价格信息
       
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //----------------------------2018-11-20---------------
    //价格屏蔽
    public class PriceShield
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Id { get; set; }//序号
        public string ProdName { get; set; }//商务团队
        public string Goods { get; set; }//商品代码
        public string GoodsName { get; set; }//商品名称
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//厂家
        public string CreateUserCode { get; set; }//创建人工号
        public string CreateUserName { get; set; }//创建人姓名
        public string CreateTime { get; set; }//创建时间

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //价格屏蔽 商品明细
    public class GoodsDetails
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string GoodId { get; set; }//商品ID
        public string Goods { get; set; }//商品代码
        public string GoodsName { get; set; }//商品名称
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//厂家
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }





}
