using System;
using System.Collections.Generic;

using System.Text;

namespace PriceManager
{
    class APDto_B2BTools
    {
    }
    public class BillQuotationTemp
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        //---------2018-10-15---新增包装字段
        public string Batchid { get; set; }//批次号
        public string Compid { get; set; }//帐套ID
        public string Ownerid { get; set; }//货主ID
        public string Saledeptid { get; set; }//部门ID
        public string Cstid { get; set; }//客户ID
        public string ExcelSeqid { get; set; }//EXCEL内序号
        public string Gengoods { get; set; }//外部商品编码
        public string Genspec { get; set; }//外部品名规格
        public string Genproducer { get; set; }//外部商品厂家
        public string Goodbar { get; set; }//条形码
        public string Ratifier { get; set; }//国药准字
        public string Hopeprc { get; set; }//客户期望价
        public string Empid { get; set; }//员工ID
        public string Createtime { get; set; }//创建时间
        public string Relatid { get; set; }//对码关系ID
        public string Goodid { get; set; }//CMS商品ID
        public string Transrate { get; set; }//转换比
        public string Goods { get; set; }//CMS商品码
        public string Goodname { get; set; }//CMS商品名
        public string Spec { get; set; }//CMS商品规格
        public string Producer { get; set; }//CMS商品厂家
        public string Bizqty { get; set; }//可用库存（本货主库存）
        public string Ownchgqty { get; set; }//可调库存
        public string Recordmark { get; set; }//备案原因
        public string Recordtime { get; set; }//备案时间
        public string Prc { get; set; }//公开销价
        public string Bottomprc { get; set; }//底价
        public string Costprc { get; set; }//成本价
        public string Prcresultcode { get; set; }//查价返回code
        public string Prcmsg { get; set; }//查价返回msg
        public string Quotation { get; set; }//系统报价
        public string Lastsotime { get; set; }//最近一个合同时间
        public string Quotationsource { get; set; }//报价来源
        public string Bargain { get; set; }//是否允许议价
        public string Quotationhis { get; set; }//历史报价
        public string Quotationhistime { get; set; }//历史报价时间
        public string Costrate { get; set; }//报价毛利率
        public string Buyer { get; set; }//责任采购员
        public string Planbuyer { get; set; }//责任计划员
        public string Abc { get; set; }//ABC分类
        public string Goodtype { get; set; }//商品大类
        public string Lastsoprc { get; set; }//最近合同价
        public string Autoflag { get; set; }//自动对码
        public string SelectFlag { get; set; }//勾选标识
        public string PackNum { get; set; }//包装

        public string PromotionFlag { get; set; }//促销活动标志



        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //////////-----------------------------2018-07-24-------------

