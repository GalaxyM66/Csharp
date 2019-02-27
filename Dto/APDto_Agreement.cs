using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceManager
{
    class APDto_Agreement
    {
    }
    public class AgreeProducerInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string ProdId { get; set; }//供应商ID
        public string ProdCode { get; set; }//供应商编号
        public string ProdName { get; set; }//商务团队
        public string Import { get; set; }//厂家性质
        public string BuyerName { get; set; }//采购员
        public string Manager { get; set; }//采购经理
        public string MiddleMan { get; set; }//销售对接人
        public string AgreeType { get; set; }//协议性质
        public string CreateUser { get; set; }//创建人
        public string CreateTime { get; set; }//创建时间
        public string ModifyUser { get; set; }//修改人
        public string ModifyTime { get; set; }//修改时间
        public string BeginDate { get; set; }//协议启动时间
        public string CompId { get; set; }//账套Id
        public string OwnerId { get; set; }//货主Id

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //删除临时表
    public class DelTemp
    {

        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public int Batchid { get; set; }//批次号
        public string  RelateId { get; set; }//关系ID
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //协议客户
    public class AgreeClient
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string AgreementId { get; set; }//协议ID
        public string ProdId { get; set; }//供应商ID
        public string ProdCode { get; set; }//供应商编号
        public string ProdName { get; set; }//商务团队
        public string Import { get; set; }//厂家性质
        public string BuyerName { get; set; }//采购员
        public string Manager { get; set; }//采购经理
        public string MiddleMan { get; set; }//销售对接人
        public string AgreeType { get; set; }//协议性质
        public string YearNum { get; set; }//年份
        public string CstId { get; set; }//客户ID
        public string CstCode { get; set; }//客户编码
        public string CstName { get; set; }//客户姓名
        public string Saller { get; set; }//销售代表
        public string SallManager { get; set; }//销售经理
        public string SallLeader { get; set; }//销售副总
        public string AgreeLevel { get; set; }//协议级别
        public string LastValues { get; set; }//去年销售额
        public string ForecastValues { get; set; }//今年预计销售额
        public string LastUpStream { get; set; }//去年对应上游
        public string TarGet { get; set; }//目标分级
        public string CstIntention { get; set; }//客户意向
        public string CstIntentionTime { get; set; }//客户意见修改时间
        public string ProdIntention { get; set; }//厂家意向
        public string Dynamics { get; set; }//签约动态
        public string FinalChannel { get; set; }//最终签约渠道
        public string ThisYearValues { get; set; }//今年产生销售额
        public string HopeValues { get; set; }//意向协议量
        public string Seal { get; set; }//是否盖章
        public string Onfile { get; set; }//是否协议存档
        public string FinalValues { get; set; }//最终协议量
        public string BeginDate { get; set; }//协议启动时间
        public string CreateUser { get; set; }//创建人
        public string CreateTime { get; set; }//创建时间
        public string ModifyUser { get; set; }//修改人
        public string ModifyTime { get; set; }//修改时间
        public string ModifySaller { get; set; }//修改销售代表
        public string ModifySallerTime { get; set; }//销售最近修改时间
        public string ModifyService { get; set; }//修改服务
        public string ModifyServiceTime { get; set; }//修改服务时间
        public string Mark { get; set; }//备注
        public string CompId { get; set; }//账套Id
        public string OwnerId { get; set; }//货主Id
        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }
    //协议维护查询信息
    public class SearchClientInfo
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string CstId{ get; set; }//客户ID
        public string CstCode { get; set; }//客户编码
        public string CstName { get; set; }//客户姓名
        public string SallManager { get; set; }//销售经理
        public string SallLeader { get; set; }//销售副总
        public string Saller { get; set; }//销售代表

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }
    //协议客户信息导入
    public class ClientXlsInfo
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
        public string SaleDeptid { get; set; }//部门ID
        public string Empid { get; set; }//员工ID
        public string YearNum { get; set; }//年份
        public string ProdId { get; set; }//供应商id
        public string AgreeLevel { get; set; }//协议级别
        public string CstCode { get; set; }//客户代码
        public string CstName { get; set; }//客户名称
        public string LastUpStream { get; set; }//去年对应上游
        public string ForeCastValues { get; set; }//今年厂家预测量
        public string TarGet { get; set; }//目标分级
        public string LastValues { get; set; }//去年销售额
        public string Saller { get; set; }//销售代表
        public string SallManager { get; set; }//销售经理
        public string SallLeader { get; set; }//销售副总
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

    //客户名称匹配
    public class CstNameMatch
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
        public string Empid { get; set; }//员工ID
        public string SaleDeptid { get; set; }//部门ID
        public string CstName { get; set; }//客户名称
        public string CstCode { get; set; }//客户编码
        public string CheckMsg { get; set; }//导出说明

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }
   
    //销售反馈
    public class SaleFeed
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string YearNum { get; set; }//年份
        public string AgreementId { get; set; }//协议ID
        public string ProdId { get; set; }//供应商ID
        public string ProdCode { get; set; }//供应商编号
        public string ProdName { get; set; }//商务团队
        public string TarGet { get; set; }//目标分级
        public string CstCode { get; set; }//客户编码
        public string CstName { get; set; }//客户姓名
        public string SallLeader { get; set; }//销售副总
        public string SallManager { get; set; }//销售经理
        public string Saller { get; set; }//销售代表
        public string CstIntention { get; set; }//客户意向
        public string ProdIntention { get; set; }//厂家意向
        public string Dynamics { get; set; }//签约动态
        public string FinalChannel { get; set; }//最终签约渠道
        public string ThisYearValues { get; set; }//今年产生销售额
        public string HopeValues { get; set; }//意向协议量
        public string Import { get; set; }//厂家性质
        public string BuyerName { get; set; }//采购员
        public string Manager { get; set; }//采购经理
        public string AgreeType { get; set; }//协议性质
        public string MiddleMan { get; set; }//销售对接人
        public string AgreeLevel { get; set; }//协议级别
        public string LastValues { get; set; }//去年销售额
        public string LastUpStream { get; set; }//去年对应上游
        public string ForecastValues { get; set; }//今年预计销售额
        public string BeginDate { get; set; }//协议启动时间
        public string Seal { get; set; }//是否盖章
        public string Onfile { get; set; }//是否协议存档
        public string FinalValues { get; set; }//最终协议量

        //-----2018-11-16-----------新增字段--------------
        public string Mark { get; set; }//备注
        public string ModifyBuyerTime { get; set; }//采购最近修改时间

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }
    //协议报表
    public class AgreeReport
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string YearNum { get; set; }//年份
        public string ProdName { get; set; }//商务团队
        public string SallManager { get; set; }//销售经理
        public string Saller { get; set; }//销售代表
        public string MiddleMan { get; set; }//销售对接人
        public string SallLeader { get; set; }//销售副总
        public string BuyerName { get; set; }//采购员
        public string AgreeType { get; set; }//协议性质
        public string BeginDate { get; set; }//协议启动时间
        public string BiBao { get; set; }//必保客户数
        public string ZhengQu { get; set; }//争取客户数
        public string QiTa { get; set; }//其他客户数
        public string BbZq { get; set; }//必保+争取
        public string Xsfk { get; set; }//销售反馈客户数
        public string YxHx { get; set; }//意向可签客户数
        public string BbYxHx { get; set; }//必保签订客户数
        public string QdHx { get; set; }//已签订客户数
        public string Fkl { get; set; }//反馈率
        public string Yxl { get; set; }//意向率
        public string BbQdl { get; set; }//必保签订率
        public string Qdl { get; set; }//签订率
        public string ZqHx { get; set; }//争取客户签订数
        public string ZqQdl { get; set; }//争取客户签订率


        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }

    //员工表
    public class EmpInfos
    {
        public Object TagPtr
        {
            get
            {
                return this;
            }
        }
        public string EmpCode { get; set; }//工号
        public string EmpName { get; set; }//姓名
        public string EmpId { get; set; }//系统ID

        public object Clone()
        {
            return this.MemberwiseClone();  //浅复制
        }
    }



}