    //外部商品对码
    public class GenCstGood
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Relatid { get; set; }//关系ID
        public string Compid { get; set; }//帐套ID
        public string Ownerid { get; set; }//货主ID
        public string Saledeptid { get; set; }//销售部门
        public string Iotype { get; set; }//接口类型ID
        public string Cstid { get; set; }//我方客户ID
        public string Cstcode { get; set; }//我方客户代码
        public string Dname { get; set; }//客户名称
        public string Goodid { get; set; }//商品ID
        public string Goods { get; set; }//商品名称
        public string Namespec { get; set; }//cms品名品规
        public string Producer { get; set; }//cms生产厂家
        public string Gengoods { get; set; }//对方商品代码
        public string Genspec { get; set; }//对方商品名称
        public string Genproducer { get; set; }//对方生产厂家
        public string Transrate { get; set; }//转换比
        public string Goodbar { get; set; }//条形码
        public string Ratifier { get; set; }//国药准字
        public string Autoflag { get; set; }//自动对码
        public string Createuser { get; set; }//创建人工号
        public string Createusername { get; set; }// 创建人名称
        public string Createdate { get; set; }//创建日期
        public string Sendaddrcode { get; set; }//对方送货地址代码
        public string Gencstcode { get; set; }//对方客户代码
        public string Gencstname { get; set; }//对方客户名称
        public string Sendaddr { get; set; }//对方送货地址名称
        public string Sendaddrid { get; set; }//我发送货地址ID
        public bool SelectFlag { get; set; }//勾选标识

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //客户对码删除临时表
    public class DelTempGenCstGood
    {

        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public int Batchid { get; set; }//批次号
        public int Relatid { get; set; }//关系ID
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }
    ////////客户对码Excel -----2018-07-25--------------------  
    public class InoutGenCstGoodXlstemp
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Batchid { get; set; }//批次号
        public string ExcelSeqid { get; set; }//Excel表序号
        public string Compid { get; set; }//账套ID
        public string Ownerid { get; set; }//货主ID
        public string Cstcode { get; set; }//客户代码
        public string Gengoods { get; set; }//外部商品代码
        public string Goods { get; set; }//cms商品代码
        public string Genspec { get; set; }//外部商品品名品规
        public string Transrate { get; set; }//转换比
        public string Genproducer { get; set; }//外部生产厂家
        public string Goodbar { get; set; }//条形码
        public string Ratifier { get; set; }//国药准字
        public string Rowstate { get; set; }//导入结果
        public string Rowmsg { get; set; }//导入结果相关信息
        public string Empid { get; set; }//,//员工ID
        public bool SelectFlag { get; set; }//勾选标识

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }
    ////////备案品种  -----2018-07-30--------------------  
    public class GenGoodRecord
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Goodrecordid { get; set; }//备案品种ID
        public string Compid { get; set; }//账套ID
        public string Ownerid { get; set; }//货主ID
        public string Cstid { get; set; }//客户ID
        public string Cstcode { get; set; }//客户代码
        public string Dname { get; set; }//客户名称
        public string Gengoods { get; set; }//商品外码
        public string Genspec { get; set; }//外部品名品规
        public string Genproducer { get; set; }//外部厂家
        public string Goodbar { get; set; }//外部条形码
        public string Ratifier { get; set; }//国药准字
        public string Recordmark { get; set; }//备案原因
        public string Recordtime { get; set; }//最后备案时间
        public string Createuser { get; set; }//创建人ID
        public string Createusername { get; set; }//创建人姓名
        public string Createdate { get; set; }//创建时间
        public string Modifyuser { get; set; }//修改人ID
        public string Modifyusername { get; set; }//修改人姓名
        public bool SelectFlag { get; set; }//勾选标识

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //备案品种删除临时表
    public class DelTempGenGoodRecord
    {

        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public int Batchid { get; set; }//批次号
        public int Goodrecordid { get; set; }//关系ID
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    ////////备案品种Excel  -----2018-8-1--------------------  
    public class InoutGenGoodRecordXlstemp
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Batchid { get; set; }//批次号
        public string ExcelSeqid { get; set; }//序号
        public string Compid { get; set; }//账套ID
        public string Ownerid { get; set; }//货主ID
        public string Cstcode { get; set; }//客户代码
        public string Gengoods { get; set; }//商品外码
        public string Genspec { get; set; }//外部品名品规
        public string Transrate { get; set; }//转换比
        public string Genproducer { get; set; }//生产厂家
        public string Goodbar { get; set; }//条形码
        public string Ratifier { get; set; }//国药准字
        public string Recordmark { get; set; }//备案原因
        public string Rowstate { get; set; }//导入状态
        public string Rowmsg { get; set; }//结果相关信息
        public string Empid { get; set; }//员工ID 

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    ///////////////  合同导入  -----2018-8-8-----
    ///////////////  字段增加修改  -----2018-10-22-----
    public class InoutGenCmsbillXlstemp
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Batchid { get; set; }//批次号
        public string ExcelSeqid { get; set; }//序号
        public string Compid { get; set; }//账套ID
        public string Ownerid { get; set; }//货主ID
        public string Cstid { get; set; }//客户ID
        public string Empid { get; set; }//员工ID
        public string Gengoods { get; set; }//客户商品代码
        public string Goods { get; set; }//cms商品代码
        public string Planprc { get; set; }//计划单价
        public string Plancount { get; set; }//计划下单量
        public string Pmark { get; set; }//批卡备注
        public string Amark { get; set; }//审批备注
        public string Genaddresscode { get; set; }//地址外码
        public string Cmsaddresscode { get; set; }//地址cms码
        public string Checkstate { get; set; }//检查状态
        public string Checkmsg { get; set; }//检查信息
        public string Importstate { get; set; }//导入状态
        public string Importmsg { get; set; }//导入信息
        public string Goodtype { get; set; }//商品大类
        public string Saledeptid { get; set; }//部门ID
        public string Createdate { get; set; }//创建时间
        public string Goodid { get; set; }//商品ID
        public string Prc { get; set; }//公开销价
        public string Price { get; set; }//无税销价
        public string Bottomprc { get; set; }//含税底价
        public string Bottomprice { get; set; }//无税底价
        public string Costprc { get; set; }//含税成本价
        public string Costprice { get; set; }//无税成本价
        public string Prcresultcode { get; set; }//查价状态
        public string Prcmsg { get; set; }//查价信息
        public string Quotation { get; set; }//系统报价
        public string Bargain { get; set; }//是否可议价
        public string Importprc { get; set; }//cms导入价
        //-----------2018-10-22-----新增字段
        public string GoodName { get; set; }//cms品名
        public string Spec { get; set; }//cms规格
        public string BizownQty { get; set; }//总库存
        public string AllowQty { get; set; }//满足效期库存
        public string HdrmMark { get; set; }//总备注
        public string AllowBatNum { get; set; }//满足效期批号数
        public string GoodSaleCheck { get; set; }//三天内销售过品种
        public string ColdChain { get; set; }//是否冷链
        public string LastSoTime { get; set; }//最近一个合同时间
        public string ChooseFlag { get; set; }//是否被选中


        public bool SelectFlag { get; set; }//勾选标识

        public string PromotionFlag { get; set; }//促销活动标志

        //-----------2018-12-26-----新增字段
        public string InvoiceType { get; set; }//发票类型
        public string AttchInv { get; set; }//开发票类型
        public string InvPostFlag { get; set; }//暂缓开票
        public string Producer { get; set; }//厂家

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }
    //////////////对码报价新增字段  -----2018-10-15--
    public class AllPrcinfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Prc { get; set; }//公开销价
        public string Bottomprc { get; set; }//底价
        public string Quotation { get; set; }//系统报价
        public string Lastsoprc { get; set; }//最近合同价
        public string Lastsotime { get; set; }//最近合同时间
        public string Costprc{ get; set; }//
        public string Prcresultcode { get; set; }//
        public string Prcmsg { get; set; }//


        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //////////////库存信息  -----2018-10-24--
    public class BizInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string OwnerName { get; set; }//所有者
        public string Lotno { get; set; }//批号
        public string BatchNo { get; set; }//批次号
        public string EmpName { get; set; }//责任采购员
        public string PrdDate { get; set; }//生产日期
        public string EndDate { get; set; }//效期
        public string AlloQty { get; set; }//可分配数量
        public string UnAlloQty { get; set; }//不可分配数量
        public string Description { get; set; }//库存状态
        public string SalbillType { get; set; }//是否可销

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }
    //////////////收货要求配置信息  -----2018-10-31--
    public class CstCheckConfig
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string OwnerId { get; set; }//货主ID
        public string CompId { get; set; }//账套ID
        public string Cstid { get; set; }//客户ID
        public string CstCode { get; set; }//客户代码
        public string CstName { get; set; }//客户姓名
        public string ManuFacture { get; set; }//生产日期
        public string ExtMark { get; set; }//特殊条件
        public string ModifyUser { get; set; }//修改人
        public string ModifyTime { get; set; }//修改时间

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }
    //删除临时表
    public class DelTempCstCheckConfig
    {

        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public int Batchid { get; set; }//批次号
        public string Cstid { get; set; }//客户ID
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //对接订单处理 实体类
    public class OrderButtDto
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string InfCstName { get; set; }//接口来源
        public string OrderId { get; set; }//订单ID
        public string OrderTime { get; set; }//下单时间
        public string CstCode { get; set; }//客户CMS码
        public string CstName { get; set; }//客户姓名
        public string Goods { get; set; }//商品CMS码
        public string GoodName { get; set; }//商品名称
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//厂家
        public string GenGoods { get; set; }//商品外码
        public string PlanPrc { get; set; }//计划单价
        public string PlanCount { get; set; }//计划数量
        public string Quotation { get; set; }//系统报价
        public string GoodSaleCheck { get; set; }//三天内销售过品种
        public string BizOwnQty { get; set; }//库存数
        public string AllowQty { get; set; }//满足效期库存
        public string AllowBatNum { get; set; }//满足效期批号数
        public string CheckMsg { get; set; }//检查信息
        public string ColdChain { get; set; }//是否冷链
        public string Prc { get; set; }//公开销价
        public string Pmark { get; set; }//批卡备注
        public string Amark { get; set; }//审批备注
        public string CmsAddressCode { get; set; }//地址cms码
        public string Addr_Line_1 { get; set; }//送货地址
        public string Bargain { get; set; }//是否可议价
        public string BottomPrc { get; set; }//含税底价
        public string GoodType { get; set; }//商品大类
        public string CheckState { get; set; }//检查状态
        public string ImportState { get; set; }//导入状态
        public string ImportMsg { get; set; }//导入信息
        public string CstMsg { get; set; }//客户留言
        public string ChooseFlag { get; set; }//是否被选中
        public string Batchid { get; set; }//批次号
        public string ExcelSeqid { get; set; }//序号
        public string GoodId { get; set; }//商品ID
        public string CstId { get; set; }//客户ID
        public string CompId { get; set; }//账套ID
        public string OwnerId { get; set; }//用户ID
        public string SaleDeptId { get; set; }//部门ID
        public string Price { get; set; }//无税销价
        public string BottomPrice { get; set; }//无税底价
        public string CostPrc { get; set; }//含税成本价
        public string CostPrice { get; set; }//无税成本价
        public string Prcresultcode { get; set; }//查价状态
        public string PrcMsg { get; set; }//价格信息


        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //对接订单处理 实体类
    public class NewBatchId
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string BatchMainId { get; set; }//新ID
        public string BatchId { get; set; }//批次ID
        public string ExcelSeqid { get; set; }//序号

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //////////-----------------------------2018-12-10-------------

    //外部商品对码
    public class PromotionInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Compid { get; set; }//帐套ID
        public string Ownerid { get; set; }//货主ID
        public string PocName { get; set; }//政策名称
        public string GoodName { get; set; }//商品名称
        public string Goods { get; set; }//商品代码
        public string GoodId { get; set; }//商品ID
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//厂家
        public string CstCode { get; set; }//客户代码
        public string CstId { get; set; }//客户ID
        public string CstName { get; set; }//客户名称
        public string BeginTime { get; set; }//开始时间
        public string EndTime { get; set; }//结束时间
        public string Policy { get; set; }//活动内容
        public string Remark { get; set; }//备注
        public string LModifyUser { get; set; }//最近修改人
        public string LModifyTime { get; set; }//最近修改时间
        public string Id { get; set; }//标志符号
        public string CreateTime { get; set; }//创建时间


        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }
    //查询商品信息
    public class GoodInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string GoodName { get; set; }//商品名称
        public string Goods { get; set; }//商品代码
        public string GoodId { get; set; }//商品ID
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//厂家

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //查询活动对象信息
    public class CstInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string CstCode { get; set; }//商品名称
        public string CstName { get; set; }//商品代码
        public string CstId { get; set; }//商品ID

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //活动信息导入
    public class PromoExcelInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Batchid { get; set; }//批次号
        public string ExcelSeqid { get; set; }//Excel表序号
        public string Compid { get; set; }//账套ID
        public string Ownerid { get; set; }//货主ID
        public string EmpName { get; set; }//员工姓名
        public string PocName { get; set; }//政策名称
        public string Goods { get; set; }//商品代码
        public string CstCode { get; set; }//客户代码
        public string BeginTime { get; set; }//开始时间
        public string EndTime { get; set; }//结束时间
        public string Policy { get; set; }//活动内容
        public string Remark { get; set; }//备注
        public string CreateTime { get; set; }//创建时间
        public string CheckState { get; set; }//检查状态
        public string CheckMsg { get; set; }//检查信息
        public string ImportState { get; set; }//导入状态
        public string ImportMsg { get; set; }//导入信息

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //促销信息
    public class SalePromoInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string BeginTime { get; set; }//开始时间
        public string EndTime { get; set; }//结束时间
        public string Policy { get; set; }//活动内容
        public string Remark { get; set; }//备注
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //促销信息
    public class ContractInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Batchid { get; set; }//批次号
        public string ExcelSeqid { get; set; }//序号
        public string Goods { get; set; }//cms商品代码
        public string GoodName { get; set; }//商品名称
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//厂家
        public string Importprc { get; set; }//计划单价
        public string Plancount { get; set; }//计划数量
        public string Importmsg { get; set; }//导入信息
        public string Importstate { get; set; }//导入状态        
        public string Pmark { get; set; }//批卡备注
        public string Amark { get; set; }//审批备注
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }
    //下拉框ID
    public class CbId
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Payid { get; set; }//
        public string Transid { get; set; }//
        public string Kdfs { get; set; }//

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //校验字段 （合同自动生成模块）
    public class CheckInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Batchid { get; set; }//批次号
        public string ExcelSeqid { get; set; }//序号
        public string CheckPara { get; set; }//校验参数串
        public string CheckRet { get; set; }//校验状态
        public string ImportMsg { get; set; }//校验信息
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //导入合同生成Cms字段 （合同自动生成模块）
    public class CommitInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Batchid { get; set; }//批次号
        public string ExcelSeqid { get; set; }//序号
        public string ImportParas { get; set; }//导入参数串1
        public string ImpoetRets { get; set; }//导入状态1
        public string ImportParass { get; set; }//导入参数串2
        public string ImpoetRetss { get; set; }//导入状态2
        public string CheckPara { get; set; }//校验参数串
        public string CheckRet { get; set; }//校验状态
        public string ImportMsg { get; set; }//校验信息
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }
    //库存对接 ---2019-1-9---------------
    public class StockDockInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Goods { get; set; }//商品代码
        public string Name { get; set; }//商品名称
        public string Spec { get; set; }//商品规格
        public string Producer { get; set; }//厂家
        public string BizOwnQty { get; set; }//总库存
        public string AbutJoinQty { get; set; }//平台可用库存
        public string TopLimit { get; set; }//上限
        public string LowerLimit { get; set; }//下限
        public string ModifyTime { get; set; }//修改时间
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //系统库存对接 主数据 ---2019-1-16---------------
    public class MasterDataInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string AccountId { get; set; }//账套ID
        public string AccountName { get; set; }//平台名称
        public string AccountPercent { get; set; }//库存百分比
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //系统库存对接 仓库信息 -----2019-1-16-----------
    public class DepotInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string OwnerId { get; set; }//货主ID
        public string DeptCode { get; set; }//仓库代码
        public string DeptName { get; set; }//仓库名称
        public string Percent { get; set; }//百分比
        public string StorageId { get; set; }//仓库Id
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }


    //系统库存对接 配置信息 -----2019-1-16-----------
    public class ConfigInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string SpecialId { get; set; }//数据标识
        public string OwnerId { get; set; }//货主ID
        public string StorageId { get; set; }//仓库ID
        public string DeptCode { get; set; }//仓库代码
        public string DeptName { get; set; }//仓库名称
        public string Goods { get; set; }//商品代码
        public string GoodId { get; set; }//商品ID
        public string Name { get; set; }//商品名称
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//厂家
        public string Percent { get; set; }//百分比
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //系统库存对接 商品信息 -----2019-1-16-----------
    public class GoodsInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string AccountId { get; set; }//账套ID
        public string Goods { get; set; }//商品代码
        public string GoodId { get; set; }//商品ID
        public string Name { get; set; }//商品名称
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//厂家
        public string CurQty { get; set; }//可用库存
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //系统库存对接 仓库新增信息 -----2019-1-16-----------
    public class Dept
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string DeptId { get; set; }//仓库ID
        public string DeptCode { get; set; }//仓库代码
        public string DeptName { get; set; }//仓库名称
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //系统库存对接 特殊配置 新增信息 -----2019-1-16-----------
    public class ConfigGood
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
        public string Name { get; set; }//商品名称
        public string Spec { get; set; }//规格
        public string Producer { get; set; }//厂家
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //删除临时表
    public class DelSystemDock
    {

        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public int Batchid { get; set; }//批次ID
        public int Relatid { get; set; }//关系ID
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //系统库存对接信息导入
    public class SysDockXlsInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string Batchid { get; set; }//批次号
        public string ExcelSeqid { get; set; }//Excel表序号
        public string Compid { get; set; }//账套ID
        public string Ownerid { get; set; }//货主ID
        public string AccountId { get; set; }//部门ID
        public string Goods { get; set; }//商品代码
        public string GoodId { get; set; }//商品ID
        public string CheckState { get; set; }//检查状态
        public string CheckMsg { get; set; }//检查信息
        public string ImportState { get; set; }//导入状态
        public string ImportMsg { get; set; }//导入信息
        public string CreateUser { get; set; }//创建人
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }



}

