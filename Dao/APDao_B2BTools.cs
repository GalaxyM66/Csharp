using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;
using Oracle.DataAccess.Client;
using System.Windows.Forms;
using System.Data;
using Thrift.Transport;
using Thrift.Server;
using Thrift.Protocol;

namespace PriceManager
{
    class APDao_B2BTools : MySQLHelper
    {


        //----------------------服务连接-----------------------------------------------------------------------
        private PriceService.Client client = null;
        TSocket socket = null;
        TTransport transport = null;
        TProtocol protocol = null;
        private String Server = "";
        private int Port = 0;
        //ContactControl ccData = null;
        Dictionary<string, KVPair> kpMap = new Dictionary<string, KVPair>();

        public int ConnectServer(String server, int port)
        {
            Server = server;
            Port = port;
            try
            {
                socket = new TSocket(server, port);
                socket.TcpClient.NoDelay = true;
                transport = new TBufferedTransport(socket, 1 * 1024 * 1024);
                protocol = new TCompactProtocol(transport);
                client = new PriceService.Client(protocol);
                transport.Open();

                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("连接服务失败,请检查连接{0}:{1} Msg:{2}", server, port, e.Message));
                return -1;
            }



        }
        public int ReConnectServer()
        {
            try
            {
                socket = new TSocket(Server, Port);
                socket.TcpClient.NoDelay = true;
                transport = new TBufferedTransport(socket, 1 * 1024 * 1024);
                protocol = new TCompactProtocol(transport);
                client = new PriceService.Client(protocol);
                transport.Open();

                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("连接服务失败,请检查连接{0}:{1} Msg:{2}", Server, Port, e.Message));
                return -1;
            }



        }
        public void DisConnect()
        {
            transport.Close();
        }

        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 客户代码查询存储过程
        /// </summary>
        /// <param name="cstcode">客户代码</param>
        /// <param name="retinfo">存储过程返回数据</param>
        /// <returns></returns>
        public int PCstCode(string cstcode, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            //OracleCommand selCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_BILL_QUOTE.P_GET_CSTINFO";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_CSTCODE",OracleDbType.Varchar2,ParameterDirection.Input),
                    //new OracleParameter("AN_USER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("OUT_CSTID",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = cstcode;
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 200;
                parameters[2].Size = 200;
                parameters[3].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[2].Value.ToString().Trim();
                retinfo.msg = parameters[3].Value.ToString().Trim();
                retinfo.result = parameters[1].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {


                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }

        /// <summary>
        /// 导入excel表调用存储过程
        /// </summary>
        /// <param name="List"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int PGetBillData(SortableBindingList<BillQuotationTemp> List, SPRetInfo retinfo)
        {
            string selectSql = "SELECT F_GET_SEQ('BATCHID') as SEQ FROM dual";

            string insertSql = "INSERT INTO BILL_QUOTATION_TEMP(batchid, compid, ownerid,"
            + " cstid, excel_seqid, gengoods, genspec, genproducer, goodbar, ratifier, hopeprc, empid, createtime)"
            + " VALUES(:batchid, :compid, :ownerid, :cstid, :excel_seqid, :gengoods,"
            + ":genspec, :genproducer, :goodbar, :ratifier, :hopeprc, :empid, :createtime) ";
            OracleCommand spCmd = null;
            OracleCommand selCmd = null;
            OracleCommand insetCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            string seqID = "0";
            //string date = "";
            //int recCount = 0;
            try
            {

                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    seqID = res["SEQ"].ToString().Trim();

                res.Close();
                res.Dispose();

                selCmd.Dispose();
                selCmd = null;


                trans = Conn_datacenter_cmszh.BeginTransaction();

                insetCmd = Conn_datacenter_cmszh.CreateCommand();
                insetCmd.Connection = Conn_datacenter_cmszh;
                insetCmd.CommandType = System.Data.CommandType.Text;
                insetCmd.CommandText = insertSql.ToString();
                insetCmd.CommandTimeout = 3600;

                insetCmd.Transaction = trans;
                int recCount = 0;
                foreach (BillQuotationTemp selected in List)
                {
                    recCount++;

                    insetCmd.Parameters.Clear();

                    insetCmd.Parameters.Add("batchid", Int32.Parse(seqID));
                    insetCmd.Parameters.Add("compid", Int32.Parse(selected.Compid));
                    insetCmd.Parameters.Add("ownerid", Int32.Parse(selected.Ownerid));
                    //insetCmd.Parameters.Add("saledeptid", "");
                    insetCmd.Parameters.Add("cstid", Int32.Parse(selected.Cstid));
                    insetCmd.Parameters.Add("excel_seqid", Int32.Parse(selected.ExcelSeqid));
                    if (selected.Gengoods != "")
                    {
                        insetCmd.Parameters.Add("gengoods", selected.Gengoods);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("gengoods", "");
                    }
                    if (selected.Genspec != "")
                    {
                        insetCmd.Parameters.Add("genspec", selected.Genspec);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("genspec", "");
                    }
                    if (selected.Genproducer != "")
                    {
                        insetCmd.Parameters.Add("genproducer", selected.Genproducer);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("genproducer", "");
                    }
                    if (selected.Goodbar != "")
                    {
                        insetCmd.Parameters.Add("goodbar", selected.Goodbar);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("goodbar", "");
                    }
                    if (selected.Ratifier != "")
                    {
                        insetCmd.Parameters.Add("ratifier", selected.Ratifier);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("ratifier", "");
                    }
                    if (selected.Hopeprc != "")
                    {
                        insetCmd.Parameters.Add("hopeprc", double.Parse(selected.Hopeprc));
                    }
                    else
                    {
                        insetCmd.Parameters.Add("hopeprc", null);
                    }

                    insetCmd.Parameters.Add("empid", Int32.Parse(selected.Empid));
                    insetCmd.Parameters.Add("createtime", DateTime.Now);



                    insetCmd.ExecuteNonQuery();
                }

                trans.Commit();
                trans.Dispose();
                trans = null;

                insetCmd.Dispose();
                insetCmd = null;

                /*
                 * AI_SEQ_ID NUMBER, --程序前台ID
                   --AI_BARCODE_TYPE NUMBER; --条码类型
                   OUT_RTD    OUT NUMBER, --返回值，可以判断执行是否成功
                   OUT_ARMSG  OUT VARCHAR2, --返回执行说明
                   OUT_RESULT OUT VARCHAR2 --返回执行代码
                */
                if (recCount < 1)
                {
                    retinfo.num = "0";
                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return 0;
                }

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_BILL_QUOTE.P_GET_BILL_DATA";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OP_TYPE",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output),
                    //new OracleParameter("OUT_RESULT",OracleDbType.Varchar2,ParameterDirection.Output)

                };


                parameters[0].Value = long.Parse(seqID);
                parameters[1].Value = -1;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[2].Value = 0;
                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[4].Value.ToString().Trim();
                    retinfo.result = seqID;
                }
                else
                {
                    retinfo.msg = parameters[4].Value.ToString().Trim();
                }
                //retinfo.result = parameters[4].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (selCmd != null)
                    selCmd.Dispose();

                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
                if (insetCmd != null)
                    insetCmd.Dispose();
            }

            return retCode;
        }


        /// <summary>
        /// 查询goodid存在的数据
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        public SortableBindingList<BillQuotationTemp> GetGoodidExistList(string batchid)
        {
            SortableBindingList<BillQuotationTemp> infoList = new SortableBindingList<BillQuotationTemp>();

            string sql = "SELECT batchid ,GOODID,t.compid,t.ownerid,t.saledeptid,t.cstid,"
            + "t.excel_seqid FROM BILL_QUOTATION_TEMP t WHERE BATCHID =:batchid AND GOODID IS NOT NULL";


            OracleCommand cmd = null;
            try
            {

                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;

                cmd.CommandText = sql.ToString();
                cmd.Parameters.Add("batchid", batchid);


                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    BillQuotationTemp info = new BillQuotationTemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.Goodid = res["GOODID"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();

                    infoList.Add(info);

                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infoList;
        }

        /// <summary>
        /// 调用Mysql查询价格
        /// </summary>
        /// <param name="InputInfo"></param>
        /// <param name="OutputInfo"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int PGetB2BPrc(BillQuotationTemp InputInfo, BillQuotationTemp OutputInfo)
        {

            MySqlCommand spCmd = null;
            int retCode = -1;
            try
            {
                //从库 ----2019-1-7---
                if (ConnectionTests() == -1)//打开mysql从库连接
                {
                    MessageBox.Show("连接从库失败!");
                    Environment.Exit(0);
                }
                spCmd = connections.CreateCommand();
                spCmd.Connection = connections;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "P_GET_B2BPRC";
                spCmd.CommandTimeout = 3600;
                MySqlParameter[] parameters ={
                    new MySqlParameter("@in_compid",MySqlDbType.Int64),
                    new MySqlParameter("@in_ownerid",MySqlDbType.Int64),
                    new MySqlParameter("@in_saledeptid",MySqlDbType.Int64),
                    new MySqlParameter("@in_cstid",MySqlDbType.Int64),//`` 
                    new MySqlParameter("@in_goodid",MySqlDbType.Int64),

                    new MySqlParameter("@out_prcid",MySqlDbType.Int64),//5 
                    new MySqlParameter("@out_prc",MySqlDbType.Decimal),//6.
                    new MySqlParameter("@out_price",MySqlDbType.Decimal),//7
                    new MySqlParameter("@out_bottomprc",MySqlDbType.Decimal),//8.
                    new MySqlParameter("@out_bottomprice",MySqlDbType.Decimal),//9
                    new MySqlParameter("@out_costprc",MySqlDbType.Decimal),//10. 
                    new MySqlParameter("@out_costprice",MySqlDbType.Decimal),//11
                    new MySqlParameter("@out_costrate",MySqlDbType.Decimal),//12
                    new MySqlParameter("@out_hdrid",MySqlDbType.Int64),//13
                    new MySqlParameter("@out_hdrname",MySqlDbType.VarChar),//14
                    new MySqlParameter("@out_prcsourc",MySqlDbType.VarChar),// 15
                    new MySqlParameter("@out_resultcode",MySqlDbType.Int64),//16.
                    new MySqlParameter("@out_msg",MySqlDbType.VarChar),//17.
                                             };

                parameters[0].Value = Int64.Parse(InputInfo.Compid);
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Int64.Parse(InputInfo.Ownerid);
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = Int64.Parse(InputInfo.Saledeptid);
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = Int64.Parse(InputInfo.Cstid);
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = Int64.Parse(InputInfo.Goodid);
                parameters[4].Direction = ParameterDirection.Input;

                parameters[5].Size = 200;
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6].Size = 200;//
                parameters[6].Direction = ParameterDirection.Output;
                parameters[7].Size = 200;
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8].Size = 200;//
                parameters[8].Direction = ParameterDirection.Output;
                parameters[9].Size = 2048;
                parameters[9].Direction = ParameterDirection.Output;
                parameters[10].Size = 200;//
                parameters[10].Direction = ParameterDirection.Output;
                parameters[11].Size = 200;
                parameters[11].Direction = ParameterDirection.Output;
                parameters[12].Size = 200;
                parameters[12].Direction = ParameterDirection.Output;
                parameters[13].Size = 200;
                parameters[13].Direction = ParameterDirection.Output;
                parameters[14].Size = 2048;
                parameters[14].Direction = ParameterDirection.Output;
                parameters[15].Size = 2048;
                parameters[15].Direction = ParameterDirection.Output;
                parameters[16].Size = 200;//
                parameters[16].Direction = ParameterDirection.Output;
                parameters[17].Size = 2048;//
                parameters[17].Direction = ParameterDirection.Output;

                foreach (MySqlParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                OutputInfo.Prcresultcode = parameters[16].Value.ToString().Trim();
                if (OutputInfo.Prcresultcode == "1")
                {
                    OutputInfo.Prc = parameters[6].Value.ToString().Trim();
                    OutputInfo.Bottomprc = parameters[8].Value.ToString().Trim();
                    OutputInfo.Costprc = parameters[10].Value.ToString().Trim();
                    OutputInfo.Prcmsg = parameters[17].Value.ToString().Trim();
                }
                else
                {
                    OutputInfo.Prcmsg = parameters[17].Value.ToString().Trim();
                }

                spCmd.Dispose();
                spCmd = null;
                retCode = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }

        /// <summary>
        /// 将价格写回中间表
        /// </summary>
        /// <param name="List"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int PSetBillPrc(BillQuotationTemp info, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            //OracleCommand selCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_BILL_QUOTE.P_SET_BILL_PRC";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_PRC",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_BOTTOMPRC",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_COSTPRC",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_PRCRESULTCODE",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("IN_PRCMSG",OracleDbType.Varchar2,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Int64.Parse(info.Batchid);
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Value = Int64.Parse(info.ExcelSeqid);
                parameters[2].Value = Decimal.Parse(info.Prc);
                parameters[3].Value = Decimal.Parse(info.Bottomprc);
                parameters[4].Value = Decimal.Parse(info.Costprc);
                parameters[5].Value = double.Parse(info.Prcresultcode);
                parameters[6].Value = info.Prcmsg;
                parameters[7].Size = 200;
                parameters[8].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[7].Value.ToString().Trim();
                retinfo.msg = parameters[8].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {


                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }

        /// <summary>
        /// 调用BillQuotation
        /// </summary>
        /// <param name="info"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int PSetBillQuotation(string Batchid, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            //OracleCommand selCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_BILL_QUOTE.P_SET_BILL_QUOTATION";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OP_TYPE",OracleDbType.Int64,ParameterDirection.Input),


                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };


                parameters[0].Value = Int64.Parse(Batchid);
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Value = -1;
                parameters[2].Value = 0;
                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                //parameters[5].Value = Int64.Parse(info.Prcresultcode);
                //parameters[6].Value = info.Prcmsg;
                //parameters[7].Size = 200;
                //parameters[8].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                retinfo.msg = parameters[4].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {


                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }

        /// <summary>
        /// 查询处理完成的中间表
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        public SortableBindingList<BillQuotationTemp> GetBillQuotationList(string batchid)
        {
            SortableBindingList<BillQuotationTemp> infoList = new SortableBindingList<BillQuotationTemp>();

            string sql = "SELECT batchid, compid, ownerid, saledeptid, cstid, excel_seqid, gengoods,"
            + "genspec, genproducer, goodbar, ratifier, hopeprc, empid, createtime, relatid, goodid, "
            + "transrate, goods, goodname, spec, producer, bizqty, ownchgqty, recordmark, recordtime, prc,"
            + "bottomprc, costprc, prcresultcode, prcmsg, quotation, lastsotime, quotationsource, bargain,"
            + "quotationhis, quotationhistime, costrate, buyer, planbuyer, abc, goodtype, "
            + "lastsoprc, autoflag, SelectFlag, Packnum,PromotionFlag from bill_quotation_temp t WHERE batchid =:batchid ";

            OracleCommand cmd = null;
            try
            {

                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("batchid", batchid);

                cmd.CommandText = sql.ToString();
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    BillQuotationTemp info = new BillQuotationTemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();

                    info.Gengoods = res["gengoods"].ToString().Trim();
                    info.Genspec = res["genspec"].ToString().Trim();
                    info.Genproducer = res["genproducer"].ToString().Trim();
                    info.Goodbar = res["goodbar"].ToString().Trim();
                    info.Ratifier = res["ratifier"].ToString().Trim();
                    info.Hopeprc = res["hopeprc"].ToString().Trim();
                    info.Empid = res["empid"].ToString().Trim();

                    info.Createtime = res["createtime"].ToString().Trim();
                    info.Relatid = res["relatid"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Transrate = res["transrate"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Goodname = res["goodname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();

                    info.Producer = res["producer"].ToString().Trim();
                    info.Bizqty = res["bizqty"].ToString().Trim();
                    info.Ownchgqty = res["ownchgqty"].ToString().Trim();
                    info.Recordmark = res["recordmark"].ToString().Trim();
                    info.Recordtime = res["recordtime"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();

                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Prcresultcode = res["prcresultcode"].ToString().Trim();
                    info.Prcmsg = res["prcmsg"].ToString().Trim();
                    info.Quotation = res["quotation"].ToString().Trim();
                    info.Lastsotime = res["lastsotime"].ToString().Trim();
                    info.Quotationsource = res["quotationsource"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();

                    info.Quotationhis = res["quotationhis"].ToString().Trim();
                    info.Quotationhistime = res["quotationhistime"].ToString().Trim();
                    info.Costrate = res["costrate"].ToString().Trim();
                    info.Buyer = res["buyer"].ToString().Trim();
                    info.Planbuyer = res["planbuyer"].ToString().Trim();
                    info.Abc = res["abc"].ToString().Trim();
                    info.Goodtype = res["goodtype"].ToString().Trim();
                    info.Lastsoprc = res["lastsoprc"].ToString().Trim();
                    info.Autoflag = res["autoflag"].ToString().Trim();
                    info.SelectFlag = res["SelectFlag"].ToString().Trim();
                    info.PackNum = res["Packnum"].ToString().Trim();
                    info.PromotionFlag = res["PromotionFlag"].ToString().Trim();

                    infoList.Add(info);

                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infoList;
        }

        /// <summary>
        /// 修改系统报价存储过程
        /// </summary>
        /// <param name="cstcode"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int PBqHis(int Batchid, int ExcelSeqid, double Quotation, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            //OracleCommand selCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_BILL_QUOTE.P_BQ_HIS";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_QUOTATION",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Batchid;
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Value = ExcelSeqid;
                parameters[2].Value = Quotation;
                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                retinfo.msg = parameters[4].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {


                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }

        ////////-----2018-07-24--------------------
        //查询客户对码
        public SortableBindingList<GenCstGood> GetGenCstGoodList(Dictionary<string, string> sqlkeydict)
        {
            SortableBindingList<GenCstGood> infoList = new SortableBindingList<GenCstGood>();
            string sql = "select relatid, compid, ownerid, saledeptid, iotype, cstid, cstcode, dname, goodid,"
            + "goods, namespec, producer, gengoods, genspec, genproducer, transrate, goodbar, ratifier,"
            + "createuser, createusername, createdate, sendaddrcode, gencstcode, gencstname, sendaddr, "
            + "sendaddrid,autoflag from v_gen_cst_good t where t.compid=:compid and t.ownerid=:ownerid $ ";
            //连接oracle数据库
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                string whereStr = "";
                //遍历Dictionary中的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                        }

                    }

                }
                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                cmd.Parameters.Add("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.Add(kv.Key, kv.Value);
                        }
                    }
                }
                string s = sql;
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    GenCstGood info = new GenCstGood();
                    info.Relatid = res["relatid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Iotype = res["iotype"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Cstcode = res["cstcode"].ToString().Trim();
                    info.Dname = res["dname"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Namespec = res["namespec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.Gengoods = res["gengoods"].ToString().Trim();
                    info.Genspec = res["genspec"].ToString().Trim();
                    info.Genproducer = res["genproducer"].ToString().Trim();
                    info.Transrate = res["transrate"].ToString().Trim();
                    info.Goodbar = res["goodbar"].ToString().Trim();
                    info.Ratifier = res["ratifier"].ToString().Trim();
                    info.Createuser = res["createuser"].ToString().Trim();
                    info.Createusername = res["createusername"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Sendaddrcode = res["sendaddrcode"].ToString().Trim();
                    info.Gencstcode = res["gencstcode"].ToString().Trim();
                    info.Gencstname = res["gencstname"].ToString().Trim();
                    info.Sendaddr = res["sendaddr"].ToString().Trim();
                    info.Sendaddrid = res["sendaddrid"].ToString().Trim();
                    info.Autoflag = res["autoflag"].ToString().Trim();
                    info.SelectFlag = true;
                    infoList.Add(info);
                }
                res.Close();
                res = null;
                cmd.Dispose();
                cmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
            }
            return infoList;
        }

        //对码新增时候，填完cms商品码后，自动带出部门编码
        public int DeptidForGoods(string Goods, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CSTGOOD.P_DEFAULT_DEPT";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters = {
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_GOODS",OracleDbType.Varchar2,ParameterDirection.Input),

                    new OracleParameter("OUT_SALEDEPTID",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                };
                parameters[0].Value = Int64.Parse(Properties.Settings.Default.COMPID);
                parameters[1].Value = Int64.Parse(Properties.Settings.Default.OWNERID);
                parameters[2].Value = Goods;

                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                parameters[5].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }
                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[4].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[5].Value.ToString().Trim();

                }
                else
                {
                    retinfo.msg = parameters[5].Value.ToString().Trim();

                }
                retinfo.result = parameters[3].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;
        }

        //新增OR修改客户对码数据
        public int AddOrUpdateGenCstGood(string Saledeptid, string Cstcode,
            string Goods, string Gengoods, string Genspec, string Genproducer,
            string Transrate, string Goodbar, string Ratifier, int IN_RELATID, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            //string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            //string insertSql = "INSERT INTO DELTEMP_GEN_CST_GOOD()VALUES()";

            try
            {
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CSTGOOD.P_GEN_ADDMODIFY";
                spCmd.CommandTimeout = 3600;
                //向存储过程中添加输入输出
                OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_SALEDEPTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_IOTYPE",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTCODE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GOODS",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GENGOODS",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GENSPEC",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GENPRODUCER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_TRANSRATE",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_GOODBAR",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_RATIFIER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_RELATID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(Properties.Settings.Default.COMPID);
                parameters[1].Value = Int64.Parse(Properties.Settings.Default.OWNERID);
                parameters[2].Value = Int64.Parse(Saledeptid);
                parameters[3].Value = Int64.Parse("1");
                parameters[4].Value = Cstcode;
                parameters[5].Value = Goods;
                parameters[6].Value = Gengoods;
                parameters[7].Value = Genspec;
                parameters[8].Value = Genproducer;
                parameters[9].Value = Int64.Parse(Transrate);
                parameters[10].Value = Goodbar;
                parameters[11].Value = Ratifier;
                parameters[12].Value = SessionDto.Empid;
                parameters[13].Value = IN_RELATID;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/

                parameters[14].Size = 8;
                parameters[15].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[14].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[15].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[15].Value.ToString().Trim();
                }
                //retinfo.result = parameters[4].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;
        }

        //删除客户对码
        public int DeleteGenCstGood(SortableBindingList<DelTempGenCstGood> List, SPRetInfo retinfo)
        {
            string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            string insertSql = "insert into DELTEMP_GEN_CST_GOOD(BATCHID,RELATID)values(:Batchid,:Relatid)";//插入中间表

            OracleCommand selCmd = null;
            OracleCommand insertCmd = null;
            OracleCommand spCmd = null;
            OracleTransaction trans = null;

            int retCode = -1;
            string sqlID = "0";

            try
            {
                //查询函数获取批次号
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    sqlID = res["SEQ"].ToString().Trim();
                res.Close();
                res.Dispose();
                selCmd.Dispose();
                selCmd = null;

                trans = Conn_datacenter_cmszh.BeginTransaction();
                //插入中间表
                insertCmd = Conn_datacenter_cmszh.CreateCommand();
                insertCmd.Connection = Conn_datacenter_cmszh;
                insertCmd.CommandType = System.Data.CommandType.Text;
                insertCmd.CommandText = insertSql.ToString();
                insertCmd.CommandTimeout = 3600;
                insertCmd.Transaction = trans;
                int recCount = 0;

                foreach (DelTempGenCstGood selected in List)
                {
                    recCount++;

                    insertCmd.Parameters.Clear();

                    insertCmd.Parameters.Add("Batchid", int.Parse(sqlID));
                    insertCmd.Parameters.Add("Relatid", selected.Relatid);

                    insertCmd.ExecuteNonQuery();
                }
                trans.Commit();
                trans.Dispose();
                trans = null;

                insertCmd.Dispose();
                insertCmd = null;

                if (recCount < 1)
                {
                    retinfo.num = "0";
                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return 0;
                }

                //执行存储过程,进行删除
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CSTGOOD.P_GEN_DEL";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
            };
                parameters[0].Value = Int64.Parse(sqlID);

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[1].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[2].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[2].Value.ToString().Trim();
                }
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
                if (trans != null)
                {
                    trans.Rollback();
                    trans.Dispose();
                    trans = null;
                }
            }
            return retCode;
        }

        //统一改码的方法
        public int UnifiedUpdateCode(SortableBindingList<GenCstGood> UpdateGenCstGoodsList, string NewGoods, SPRetInfo retinfo)
        {
            string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            string insertSql = "insert into DELTEMP_GEN_CST_GOOD(BATCHID,RELATID)values(:Batchid,:Relatid)";//插中间表


            OracleCommand selCmd = null;
            OracleCommand insertCmd = null;
            OracleCommand spCmd = null;
            OracleTransaction trans = null;

            int retCode = -1;
            string sqlID = "0";
            string oldGoods = UpdateGenCstGoodsList[0].Goods;
            try
            {
                //查询函数获取批次号
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    sqlID = res["SEQ"].ToString().Trim();
                res.Close();
                res.Dispose();
                selCmd.Dispose();
                selCmd = null;

                trans = Conn_datacenter_cmszh.BeginTransaction();
                //插入中间表
                insertCmd = Conn_datacenter_cmszh.CreateCommand();
                insertCmd.Connection = Conn_datacenter_cmszh;
                insertCmd.CommandType = System.Data.CommandType.Text;
                insertCmd.CommandText = insertSql.ToString();
                insertCmd.CommandTimeout = 3600;
                insertCmd.Transaction = trans;
                int recCount = 0;

                foreach (GenCstGood selected in UpdateGenCstGoodsList)
                {
                    recCount++;

                    insertCmd.Parameters.Clear();

                    insertCmd.Parameters.Add("Batchid", int.Parse(sqlID));
                    insertCmd.Parameters.Add("Relatid", selected.Relatid);


                    insertCmd.ExecuteNonQuery();
                }
                trans.Commit();
                trans.Dispose();
                trans = null;

                insertCmd.Dispose();
                insertCmd = null;

                if (recCount < 1)
                {
                    retinfo.num = "0";
                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return 0;
                }


                //执行储存过程，进行统一改码
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CSTGOOD.P_UNIFY_MODIFY";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OLD_GOODS",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_NEW_GOODS",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
            };

                parameters[0].Value = Int64.Parse(sqlID);
                parameters[1].Value = Int64.Parse(Properties.Settings.Default.COMPID);
                parameters[2].Value = oldGoods;
                parameters[3].Value = NewGoods;

                parameters[4].Size = 8;
                parameters[5].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[4].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[5].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[5].Value.ToString().Trim();
                }
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
                if (trans != null)
                {
                    trans.Rollback();
                    trans.Dispose();
                    trans = null;
                }
            }
            return retCode;

        }
        ////////-----2018-07-26--------------------
        /// <summary>
        /// GenCstGood  导入excel表调用存储过程
        /// </summary>
        /// <param name="List"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public SortableBindingList<InoutGenCstGoodXlstemp> PGetGenCstGoodData(SortableBindingList<InoutGenCstGoodXlstemp> List,
            SPRetInfo retinfo)
        {
            SortableBindingList<InoutGenCstGoodXlstemp> InoutGenCstGoodXlstempList =
                new SortableBindingList<InoutGenCstGoodXlstemp>();
            SortableBindingList<InoutGenCstGoodXlstemp> InoutList = new SortableBindingList<InoutGenCstGoodXlstemp>();

            string selectSql = "SELECT F_GET_SEQ('BATCHID') as SEQ FROM dual";
            string insertSql = "INSERT INTO INOUT_GEN_CST_GOOD_XLSTEMP(batchid,excel_seqid,compid,ownerid,"
                + "cstcode,gengoods,goods,genspec,transrate,genproducer,goodbar,ratifier,rowstate,rowmsg,"
                + "empid)VALUES(:Batchid,:Excel_seqid,:Compid,:Ownerid,:Cstcode,:Gengoods,:Goods,:Genspec,"
                + ":Transrate,:Genproducer,:Goodbar,:Ratifier,:Rowstate,:Rowmsg,:Empid)";

            string selSql = "SELECT * FROM INOUT_GEN_CST_GOOD_XLSTEMP  WHERE batchid =:batchid";

            OracleCommand spCmd = null;
            OracleCommand selCmd = null;
            OracleCommand insetCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            string seqID = "0";

            //查询批次号
            try
            {
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    seqID = res["SEQ"].ToString().Trim();

                res.Close();
                res.Dispose();

                selCmd.Dispose();
                selCmd = null;

                //将信息插入中间表
                trans = Conn_datacenter_cmszh.BeginTransaction();

                insetCmd = Conn_datacenter_cmszh.CreateCommand();
                insetCmd.Connection = Conn_datacenter_cmszh;
                insetCmd.CommandType = System.Data.CommandType.Text;
                insetCmd.CommandText = insertSql.ToString();
                insetCmd.CommandTimeout = 3600;

                insetCmd.Transaction = trans;
                int recCount = 0;
                foreach (InoutGenCstGoodXlstemp selected in List)
                {
                    recCount++;

                    insetCmd.Parameters.Clear();

                    insetCmd.Parameters.Add("Batchid", Int32.Parse(seqID));
                    insetCmd.Parameters.Add("Excel_seqid", Int32.Parse(selected.ExcelSeqid));
                    insetCmd.Parameters.Add("Compid", Int32.Parse(selected.Compid));
                    insetCmd.Parameters.Add("Ownerid", Int32.Parse(selected.Ownerid));
                    insetCmd.Parameters.Add("Cstcode", selected.Cstcode);
                    insetCmd.Parameters.Add("Gengoods", selected.Gengoods);
                    insetCmd.Parameters.Add("Goods", selected.Goods);

                    if (selected.Genspec != "")
                    {
                        insetCmd.Parameters.Add("Genspec", selected.Genspec);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Genspec", "");
                    }
                    if (selected.Transrate != "")
                    {
                        insetCmd.Parameters.Add("Transrate", Int32.Parse(selected.Transrate));
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Transrate", "");
                    }
                    if (selected.Genproducer != "")
                    {
                        insetCmd.Parameters.Add("Genproducer", selected.Genproducer);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Genproducer", "");
                    }
                    if (selected.Goodbar != "")
                    {
                        insetCmd.Parameters.Add("Goodbar", selected.Goodbar);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Goodbar", "");
                    }
                    if (selected.Ratifier != "")
                    {
                        insetCmd.Parameters.Add("Ratifier", selected.Ratifier);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Ratifier", "");
                    }
                    if (selected.Rowstate != "")
                    {
                        insetCmd.Parameters.Add("Rowstate", selected.Rowstate);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Rowstate", "");
                    }
                    if (selected.Rowmsg != "")
                    {
                        insetCmd.Parameters.Add("Rowmsg", selected.Rowmsg);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Rowmsg", "");
                    }
                    insetCmd.Parameters.Add("Empid", Int32.Parse(selected.Empid));
                    //MessageBox.Show("=="+ insetCmd.Parameters[0].Value);
                    insetCmd.ExecuteNonQuery();
                }

                trans.Commit();
                trans.Dispose();
                trans = null;

                insetCmd.Dispose();
                insetCmd = null;

                /*
                 * AI_SEQ_ID NUMBER, --程序前台ID
                   --AI_BARCODE_TYPE NUMBER; --条码类型
                   OUT_RTD    OUT NUMBER, --返回值，可以判断执行是否成功
                   OUT_ARMSG  OUT VARCHAR2, --返回执行说明
                   OUT_RESULT OUT VARCHAR2 --返回执行代码
                */
                if (recCount < 1)
                {
                    retinfo.num = "0";
                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return null;
                }

                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CSTGOOD.P_EXCEL_IMPORT";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                };


                parameters[0].Value = Int64.Parse(seqID);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[1].Value.ToString();
                retinfo.msg = parameters[2].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

                //再次查询此表
                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.Parameters.Add("batchid", seqID);
                seCmd.CommandTimeout = 3600;

                OracleDataReader ress = seCmd.ExecuteReader();
                foreach (InoutGenCstGoodXlstemp info in List)
                {
                    if (ress.Read())
                        info.Batchid = ress["batchid"].ToString().Trim();
                    info.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    info.Compid = ress["compid"].ToString().Trim();
                    info.Ownerid = ress["ownerid"].ToString().Trim();
                    info.Cstcode = ress["cstcode"].ToString().Trim();
                    info.Gengoods = ress["gengoods"].ToString().Trim();
                    info.Goods = ress["goods"].ToString().Trim();
                    info.Genspec = ress["genspec"].ToString().Trim();
                    info.Transrate = ress["transrate"].ToString().Trim();
                    info.Genproducer = ress["genproducer"].ToString().Trim();
                    info.Goodbar = ress["goodbar"].ToString().Trim();
                    info.Ratifier = ress["ratifier"].ToString().Trim();
                    info.Rowstate = ress["rowstate"].ToString().Trim();
                    info.Rowmsg = ress["rowmsg"].ToString().Trim();
                    info.Empid = ress["empid"].ToString().Trim();
                    InoutList.Add(info);
                }

                ress.Close();
                ress.Dispose();

                seCmd.Dispose();
                seCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (selCmd != null)
                    selCmd.Dispose();

                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();

                if (insetCmd != null)
                    insetCmd.Dispose();

                if (seCmd != null)
                    seCmd.Dispose();


            }
            return InoutList;
        }

        //调用服务查询对码18-07-27
        public List<CmsSku> MatchSku(String text, string ownerid)
        {
            List<CmsSku> list = null;
            try
            {
                list = client.QuerySku(text, ownerid);
            }
            catch (ReturnMessage e)
            {
                MessageBox.Show(e.RetMessage);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return list;
        }

        //调用服务查询对码18-08-03
        public List<CmsSku> QuerySkuMatch(string extSkuCode, string cstID, string ownerid, string matchText)
        {
            List<CmsSku> list = null;
            try
            {
                list = client.QuerySkuMatch(extSkuCode, cstID, ownerid, matchText);
            }
            catch (ReturnMessage e)
            {
                MessageBox.Show(e.RetMessage);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return list;
        }
        //删除对码18-08-03
        public int PGenDel(string Relid, SPRetInfo retinfo)
        {
            string selectSql = "SELECT F_GET_SEQ('BATCHID') as SEQ FROM dual";

            string insertSql = "INSERT INTO DELTEMP_GEN_CST_GOOD(batchid, RELATID)"
            + " VALUES(:batchid, :RELATID) ";
            OracleCommand spCmd = null;
            OracleCommand selCmd = null;
            OracleCommand insetCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            string seqID = "0";
            //string date = "";

            try
            {
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    seqID = res["SEQ"].ToString().Trim();

                res.Close();
                res.Dispose();

                selCmd.Dispose();
                selCmd = null;


                trans = Conn_datacenter_cmszh.BeginTransaction();

                insetCmd = Conn_datacenter_cmszh.CreateCommand();
                insetCmd.Connection = Conn_datacenter_cmszh;
                insetCmd.CommandType = System.Data.CommandType.Text;
                insetCmd.CommandText = insertSql.ToString();
                insetCmd.CommandTimeout = 3600;

                insetCmd.Transaction = trans;
                int recCount = 0;

                insetCmd.Parameters.Clear();

                insetCmd.Parameters.Add("batchid", Int32.Parse(seqID));
                insetCmd.Parameters.Add("RELATID", Int32.Parse(Relid));

                recCount = insetCmd.ExecuteNonQuery();


                trans.Commit();
                trans.Dispose();
                trans = null;

                insetCmd.Dispose();
                insetCmd = null;

                /*
                 * AI_SEQ_ID NUMBER, --程序前台ID
                   --AI_BARCODE_TYPE NUMBER; --条码类型
                   OUT_RTD    OUT NUMBER, --返回值，可以判断执行是否成功
                   OUT_ARMSG  OUT VARCHAR2, --返回执行说明
                   OUT_RESULT OUT VARCHAR2 --返回执行代码
                */
                if (recCount < 0)
                {
                    retinfo.num = "0";
                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return 0;
                }


                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CSTGOOD.P_GEN_DEL";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    //new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    //new OracleParameter("IN_GOODS",OracleDbType.Varchar2,ParameterDirection.Input),
                    //new OracleParameter("OUT_SALEDEPTID",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = int.Parse(seqID);
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[1].Value.ToString().Trim();
                retinfo.msg = parameters[2].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {


                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }



        //调用存储过程查询部门18-07-31
        public int PDefaultDept(string Goods, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "pg_gen_cstgood.p_default_dept";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_GOODS",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("OUT_SALEDEPTID",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = int.Parse(Properties.Settings.Default.COMPID);
                parameters[1].Value = int.Parse(Properties.Settings.Default.OWNERID);
                parameters[2].Value = Goods;
                parameters[3].Size = 48;
                parameters[4].Size = 8;
                parameters[5].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[4].Value.ToString().Trim();
                retinfo.msg = parameters[5].Value.ToString().Trim();
                retinfo.result = parameters[3].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {


                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }
        //----------2018-10-15-------------
        //调用存储过程查询货主库存,可调库存
        public int Qty(string Goodsid, string deptId, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "p_get_ALLO_QTY";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_SALEDEPTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_GOODID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("OUT_BIZQTY",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_OWNCHGQTY",OracleDbType.Int64,ParameterDirection.Output)

                };
                parameters[0].Value = int.Parse(Properties.Settings.Default.COMPID);
                parameters[1].Value = int.Parse(Properties.Settings.Default.OWNERID);
                parameters[2].Value = int.Parse(deptId);
                parameters[3].Value = int.Parse(Goodsid);
                parameters[4].Size = 2048;
                parameters[5].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[4].Value.ToString().Trim();
                retinfo.msg = parameters[5].Value.ToString().Trim();
                retinfo.result = parameters[3].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {


                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }


        //新增对码
        public int PGenAddmodify(string Saledeptid, string cstcode, string goods, string gengoods,
            string genspec, string genproducer, string transrate, string goodbar, string ratifier,
            SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CSTGOOD.P_GEN_ADDMODIFY";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_SALEDEPTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_IOTYPE",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTCODE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GOODS",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GENGOODS",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GENSPEC",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GENPRODUCER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_TRANSRATE",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_GOODBAR",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_RATIFIER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_RELATID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = int.Parse(Properties.Settings.Default.COMPID);
                parameters[1].Value = int.Parse(Properties.Settings.Default.OWNERID);
                parameters[2].Value = int.Parse(Saledeptid);
                parameters[3].Value = 1;
                parameters[4].Value = cstcode;
                parameters[5].Value = goods;
                parameters[6].Value = gengoods;
                parameters[7].Value = genspec;
                parameters[8].Value = genproducer;
                parameters[9].Value = int.Parse(transrate);
                parameters[10].Value = goodbar;
                parameters[11].Value = ratifier;
                parameters[12].Value = int.Parse(SessionDto.Empid);
                parameters[13].Value = 0;
                parameters[14].Size = 8;
                parameters[15].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[14].Value.ToString().Trim();
                retinfo.msg = parameters[15].Value.ToString().Trim();
                //retinfo.result = parameters[3].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {


                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }
        //---------------2018-10-15---------------GetBillData
        public int SetBillData(BillQuotationTemp MatchInfo, SPRetInfo retinfo) {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_BILL_QUOTE.P_GET_BILL_DATA";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OP_TYPE",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = int.Parse(MatchInfo.Batchid);
                parameters[1].Value = int.Parse(MatchInfo.ExcelSeqid);
                parameters[2].Value = 1;

                parameters[3].Size = 8;
                parameters[4].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                retinfo.msg = parameters[4].Value.ToString().Trim();
                //retinfo.result = parameters[3].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            } finally {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }




            return retCode;
        }

        //----------2018-10-15-----------GetB2BPrcs
        public AllPrcinfo GetB2BPrcs(BillQuotationTemp MatchInfo, string goodid, string saledeptid) {
            AllPrcinfo info = new AllPrcinfo();
            MySqlCommand spCmd = null;
            int retCode = -1;
            try
            {
                spCmd = connection.CreateCommand();
                spCmd.Connection = connection;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "P_GET_B2BPRC";
                spCmd.CommandTimeout = 3600;
                MySqlParameter[] parameters ={
                    new MySqlParameter("@in_compid",MySqlDbType.Int64),
                    new MySqlParameter("@in_ownerid",MySqlDbType.Int64),
                    new MySqlParameter("@in_saledeptid",MySqlDbType.Int64),
                    new MySqlParameter("@in_cstid",MySqlDbType.Int64),//`` 
                    new MySqlParameter("@in_goodid",MySqlDbType.Int64),

                    new MySqlParameter("@out_prcid",MySqlDbType.Int64),//5 
                    new MySqlParameter("@out_prc",MySqlDbType.Decimal),//6.
                    new MySqlParameter("@out_price",MySqlDbType.Decimal),//7
                    new MySqlParameter("@out_bottomprc",MySqlDbType.Decimal),//8.
                    new MySqlParameter("@out_bottomprice",MySqlDbType.Decimal),//9
                    new MySqlParameter("@out_costprc",MySqlDbType.Decimal),//10. 
                    new MySqlParameter("@out_costprice",MySqlDbType.Decimal),//11
                    new MySqlParameter("@out_costrate",MySqlDbType.Decimal),//12
                    new MySqlParameter("@out_hdrid",MySqlDbType.Int64),//13
                    new MySqlParameter("@out_hdrname",MySqlDbType.VarChar),//14
                    new MySqlParameter("@out_prcsourc",MySqlDbType.VarChar),// 15
                    new MySqlParameter("@out_resultcode",MySqlDbType.Int64),//16.
                    new MySqlParameter("@out_msg",MySqlDbType.VarChar),//17.
                                             };

                parameters[0].Value = Int64.Parse(Properties.Settings.Default.COMPID);
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Int64.Parse(Properties.Settings.Default.OWNERID);
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = Int64.Parse(saledeptid);
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = Int64.Parse(MatchInfo.Cstid);
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = Int64.Parse(goodid);
                parameters[4].Direction = ParameterDirection.Input;

                parameters[5].Size = 200;
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6].Size = 200;//
                parameters[6].Direction = ParameterDirection.Output;
                parameters[7].Size = 200;
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8].Size = 200;//
                parameters[8].Direction = ParameterDirection.Output;
                parameters[9].Size = 2048;
                parameters[9].Direction = ParameterDirection.Output;
                parameters[10].Size = 200;//
                parameters[10].Direction = ParameterDirection.Output;
                parameters[11].Size = 200;
                parameters[11].Direction = ParameterDirection.Output;
                parameters[12].Size = 200;
                parameters[12].Direction = ParameterDirection.Output;
                parameters[13].Size = 200;
                parameters[13].Direction = ParameterDirection.Output;
                parameters[14].Size = 2048;
                parameters[14].Direction = ParameterDirection.Output;
                parameters[15].Size = 2048;
                parameters[15].Direction = ParameterDirection.Output;
                parameters[16].Size = 200;//
                parameters[16].Direction = ParameterDirection.Output;
                parameters[17].Size = 2048;//
                parameters[17].Direction = ParameterDirection.Output;

                foreach (MySqlParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                //int ret = spCmd.ExecuteNonQuery();

                //info.Prc = parameters[6].Value.ToString().Trim();
                //info.Bottomprc = parameters[8].Value.ToString().Trim();
                //info.Costprc = parameters[10].Value.ToString().Trim();
                //info.Prcresultcode = parameters[16].Value.ToString().Trim();
                //info.Prcmsg = parameters[17].Value.ToString().Trim();
                int ret = spCmd.ExecuteNonQuery();
                info.Prcresultcode = parameters[16].Value.ToString().Trim();
                if (info.Prcresultcode == "1")
                {
                    info.Prc = parameters[6].Value.ToString().Trim();
                    info.Bottomprc = parameters[8].Value.ToString().Trim();
                    info.Costprc = parameters[10].Value.ToString().Trim();
                    info.Prcmsg = parameters[17].Value.ToString().Trim();
                }
                else
                {
                    info.Prcmsg = parameters[17].Value.ToString().Trim();
                }


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message.ToString(), "错误信息");
                retCode = -1;
            }

            finally {
                if (spCmd != null)
                    spCmd.Dispose();
            }

            return info;
        }

        //---------------------------------2018-10-15-------SetBillPrc
        public int SetBillPrc(AllPrcinfo zet, BillQuotationTemp MatchInfo, SPRetInfo retinfo) {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {
                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_BILL_QUOTE.P_SET_BILL_PRC";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_PRC",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("IN_BOTTOMPRC",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("IN_COSTPRC",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("IN_PRCRESULTCODE",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("IN_PRCMSG",OracleDbType.Varchar2,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = int.Parse(MatchInfo.Batchid);
                parameters[1].Value = int.Parse(MatchInfo.ExcelSeqid);
                parameters[2].Value = double.Parse(zet.Prc);
                parameters[3].Value = double.Parse(zet.Bottomprc);
                parameters[4].Value = double.Parse(zet.Costprc);
                parameters[5].Value = double.Parse(zet.Prcresultcode);
                parameters[6].Value = zet.Prcmsg;

                parameters[7].Size = 8;
                parameters[8].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[7].Value.ToString().Trim();
                retinfo.msg = parameters[8].Value.ToString().Trim();
                //retinfo.result = parameters[3].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;
        }
        //----------------------2018-10-15------------
        public int SetBillQuotation(BillQuotationTemp MatchInfo, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_BILL_QUOTE.P_SET_BILL_QUOTATION";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OP_TYPE",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = int.Parse(MatchInfo.Batchid);
                parameters[1].Value = int.Parse(MatchInfo.ExcelSeqid);
                parameters[2].Value = 1;

                parameters[3].Size = 8;
                parameters[4].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                retinfo.msg = parameters[4].Value.ToString().Trim();
                //retinfo.result = parameters[3].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;
        }


        //------------2018-10-15--------getAllPrc
        public AllPrcinfo getAllPrc(BillQuotationTemp MatchInfo) {
            AllPrcinfo info = new AllPrcinfo();
            string sql = "select BOTTOMPRC,PRC,QUOTATION,LASTSOPRC,LASTSOTIME from bill_quotation_temp where batchid=:batchid and excel_seqid=:excelSeqid";
            OracleCommand cmd = null;
            try
            {

                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("batchid", MatchInfo.Batchid);
                cmd.Parameters.Add("excelSeqid", MatchInfo.ExcelSeqid);

                cmd.CommandText = sql.ToString();
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    info.Bottomprc = res["BOTTOMPRC"].ToString().Trim();
                    info.Prc = res["PRC"].ToString().Trim();
                    info.Quotation = res["QUOTATION"].ToString().Trim();
                    info.Lastsoprc = res["LASTSOPRC"].ToString().Trim();
                    info.Lastsotime = res["LASTSOTIME"].ToString().Trim();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
            }
            finally {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return info;
        }
        //---------------------2018-10-16----------
        //--------------------更新人工报价  UpdateHPrc---------
        public int UpdateHPrc(BillQuotationTemp MatchInfo, string Quotation, SPRetInfo retinfo) {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_BILL_QUOTE.P_BQ_HIS";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("IN_QUOTATION",OracleDbType.Double,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Double,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = double.Parse(MatchInfo.Batchid);
                parameters[1].Value = double.Parse(MatchInfo.ExcelSeqid);
                parameters[2].Value = double.Parse(Quotation);

                parameters[3].Size = 8;
                parameters[4].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                retinfo.msg = parameters[4].Value.ToString().Trim();
                //retinfo.result = parameters[3].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;
        }

        /// <summary>
        /// --------------2018-7-30------------
        /// </summary>
        /// <param name="sqlkeydict"></param>
        /// <returns></returns>
        public SortableBindingList<GenGoodRecord> GetGenGoodRecordList(string begintime, string endtime,
            Dictionary<string, string> sqlkeydict)
        {
            SortableBindingList<GenGoodRecord> infoList = new SortableBindingList<GenGoodRecord>();
            //连接oracle数据库
            OracleCommand cmd = null;
            if ((StringUtils.IsNotNull(begintime)) && (StringUtils.IsNotNull(endtime)))
            {
                string sql = "select goodrecordid, compid, ownerid, cstid, cstcode, dname, gengoods, genspec,"
                    + "genproducer, goodbar, ratifier,recordmark, recordtime, createuser, createusername, createdate,"
                    + "modifyuser, modifyusername from v_gen_good_record t where t.compid =:compid and t.ownerid =:ownerid"
                   + " and t.recordtime >= to_date(:begintime, 'YYYYMMDD') and t.recordtime <= to_date(:endtime, 'YYYYMMDD') $";
                try
                {
                    cmd = Conn_datacenter_cmszh.CreateCommand();
                    cmd.Connection = Conn_datacenter_cmszh;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.Add("compid", Properties.Settings.Default.COMPID);
                    cmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);
                    cmd.Parameters.Add("begintime", begintime);
                    cmd.Parameters.Add("endtime", endtime);
                    string whereStr = "";
                    //遍历Dictionary中的key值
                    foreach (KeyValuePair<string, string> kv in sqlkeydict)
                    {
                        if (kv.Key.Equals(null) || kv.Key.Equals(""))
                        {
                            whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            if (kv.Key.IndexOf("%") > -1)
                            {
                                whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");
                            }
                            else
                            {
                                whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                            }
                        }

                    }
                    sql = sql.Replace("$", whereStr);
                    cmd.CommandText = sql.ToString();
                    //遍历Dictionary中的value值
                    foreach (KeyValuePair<string, string> kv in sqlkeydict)
                    {
                        if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                        {
                            if (kv.Key.IndexOf("%") > -1)
                            {
                                cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                            }
                            else
                            {
                                cmd.Parameters.Add(kv.Key, kv.Value);
                            }
                        }
                    }
                    string s = sql;
                    OracleDataReader res = cmd.ExecuteReader();
                    while (res.Read())
                    {
                        GenGoodRecord info = new GenGoodRecord();
                        info.Goodrecordid = res["goodrecordid"].ToString().Trim();
                        info.Compid = res["compid"].ToString().Trim();
                        info.Ownerid = res["ownerid"].ToString().Trim();
                        info.Cstid = res["cstid"].ToString().Trim();
                        info.Cstcode = res["cstcode"].ToString().Trim();
                        info.Dname = res["dname"].ToString().Trim();
                        info.Gengoods = res["gengoods"].ToString().Trim();
                        info.Genspec = res["genspec"].ToString().Trim();
                        info.Genproducer = res["genproducer"].ToString().Trim();
                        info.Goodbar = res["goodbar"].ToString().Trim();
                        info.Ratifier = res["ratifier"].ToString().Trim();
                        info.Recordmark = res["recordmark"].ToString().Trim();
                        info.Recordtime = res["recordtime"].ToString().Trim();
                        info.Createuser = res["createuser"].ToString().Trim();
                        info.Createusername = res["createusername"].ToString().Trim();
                        info.Createdate = res["createdate"].ToString().Trim();
                        info.Modifyuser = res["modifyuser"].ToString().Trim();
                        info.Modifyusername = res["modifyusername"].ToString().Trim();
                        info.SelectFlag = true;
                        infoList.Add(info);
                    }
                    res.Close();
                    res = null;
                    cmd.Dispose();
                    cmd = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "错误信息");
                }
                finally
                {
                    if (cmd != null)
                        cmd.Dispose();
                }
                return infoList;

            }
            else
            {
                string sql = "select goodrecordid, compid, ownerid, cstid, cstcode, dname, gengoods,"
                + "genspec, genproducer, goodbar, ratifier, recordmark, recordtime, createuser, "
                + "createusername, createdate, modifyuser, modifyusername from v_gen_good_record t "
                + "where t.compid=:compid and t.ownerid=:ownerid $";
                try
                {
                    cmd = Conn_datacenter_cmszh.CreateCommand();
                    cmd.Connection = Conn_datacenter_cmszh;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.CommandTimeout = 3600;

                    string whereStr = "";
                    //遍历Dictionary中的key值
                    foreach (KeyValuePair<string, string> kv in sqlkeydict)
                    {
                        if (kv.Key.Equals(null) || kv.Key.Equals(""))
                        {
                            whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            if (kv.Key.IndexOf("%") > -1)
                            {
                                whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");
                            }
                            else
                            {
                                whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                            }
                        }

                    }
                    sql = sql.Replace("$", whereStr);
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.Add("compid", Properties.Settings.Default.COMPID);
                    cmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);

                    //遍历Dictionary中的value值
                    foreach (KeyValuePair<string, string> kv in sqlkeydict)
                    {
                        if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                        {
                            if (kv.Key.IndexOf("%") > -1)
                            {
                                cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                            }
                            else
                            {
                                cmd.Parameters.Add(kv.Key, kv.Value);
                            }
                        }
                    }
                    string s = sql;
                    OracleDataReader res = cmd.ExecuteReader();
                    while (res.Read())
                    {
                        GenGoodRecord info = new GenGoodRecord();
                        info.Goodrecordid = res["goodrecordid"].ToString().Trim();
                        info.Compid = res["compid"].ToString().Trim();
                        info.Ownerid = res["ownerid"].ToString().Trim();
                        info.Cstid = res["cstid"].ToString().Trim();
                        info.Cstcode = res["cstcode"].ToString().Trim();
                        info.Dname = res["dname"].ToString().Trim();
                        info.Gengoods = res["gengoods"].ToString().Trim();
                        info.Genspec = res["genspec"].ToString().Trim();
                        info.Genproducer = res["genproducer"].ToString().Trim();
                        info.Goodbar = res["goodbar"].ToString().Trim();
                        info.Ratifier = res["ratifier"].ToString().Trim();
                        info.Recordmark = res["recordmark"].ToString().Trim();
                        info.Recordtime = res["recordtime"].ToString().Trim();
                        info.Createuser = res["createuser"].ToString().Trim();
                        info.Createusername = res["createusername"].ToString().Trim();
                        info.Createdate = res["createdate"].ToString().Trim();
                        info.Modifyuser = res["modifyuser"].ToString().Trim();
                        info.Modifyusername = res["modifyusername"].ToString().Trim();
                        info.SelectFlag = true;
                        infoList.Add(info);
                    }
                    res.Close();
                    res = null;
                    cmd.Dispose();
                    cmd = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "错误信息");
                }
                finally
                {
                    if (cmd != null)
                        cmd.Dispose();
                }
                return infoList;

            }

        }


        //删除备案品种信息
        public int DeleteGenGoodRecord(SortableBindingList<DelTempGenGoodRecord> List, SPRetInfo retinfo)
        {

            string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            string insertSql = "insert into DELTEMP_GEN_GOODRECORD(BATCHID,GOODRECORDID)values(:Batchid,:Goodrecordid)";
            //插入中间表

            OracleCommand selCmd = null;
            OracleCommand insertCmd = null;
            OracleCommand spCmd = null;
            OracleTransaction trans = null;

            int retCode = -1;
            string sqlID = "0";

            try
            {
                //查询函数获取批次号
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    sqlID = res["SEQ"].ToString().Trim();
                res.Close();
                res.Dispose();
                selCmd.Dispose();
                selCmd = null;

                trans = Conn_datacenter_cmszh.BeginTransaction();
                //插入中间表
                insertCmd = Conn_datacenter_cmszh.CreateCommand();
                insertCmd.Connection = Conn_datacenter_cmszh;
                insertCmd.CommandType = System.Data.CommandType.Text;
                insertCmd.CommandText = insertSql.ToString();
                insertCmd.CommandTimeout = 3600;
                insertCmd.Transaction = trans;
                int recCount = 0;

                foreach (DelTempGenGoodRecord selected in List)
                {
                    recCount++;

                    insertCmd.Parameters.Clear();

                    insertCmd.Parameters.Add("Batchid", int.Parse(sqlID));
                    insertCmd.Parameters.Add("Goodrecordid", selected.Goodrecordid);

                    insertCmd.ExecuteNonQuery();
                }
                trans.Commit();
                trans.Dispose();
                trans = null;

                insertCmd.Dispose();
                insertCmd = null;

                if (recCount < 1)
                {
                    retinfo.num = "0";

                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return 0;
                }

                //执行存储过程,进行删除
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GOOD_RECORD.P_RECORD_DEL";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
            };
                parameters[0].Value = Int64.Parse(sqlID);

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[1].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[2].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[2].Value.ToString().Trim();
                }
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
                if (trans != null)
                {
                    trans.Rollback();
                    trans.Dispose();
                    trans = null;
                }
            }
            return retCode;


        }


        /// <summary>
        /// 
        /// ---------------------------2018-8-1---------------
        /// 新增备案品种
        /// </summary>
        /// <returns></returns>
        public int AddGenGoodRecord(string Cstcode, string Gengoods, string Genspec,
            string Genproducer, string Goodbar, string Ratifier, string Recordmark, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GOOD_RECORD.P_RECORD_ADD";
                spCmd.CommandTimeout = 3600;

                // 向存储过程中添加输入输出
                OracleParameter[] parameters = {
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTCODE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GENGOODS",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GENSPEC",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GENPRODUCER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GOODBAR",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_RATIFIER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_RECORDMARK",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                    };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(Properties.Settings.Default.COMPID);
                parameters[1].Value = Int64.Parse(Properties.Settings.Default.OWNERID);
                parameters[2].Value = Cstcode;
                parameters[3].Value = Gengoods;
                parameters[4].Value = Genspec;
                parameters[5].Value = Genproducer;
                parameters[6].Value = Goodbar;
                parameters[7].Value = Ratifier;
                parameters[8].Value = Recordmark;
                parameters[9].Value = SessionDto.Empid;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[10].Size = 8;
                parameters[11].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[10].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[11].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[11].Value.ToString().Trim();
                }
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;

        }

        /// <summary>
        /// 
        /// ---------------------------2018-8-1---------------
        /// 修改备案品种
        /// <returns></returns>
        public int UpdateGenGoodRecord(string GoodRecordID, string Genspec, string Genproducer,
            string Goodbar, string Ratifier, string Recordmark, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GOOD_RECORD.P_RECORD_MODIFY";
                spCmd.CommandTimeout = 3600;

                // 向存储过程中添加输入输出
                OracleParameter[] parameters = {
                    new OracleParameter("IN_GOODRECORDID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_GENSPEC",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GENPRODUCER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GOODBAR",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_RATIFIER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_RECORDMARK",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                    };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(GoodRecordID);
                parameters[1].Value = Genspec;
                parameters[2].Value = Genproducer;
                parameters[3].Value = Goodbar;
                parameters[4].Value = Ratifier;
                parameters[5].Value = Recordmark;
                parameters[6].Value = SessionDto.Empid;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[7].Size = 8;
                parameters[8].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[7].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[8].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[8].Value.ToString().Trim();
                }
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;
        }


        /// <summary>
        /// ----------------------2018-8-1------------
        /// 将备案品种Excel导入导出
        /// </summary>
        /// <param name="List"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public SortableBindingList<InoutGenGoodRecordXlstemp>
            PGetGenGoodRecordData(SortableBindingList<InoutGenGoodRecordXlstemp> List, SPRetInfo retinfo)
        {
            SortableBindingList<InoutGenGoodRecordXlstemp> InoutGenGoodRecordXlstempList =
                new SortableBindingList<InoutGenGoodRecordXlstemp>();
            SortableBindingList<InoutGenGoodRecordXlstemp> InoutList = new SortableBindingList<InoutGenGoodRecordXlstemp>();

            string selectSql = "SELECT F_GET_SEQ('BATCHID') as SEQ FROM dual";
            string insertSql = "INSERT INTO INOUT_GEN_GOOD_RECORD_XLSTEMP(batchid,excel_seqid,compid,ownerid,"
                + "cstcode,gengoods,genspec,transrate,genproducer,goodbar,ratifier,recordmark,rowstate,rowmsg,"
                + "empid)VALUES(:Batchid,:Excel_seqid,:Compid,:Ownerid,:Cstcode,:Gengoods,:Genspec,"
                + ":Transrate,:Genproducer,:Goodbar,:Ratifier,:Recordmark,:Rowstate,:Rowmsg,:Empid)";

            string selSql = "SELECT * FROM INOUT_GEN_GOOD_RECORD_XLSTEMP  WHERE batchid =:batchid";

            OracleCommand spCmd = null;
            OracleCommand selCmd = null;
            OracleCommand insetCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            string seqID = "0";

            //查询批次号
            try
            {
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    seqID = res["SEQ"].ToString().Trim();

                res.Close();
                res.Dispose();

                selCmd.Dispose();
                selCmd = null;

                //将信息插入中间表
                trans = Conn_datacenter_cmszh.BeginTransaction();

                insetCmd = Conn_datacenter_cmszh.CreateCommand();
                insetCmd.Connection = Conn_datacenter_cmszh;
                insetCmd.CommandType = System.Data.CommandType.Text;
                insetCmd.CommandText = insertSql.ToString();
                insetCmd.CommandTimeout = 3600;

                insetCmd.Transaction = trans;
                int recCount = 0;
                foreach (InoutGenGoodRecordXlstemp selected in List)
                {
                    recCount++;

                    insetCmd.Parameters.Clear();

                    insetCmd.Parameters.Add("Batchid", Int32.Parse(seqID));
                    insetCmd.Parameters.Add("Excel_seqid", Int32.Parse(selected.ExcelSeqid));
                    insetCmd.Parameters.Add("Compid", Int32.Parse(selected.Compid));
                    insetCmd.Parameters.Add("Ownerid", Int32.Parse(selected.Ownerid));
                    insetCmd.Parameters.Add("Cstcode", selected.Cstcode);
                    insetCmd.Parameters.Add("Gengoods", selected.Gengoods);

                    if (selected.Genspec != "")
                    {
                        insetCmd.Parameters.Add("Genspec", selected.Genspec);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Genspec", "");
                    }
                    if (selected.Transrate != "")
                    {
                        insetCmd.Parameters.Add("Transrate", Int32.Parse(selected.Transrate));
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Transrate", "");
                    }
                    if (selected.Genproducer != "")
                    {
                        insetCmd.Parameters.Add("Genproducer", selected.Genproducer);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Genproducer", "");
                    }
                    if (selected.Goodbar != "")
                    {
                        insetCmd.Parameters.Add("Goodbar", selected.Goodbar);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Goodbar", "");
                    }
                    if (selected.Ratifier != "")
                    {
                        insetCmd.Parameters.Add("Ratifier", selected.Ratifier);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Ratifier", "");
                    }
                    insetCmd.Parameters.Add("Recordmark", selected.Recordmark);
                    if (selected.Rowstate != "")
                    {
                        insetCmd.Parameters.Add("Rowstate", selected.Rowstate);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Rowstate", "");
                    }
                    if (selected.Rowmsg != "")
                    {
                        insetCmd.Parameters.Add("Rowmsg", selected.Rowmsg);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Rowmsg", "");
                    }
                    insetCmd.Parameters.Add("Empid", Int32.Parse(selected.Empid));
                    insetCmd.ExecuteNonQuery();
                }

                trans.Commit();
                trans.Dispose();
                trans = null;

                insetCmd.Dispose();
                insetCmd = null;

                /*
                 * AI_SEQ_ID NUMBER, --程序前台ID
                   --AI_BARCODE_TYPE NUMBER; --条码类型
                   OUT_RTD    OUT NUMBER, --返回值，可以判断执行是否成功
                   OUT_ARMSG  OUT VARCHAR2, --返回执行说明
                   OUT_RESULT OUT VARCHAR2 --返回执行代码
                */
                if (recCount < 1)
                {
                    retinfo.num = "0";
                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return null;
                }

                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GOOD_RECORD.P_EXCEL_IMPORT";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                };


                parameters[0].Value = Int64.Parse(seqID);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[1].Value.ToString();
                retinfo.msg = parameters[2].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

                //再次查询此表
                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.Parameters.Add("batchid", seqID);
                seCmd.CommandTimeout = 3600;

                OracleDataReader ress = seCmd.ExecuteReader();
                foreach (InoutGenGoodRecordXlstemp info in List)
                {
                    if (ress.Read())
                        info.Batchid = ress["batchid"].ToString().Trim();
                    info.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    info.Compid = ress["compid"].ToString().Trim();
                    info.Ownerid = ress["ownerid"].ToString().Trim();
                    info.Cstcode = ress["cstcode"].ToString().Trim();
                    info.Gengoods = ress["gengoods"].ToString().Trim();
                    info.Genspec = ress["genspec"].ToString().Trim();
                    info.Transrate = ress["transrate"].ToString().Trim();
                    info.Genproducer = ress["genproducer"].ToString().Trim();
                    info.Goodbar = ress["goodbar"].ToString().Trim();
                    info.Ratifier = ress["ratifier"].ToString().Trim();
                    info.Recordmark = ress["recordmark"].ToString().Trim();
                    info.Rowstate = ress["rowstate"].ToString().Trim();
                    info.Rowmsg = ress["rowmsg"].ToString().Trim();
                    info.Empid = ress["empid"].ToString().Trim();
                    InoutList.Add(info);
                }

                ress.Close();
                ress.Dispose();

                seCmd.Dispose();
                seCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (selCmd != null)
                    selCmd.Dispose();

                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();

                if (insetCmd != null)
                    insetCmd.Dispose();

                if (seCmd != null)
                    seCmd.Dispose();


            }
            return InoutList;
        }

        ///2018-08-08-------------------------------------------------------------
        ///单条对码刷新
        public int PSingleCstCode(string Batchid, string ExcelSeqid, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            //OracleCommand selCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_BILL_QUOTE.P_GET_BILL_DATA";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OP_TYPE",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output),
                    //new OracleParameter("OUT_RESULT",OracleDbType.Varchar2,ParameterDirection.Output)

                };


                parameters[0].Value = int.Parse(Batchid);
                parameters[1].Value = int.Parse(ExcelSeqid);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[2].Value = 1;
                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[4].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[4].Value.ToString().Trim();
                }
                //retinfo.result = parameters[4].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {


                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }

        //单条Goodid数据查询-------------------
        public SortableBindingList<BillQuotationTemp> GetSingleGoodidExistList(string batchid, string ExcelSeqID)
        {
            SortableBindingList<BillQuotationTemp> infoList = new SortableBindingList<BillQuotationTemp>();

            string sql = "SELECT batchid ,GOODID,t.compid,t.ownerid,t.saledeptid,t.cstid,"
            + "t.excel_seqid FROM BILL_QUOTATION_TEMP t WHERE BATCHID =:batchid AND excel_seqid=:excel_seqid";


            OracleCommand cmd = null;
            try
            {

                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;

                cmd.CommandText = sql.ToString();
                cmd.Parameters.Add("batchid", batchid);
                cmd.Parameters.Add("excel_seqid", ExcelSeqID);


                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    BillQuotationTemp info = new BillQuotationTemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.Goodid = res["GOODID"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();

                    infoList.Add(info);

                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infoList;
        }

        /// <summary>
        /// 单条调用quotation
        /// </summary>
        /// <param name="Batchid"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int PSingleSetBillQuotation(string Batchid, string ExcelSeqId, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            //OracleCommand selCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_BILL_QUOTE.P_SET_BILL_QUOTATION";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OP_TYPE",OracleDbType.Int64,ParameterDirection.Input),


                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };


                parameters[0].Value = Int64.Parse(Batchid);
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Value = int.Parse(ExcelSeqId);
                parameters[2].Value = 1;
                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                //parameters[5].Value = Int64.Parse(info.Prcresultcode);
                //parameters[6].Value = info.Prcmsg;
                //parameters[7].Size = 200;
                //parameters[8].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                retinfo.msg = parameters[4].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {


                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }


        /// <summary>
        /// ----------------------2018-8-13------------
        /// Excel导入 调用存储过程
        /// </summary>
        public int PGetGenCmsBillData(SortableBindingList<InoutGenCmsbillXlstemp> List, string Cstid, SPRetInfo retinfo)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> InoutList = new SortableBindingList<InoutGenCmsbillXlstemp>();
            string selectSql = "SELECT F_GET_SEQ('BATCHID') as SEQ FROM dual";
            string insertSql = "INSERT INTO INOUT_GEN_CMSBILL_XLSTEMP(batchid,excel_seqid,compid,ownerid,"
                + "cstid,empid,gengoods,goods,planprc,plancount,pmark,amark,genaddresscode ,"
                + "cmsaddresscode,saledeptid,importprc,createdate,iotype)VALUES(:Batchid,:Excel_seqid,:Compid,:Ownerid,:Cstid,:Empid,:Gengoods,"
                + ":Goods,:Planprc,:Plancount,:Pmark,:Amark,:Genaddresscode,:Cmsaddresscode,:Saledeptid,:Importprc,to_date(:Createdate,'yyyy-mm-dd hh24-mi-ss'),:iotype)";

            OracleCommand spCmd = null;
            OracleCommand selCmd = null;
            OracleCommand insetCmd = null;
            OracleCommand tipsCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            string seqID = "0";
            //查询批次号
            try
            {
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    seqID = res["SEQ"].ToString().Trim();

                res.Close();
                res.Dispose();

                selCmd.Dispose();
                selCmd = null;

                //将信息插入中间表
                trans = Conn_datacenter_cmszh.BeginTransaction();

                insetCmd = Conn_datacenter_cmszh.CreateCommand();
                insetCmd.Connection = Conn_datacenter_cmszh;
                insetCmd.CommandType = System.Data.CommandType.Text;
                insetCmd.CommandText = insertSql.ToString();
                insetCmd.CommandTimeout = 30;

                insetCmd.Transaction = trans;
                int recCount = 0;
                foreach (InoutGenCmsbillXlstemp selected in List)
                {
                    recCount++;

                    insetCmd.Parameters.Clear();

                    insetCmd.Parameters.Add("Batchid", Int32.Parse(seqID));
                    insetCmd.Parameters.Add("Excel_seqid", Int32.Parse(selected.ExcelSeqid));
                    insetCmd.Parameters.Add("Compid", Int32.Parse(selected.Compid));
                    insetCmd.Parameters.Add("Ownerid", Int32.Parse(selected.Ownerid));
                    if (selected.Cstid != "")
                    {
                        insetCmd.Parameters.Add("Cstid", selected.Cstid);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Cstid", "");
                    }
                    insetCmd.Parameters.Add("Empid", Int32.Parse(selected.Empid));
                    if (selected.Gengoods != "")
                    {
                        insetCmd.Parameters.Add("Gengoods", selected.Gengoods);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Gengoods", "");
                    }
                    if (selected.Goods != "")
                    {
                        insetCmd.Parameters.Add("Goods", selected.Goods);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Goods", "");
                    }
                    insetCmd.Parameters.Add("Planprc", selected.Planprc);
                    insetCmd.Parameters.Add("Plancount", selected.Plancount);
                    if (selected.Pmark != "")
                    {
                        insetCmd.Parameters.Add("Pmark", selected.Pmark);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Pmark", "");
                    }
                    if (selected.Amark != "")
                    {
                        insetCmd.Parameters.Add("Amark", selected.Amark);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Amark", "");
                    }
                    if (selected.Genaddresscode != "")
                    {
                        insetCmd.Parameters.Add("Genaddresscode", selected.Genaddresscode);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Genaddresscode", "");
                    }
                    if (selected.Cmsaddresscode != "")
                    {
                        insetCmd.Parameters.Add("Cmsaddresscode", selected.Cmsaddresscode);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Cmsaddresscode", "");
                    }
                    if (selected.Saledeptid != "")
                    {
                        insetCmd.Parameters.Add("Saledeptid", Int32.Parse(selected.Saledeptid));
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Saledeptid", "");
                    }

                    insetCmd.Parameters.Add("Importprc", selected.Importprc);
                    insetCmd.Parameters.Add("Createdate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    insetCmd.Parameters.Add("iotype", 61);
                    insetCmd.ExecuteNonQuery();
                }

                trans.Commit();
                trans.Dispose();
                trans = null;

                insetCmd.Dispose();
                insetCmd = null;

                if (recCount < 1)
                {
                    retinfo.num = "0";
                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return 0;
                }
                //调用存储过程 将客户效期要求写入中间表 ---2018-11-1---------
                tipsCmd = Conn_datacenter_cmszh.CreateCommand();
                tipsCmd.Connection = Conn_datacenter_cmszh;
                tipsCmd.CommandType = System.Data.CommandType.StoredProcedure;
                tipsCmd.CommandText = "PG_GEN_CMSBILL.P_LOT_CONF_CHECK";
                tipsCmd.CommandTimeout = 3600;
                //向存储过程中添加输入输出
                OracleParameter[] parameterss ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_SALEDEPTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTID",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64 ,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameterss[0].Value = Properties.Settings.Default.COMPID;
                parameterss[1].Value = Properties.Settings.Default.OWNERID;
                parameterss[2].Value = SessionDto.Empdeptid;
                parameterss[3].Value = Cstid;
                parameterss[4].Value = Int64.Parse(seqID);

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameterss[5].Size = 8;
                parameterss[6].Size = 2048;
                foreach (OracleParameter paramete in parameterss)
                {
                    tipsCmd.Parameters.Add(paramete);
                }

                tipsCmd.ExecuteNonQuery();

                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_CMSBILL_GOODS";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SQLID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OP_TYPE",OracleDbType.Int64,ParameterDirection.Input),


                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                };


                parameters[0].Value = Int64.Parse(seqID);
                parameters[1].Value = -1;
                parameters[2].Value = 0;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString();
                retinfo.msg = parameters[4].Value.ToString().Trim();

                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[4].Value.ToString().Trim();
                    retinfo.result = seqID;
                }
                else
                {
                    retinfo.msg = parameters[4].Value.ToString().Trim();
                }

                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (selCmd != null)
                    selCmd.Dispose();

                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();

                if (insetCmd != null)
                    insetCmd.Dispose();
            }
            return retCode;
        }




        //-----------------------2018.8.13--------------------
        /// <summary>
        /// 查询goodid存在的数据
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        public SortableBindingList<InoutGenCmsbillXlstemp> GetGoodid(string batchid)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> infoList = new SortableBindingList<InoutGenCmsbillXlstemp>();

            string sql = "SELECT batchid ,GOODID,t.compid,t.ownerid,t.saledeptid,t.cstid,"
            + "t.excel_seqid FROM INOUT_GEN_CMSBILL_XLSTEMP t WHERE BATCHID =:batchid AND GOODID IS NOT NULL";

            OracleCommand cmd = null;
            try
            {

                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.CommandText = sql.ToString();

                cmd.Parameters.Add("batchid", batchid);
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    InoutGenCmsbillXlstemp info = new InoutGenCmsbillXlstemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.Goodid = res["GOODID"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();

                    infoList.Add(info);

                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infoList;
        }
        //--------------------------2018-8-13

        /// <summary>
        /// 调用Mysql查询价格
        /// </summary>
        /// <param name="InputInfo"></param>
        /// <param name="OutputInfo"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int PGetB2BPrcs(InoutGenCmsbillXlstemp InputInfo, InoutGenCmsbillXlstemp OutputInfo)
        {

            MySqlCommand spCmd = null;
            int retCode = -1;
            try
            {
                //从库 ----2019-1-7---
                if (ConnectionTests() == -1)//打开mysql从库连接
                {
                    MessageBox.Show("连接从库失败!");
                    Environment.Exit(0);
                }
                spCmd = connections.CreateCommand();
                spCmd.Connection = connections;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "P_GET_B2BPRC";
                spCmd.CommandTimeout = 3600;
                MySqlParameter[] parameters ={
                    new MySqlParameter("@in_compid",MySqlDbType.Int64),
                    new MySqlParameter("@in_ownerid",MySqlDbType.Int64),
                    new MySqlParameter("@in_saledeptid",MySqlDbType.Int64),
                    new MySqlParameter("@in_cstid",MySqlDbType.Int64),//`` 
                    new MySqlParameter("@in_goodid",MySqlDbType.Int64),

                    new MySqlParameter("@out_prcid",MySqlDbType.Int64),//5 
                    new MySqlParameter("@out_prc",MySqlDbType.Decimal),//6.
                    new MySqlParameter("@out_price",MySqlDbType.Decimal),//7
                    new MySqlParameter("@out_bottomprc",MySqlDbType.Decimal),//8.
                    new MySqlParameter("@out_bottomprice",MySqlDbType.Decimal),//9
                    new MySqlParameter("@out_costprc",MySqlDbType.Decimal),//10. 
                    new MySqlParameter("@out_costprice",MySqlDbType.Decimal),//11
                    new MySqlParameter("@out_costrate",MySqlDbType.Decimal),//12
                    new MySqlParameter("@out_hdrid",MySqlDbType.Int64),//13
                    new MySqlParameter("@out_hdrname",MySqlDbType.VarChar),//14
                    new MySqlParameter("@out_prcsourc",MySqlDbType.VarChar),// 15
                    new MySqlParameter("@out_resultcode",MySqlDbType.Int64),//16.
                    new MySqlParameter("@out_msg",MySqlDbType.VarChar),//17.
                                             };

                parameters[0].Value = Int64.Parse(InputInfo.Compid);
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Int64.Parse(InputInfo.Ownerid);
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = Int64.Parse(InputInfo.Saledeptid);
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = Int64.Parse(InputInfo.Cstid);
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = Int64.Parse(InputInfo.Goodid);
                parameters[4].Direction = ParameterDirection.Input;

                parameters[5].Size = 200;
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6].Size = 200;//
                parameters[6].Direction = ParameterDirection.Output;
                parameters[7].Size = 200;
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8].Size = 200;//
                parameters[8].Direction = ParameterDirection.Output;
                parameters[9].Size = 2048;
                parameters[9].Direction = ParameterDirection.Output;
                parameters[10].Size = 200;//
                parameters[10].Direction = ParameterDirection.Output;
                parameters[11].Size = 200;
                parameters[11].Direction = ParameterDirection.Output;
                parameters[12].Size = 200;
                parameters[12].Direction = ParameterDirection.Output;
                parameters[13].Size = 200;
                parameters[13].Direction = ParameterDirection.Output;
                parameters[14].Size = 2048;
                parameters[14].Direction = ParameterDirection.Output;
                parameters[15].Size = 2048;
                parameters[15].Direction = ParameterDirection.Output;
                parameters[16].Size = 200;//
                parameters[16].Direction = ParameterDirection.Output;
                parameters[17].Size = 2048;//
                parameters[17].Direction = ParameterDirection.Output;

                foreach (MySqlParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                OutputInfo.Prcresultcode = parameters[16].Value.ToString().Trim();
                if (OutputInfo.Prcresultcode == "1")
                {
                    OutputInfo.Prc = parameters[6].Value.ToString().Trim();
                    OutputInfo.Price = parameters[7].Value.ToString().Trim();
                    OutputInfo.Bottomprc = parameters[8].Value.ToString().Trim();
                    OutputInfo.Bottomprice = parameters[9].Value.ToString().Trim();
                    OutputInfo.Costprc = parameters[10].Value.ToString().Trim();
                    OutputInfo.Costprice = parameters[11].Value.ToString().Trim();
                    OutputInfo.Prcresultcode = parameters[16].Value.ToString().Trim();
                    OutputInfo.Prcmsg = parameters[17].Value.ToString().Trim();
                }
                else
                {
                    OutputInfo.Prcresultcode = parameters[16].Value.ToString().Trim();
                    OutputInfo.Prcmsg = parameters[17].Value.ToString().Trim();
                }

                spCmd.Dispose();
                spCmd = null;
                retCode = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }

        //-------------------2018-8-13----------------

        /// <summary>
        /// 将价格写回中间表
        /// </summary>
        /// <param name="List"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int PSetBillPrcs(InoutGenCmsbillXlstemp info, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            //OracleCommand selCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_SET_BILL_PRC";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_PRC",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_PRICE",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_BOTTOMPRC",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_BOTTOMPRICE",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_COSTPRC",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_COSTPRICE",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_PRCRESULTCODE",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("IN_PRCMSG",OracleDbType.Varchar2,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Int64.Parse(info.Batchid);
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Value = Int64.Parse(info.ExcelSeqid);
                parameters[2].Value = Decimal.Parse(info.Prc);
                parameters[3].Value = Decimal.Parse(info.Price);
                parameters[4].Value = Decimal.Parse(info.Bottomprc);
                parameters[5].Value = Decimal.Parse(info.Bottomprice);
                parameters[6].Value = Decimal.Parse(info.Costprc);
                parameters[7].Value = Decimal.Parse(info.Costprice);

                parameters[8].Value = double.Parse(info.Prcresultcode);
                parameters[9].Value = info.Prcmsg;
                parameters[10].Size = 200;
                parameters[11].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[9].Value.ToString().Trim();
                retinfo.msg = parameters[10].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();

                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }

        //---------------2018-8-13------------
        /// <summary>
        /// 查询处理完成的中间表
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        public SortableBindingList<InoutGenCmsbillXlstemp> GetInoutGenCmsbillXlstempList(string batchid)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> infoList = new SortableBindingList<InoutGenCmsbillXlstemp>();

            string sql = "SELECT * FROM INOUT_GEN_CMSBILL_XLSTEMP WHERE batchid =:batchid ";

            OracleCommand cmd = null;
            try
            {

                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("batchid", batchid);

                cmd.CommandText = sql.ToString();
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    InoutGenCmsbillXlstemp info = new InoutGenCmsbillXlstemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Empid = res["empid"].ToString().Trim();
                    info.Gengoods = res["gengoods"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Planprc = res["planprc"].ToString().Trim();
                    info.Plancount = res["plancount"].ToString().Trim();
                    info.Pmark = res["pmark"].ToString().Trim();
                    info.Amark = res["amark"].ToString().Trim();
                    info.Genaddresscode = res["genaddresscode"].ToString().Trim();
                    info.Cmsaddresscode = res["cmsaddresscode"].ToString().Trim();
                    info.Checkstate = res["checkstate"].ToString().Trim();
                    info.Checkmsg = res["checkmsg"].ToString().Trim();
                    info.Importstate = res["importstate"].ToString().Trim();
                    info.Importmsg = res["importmsg"].ToString().Trim();
                    info.Goodtype = res["goodtype"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Prcresultcode = res["prcresultcode"].ToString().Trim();
                    info.Prcmsg = res["prcmsg"].ToString().Trim();
                    info.Quotation = res["quotation"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.Importprc = res["importprc"].ToString().Trim();
                    //---------2018-10-22-----新增字段
                    info.GoodName = res["goodname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.BizownQty = res["bizownqty"].ToString().Trim();
                    info.AllowQty = res["allowqty"].ToString().Trim();
                    info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                    info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                    info.ColdChain = res["coldchain"].ToString().Trim();
                    info.LastSoTime = res["lastsotime"].ToString().Trim();
                    info.ChooseFlag = res["chooseflag"].ToString().Trim();
                    info.PromotionFlag = res["promotionflag"].ToString().Trim();

                    //---------2018-12-26-----新增字段
                    info.InvoiceType = res["invoicetypename"].ToString().Trim();
                    info.AttchInv = res["attchinvname"].ToString().Trim();
                    info.InvPostFlag = res["invpostflagname"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();

                    infoList.Add(info);
                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infoList;
        }

        //---------------2018-11-9------------
        /// <summary>
        /// EXCEL 导出查询处理完成的中间表
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        public SortableBindingList<InoutGenCmsbillXlstemp> GetInoutGenCmsbillXlstempLists(string batchid)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> infoList = new SortableBindingList<InoutGenCmsbillXlstemp>();

            string sql = "SELECT * FROM INOUT_GEN_CMSBILL_XLSTEMP WHERE batchid =:batchid ";

            OracleCommand cmd = null;
            try
            {

                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("batchid", batchid);

                cmd.CommandText = sql.ToString();
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    InoutGenCmsbillXlstemp info = new InoutGenCmsbillXlstemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Empid = res["empid"].ToString().Trim();
                    info.Gengoods = res["gengoods"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Planprc = res["planprc"].ToString().Trim();
                    info.Plancount = res["plancount"].ToString().Trim();
                    info.Pmark = res["pmark"].ToString().Trim();
                    info.Amark = res["amark"].ToString().Trim();
                    info.Genaddresscode = res["genaddresscode"].ToString().Trim();
                    info.Cmsaddresscode = res["cmsaddresscode"].ToString().Trim();
                    info.Checkstate = res["checkstate"].ToString().Trim();
                    info.Checkmsg = res["checkmsg"].ToString().Trim();
                    info.Importstate = res["importstate"].ToString().Trim();
                    info.Importmsg = res["importmsg"].ToString().Trim();
                    info.Goodtype = res["goodtype"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Prcresultcode = res["prcresultcode"].ToString().Trim();
                    info.Prcmsg = res["prcmsg"].ToString().Trim();
                    info.Quotation = res["quotation"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.Importprc = res["importprc"].ToString().Trim();
                    //---------2018-10-22-----新增字段
                    info.HdrmMark = res["hdrMark"].ToString().Trim();
                    info.GoodName = res["goodname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.BizownQty = res["bizownqty"].ToString().Trim();
                    info.AllowQty = res["allowqty"].ToString().Trim();
                    info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                    info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                    info.ColdChain = res["coldchain"].ToString().Trim();
                    info.LastSoTime = res["lastsotime"].ToString().Trim();
                    info.ChooseFlag = res["chooseflag"].ToString().Trim();



                    infoList.Add(info);
                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infoList;
        }

        /// <summary>
        /// ----------------------2018-8-13------------
        /// 检验合同数据
        /// </summary>
        public SortableBindingList<InoutGenCmsbillXlstemp> CheckGenCmsBillData(string batchid, SPRetInfo retinfo)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> InoutList = new SortableBindingList<InoutGenCmsbillXlstemp>();
            string selSql = "SELECT * FROM INOUT_GEN_CMSBILL_XLSTEMP  WHERE batchid =:batchid";

            OracleCommand spCmd = null;
            OracleCommand selCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            string sqlid = "0";

            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_CMSBILL_CHECK";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                };


                parameters[0].Value = Int64.Parse(batchid);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[1].Value.ToString();
                retinfo.msg = parameters[2].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

                //再次查询此表
                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.CommandTimeout = 3600;
                seCmd.Parameters.Add("batchid", batchid);

                OracleDataReader res = seCmd.ExecuteReader();
                while (res.Read())
                {
                    InoutGenCmsbillXlstemp info = new InoutGenCmsbillXlstemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Empid = res["empid"].ToString().Trim();
                    info.Gengoods = res["gengoods"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Planprc = res["planprc"].ToString().Trim();
                    info.Plancount = res["plancount"].ToString().Trim();
                    info.Pmark = res["pmark"].ToString().Trim();
                    info.Amark = res["amark"].ToString().Trim();
                    info.Genaddresscode = res["genaddresscode"].ToString().Trim();
                    info.Cmsaddresscode = res["cmsaddresscode"].ToString().Trim();
                    info.Checkstate = res["checkstate"].ToString().Trim();
                    info.Checkmsg = res["checkmsg"].ToString().Trim();
                    info.Importstate = res["importstate"].ToString().Trim();
                    info.Importmsg = res["importmsg"].ToString().Trim();
                    info.Goodtype = res["goodtype"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Prcresultcode = res["prcresultcode"].ToString().Trim();
                    info.Prcmsg = res["prcmsg"].ToString().Trim();
                    info.Quotation = res["quotation"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.Importprc = res["importprc"].ToString().Trim();

                    //---------2018-10-22-----新增字段
                    info.GoodName = res["goodname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.BizownQty = res["bizownqty"].ToString().Trim();
                    info.AllowQty = res["allowqty"].ToString().Trim();
                    info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                    info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                    info.ColdChain = res["coldchain"].ToString().Trim();
                    info.LastSoTime = res["lastsotime"].ToString().Trim();
                    info.ChooseFlag = res["chooseflag"].ToString().Trim();
                    info.PromotionFlag = res["promotionflag"].ToString().Trim();

                    InoutList.Add(info);
                }

                res.Close();
                res.Dispose();

                seCmd.Dispose();
                seCmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (selCmd != null)
                    selCmd.Dispose();

                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();

                if (seCmd != null)
                    seCmd.Dispose();
            }
            return InoutList;
        }

        /// <summary>
        /// ----------------------2018-8-13------------
        /// 提交到cms
        /// </summary>
        public SortableBindingList<InoutGenCmsbillXlstemp> ImportGenCmsBillData(string batchid, SPRetInfo retinfo)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> InoutList = new SortableBindingList<InoutGenCmsbillXlstemp>();
            string selSql = "SELECT * FROM INOUT_GEN_CMSBILL_XLSTEMP  WHERE batchid =:batchid";

            OracleCommand spCmd = null;
            OracleCommand selCmd = null;
            OracleCommand insetCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;

            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_CMSBILL_IMPORT";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OP_TYPE",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                };


                parameters[0].Value = Int64.Parse(batchid);
                parameters[1].Value = 0;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[2].Size = 8;
                parameters[3].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[2].Value.ToString();
                retinfo.msg = parameters[3].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

                //再次查询此表
                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.Parameters.Add("batchid", batchid);
                seCmd.CommandTimeout = 3600;

                OracleDataReader res = seCmd.ExecuteReader();

                while (res.Read())
                {
                    InoutGenCmsbillXlstemp info = new InoutGenCmsbillXlstemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Empid = res["empid"].ToString().Trim();
                    info.Gengoods = res["gengoods"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Planprc = res["planprc"].ToString().Trim();
                    info.Plancount = res["plancount"].ToString().Trim();
                    info.Pmark = res["pmark"].ToString().Trim();
                    info.Amark = res["amark"].ToString().Trim();
                    info.Genaddresscode = res["genaddresscode"].ToString().Trim();
                    info.Cmsaddresscode = res["cmsaddresscode"].ToString().Trim();
                    info.Checkstate = res["checkstate"].ToString().Trim();
                    info.Checkmsg = res["checkmsg"].ToString().Trim();
                    info.Importstate = res["importstate"].ToString().Trim();
                    info.Importmsg = res["importmsg"].ToString().Trim();
                    info.Goodtype = res["goodtype"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Prcresultcode = res["prcresultcode"].ToString().Trim();
                    info.Prcmsg = res["prcmsg"].ToString().Trim();
                    //---------2018-10-22-----新增字段
                    info.GoodName = res["goodname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.BizownQty = res["bizownqty"].ToString().Trim();
                    info.AllowQty = res["allowqty"].ToString().Trim();
                    info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                    info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                    info.ColdChain = res["coldchain"].ToString().Trim();
                    info.LastSoTime = res["lastsotime"].ToString().Trim();
                    info.ChooseFlag = res["chooseflag"].ToString().Trim();
                    info.PromotionFlag = res["promotionflag"].ToString().Trim();

                    //---------2018-12-26-----新增字段
                    info.InvoiceType = res["invoicetypename"].ToString().Trim();
                    info.AttchInv = res["attchinvname"].ToString().Trim();
                    info.InvPostFlag = res["invpostflagname"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();

                    InoutList.Add(info);
                }

                res.Close();
                res.Dispose();

                seCmd.Dispose();
                seCmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (selCmd != null)
                    selCmd.Dispose();

                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();

                if (insetCmd != null)
                    insetCmd.Dispose();

                if (seCmd != null)
                    seCmd.Dispose();
            }
            return InoutList;
        }
        /// <summary>
        /// ----------------------2018-8-13------------
        /// 部分提交到cms
        /// </summary>
        public SortableBindingList<InoutGenCmsbillXlstemp> ImportGenCmsBillDatas(string batchid, SPRetInfo retinfo)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> InoutList = new SortableBindingList<InoutGenCmsbillXlstemp>();
            string selSql = "SELECT * FROM INOUT_GEN_CMSBILL_XLSTEMP  WHERE batchid =:batchid";

            OracleCommand spCmd = null;
            OracleCommand selCmd = null;
            OracleCommand insetCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;

            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_CMSBILL_IMPORT";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OP_TYPE",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                };


                parameters[0].Value = Int64.Parse(batchid);
                parameters[1].Value = 1;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[2].Size = 8;
                parameters[3].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[2].Value.ToString();
                retinfo.msg = parameters[3].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

                //再次查询此表
                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.Parameters.Add("batchid", batchid);
                seCmd.CommandTimeout = 3600;

                OracleDataReader res = seCmd.ExecuteReader();

                while (res.Read())
                {
                    InoutGenCmsbillXlstemp info = new InoutGenCmsbillXlstemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Empid = res["empid"].ToString().Trim();
                    info.Gengoods = res["gengoods"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Planprc = res["planprc"].ToString().Trim();
                    info.Plancount = res["plancount"].ToString().Trim();
                    info.Pmark = res["pmark"].ToString().Trim();
                    info.Amark = res["amark"].ToString().Trim();
                    info.Genaddresscode = res["genaddresscode"].ToString().Trim();
                    info.Cmsaddresscode = res["cmsaddresscode"].ToString().Trim();
                    info.Checkstate = res["checkstate"].ToString().Trim();
                    info.Checkmsg = res["checkmsg"].ToString().Trim();
                    info.Importstate = res["importstate"].ToString().Trim();
                    info.Importmsg = res["importmsg"].ToString().Trim();
                    info.Goodtype = res["goodtype"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Prcresultcode = res["prcresultcode"].ToString().Trim();
                    info.Prcmsg = res["prcmsg"].ToString().Trim();
                    //---------2018-10-22-----新增字段
                    info.GoodName = res["goodname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.BizownQty = res["bizownqty"].ToString().Trim();
                    info.AllowQty = res["allowqty"].ToString().Trim();
                    info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                    info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                    info.ColdChain = res["coldchain"].ToString().Trim();
                    info.LastSoTime = res["lastsotime"].ToString().Trim();
                    info.ChooseFlag = res["chooseflag"].ToString().Trim();
                    info.PromotionFlag = res["promotionflag"].ToString().Trim();

                    //---------2018-12-26-----新增字段
                    info.InvoiceType = res["invoicetypename"].ToString().Trim();
                    info.AttchInv = res["attchinvname"].ToString().Trim();
                    info.InvPostFlag = res["invpostflagname"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();

                    InoutList.Add(info);
                }

                res.Close();
                res.Dispose();

                seCmd.Dispose();
                seCmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (selCmd != null)
                    selCmd.Dispose();

                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();

                if (insetCmd != null)
                    insetCmd.Dispose();

                if (seCmd != null)
                    seCmd.Dispose();
            }
            return InoutList;
        }
        #region  修改
        /// <summary>
        /// 修改cms导入价存储过程
        /// </summary>
        /// <param name="cstcode"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int UpdateCmsprc(int Batchid, int ExcelSeqid, double Importprc, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            //OracleCommand selCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_IMPORTPRC_UPDATE";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_IMPORTPRC",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Batchid;
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Value = ExcelSeqid;
                parameters[2].Value = Importprc;
                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                retinfo.msg = parameters[4].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {


                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }

        /// <summary>
        /// 修改计划下单量存储过程
        /// </summary>  ------------2018-10-23--------------
        /// <param name="cstcode"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int UpdatePlancount(int Batchid, int ExcelSeqid, double Plancount, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            //OracleCommand selCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_PLANCOUNT_UPDATE";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_PLANCOUNT",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Batchid;
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Value = ExcelSeqid;
                parameters[2].Value = Plancount;
                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                retinfo.msg = parameters[4].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }

        /// <summary>
        /// 修改批卡备注存储过程
        /// </summary>  ------------2018-10-23--------------
        /// <param name="cstcode"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int UpdatePmark(int Batchid, int ExcelSeqid, string Pmark, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_PMARK_UPDATE";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_PMARK",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Batchid;
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Value = ExcelSeqid;
                parameters[2].Value = Pmark;
                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                retinfo.msg = parameters[4].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }

        /// <summary>
        /// 修改审批备注存储过程
        /// </summary>  ------------2018-10-23--------------
        /// <param name="cstcode"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int UpdateAmark(int Batchid, int ExcelSeqid, string Amark, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            //OracleCommand selCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_AMARK_UPDATE";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_AMARK",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Batchid;
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Value = ExcelSeqid;
                parameters[2].Value = Amark;
                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                retinfo.msg = parameters[4].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }

        /// <summary>
        /// 修改总备注存储过程
        /// </summary>  ------------2018-10-23--------------
        /// <param name="cstcode"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int UpdateHdrMark(int Batchid, string HdrMark, SPRetInfo retinfo)
        {

            OracleCommand spCmd = null;
            //OracleCommand selCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_HDRMARK_UPDATE";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("N_HDRMARK",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Batchid;
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Value = HdrMark;
                parameters[2].Size = 8;
                parameters[3].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[2].Value.ToString().Trim();
                retinfo.msg = parameters[3].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;
        }
        #endregion
        public Dictionary<string, string> getCheckMsg() {
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            string selSql = "select * from GEN_CMSBILL_CHECKERROR order by ERRORCODE asc";
            OracleCommand selCmd = null;
            OracleTransaction trans = null;
            try
            {
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                while (res.Read())
                {
                    string ErrorInfo = res["ERRORINFO"].ToString().Trim();
                    string ErrorCode = res["ERRORCODE"].ToString().Trim();
                    myDictionary.Add(ErrorCode, ErrorInfo);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();

            }
            finally
            {
                if (selCmd != null)
                    selCmd.Dispose();

                if (trans != null)
                    trans.Dispose();

            }
            return myDictionary;
        }

        //筛选功能1
        public SortableBindingList<InoutGenCmsbillXlstemp> ScreenInfo(Dictionary<string, string> sqlkeydict, string batchid, string flag, SPRetInfo retinfo)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> infoList = new SortableBindingList<InoutGenCmsbillXlstemp>();
            OracleCommand cmd = null;
            string sql = "";
            if (flag == "yes")
            {
                sql = "select * from INOUT_GEN_CMSBILL_XLSTEMP where batchid =:batchid and checkstate='成功' $";
            }
            else {
                sql = "select * from INOUT_GEN_CMSBILL_XLSTEMP where batchid =:batchid $";
            }
            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("batchid", batchid);
                string whereStr = "";
                //遍历Dictionary中的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                        }
                    }

                }
                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.Add(kv.Key, kv.Value);
                        }
                    }
                }
                string s = sql;
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    InoutGenCmsbillXlstemp info = new InoutGenCmsbillXlstemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Empid = res["empid"].ToString().Trim();
                    info.Gengoods = res["gengoods"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Planprc = res["planprc"].ToString().Trim();
                    info.Plancount = res["plancount"].ToString().Trim();
                    info.Pmark = res["pmark"].ToString().Trim();
                    info.Amark = res["amark"].ToString().Trim();
                    info.Genaddresscode = res["genaddresscode"].ToString().Trim();
                    info.Cmsaddresscode = res["cmsaddresscode"].ToString().Trim();
                    info.Checkstate = res["checkstate"].ToString().Trim();
                    info.Checkmsg = res["checkmsg"].ToString().Trim();
                    info.Importstate = res["importstate"].ToString().Trim();
                    info.Importmsg = res["importmsg"].ToString().Trim();
                    info.Goodtype = res["goodtype"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Prcresultcode = res["prcresultcode"].ToString().Trim();
                    info.Prcmsg = res["prcmsg"].ToString().Trim();
                    info.Quotation = res["quotation"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.Importprc = res["importprc"].ToString().Trim();
                    //---------2018-10-22-----新增字段
                    info.GoodName = res["goodname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.BizownQty = res["bizownqty"].ToString().Trim();
                    info.AllowQty = res["allowqty"].ToString().Trim();
                    info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                    info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                    info.ColdChain = res["coldchain"].ToString().Trim();
                    info.LastSoTime = res["lastsotime"].ToString().Trim();
                    info.ChooseFlag = res["chooseflag"].ToString().Trim();
                    info.PromotionFlag = res["promotionflag"].ToString().Trim();

                    //---------2018-12-26-----新增字段
                    info.InvoiceType = res["invoicetypename"].ToString().Trim();
                    info.AttchInv = res["attchinvname"].ToString().Trim();
                    info.InvPostFlag = res["invpostflagname"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();

                    infoList.Add(info);
                }
                res.Close();
                res = null;
                cmd.Dispose();
                cmd = null;


            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
            }
            finally {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infoList;
        }

        //筛选功能2
        public SortableBindingList<InoutGenCmsbillXlstemp> ScreenInfos(Dictionary<string, string> sqlkeydict, string batchid, string flag, SPRetInfo retinfo)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> infoList = new SortableBindingList<InoutGenCmsbillXlstemp>();
            OracleCommand cmd = null;
            string sql = "";
            if (flag == "yes")
            {
                sql = "select * from INOUT_GEN_CMSBILL_XLSTEMP where batchid =:batchid and checkstate='成功' and coldchain is null $";
            }
            else {
                sql = "select * from INOUT_GEN_CMSBILL_XLSTEMP where batchid =:batchid and coldchain is null $";
            }
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("batchid", batchid);
                string whereStr = "";
                //遍历Dictionary中的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                        }
                    }

                }
                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.Add(kv.Key, kv.Value);
                        }
                    }
                }
                string s = sql;
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    InoutGenCmsbillXlstemp info = new InoutGenCmsbillXlstemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Empid = res["empid"].ToString().Trim();
                    info.Gengoods = res["gengoods"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Planprc = res["planprc"].ToString().Trim();
                    info.Plancount = res["plancount"].ToString().Trim();
                    info.Pmark = res["pmark"].ToString().Trim();
                    info.Amark = res["amark"].ToString().Trim();
                    info.Genaddresscode = res["genaddresscode"].ToString().Trim();
                    info.Cmsaddresscode = res["cmsaddresscode"].ToString().Trim();
                    info.Checkstate = res["checkstate"].ToString().Trim();
                    info.Checkmsg = res["checkmsg"].ToString().Trim();
                    info.Importstate = res["importstate"].ToString().Trim();
                    info.Importmsg = res["importmsg"].ToString().Trim();
                    info.Goodtype = res["goodtype"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Prcresultcode = res["prcresultcode"].ToString().Trim();
                    info.Prcmsg = res["prcmsg"].ToString().Trim();
                    info.Quotation = res["quotation"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.Importprc = res["importprc"].ToString().Trim();
                    //---------2018-10-22-----新增字段
                    info.GoodName = res["goodname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.BizownQty = res["bizownqty"].ToString().Trim();
                    info.AllowQty = res["allowqty"].ToString().Trim();
                    info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                    info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                    info.ColdChain = res["coldchain"].ToString().Trim();
                    info.LastSoTime = res["lastsotime"].ToString().Trim();
                    info.ChooseFlag = res["chooseflag"].ToString().Trim();
                    info.PromotionFlag = res["promotionflag"].ToString().Trim();

                    //---------2018-12-26-----新增字段
                    info.InvoiceType = res["invoicetypename"].ToString().Trim();
                    info.AttchInv = res["attchinvname"].ToString().Trim();
                    info.InvPostFlag = res["invpostflagname"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();

                    infoList.Add(info);
                }
                res.Close();
                res = null;
                cmd.Dispose();
                cmd = null;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infoList;
        }
        //获取没有goodid的数据（未对码的数据）
        public SortableBindingList<InoutGenCmsbillXlstemp> getNotGoodidData(string batchid)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> infoList = new SortableBindingList<InoutGenCmsbillXlstemp>();
            string sql = "select batchid,excel_seqid from inout_gen_cmsbill_xlstemp where goodid is null and batchid =:batchid";
            OracleCommand cmd = null;
            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("batchid", batchid);

                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read()) {
                    InoutGenCmsbillXlstemp info = new InoutGenCmsbillXlstemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    infoList.Add(info);
                }
                res.Close();
                res = null;
                cmd.Dispose();
                cmd = null;

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误提示");
            } finally {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infoList;
        }

        //调用存储过程 去获取goodid  GetGoodids
        public int GetGoodids(InoutGenCmsbillXlstemp info, SPRetInfo retinfo) {
            SortableBindingList<InoutGenCmsbillXlstemp> infoList = new SortableBindingList<InoutGenCmsbillXlstemp>();
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_CMSBILL_GOODS";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SQLID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OP_TYPE",OracleDbType.Int64,ParameterDirection.Input),


                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                };


                parameters[0].Value = Int64.Parse(info.Batchid);
                parameters[1].Value = Int64.Parse(info.ExcelSeqid);
                parameters[2].Value = 1;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString();
                retinfo.msg = parameters[4].Value.ToString().Trim();

                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[4].Value.ToString().Trim();
                    retinfo.result = info.Batchid;
                }
                else
                {
                    retinfo.msg = parameters[4].Value.ToString().Trim();
                }

                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;
        }

        //

        //再次查询中间表 确定未对码数据
        public InoutGenCmsbillXlstemp SelData(InoutGenCmsbillXlstemp infos) {

            InoutGenCmsbillXlstemp info = new InoutGenCmsbillXlstemp();
            string selSql = "select * from INOUT_GEN_CMSBILL_XLSTEMP WHERE batchid =:batchid and excel_seqid=:excel_seqid and goodid is not null";
            OracleCommand cmd = null;
            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = selSql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("batchid", infos.Batchid);
                cmd.Parameters.Add("excel_seqid", infos.ExcelSeqid);

                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Empid = res["empid"].ToString().Trim();
                    info.Gengoods = res["gengoods"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Planprc = res["planprc"].ToString().Trim();
                    info.Plancount = res["plancount"].ToString().Trim();
                    info.Pmark = res["pmark"].ToString().Trim();
                    info.Amark = res["amark"].ToString().Trim();
                    info.Genaddresscode = res["genaddresscode"].ToString().Trim();
                    info.Cmsaddresscode = res["cmsaddresscode"].ToString().Trim();
                    info.Checkstate = res["checkstate"].ToString().Trim();
                    info.Checkmsg = res["checkmsg"].ToString().Trim();
                    info.Importstate = res["importstate"].ToString().Trim();
                    info.Importmsg = res["importstate"].ToString().Trim();
                    info.Goodtype = res["goodtype"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Prcresultcode = res["prcresultcode"].ToString().Trim();
                    info.Prcmsg = res["prcmsg"].ToString().Trim();
                    info.Quotation = res["quotation"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.Importprc = res["importprc"].ToString().Trim();
                    info.GoodName = res["goodname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.BizownQty = res["bizownqty"].ToString().Trim();
                    info.AllowQty = res["allowqty"].ToString().Trim();
                    info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                    info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                    info.ColdChain = res["coldchain"].ToString().Trim();
                    info.LastSoTime = res["lastsotime"].ToString().Trim();
                }
                res.Close();
                res = null;
                cmd.Dispose();
                cmd = null;

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误提示");
            } finally {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return info;
        }



        //获取库存详细信息
        public SortableBindingList<BizInfo> getBizInfo(InoutGenCmsbillXlstemp inoutGenCmsbillXlstemp, SPRetInfo retinfo) {

            SortableBindingList<BizInfo> infoList = new SortableBindingList<BizInfo>();
            string sql = "select * from V_SALE_OWNERCHG_LOTSTOCK where goodid =:goodid and lownerid=:lownerid and lsaledeptid=:lsaledeptid ORDER BY GLEVEL,ENDDATE,PRDDATE";
            OracleCommand cmd = null;
            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("goodid", inoutGenCmsbillXlstemp.Goodid);
                cmd.Parameters.Add("lownerid", inoutGenCmsbillXlstemp.Ownerid);
                cmd.Parameters.Add("lsaledeptid", inoutGenCmsbillXlstemp.Saledeptid);
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read()) {
                    BizInfo bizInfo = new BizInfo();
                    bizInfo.OwnerName = res["ownername"].ToString().Trim();
                    bizInfo.Lotno = res["lotno"].ToString().Trim();
                    bizInfo.BatchNo = res["batchno"].ToString().Trim();
                    bizInfo.EmpName = res["empname"].ToString().Trim();

                    string time = res["prddate"].ToString();
                    DateTime dt2 = Convert.ToDateTime(time);
                    string nm = dt2.ToString("yyyy-MM-dd");
                    bizInfo.PrdDate = nm;

                    string times = res["enddate"].ToString().Trim();
                    DateTime dt1 = Convert.ToDateTime(times);
                    string mf = dt1.ToString("yyyy-MM-dd");
                    bizInfo.EndDate = mf;

                    bizInfo.AlloQty = res["allo_qty"].ToString().Trim();
                    bizInfo.UnAlloQty = res["unallo_qty"].ToString().Trim();
                    bizInfo.Description = res["description"].ToString().Trim();
                    bizInfo.SalbillType = res["salbilltype"].ToString().Trim();

                    infoList.Add(bizInfo);
                }
                res.Close();
                res = null;
                cmd.Dispose();
                cmd = null;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");

            } finally {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infoList;
        }

        #region 勾选处理
        //勾选 更新为1
        public void UpdateChooseFlag(InoutGenCmsbillXlstemp info) {

            string sql = "update INOUT_GEN_CMSBILL_XLSTEMP set chooseflag=1 where batchid=:batchid and excel_seqid =:excel_seqid ";
            OracleCommand cmd = null;
            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("batchid", info.Batchid);
                cmd.Parameters.Add("excel_seqid", info.ExcelSeqid);

                cmd.ExecuteNonQuery();


            } catch (Exception ex) {

                MessageBox.Show(ex.ToString());

            } finally {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }

        }

        //取消勾选 UpdateChooseFlagFalse
        public void UpdateChooseFlagFalse(InoutGenCmsbillXlstemp info)
        {

            string sql = "update INOUT_GEN_CMSBILL_XLSTEMP set chooseflag=0 where batchid=:batchid and excel_seqid =:excel_seqid ";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("batchid", info.Batchid);
                cmd.Parameters.Add("excel_seqid", info.ExcelSeqid);

                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }

        }
        //取消全部勾选 
        public void Cancel(string batchid)
        {

            string sql = "update INOUT_GEN_CMSBILL_XLSTEMP set chooseflag=0 where batchid=:batchid";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("batchid", batchid);

                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }

        }
        #endregion

        //收货配置信息查询
        public SortableBindingList<CstCheckConfig> GetCstCheckConfig(Dictionary<string, string> sqlkeydict)
        {
            SortableBindingList<CstCheckConfig> infoList = new SortableBindingList<CstCheckConfig>();
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            string sql = "select * from GEN_CMSBILL_CSTCHECK_CONFIG where ownerid=:ownerid and compid=:compid $";
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;


                string whereStr = "";
                //遍历Dictionary中的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");

                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                        }

                    }

                }
                sql = sql.Replace("$", whereStr);
                cmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);
                cmd.Parameters.Add("compid", Properties.Settings.Default.COMPID);
                cmd.CommandText = sql.ToString();
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.Add(kv.Key, kv.Value);
                        }
                    }
                }
                string s = sql;
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    CstCheckConfig infos = new CstCheckConfig();
                    infos.OwnerId = res["OWNERID"].ToString().Trim();
                    infos.CompId = res["COMPID"].ToString().Trim();
                    infos.Cstid = res["CSTID"].ToString().Trim();
                    infos.CstCode = res["CSTCODE"].ToString().Trim();
                    infos.CstName = res["CSTNAME"].ToString().Trim();
                    infos.ManuFacture = res["MANUFACTURE"].ToString().Trim();
                    infos.ExtMark = res["EXTMARK"].ToString().Trim();
                    infos.ModifyUser = res["MODIFYUSER"].ToString().Trim();
                    infos.ModifyTime = res["MODIFYTIME"].ToString().Trim();
                    infoList.Add(infos);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                if (trans != null)
                    trans.Dispose();

            }
            return infoList;
        }


        //删除收货配置信息
        public int DeleteCstCheckConfig(SortableBindingList<DelTempCstCheckConfig> List, SPRetInfo retinfo)
        {
            string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            string insertSql = "insert into GEN_CMSBILL_CSTCONF_DELTEMP(BATCHID,CSTID,COMPID,OWNERID)values(:Batchid,:CstId,:Compid,:OwnerId)";//插入中间表

            OracleCommand selCmd = null;
            OracleCommand insertCmd = null;
            OracleCommand spCmd = null;
            OracleTransaction trans = null;

            int retCode = -1;
            string sqlID = "0";

            try
            {
                //查询函数获取批次号
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    sqlID = res["SEQ"].ToString().Trim();
                res.Close();
                res.Dispose();
                selCmd.Dispose();
                selCmd = null;

                trans = Conn_datacenter_cmszh.BeginTransaction();
                //插入中间表
                insertCmd = Conn_datacenter_cmszh.CreateCommand();
                insertCmd.Connection = Conn_datacenter_cmszh;
                insertCmd.CommandType = System.Data.CommandType.Text;
                insertCmd.CommandText = insertSql.ToString();
                insertCmd.CommandTimeout = 3600;
                insertCmd.Transaction = trans;
                int recCount = 0;

                foreach (DelTempCstCheckConfig selected in List)
                {
                    recCount++;

                    insertCmd.Parameters.Clear();

                    insertCmd.Parameters.Add("Batchid", int.Parse(sqlID));
                    insertCmd.Parameters.Add("CstId", selected.Cstid);
                    insertCmd.Parameters.Add("Compid", Properties.Settings.Default.COMPID);
                    insertCmd.Parameters.Add("OwnerId", Properties.Settings.Default.OWNERID);

                    insertCmd.ExecuteNonQuery();
                }
                trans.Commit();
                trans.Dispose();
                trans = null;

                insertCmd.Dispose();
                insertCmd = null;

                if (recCount < 1)
                {
                    retinfo.num = "0";
                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return 0;
                }

                //执行存储过程,进行删除
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_CSTCONF_DEL";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
            };
                parameters[0].Value = Int64.Parse(sqlID);

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[1].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[2].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[2].Value.ToString().Trim();
                }
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
                if (trans != null)
                {
                    trans.Rollback();
                    trans.Dispose();
                    trans = null;
                }
            }
            return retCode;
        }
        //修改  带出客户名称
        public string GetCstName(string cstcode, SPRetInfo retinfo) {
            string cstid = "";
            OracleCommand cmd = null;
            OracleTransaction trans = null;

            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PG_BILL_QUOTE.P_GET_CSTINFO";
                cmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_CSTCODE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("OUT_CSTID",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
            };
                parameters[0].Value = cstcode;

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 8;
                parameters[3].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                int ret = cmd.ExecuteNonQuery();
                retinfo.num = parameters[2].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[3].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[3].Value.ToString().Trim();
                }
                cmd.Dispose();
                cmd = null;
                cstid = parameters[1].Value.ToString().Trim();

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
            } finally {
                if (cmd != null)
                    cmd.Dispose();
                if (trans != null)
                {
                    trans.Rollback();
                    trans.Dispose();
                    trans = null;
                }
            }

            return cstid;
        }

        //修改收货配置信息
        public int UpdateCstCheckConfig(string Cstid, string ManuFacture, string ExtMark, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_CSTCONF_MODIFY";
                spCmd.CommandTimeout = 3600;
                //向存储过程中添加输入输出
                OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_MANUFACTURE",OracleDbType.Int64 ,ParameterDirection.Input),
                    new OracleParameter("IN_EXTMARK",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[2].Value = Int64.Parse(Cstid);
                parameters[3].Value = Int64.Parse(ManuFacture);
                parameters[4].Value = ExtMark;
                parameters[5].Value = SessionDto.Empid;


                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[6].Size = 8;
                parameters[7].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[6].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[7].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[7].Value.ToString().Trim();
                }
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;
        }
        //新增 收货配置信息
        public int AddCstCheckConfig(string Cstid, string Cstcode, string ManuFacture, string ExtMark, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_CSTCONF_ADD";
                spCmd.CommandTimeout = 3600;
                //向存储过程中添加输入输出
                OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTCODE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_MANUFACTURE",OracleDbType.Int64 ,ParameterDirection.Input),
                    new OracleParameter("IN_EXTMARK",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[2].Value = Int64.Parse(Cstid);
                parameters[3].Value = Cstcode;
                parameters[4].Value = Int64.Parse(ManuFacture);
                parameters[5].Value = ExtMark;
                parameters[6].Value = SessionDto.Empid;


                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[7].Size = 8;
                parameters[8].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[7].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[8].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[8].Value.ToString().Trim();
                }
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;
        }

        //--------2018-11-1---------
        //提示客户效期
        public int Tips(string Cstid, SPRetInfo retinfo) {
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PG_GEN_CMSBILL.P_LOT_CONF_CHECK";
                cmd.CommandTimeout = 3600;

                //向存储过程中添加输入输出
                OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_SALEDEPTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTID",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64 ,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[2].Value = SessionDto.Empdeptid;
                parameters[3].Value = Cstid;
                parameters[4].Value = -1;

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[5].Size = 8;
                parameters[6].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                int ret = cmd.ExecuteNonQuery();
                retinfo.num = parameters[5].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[6].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[6].Value.ToString().Trim();
                }
                cmd.Dispose();
                cmd = null;
                retCode = 0;
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            } finally {
                if (cmd != null)
                    cmd.Dispose();
            }
            return retCode;
        }


        #region 对接订单处理
        public Dictionary<string, string> getInfCstName()
        {
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            string selSql = "SELECT ACCOUNTID,ACCOUNTNAME FROM V_ABUTJOIN_INF_DISPATCH order by ACCOUNTID asc";
            OracleCommand selCmd = null;
            OracleTransaction trans = null;
            try
            {
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                while (res.Read())
                {
                    string AccountId = res["ACCOUNTID"].ToString().Trim();
                    string AccountName = res["ACCOUNTNAME"].ToString().Trim();
                    myDictionary.Add(AccountId, AccountName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();

            }
            finally
            {
                if (selCmd != null)
                    selCmd.Dispose();

                if (trans != null)
                    trans.Dispose();

            }
            return myDictionary;
        }
        //查询
        public SortableBindingList<OrderButtDto> GetOrderButtInfo(string begintime, string endtime, Dictionary<string, string> sqlkeydict) {
            SortableBindingList<OrderButtDto> infoList = new SortableBindingList<OrderButtDto>();
            OracleCommand cmd = null;
            if ((StringUtils.IsNotNull(begintime)) && (StringUtils.IsNotNull(endtime))) {
                string sql = "select t.excel_seqid,t.batchid,t.infcstname,t.orderid,t.ordertime,t.cstcode,t.cstname,t.goods,t.goodname,t.spec,t.producer,t.gengoods,t.planprc," +
                    "t.plancount,t.quotation,t.goodsalecheck,t.bizownqty,t.allowqty,t.allowbatnum,t.checkmsg,t.coldchain,t.prc,t.pmark,t.amark," +
                    " t.cmsaddresscode,t.addr_line_1,t.bargain,t.bottomprc,t.goodtype,t.checkstate,t.importstate,t.importmsg,t.cstmsg,t.chooseflag,t.goodid,t.cstid,t.compid,t.ownerid,t.saledeptid,t.price,t.bottomprice,t.costprc,t.costprice from INOUT_GEN_CMSBILL_XLSTEMP t where t.ownerid=:ownerid and t.compid=:compid and t.ordertime >= to_date(:begintime, 'YY-MM-DD hh24:mi:ss') and t.ordertime <= to_date(:endtime, 'YY-MM-DD hh24:mi:ss') $ ";
                try {
                    cmd = Conn_datacenter_cmszh.CreateCommand();
                    cmd.Connection = Conn_datacenter_cmszh;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);
                    cmd.Parameters.Add("compid", Properties.Settings.Default.COMPID);

                    cmd.Parameters.Add("begintime", begintime);
                    cmd.Parameters.Add("endtime", endtime);
                    string whereStr = "";
                    //遍历Dictionary中的key值
                    foreach (KeyValuePair<string, string> kv in sqlkeydict)
                    {
                        if (kv.Key.Equals(null) || kv.Key.Equals(""))
                        {
                            whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            if (kv.Key.IndexOf("%") > -1)
                            {
                                whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");
                            }
                            else
                            {
                                whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                            }
                        }

                    }
                    sql = sql.Replace("$", whereStr);
                    cmd.CommandText = sql.ToString();
                    //遍历Dictionary中的value值
                    foreach (KeyValuePair<string, string> kv in sqlkeydict)
                    {
                        if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                        {
                            if (kv.Key.IndexOf("%") > -1)
                            {
                                cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                            }
                            else
                            {
                                cmd.Parameters.Add(kv.Key, kv.Value);
                            }
                        }
                    }
                    string s = sql;
                    OracleDataReader res = cmd.ExecuteReader();
                    while (res.Read())
                    {
                        OrderButtDto info = new OrderButtDto();
                        info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                        info.Batchid = res["batchid"].ToString().Trim();
                        info.InfCstName = res["infcstname"].ToString().Trim();
                        info.OrderId = res["orderid"].ToString().Trim();
                        info.OrderTime = res["ordertime"].ToString().Trim();
                        info.CstCode = res["cstcode"].ToString().Trim();
                        info.CstName = res["cstname"].ToString().Trim();
                        info.Goods = res["goods"].ToString().Trim();
                        info.GoodName = res["goodname"].ToString().Trim();
                        info.Spec = res["spec"].ToString().Trim();
                        info.Producer = res["producer"].ToString().Trim();
                        info.GenGoods = res["gengoods"].ToString().Trim();
                        info.PlanPrc = res["planprc"].ToString().Trim();
                        info.PlanCount = res["plancount"].ToString().Trim();
                        info.Quotation = res["quotation"].ToString().Trim();
                        info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                        info.BizOwnQty = res["bizownqty"].ToString().Trim();
                        info.AllowQty = res["allowqty"].ToString().Trim();
                        info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                        info.CheckMsg = res["checkmsg"].ToString().Trim();
                        info.ColdChain = res["coldchain"].ToString().Trim();
                        info.Prc = res["prc"].ToString().Trim();
                        info.Pmark = res["pmark"].ToString().Trim();
                        info.Amark = res["amark"].ToString().Trim();
                        info.CmsAddressCode = res["cmsaddresscode"].ToString().Trim();
                        info.Addr_Line_1 = res["addr_line_1"].ToString().Trim();
                        info.Bargain = res["bargain"].ToString().Trim();
                        info.BottomPrc = res["bottomprc"].ToString().Trim();
                        info.GoodType = res["goodtype"].ToString().Trim();
                        info.CheckState = res["checkstate"].ToString().Trim();
                        info.ImportState = res["importstate"].ToString().Trim();
                        info.ImportMsg = res["importmsg"].ToString().Trim();
                        info.CstMsg = res["cstmsg"].ToString().Trim();
                        info.ChooseFlag = res["chooseflag"].ToString().Trim();
                        info.GoodId = res["goodid"].ToString().Trim();
                        info.CstId = res["cstid"].ToString().Trim();
                        info.CompId = res["compid"].ToString().Trim();
                        info.OwnerId = res["ownerid"].ToString().Trim();
                        info.SaleDeptId = res["saledeptid"].ToString().Trim();
                        info.Price = res["price"].ToString().Trim();
                        info.BottomPrice = res["bottomprice"].ToString().Trim();
                        info.CostPrc = res["costprc"].ToString().Trim();
                        info.CostPrice = res["costprice"].ToString().Trim();
                        infoList.Add(info);
                    }
                    res.Close();
                    res = null;
                    cmd.Dispose();
                    cmd = null;
                } catch (Exception ex) {
                    MessageBox.Show(ex.ToString(), "错误信息");
                } finally {
                    if (cmd != null)
                        cmd.Dispose();
                    cmd = null;
                }

            }
            if ((StringUtils.IsNotNull(begintime)) && (StringUtils.IsNull(endtime))) {
                string sql = "select t.excel_seqid,t.batchid,t.infcstname,t.orderid,t.ordertime,t.cstcode,t.cstname,t.goods,t.goodname,t.spec,t.producer,t.gengoods,t.planprc," +
                   "t.plancount,t.quotation,t.goodsalecheck,t.bizownqty,t.allowqty,t.allowbatnum,t.checkmsg,t.coldchain,t.prc,t.pmark,t.amark," +
                   " t.cmsaddresscode,t.addr_line_1,t.bargain,t.bottomprc,t.goodtype,t.checkstate,t.importstate,t.importmsg,t.cstmsg,t.chooseflag,t.goodid,t.cstid,t.compid,t.ownerid,t.saledeptid,t.price,t.bottomprice,t.costprc,t.costprice from INOUT_GEN_CMSBILL_XLSTEMP t where t.ownerid=:ownerid and t.compid=:compid and t.ordertime >= to_date(:begintime, 'YY-MM-DD hh24:mi:ss') $ ";
                try
                {
                    cmd = Conn_datacenter_cmszh.CreateCommand();
                    cmd.Connection = Conn_datacenter_cmszh;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);
                    cmd.Parameters.Add("compid", Properties.Settings.Default.COMPID);

                    cmd.Parameters.Add("begintime", begintime);
                    string whereStr = "";
                    //遍历Dictionary中的key值
                    foreach (KeyValuePair<string, string> kv in sqlkeydict)
                    {
                        if (kv.Key.Equals(null) || kv.Key.Equals(""))
                        {
                            whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            if (kv.Key.IndexOf("%") > -1)
                            {
                                whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");
                            }
                            else
                            {
                                whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                            }
                        }

                    }
                    sql = sql.Replace("$", whereStr);
                    cmd.CommandText = sql.ToString();
                    //遍历Dictionary中的value值
                    foreach (KeyValuePair<string, string> kv in sqlkeydict)
                    {
                        if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                        {
                            if (kv.Key.IndexOf("%") > -1)
                            {
                                cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                            }
                            else
                            {
                                cmd.Parameters.Add(kv.Key, kv.Value);
                            }
                        }
                    }
                    string s = sql;
                    OracleDataReader res = cmd.ExecuteReader();
                    while (res.Read())
                    {
                        OrderButtDto info = new OrderButtDto();
                        info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                        info.Batchid = res["batchid"].ToString().Trim();
                        info.InfCstName = res["infcstname"].ToString().Trim();
                        info.OrderId = res["orderid"].ToString().Trim();
                        info.OrderTime = res["ordertime"].ToString().Trim();
                        info.CstCode = res["cstcode"].ToString().Trim();
                        info.CstName = res["cstname"].ToString().Trim();
                        info.Goods = res["goods"].ToString().Trim();
                        info.GoodName = res["goodname"].ToString().Trim();
                        info.Spec = res["spec"].ToString().Trim();
                        info.Producer = res["producer"].ToString().Trim();
                        info.GenGoods = res["gengoods"].ToString().Trim();
                        info.PlanPrc = res["planprc"].ToString().Trim();
                        info.PlanCount = res["plancount"].ToString().Trim();
                        info.Quotation = res["quotation"].ToString().Trim();
                        info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                        info.BizOwnQty = res["bizownqty"].ToString().Trim();
                        info.AllowQty = res["allowqty"].ToString().Trim();
                        info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                        info.CheckMsg = res["checkmsg"].ToString().Trim();
                        info.ColdChain = res["coldchain"].ToString().Trim();
                        info.Prc = res["prc"].ToString().Trim();
                        info.Pmark = res["pmark"].ToString().Trim();
                        info.Amark = res["amark"].ToString().Trim();
                        info.CmsAddressCode = res["cmsaddresscode"].ToString().Trim();
                        info.Addr_Line_1 = res["addr_line_1"].ToString().Trim();
                        info.Bargain = res["bargain"].ToString().Trim();
                        info.BottomPrc = res["bottomprc"].ToString().Trim();
                        info.GoodType = res["goodtype"].ToString().Trim();
                        info.CheckState = res["checkstate"].ToString().Trim();
                        info.ImportState = res["importstate"].ToString().Trim();
                        info.ImportMsg = res["importmsg"].ToString().Trim();
                        info.CstMsg = res["cstmsg"].ToString().Trim();
                        info.ChooseFlag = res["chooseflag"].ToString().Trim();
                        info.GoodId = res["goodid"].ToString().Trim();
                        info.CstId = res["cstid"].ToString().Trim();
                        info.CompId = res["compid"].ToString().Trim();
                        info.OwnerId = res["ownerid"].ToString().Trim();
                        info.SaleDeptId = res["saledeptid"].ToString().Trim();
                        info.Price = res["price"].ToString().Trim();
                        info.BottomPrice = res["bottomprice"].ToString().Trim();
                        info.CostPrc = res["costprc"].ToString().Trim();
                        info.CostPrice = res["costprice"].ToString().Trim();
                        infoList.Add(info);
                    }
                    res.Close();
                    res = null;
                    cmd.Dispose();
                    cmd = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "错误信息");
                }
                finally
                {
                    if (cmd != null)
                        cmd.Dispose();
                    cmd = null;
                }
            }
            if ((StringUtils.IsNull(begintime)) && (StringUtils.IsNotNull(endtime))) {
                string sql = "select t.excel_seqid,t.batchid,t.infcstname,t.orderid,t.ordertime,t.cstcode,t.cstname,t.goods,t.goodname,t.spec,t.producer,t.gengoods,t.planprc," +
                   "t.plancount,t.quotation,t.goodsalecheck,t.bizownqty,t.allowqty,t.allowbatnum,t.checkmsg,t.coldchain,t.prc,t.pmark,t.amark," +
                   " t.cmsaddresscode,t.addr_line_1,t.bargain,t.bottomprc,t.goodtype,t.checkstate,t.importstate,t.importmsg,t.cstmsg,t.chooseflag,t.goodid,t.cstid,t.compid,t.ownerid,t.saledeptid,t.price,t.bottomprice,t.costprc,t.costprice from INOUT_GEN_CMSBILL_XLSTEMP t where t.ownerid=:ownerid and t.compid=:compid and t.ordertime <= to_date(:endtime, 'YY-MM-DD hh24:mi:ss') $ ";
                try
                {
                    cmd = Conn_datacenter_cmszh.CreateCommand();
                    cmd.Connection = Conn_datacenter_cmszh;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);
                    cmd.Parameters.Add("compid", Properties.Settings.Default.COMPID);

                    cmd.Parameters.Add("endtime", endtime);
                    string whereStr = "";
                    //遍历Dictionary中的key值
                    foreach (KeyValuePair<string, string> kv in sqlkeydict)
                    {
                        if (kv.Key.Equals(null) || kv.Key.Equals(""))
                        {
                            whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            if (kv.Key.IndexOf("%") > -1)
                            {
                                whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");
                            }
                            else
                            {
                                whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                            }
                        }

                    }
                    sql = sql.Replace("$", whereStr);
                    cmd.CommandText = sql.ToString();
                    //遍历Dictionary中的value值
                    foreach (KeyValuePair<string, string> kv in sqlkeydict)
                    {
                        if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                        {
                            if (kv.Key.IndexOf("%") > -1)
                            {
                                cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                            }
                            else
                            {
                                cmd.Parameters.Add(kv.Key, kv.Value);
                            }
                        }
                    }
                    string s = sql;
                    OracleDataReader res = cmd.ExecuteReader();
                    while (res.Read())
                    {
                        OrderButtDto info = new OrderButtDto();
                        info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                        info.Batchid = res["batchid"].ToString().Trim();
                        info.InfCstName = res["infcstname"].ToString().Trim();
                        info.OrderId = res["orderid"].ToString().Trim();
                        info.OrderTime = res["ordertime"].ToString().Trim();
                        info.CstCode = res["cstcode"].ToString().Trim();
                        info.CstName = res["cstname"].ToString().Trim();
                        info.Goods = res["goods"].ToString().Trim();
                        info.GoodName = res["goodname"].ToString().Trim();
                        info.Spec = res["spec"].ToString().Trim();
                        info.Producer = res["producer"].ToString().Trim();
                        info.GenGoods = res["gengoods"].ToString().Trim();
                        info.PlanPrc = res["planprc"].ToString().Trim();
                        info.PlanCount = res["plancount"].ToString().Trim();
                        info.Quotation = res["quotation"].ToString().Trim();
                        info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                        info.BizOwnQty = res["bizownqty"].ToString().Trim();
                        info.AllowQty = res["allowqty"].ToString().Trim();
                        info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                        info.CheckMsg = res["checkmsg"].ToString().Trim();
                        info.ColdChain = res["coldchain"].ToString().Trim();
                        info.Prc = res["prc"].ToString().Trim();
                        info.Pmark = res["pmark"].ToString().Trim();
                        info.Amark = res["amark"].ToString().Trim();
                        info.CmsAddressCode = res["cmsaddresscode"].ToString().Trim();
                        info.Addr_Line_1 = res["addr_line_1"].ToString().Trim();
                        info.Bargain = res["bargain"].ToString().Trim();
                        info.BottomPrc = res["bottomprc"].ToString().Trim();
                        info.GoodType = res["goodtype"].ToString().Trim();
                        info.CheckState = res["checkstate"].ToString().Trim();
                        info.ImportState = res["importstate"].ToString().Trim();
                        info.ImportMsg = res["importmsg"].ToString().Trim();
                        info.CstMsg = res["cstmsg"].ToString().Trim();
                        info.ChooseFlag = res["chooseflag"].ToString().Trim();
                        info.GoodId = res["goodid"].ToString().Trim();
                        info.CstId = res["cstid"].ToString().Trim();
                        info.CompId = res["compid"].ToString().Trim();
                        info.OwnerId = res["ownerid"].ToString().Trim();
                        info.SaleDeptId = res["saledeptid"].ToString().Trim();
                        info.Price = res["price"].ToString().Trim();
                        info.BottomPrice = res["bottomprice"].ToString().Trim();
                        info.CostPrc = res["costprc"].ToString().Trim();
                        info.CostPrice = res["costprice"].ToString().Trim();
                        infoList.Add(info);
                    }
                    res.Close();
                    res = null;
                    cmd.Dispose();
                    cmd = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "错误信息");
                }
                finally
                {
                    if (cmd != null)
                        cmd.Dispose();
                    cmd = null;
                }


            }
            if ((StringUtils.IsNull(begintime)) && (StringUtils.IsNull(endtime))) {
                string sql = "select t.excel_seqid,t.batchid,t.infcstname,t.orderid,t.ordertime,t.cstcode,t.cstname,t.goods,t.goodname,t.spec,t.producer,t.gengoods,t.planprc," +
                    "t.plancount,t.quotation,t.goodsalecheck,t.bizownqty,t.allowqty,t.allowbatnum,t.checkmsg,t.coldchain,t.prc,t.pmark,t.amark," +
                    " t.cmsaddresscode,t.addr_line_1,t.bargain,t.bottomprc,t.goodtype,t.checkstate,t.importstate,t.importmsg,t.cstmsg,t.chooseflag,t.goodid,t.cstid,t.compid,t.ownerid,t.saledeptid,t.price,t.bottomprice,t.costprc,t.costprice from  INOUT_GEN_CMSBILL_XLSTEMP t where t.ownerid=:ownerid and t.compid=:compid $ ";
                try
                {
                    cmd = Conn_datacenter_cmszh.CreateCommand();
                    cmd.Connection = Conn_datacenter_cmszh;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);
                    cmd.Parameters.Add("compid", Properties.Settings.Default.COMPID);

                    string whereStr = "";
                    //遍历Dictionary中的key值
                    foreach (KeyValuePair<string, string> kv in sqlkeydict)
                    {
                        if (kv.Key.Equals(null) || kv.Key.Equals(""))
                        {
                            whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            if (kv.Key.IndexOf("%") > -1)
                            {
                                whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");
                            }
                            else
                            {
                                whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                            }
                        }

                    }
                    sql = sql.Replace("$", whereStr);
                    cmd.CommandText = sql.ToString();
                    //遍历Dictionary中的value值
                    foreach (KeyValuePair<string, string> kv in sqlkeydict)
                    {
                        if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                        {
                            if (kv.Key.IndexOf("%") > -1)
                            {
                                cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                            }
                            else
                            {
                                cmd.Parameters.Add(kv.Key, kv.Value);
                            }
                        }
                    }
                    string s = sql;
                    OracleDataReader res = cmd.ExecuteReader();
                    while (res.Read())
                    {
                        OrderButtDto info = new OrderButtDto();
                        info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                        info.Batchid = res["batchid"].ToString().Trim();
                        info.InfCstName = res["infcstname"].ToString().Trim();
                        info.OrderId = res["orderid"].ToString().Trim();
                        info.OrderTime = res["ordertime"].ToString().Trim();
                        info.CstCode = res["cstcode"].ToString().Trim();
                        info.CstName = res["cstname"].ToString().Trim();
                        info.Goods = res["goods"].ToString().Trim();
                        info.GoodName = res["goodname"].ToString().Trim();
                        info.Spec = res["spec"].ToString().Trim();
                        info.Producer = res["producer"].ToString().Trim();
                        info.GenGoods = res["gengoods"].ToString().Trim();
                        info.PlanPrc = res["planprc"].ToString().Trim();
                        info.PlanCount = res["plancount"].ToString().Trim();
                        info.Quotation = res["quotation"].ToString().Trim();
                        info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                        info.BizOwnQty = res["bizownqty"].ToString().Trim();
                        info.AllowQty = res["allowqty"].ToString().Trim();
                        info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                        info.CheckMsg = res["checkmsg"].ToString().Trim();
                        info.ColdChain = res["coldchain"].ToString().Trim();
                        info.Prc = res["prc"].ToString().Trim();
                        info.Pmark = res["pmark"].ToString().Trim();
                        info.Amark = res["amark"].ToString().Trim();
                        info.CmsAddressCode = res["cmsaddresscode"].ToString().Trim();
                        info.Addr_Line_1 = res["addr_line_1"].ToString().Trim();
                        info.Bargain = res["bargain"].ToString().Trim();
                        info.BottomPrc = res["bottomprc"].ToString().Trim();
                        info.GoodType = res["goodtype"].ToString().Trim();
                        info.CheckState = res["checkstate"].ToString().Trim();
                        info.ImportState = res["importstate"].ToString().Trim();
                        info.ImportMsg = res["importmsg"].ToString().Trim();
                        info.CstMsg = res["cstmsg"].ToString().Trim();
                        info.ChooseFlag = res["chooseflag"].ToString().Trim();
                        info.GoodId = res["goodid"].ToString().Trim();
                        info.CstId = res["cstid"].ToString().Trim();
                        info.CompId = res["compid"].ToString().Trim();
                        info.OwnerId = res["ownerid"].ToString().Trim();
                        info.SaleDeptId = res["saledeptid"].ToString().Trim();
                        info.Price = res["price"].ToString().Trim();
                        info.BottomPrice = res["bottomprice"].ToString().Trim();
                        info.CostPrc = res["costprc"].ToString().Trim();
                        info.CostPrice = res["costprice"].ToString().Trim();
                        infoList.Add(info);
                    }
                    res.Close();
                    res = null;
                    cmd.Dispose();
                    cmd = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "错误信息");
                }
                finally
                {
                    if (cmd != null)
                        cmd.Dispose();
                    cmd = null;
                }
            }
            return infoList;
        }

        //勾选 更新为1
        public void ChangeFlag(OrderButtDto info)
        {

            string sql = "update INOUT_GEN_CMSBILL_XLSTEMP set chooseflag=1 where batchid=:batchid and excel_seqid =:excel_seqid ";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("batchid", info.Batchid);
                cmd.Parameters.Add("excel_seqid", info.ExcelSeqid);

                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }

        }

        //取消勾选 UpdateChooseFlagFalse
        public void ChangeFlagNot(OrderButtDto info)
        {

            string sql = "update INOUT_GEN_CMSBILL_XLSTEMP set chooseflag=0 where batchid=:batchid and excel_seqid =:excel_seqid ";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("batchid", info.Batchid);
                cmd.Parameters.Add("excel_seqid", info.ExcelSeqid);

                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }

        }
        //获取新的批次号
        public string GetNewBatchId()
        {
            string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            OracleCommand selCmd = null;
            OracleTransaction trans = null;
            string sqlID = "0";
            try {
                //查询函数获取批次号
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    sqlID = res["SEQ"].ToString().Trim();
                res.Close();
                res.Dispose();
                selCmd.Dispose();
                selCmd = null;



            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                {
                    trans.Rollback();
                }

            } finally
            {
                if (selCmd != null)
                {
                    selCmd.Dispose();

                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
            return sqlID;
        }

        //插表
        public int InsertTemp(string BatchMainId, OrderButtDto infos) {
            string insertSql = "insert into ABUTJOIN_DETAIL_OP(BATCHMAINID,BATCHID,EXCEL_SEQID)values(:BatchMainId,:Batchid,:Excelselid)";//插入中间表
            OracleCommand insertCmd = null;
            OracleTransaction trans = null;
            int retCode = 0;
            try
            {
                trans = Conn_datacenter_cmszh.BeginTransaction();
                //插入中间表
                insertCmd = Conn_datacenter_cmszh.CreateCommand();
                insertCmd.Connection = Conn_datacenter_cmszh;
                insertCmd.CommandType = System.Data.CommandType.Text;
                insertCmd.CommandText = insertSql.ToString();
                insertCmd.CommandTimeout = 3600;
                insertCmd.Transaction = trans;
                int recCount = 0;

                insertCmd.Parameters.Add("BatchMainId", int.Parse(BatchMainId));
                insertCmd.Parameters.Add("Batchid", int.Parse(infos.Batchid));
                insertCmd.Parameters.Add("Excelselid", int.Parse(infos.ExcelSeqid));

                insertCmd.ExecuteNonQuery();

                trans.Commit();
                trans.Dispose();
                trans = null;

                insertCmd.Dispose();
                insertCmd = null;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null) {
                    trans.Rollback();
                }
            }
            finally {
                if (insertCmd != null)
                {
                    insertCmd.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }

            return retCode;
        }

        //校验
        public int CheckOrderButtInfo(string BatchMainId) {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = 0;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN.P_CMSBILL_CHECK";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHMAINID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                };


                parameters[0].Value = Int64.Parse(BatchMainId);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null) {
                    trans.Rollback();
                }

            }
            finally {
                if (spCmd != null) {
                    spCmd.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
            return retCode;
        }

        //提交CMS
        public int SubmitCms(string BatchMainId) {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = 0;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN.P_CMSBILL_IMPORT";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHMAINID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                };


                parameters[0].Value = Int64.Parse(BatchMainId);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                {
                    trans.Rollback();
                }

            }
            finally
            {
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
            return retCode;

        }
        //刷新数据
        public int LotCheck(string BatchMainId) {
            OracleCommand spCmd = null;
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            int retCode = 0;
            try {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN.P_LOT_CONF_CHECK";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHMAINID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                };


                parameters[0].Value = Int64.Parse(BatchMainId);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                spCmd.Dispose();
                spCmd = null;

                //再调用
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PG_ABUTJOIN.P_CMSBILL_GOODS";
                cmd.CommandTimeout = 3600;

                OracleParameter[] parameterss ={
                    new OracleParameter("IN_BATCHMAINID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OP_TYPE",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
                };


                parameterss[0].Value = Int64.Parse(BatchMainId);
                parameterss[1].Value = -1;
                parameterss[2].Value = 0;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameterss[3].Size = 8;
                parameterss[4].Size = 2048;
                foreach (OracleParameter parameter in parameterss)
                {
                    cmd.Parameters.Add(parameter);
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;


            } catch (Exception ex) {

                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null) {
                    trans.Rollback();
                }

            } finally {
                if (spCmd != null) {
                    spCmd.Dispose();
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
            return retCode;
        }
        //查询Goodid
        public OrderButtDto GetGoodid(OrderButtDto infos) {
            OrderButtDto info = new OrderButtDto();
            string sql = "select * from INOUT_GEN_CMSBILL_XLSTEMP where batchid=:batchid and excel_seqid =:excel_seqid ";
            OracleCommand cmd = null;
            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("batchid", infos.Batchid);
                cmd.Parameters.Add("excel_seqid", infos.ExcelSeqid);
                OracleDataReader res = cmd.ExecuteReader();

                while (res.Read()) {
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.InfCstName = res["infcstname"].ToString().Trim();
                    info.OrderId = res["orderid"].ToString().Trim();
                    info.OrderTime = res["ordertime"].ToString().Trim();
                    info.CstCode = res["cstcode"].ToString().Trim();
                    info.CstName = res["cstname"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.GoodName = res["goodname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.GenGoods = res["gengoods"].ToString().Trim();
                    info.PlanPrc = res["planprc"].ToString().Trim();
                    info.PlanCount = res["plancount"].ToString().Trim();
                    info.Quotation = res["quotation"].ToString().Trim();
                    info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                    info.BizOwnQty = res["bizownqty"].ToString().Trim();
                    info.AllowQty = res["allowqty"].ToString().Trim();
                    info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                    info.CheckMsg = res["checkmsg"].ToString().Trim();
                    info.ColdChain = res["coldchain"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Pmark = res["pmark"].ToString().Trim();
                    info.Amark = res["amark"].ToString().Trim();
                    info.CmsAddressCode = res["cmsaddresscode"].ToString().Trim();
                    info.Addr_Line_1 = res["addr_line_1"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.BottomPrc = res["bottomprc"].ToString().Trim();
                    info.GoodType = res["goodtype"].ToString().Trim();
                    info.CheckState = res["checkstate"].ToString().Trim();
                    info.ImportState = res["importstate"].ToString().Trim();
                    info.ImportMsg = res["importmsg"].ToString().Trim();
                    info.CstMsg = res["cstmsg"].ToString().Trim();
                    info.ChooseFlag = res["chooseflag"].ToString().Trim();
                    info.GoodId = res["goodid"].ToString().Trim();
                    info.CstId = res["cstid"].ToString().Trim();
                    info.CompId = res["compid"].ToString().Trim();
                    info.OwnerId = res["ownerid"].ToString().Trim();
                    info.SaleDeptId = res["saledeptid"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.BottomPrice = res["bottomprice"].ToString().Trim();
                    info.CostPrc = res["costprc"].ToString().Trim();
                    info.CostPrice = res["costprice"].ToString().Trim();
                }
                res.Close();
                res = null;
                cmd.Dispose();
                cmd = null;

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            } finally {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            return info;

        }
        //从MYSQL查价格
        public OrderButtDto GetB2BPrc(OrderButtDto info)
        {

            MySqlCommand spCmd = null;
            int retCode = -1;
            OrderButtDto orderInfo = new OrderButtDto();
            try
            {
                if (ConnectionTests() == -1)//打开mysql从库连接
                {
                    MessageBox.Show("连接从库失败!");
                    Environment.Exit(0);
                }
                //从库 ----2019-1-7---
                spCmd = connections.CreateCommand();
                spCmd.Connection = connections;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "P_GET_B2BPRC";
                spCmd.CommandTimeout = 3600;
                MySqlParameter[] parameters ={
                    new MySqlParameter("@in_compid",MySqlDbType.Int64),
                    new MySqlParameter("@in_ownerid",MySqlDbType.Int64),
                    new MySqlParameter("@in_saledeptid",MySqlDbType.Int64),
                    new MySqlParameter("@in_cstid",MySqlDbType.Int64),//`` 
                    new MySqlParameter("@in_goodid",MySqlDbType.Int64),

                    new MySqlParameter("@out_prcid",MySqlDbType.Int64),//5 
                    new MySqlParameter("@out_prc",MySqlDbType.Decimal),//6.
                    new MySqlParameter("@out_price",MySqlDbType.Decimal),//7
                    new MySqlParameter("@out_bottomprc",MySqlDbType.Decimal),//8.
                    new MySqlParameter("@out_bottomprice",MySqlDbType.Decimal),//9
                    new MySqlParameter("@out_costprc",MySqlDbType.Decimal),//10. 
                    new MySqlParameter("@out_costprice",MySqlDbType.Decimal),//11
                    new MySqlParameter("@out_costrate",MySqlDbType.Decimal),//12
                    new MySqlParameter("@out_hdrid",MySqlDbType.Int64),//13
                    new MySqlParameter("@out_hdrname",MySqlDbType.VarChar),//14
                    new MySqlParameter("@out_prcsourc",MySqlDbType.VarChar),// 15
                    new MySqlParameter("@out_resultcode",MySqlDbType.Int64),//16.
                    new MySqlParameter("@out_msg",MySqlDbType.VarChar),//17.
                                             };

                parameters[0].Value = Int64.Parse(info.CompId);
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Int64.Parse(info.OwnerId);
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = Int64.Parse(info.SaleDeptId);
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = Int64.Parse(info.CstId);
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = Int64.Parse(info.GoodId);
                parameters[4].Direction = ParameterDirection.Input;

                parameters[5].Size = 200;
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6].Size = 200;//
                parameters[6].Direction = ParameterDirection.Output;
                parameters[7].Size = 200;
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8].Size = 200;//
                parameters[8].Direction = ParameterDirection.Output;
                parameters[9].Size = 2048;
                parameters[9].Direction = ParameterDirection.Output;
                parameters[10].Size = 200;//
                parameters[10].Direction = ParameterDirection.Output;
                parameters[11].Size = 200;
                parameters[11].Direction = ParameterDirection.Output;
                parameters[12].Size = 200;
                parameters[12].Direction = ParameterDirection.Output;
                parameters[13].Size = 200;
                parameters[13].Direction = ParameterDirection.Output;
                parameters[14].Size = 2048;
                parameters[14].Direction = ParameterDirection.Output;
                parameters[15].Size = 2048;
                parameters[15].Direction = ParameterDirection.Output;
                parameters[16].Size = 200;//
                parameters[16].Direction = ParameterDirection.Output;
                parameters[17].Size = 2048;//
                parameters[17].Direction = ParameterDirection.Output;

                foreach (MySqlParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                orderInfo.Prcresultcode = parameters[16].Value.ToString().Trim();
                if (orderInfo.Prcresultcode == "1")
                {
                    orderInfo.Prc = parameters[6].Value.ToString().Trim();
                    orderInfo.Price = parameters[7].Value.ToString().Trim();
                    orderInfo.BottomPrc = parameters[8].Value.ToString().Trim();
                    orderInfo.BottomPrice = parameters[9].Value.ToString().Trim();
                    orderInfo.CostPrc = parameters[10].Value.ToString().Trim();
                    orderInfo.CostPrice = parameters[11].Value.ToString().Trim();
                    orderInfo.Prcresultcode = parameters[16].Value.ToString().Trim();
                    orderInfo.PrcMsg = parameters[17].Value.ToString().Trim();
                }
                else
                {
                    orderInfo.Prcresultcode = parameters[16].Value.ToString().Trim();
                    orderInfo.PrcMsg = parameters[17].Value.ToString().Trim();
                }
                spCmd.Dispose();
                spCmd = null;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
            }
            finally {
                if (spCmd != null)
                    spCmd.Dispose();
            }
            return orderInfo;
        }

        //将价格写进中间表
        public int SetBillPrc(OrderButtDto info, SPRetInfo retinfo) {
            OracleCommand spCmd = null;
            //OracleCommand selCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {

                trans = Conn_datacenter_cmszh.BeginTransaction();

                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_SET_BILL_PRC";
                spCmd.CommandTimeout = 3600;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_PRC",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_PRICE",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_BOTTOMPRC",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_BOTTOMPRICE",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_COSTPRC",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_COSTPRICE",OracleDbType.Decimal,ParameterDirection.Input),
                    new OracleParameter("IN_PRCRESULTCODE",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("IN_PRCMSG",OracleDbType.Varchar2,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Int64.Parse(info.Batchid);
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Value = Int64.Parse(info.ExcelSeqid);
                parameters[2].Value = Decimal.Parse(info.Prc);
                parameters[3].Value = Decimal.Parse(info.Price);
                parameters[4].Value = Decimal.Parse(info.BottomPrc);
                parameters[5].Value = Decimal.Parse(info.BottomPrice);
                parameters[6].Value = Decimal.Parse(info.CostPrc);
                parameters[7].Value = Decimal.Parse(info.CostPrice);

                parameters[8].Value = double.Parse(info.Prcresultcode);
                parameters[9].Value = info.PrcMsg;
                parameters[10].Size = 200;
                parameters[11].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[9].Value.ToString().Trim();
                retinfo.msg = parameters[10].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();

                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

            return retCode;
        }

        //获取库存详细信息
        public SortableBindingList<BizInfo> getBizInfos(OrderButtDto orderInfo, SPRetInfo retinfo)
        {
            SortableBindingList<BizInfo> infoList = new SortableBindingList<BizInfo>();
            string sql = "select * from V_SALE_OWNERCHG_LOTSTOCK where goodid =:goodid and lownerid=:lownerid and lsaledeptid=:lsaledeptid ORDER BY GLEVEL,ENDDATE,PRDDATE";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("goodid", orderInfo.GoodId);
                cmd.Parameters.Add("lownerid", orderInfo.OwnerId);
                cmd.Parameters.Add("lsaledeptid", orderInfo.SaleDeptId);
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    BizInfo bizInfo = new BizInfo();
                    bizInfo.OwnerName = res["ownername"].ToString().Trim();
                    bizInfo.Lotno = res["lotno"].ToString().Trim();
                    bizInfo.BatchNo = res["batchno"].ToString().Trim();
                    bizInfo.EmpName = res["empname"].ToString().Trim();

                    string time = res["prddate"].ToString();
                    DateTime dt2 = Convert.ToDateTime(time);
                    string nm = dt2.ToString("yyyy-MM-dd");
                    bizInfo.PrdDate = nm;

                    string times = res["enddate"].ToString().Trim();
                    DateTime dt1 = Convert.ToDateTime(times);
                    string mf = dt1.ToString("yyyy-MM-dd");
                    bizInfo.EndDate = mf;

                    bizInfo.AlloQty = res["allo_qty"].ToString().Trim();
                    bizInfo.UnAlloQty = res["unallo_qty"].ToString().Trim();
                    bizInfo.Description = res["description"].ToString().Trim();
                    bizInfo.SalbillType = res["salbilltype"].ToString().Trim();

                    infoList.Add(bizInfo);
                }
                res.Close();
                res = null;
                cmd.Dispose();
                cmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infoList;
        }

        //作废
        public void CanCelOrderButt(OrderButtDto orderInfo) {
            string sql = "update INOUT_GEN_CMSBILL_XLSTEMP set importstate='作废' where batchid=:batchid and excel_seqid =:excel_seqid";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                //cmd.Parameters.Add("importstate", cancel);
                cmd.Parameters.Add("batchid", orderInfo.Batchid);
                cmd.Parameters.Add("excel_seqid", orderInfo.ExcelSeqid);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
        }

        //取消作废
        public void NotCanCelOrderButt(OrderButtDto orderInfo)
        {
            string sql = "update INOUT_GEN_CMSBILL_XLSTEMP set importstate='待处理' where batchid=:batchid and excel_seqid =:excel_seqid";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                //cmd.Parameters.Add("importstate", cancel);
                cmd.Parameters.Add("batchid", orderInfo.Batchid);
                cmd.Parameters.Add("excel_seqid", orderInfo.ExcelSeqid);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
        }

        //更新Empid
        public void UpdateEmpid(OrderButtDto infos) {
            string sql = "update INOUT_GEN_CMSBILL_XLSTEMP set empid=:empid where batchid=:batchid and excel_seqid =:excel_seqid";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("empid", SessionDto.Empid);
                cmd.Parameters.Add("batchid", infos.Batchid);
                cmd.Parameters.Add("excel_seqid", infos.ExcelSeqid);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
        }
        #endregion
        #region 促销信息维护
        //查询
        public SortableBindingList<PromotionInfo> GetPromotionInfo(Dictionary<string, string> sqlkeydict) {

            SortableBindingList<PromotionInfo> infolist = new SortableBindingList<PromotionInfo>();
            string sql = "select *  from INOUT_GEN_PROMOTION_IFO where 1=1 $";
            OracleCommand cmd = null;
            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                string whereStr = "";
                //遍历Dictionary中的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                        }
                    }

                }
                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.Add(kv.Key, kv.Value);
                        }
                    }
                }
                string s = sql;
                OracleDataReader res = cmd.ExecuteReader();

                while (res.Read()) {
                    PromotionInfo info = new PromotionInfo();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.PocName = res["pocname"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.GoodName = res["goodname"].ToString().Trim();
                    info.GoodId = res["goodid"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.CstCode = res["cstcode"].ToString().Trim();
                    info.CstId = res["cstid"].ToString().Trim();
                    info.CstName = res["cstname"].ToString().Trim();
                    info.BeginTime = res["begintime"].ToString().Trim();
                    info.EndTime = res["endtime"].ToString().Trim();
                    info.Policy = res["policy"].ToString().Trim();
                    info.Remark = res["remark"].ToString().Trim();
                    info.LModifyUser = res["lmodifyuser"].ToString().Trim();
                    info.LModifyTime = res["lmodifytime"].ToString().Trim();
                    info.Id = res["id"].ToString().Trim();
                    info.CreateTime = res["createtime"].ToString().Trim();

                    infolist.Add(info);
                }
                res.Close();
                res = null;
                cmd.Dispose();
                cmd = null;

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
            } finally {
                if (cmd != null) {

                    cmd.Dispose();
                }
            }
            return infolist;
        }

        //删除
        public int DelInfo(SortableBindingList<DelTempGenCstGood> List, SPRetInfo retinfo)
        {
            string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            string insertSql = "insert into DELTEMP_GEN_PROMOTION(BATCHID,ID)values(:Batchid,:Id)";//插入中间表

            OracleCommand selCmd = null;
            OracleCommand insertCmd = null;
            OracleCommand spCmd = null;
            OracleTransaction trans = null;

            int retCode = -1;
            string sqlID = "0";

            try
            {
                //查询函数获取批次号
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    sqlID = res["SEQ"].ToString().Trim();
                res.Close();
                res.Dispose();
                selCmd.Dispose();
                selCmd = null;

                trans = Conn_datacenter_cmszh.BeginTransaction();
                //插入中间表
                insertCmd = Conn_datacenter_cmszh.CreateCommand();
                insertCmd.Connection = Conn_datacenter_cmszh;
                insertCmd.CommandType = System.Data.CommandType.Text;
                insertCmd.CommandText = insertSql.ToString();
                insertCmd.CommandTimeout = 3600;
                insertCmd.Transaction = trans;
                int recCount = 0;

                foreach (DelTempGenCstGood selected in List)
                {
                    recCount++;

                    insertCmd.Parameters.Clear();

                    insertCmd.Parameters.Add("Batchid", int.Parse(sqlID));
                    insertCmd.Parameters.Add("Id", selected.Relatid);

                    insertCmd.ExecuteNonQuery();
                }
                trans.Commit();
                trans.Dispose();
                trans = null;

                insertCmd.Dispose();
                insertCmd = null;

                if (recCount < 1)
                {
                    retinfo.num = "0";
                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return 0;
                }

                //执行存储过程,进行删除
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_PROMOTION_IFO.P_PROMOTION_DEL";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
            };
                parameters[0].Value = Int64.Parse(sqlID);

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[1].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[2].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[2].Value.ToString().Trim();
                }
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
                if (trans != null)
                {
                    trans.Rollback();
                    trans.Dispose();
                    trans = null;
                }
            }
            return retCode;
        }

        //查询带入信息
        public List<GoodInfo> searchPromo(Dictionary<string, string> sqlkeydict) {
            List<GoodInfo> list = new List<GoodInfo>();
            string sql = "select * from V_PUB_WAREDICTS where 1=1 $";
            OracleCommand cmd = null;
            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                string whereStr = "";
                //遍历Dictionary中的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                        }
                    }

                }
                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.Add(kv.Key, kv.Value);
                        }
                    }
                }
                string s = sql;
                OracleDataReader res = cmd.ExecuteReader();

                while (res.Read()) {
                    GoodInfo info = new GoodInfo();
                    info.Goods = res["goods"].ToString().Trim();
                    info.GoodName = res["name"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.GoodId = res["goodid"].ToString().Trim();

                    list.Add(info);
                }
                res.Close();
                res = null;
                cmd.Dispose();
                cmd = null;

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
            } finally {
                if (cmd != null) {
                    cmd.Dispose();
                }
            }
            return list;
        }

        //查询带入信息
        public CstInfo searchCstInfo(string CstCode)
        {
            CstInfo info = new CstInfo();
            string sql = "select * from  V_PUB_CLIENTS where cstcode=:cstcode";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("cstcode", CstCode);

                OracleDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    info.CstId = res["cstid"].ToString().Trim();
                    info.CstCode = res["cstcode"].ToString().Trim();
                    info.CstName = res["dname"].ToString().Trim();
                }
                res.Close();
                res = null;
                cmd.Dispose();
                cmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            return info;
        }

        //新增OR修改促销信息数据
        public int AdOrUpPromo(string goodId, string cstId, string proName, string Goods,
            string GoodName, string Spec, string Producer, string CstCode,
            string CstName, string beginTime, string endTime, string Policy, string reMark, int IN_RELATID, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            //string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            //string insertSql = "INSERT INTO DELTEMP_GEN_CST_GOOD()VALUES()";
            try
            {
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_PROMOTION_IFO.P_PROMOTION_ADD";
                spCmd.CommandTimeout = 3600;
                //向存储过程中添加输入输出
                OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_POCNAME",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GOODS",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GOODID",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GOODNAME",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_SPEC",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_PRODUCER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_CSTCODE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_CSTID",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_CSTNAME",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_BEGINTIME",OracleDbType.Date,ParameterDirection.Input),
                    new OracleParameter("IN_ENDTIME",OracleDbType.Date,ParameterDirection.Input),
                    new OracleParameter("IN_POLICY",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_REMARK",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_EMPNAME",OracleDbType.Varchar2,ParameterDirection.Input),


                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(Properties.Settings.Default.COMPID);
                parameters[1].Value = Int64.Parse(Properties.Settings.Default.OWNERID);
                parameters[2].Value = proName;
                parameters[3].Value = Goods;
                parameters[4].Value = goodId;
                parameters[5].Value = GoodName;
                parameters[6].Value = Spec;
                parameters[7].Value = Producer;
                parameters[8].Value = CstCode;
                parameters[9].Value = cstId;
                parameters[10].Value = CstName;
                parameters[11].Value = DateTime.ParseExact(beginTime, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                parameters[12].Value = DateTime.ParseExact(endTime, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                parameters[13].Value = Policy;
                parameters[14].Value = reMark;
                parameters[15].Value = SessionDto.Empname;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/

                parameters[16].Size = 8;
                parameters[17].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[16].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[17].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[17].Value.ToString().Trim();
                }
                //retinfo.result = parameters[4].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;
        }

        //批量修改
        public int BatchUpdate(SortableBindingList<PromotionInfo> BatchList, string PocName, string beginTime,
        string endTime, string Policy, string Remark, SPRetInfo retinfo)
        {
            string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            string insertSql = "insert into INOUT_GEN_PROMOTION_BATCHTEMP(BATCHID,ID)values(:Batchid,:Id)";

            OracleCommand selCmd = null;
            OracleCommand insertCmd = null;
            OracleCommand spCmd = null;
            OracleTransaction trans = null;

            string sqlID = "0";
            int retCode = -1;
            try
            {
                //查询函数获取批次号
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    sqlID = res["SEQ"].ToString().Trim();
                res.Close();
                res.Dispose();
                selCmd.Dispose();
                selCmd = null;

                trans = Conn_datacenter_cmszh.BeginTransaction();

                //插入中间表
                insertCmd = Conn_datacenter_cmszh.CreateCommand();
                insertCmd.Connection = Conn_datacenter_cmszh;
                insertCmd.CommandType = System.Data.CommandType.Text;
                insertCmd.CommandText = insertSql.ToString();
                insertCmd.CommandTimeout = 3600;
                insertCmd.Transaction = trans;
                int recCount = 0;

                foreach (PromotionInfo selected in BatchList)
                {
                    recCount++;

                    insertCmd.Parameters.Clear();

                    insertCmd.Parameters.Add("Batchid", int.Parse(sqlID));
                    insertCmd.Parameters.Add("Id", int.Parse(selected.Id));

                    insertCmd.ExecuteNonQuery();
                }
                trans.Commit();
                trans.Dispose();
                trans = null;

                insertCmd.Dispose();
                insertCmd = null;

                if (recCount < 1)
                {
                    retinfo.num = "0";
                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return 0;
                }


                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_PROMOTION_IFO.P_PROMOTION_BATCHMODIFY";
                spCmd.CommandTimeout = 3600;
                //向存储过程中添加输入输出
                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_POCNAME",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_BEGINTIME",OracleDbType.Date,ParameterDirection.Input),
                    new OracleParameter("IN_ENDTIME",OracleDbType.Date ,ParameterDirection.Input),
                    new OracleParameter("IN_POLICY",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_REMARK",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_EMPNAME",OracleDbType.Varchar2,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = sqlID;
                parameters[1].Value = PocName;
                parameters[2].Value = DateTime.ParseExact(beginTime, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); ;
                parameters[3].Value = DateTime.ParseExact(endTime, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); ;
                parameters[4].Value = Policy;
                parameters[5].Value = Remark;
                parameters[6].Value = SessionDto.Empname;

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[7].Size = 8;
                parameters[8].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[7].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[8].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[8].Value.ToString().Trim();
                }
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
                retCode = -1;
            }
            finally
            {
                if (selCmd != null)
                    selCmd.Dispose();
                if (insertCmd != null)
                    insertCmd.Dispose();
                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;
        }

        //Excel导入
        public SortableBindingList<PromoExcelInfo> GetPromosData(SortableBindingList<PromoExcelInfo> ClientList, SPRetInfo retinfo)
        {
            SortableBindingList<PromoExcelInfo> infoList = new SortableBindingList<PromoExcelInfo>();

            string selectSql = "SELECT F_GET_SEQ('BATCHID') as SEQ FROM dual";
            string insertSql = "INSERT INTO INOUT_GEN_PROMOTION_XLSTEMP(batchid,excel_seqid,compid,ownerid,empname,"
                + "pocname,goods,cstcode,begintime,endtime,policy,remark,"
                + "createtime)VALUES(:Batchid,:Excel_seqid,:Compid,:Ownerid,:EmpName,:PocName,:Goods,"
                + ":CstCode,:BeginTime,:EndTime,:Policy,:Remark,:CreateTime)";

            string selSql = "SELECT * FROM INOUT_GEN_PROMOTION_XLSTEMP  WHERE batchid =:batchid";

            OracleCommand spCmd = null;
            OracleCommand selCmd = null;
            OracleCommand insetCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            string seqID = "0";

            //查询批次号
            try
            {
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    seqID = res["SEQ"].ToString().Trim();

                retinfo.result = seqID;

                res.Close();
                res.Dispose();

                selCmd.Dispose();
                selCmd = null;

                //将信息插入中间表
                trans = Conn_datacenter_cmszh.BeginTransaction();

                insetCmd = Conn_datacenter_cmszh.CreateCommand();
                insetCmd.Connection = Conn_datacenter_cmszh;
                insetCmd.CommandType = System.Data.CommandType.Text;
                insetCmd.CommandText = insertSql.ToString();
                insetCmd.CommandTimeout = 3600;

                insetCmd.Transaction = trans;
                int recCount = 0;
                foreach (PromoExcelInfo selected in ClientList)
                {
                    recCount++;

                    insetCmd.Parameters.Clear();

                    insetCmd.Parameters.Add("Batchid", Int32.Parse(seqID));
                    insetCmd.Parameters.Add("Excel_seqid", Int32.Parse(selected.ExcelSeqid));
                    insetCmd.Parameters.Add("Compid", Int32.Parse(selected.Compid));
                    insetCmd.Parameters.Add("Ownerid", Int32.Parse(selected.Ownerid));
                    insetCmd.Parameters.Add("EmpName", selected.EmpName);

                    if (selected.PocName != "")
                    {
                        insetCmd.Parameters.Add("PocName", selected.PocName);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("PocName", "");
                    }
                    if (selected.Goods != "")
                    {
                        insetCmd.Parameters.Add("Goods", selected.Goods);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Goods", "");
                    }
                    if (selected.CstCode != "")
                    {
                        insetCmd.Parameters.Add("CstCode", selected.CstCode);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("CstCode", "");
                    }
                    if (selected.BeginTime != "")
                    {
                        insetCmd.Parameters.Add("BeginTime", DateTime.ParseExact(selected.BeginTime, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture));
                        //insetCmd.Parameters.Add("BeginTime", selected.BeginTime);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("BeginTime", DateTime.ParseExact("", "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture));
                        //insetCmd.Parameters.Add("BeginTime", "");
                    }
                    if (selected.EndTime != "")
                    {
                        insetCmd.Parameters.Add("EndTime", DateTime.ParseExact(selected.EndTime, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture));
                        //insetCmd.Parameters.Add("EndTime", selected.EndTime);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("EndTime", DateTime.ParseExact("", "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture));
                        //insetCmd.Parameters.Add("EndTime", "");
                    }
                    if (selected.Policy != "")
                    {
                        insetCmd.Parameters.Add("Policy", selected.Policy);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Policy", "");
                    }
                    if (selected.Remark != "")
                    {
                        insetCmd.Parameters.Add("Remark", selected.Remark);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Remark", "");
                    }

                    insetCmd.Parameters.Add("CreateTime", System.DateTime.Now);
                    //MessageBox.Show("=="+ insetCmd.Parameters[0].Value);
                    insetCmd.ExecuteNonQuery();
                }

                trans.Commit();
                trans.Dispose();
                trans = null;

                insetCmd.Dispose();
                insetCmd = null;

                //再次查询此表
                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.Parameters.Add("batchid", seqID);
                seCmd.CommandTimeout = 3600;

                OracleDataReader ress = seCmd.ExecuteReader();
                foreach (PromoExcelInfo info in ClientList)
                {
                    if (ress.Read())
                        info.Batchid = ress["batchid"].ToString().Trim();
                    info.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    info.Compid = ress["compid"].ToString().Trim();
                    info.Ownerid = ress["ownerid"].ToString().Trim();
                    info.EmpName = ress["empname"].ToString().Trim();
                    info.PocName = ress["pocname"].ToString().Trim();
                    info.Goods = ress["goods"].ToString().Trim();
                    info.CstCode = ress["cstcode"].ToString().Trim();
                    info.BeginTime = ress["begintime"].ToString().Trim().Substring(0, 10);
                    info.EndTime = ress["endtime"].ToString().Trim().Substring(0, 10);
                    info.Policy = ress["policy"].ToString().Trim();
                    info.Remark = ress["remark"].ToString().Trim();
                    info.CreateTime = ress["createtime"].ToString().Trim();
                    infoList.Add(info);
                }

                ress.Close();
                ress.Dispose();

                seCmd.Dispose();
                seCmd = null;


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();

            }
            finally
            {

                if (selCmd != null)
                    selCmd.Dispose();

                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();

                if (insetCmd != null)
                    insetCmd.Dispose();

                if (seCmd != null)
                    seCmd.Dispose();

            }

            return infoList;
        }

        //检查导入
        public SortableBindingList<PromoExcelInfo> chackPromosInfo(SortableBindingList<PromoExcelInfo> list, string batchid)
        {
            SortableBindingList<PromoExcelInfo> infolist = new SortableBindingList<PromoExcelInfo>();
            string selSql = "SELECT * FROM INOUT_GEN_PROMOTION_XLSTEMP  WHERE batchid =:batchid";
            OracleCommand spCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_PROMOTION_IFO.P_PROMOTION_XLSCHECK";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(batchid);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                spCmd.Dispose();
                spCmd = null;

                //再次查询中间表
                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.Parameters.Add("batchid", batchid);
                seCmd.CommandTimeout = 3600;

                OracleDataReader ress = seCmd.ExecuteReader();
                foreach (PromoExcelInfo info in list)
                {
                    if (ress.Read())
                        info.Batchid = ress["batchid"].ToString().Trim();
                    info.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    info.Compid = ress["compid"].ToString().Trim();
                    info.Ownerid = ress["ownerid"].ToString().Trim();
                    info.EmpName = ress["empname"].ToString().Trim();
                    info.PocName = ress["pocname"].ToString().Trim();
                    info.Goods = ress["goods"].ToString().Trim();
                    info.CstCode = ress["cstcode"].ToString().Trim();
                    info.BeginTime = ress["begintime"].ToString().Trim().Substring(0, 10);
                    info.EndTime = ress["endtime"].ToString().Trim().Substring(0, 10);
                    info.Policy = ress["policy"].ToString().Trim();
                    info.Remark = ress["remark"].ToString().Trim();
                    info.CreateTime = ress["createtime"].ToString().Trim();
                    info.CheckState = ress["checkstate"].ToString().Trim();
                    info.CheckMsg = ress["checkmsg"].ToString().Trim();
                    infolist.Add(info);
                }

                ress.Close();
                ress.Dispose();

                seCmd.Dispose();
                seCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
                if (seCmd != null)
                    seCmd.Dispose();
            }

            return infolist;
        }

        //导入 
        public SortableBindingList<PromoExcelInfo> insertPromosInfo(SortableBindingList<PromoExcelInfo> list, string batchid)
        {
            SortableBindingList<PromoExcelInfo> infolist = new SortableBindingList<PromoExcelInfo>();
            string selSql = "SELECT * FROM INOUT_GEN_PROMOTION_XLSTEMP  WHERE batchid =:batchid";
            OracleCommand spCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_PROMOTION_IFO.P_PROMOTION_XLSIMPORT";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(batchid);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                spCmd.Dispose();
                spCmd = null;

                //再次查询中间表
                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.Parameters.Add("batchid", batchid);
                seCmd.CommandTimeout = 3600;

                OracleDataReader ress = seCmd.ExecuteReader();
                foreach (PromoExcelInfo info in list)
                {
                    if (ress.Read())
                        info.Batchid = ress["batchid"].ToString().Trim();
                    info.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    info.Compid = ress["compid"].ToString().Trim();
                    info.Ownerid = ress["ownerid"].ToString().Trim();
                    info.EmpName = ress["empname"].ToString().Trim();
                    info.PocName = ress["pocname"].ToString().Trim();
                    info.Goods = ress["goods"].ToString().Trim();
                    info.CstCode = ress["cstcode"].ToString().Trim();
                    info.BeginTime = ress["begintime"].ToString().Trim().Substring(0, 10);
                    info.EndTime = ress["endtime"].ToString().Trim().Substring(0, 10);
                    info.Policy = ress["policy"].ToString().Trim();
                    info.Remark = ress["remark"].ToString().Trim();
                    info.CreateTime = ress["createtime"].ToString().Trim();
                    info.CheckState = ress["checkstate"].ToString().Trim();
                    info.CheckMsg = ress["checkmsg"].ToString().Trim();
                    info.ImportState = ress["importstate"].ToString().Trim();
                    info.ImportMsg = ress["importmsg"].ToString().Trim();
                    infolist.Add(info);
                }

                ress.Close();
                ress.Dispose();

                seCmd.Dispose();
                seCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
                if (seCmd != null)
                    seCmd.Dispose();
            }

            return infolist;
        }
        #endregion
        //获取促销信息
        public SalePromoInfo GetPromosInfos(string compid, string ownerid, string cstid, string goodid) {
            SalePromoInfo info = new SalePromoInfo();
            string sql = "select begintime,endtime,policy,remark from INOUT_GEN_PROMOTION_IFO where compid=:compid and ownerid=:ownerid and cstid=:cstid and goodid=:goodid and begintime < sysdate and endtime > sysdate";
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.Parameters.Add("compid", compid);
                cmd.Parameters.Add("ownerid", ownerid);
                cmd.Parameters.Add("cstid", cstid);
                cmd.Parameters.Add("goodid", goodid);

                cmd.CommandTimeout = 3600;

                OracleDataReader res = cmd.ExecuteReader();

                while (res.Read()) {
                    info.BeginTime = res["begintime"].ToString().Trim().Substring(0, 10);
                    info.EndTime = res["endtime"].ToString().Trim().Substring(0, 10);
                    info.Policy = res["policy"].ToString().Trim();
                    info.Remark = res["remark"].ToString().Trim();
                }
                res.Close();
                res.Dispose();

                cmd.Dispose();
                cmd = null;
            } catch (Exception ex) {

                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            } finally {

                if (cmd != null)
                    cmd.Dispose();
            }
            return info;
        }

        //更新暂缓开票
        public void UpdateInvPostFlag(string batchid, string excelsqlid, string invpostflag, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_INVPOSTFLAG_UPDATE";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EXCEL_SEQID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_INVPOSTFLAG",OracleDbType.Varchar2,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(batchid);
                parameters[1].Value = Int64.Parse(excelsqlid);
                parameters[2].Value = invpostflag;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                retinfo.msg = parameters[4].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
            }

        }
        //获取勾选的数据
        public SortableBindingList<InoutGenCmsbillXlstemp> GetSelectData(string batchid)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> infoList = new SortableBindingList<InoutGenCmsbillXlstemp>();

            string sql = "SELECT * FROM INOUT_GEN_CMSBILL_XLSTEMP WHERE batchid =:batchid and chooseflag=1 ";

            OracleCommand cmd = null;
            try
            {

                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("batchid", batchid);

                cmd.CommandText = sql.ToString();
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    InoutGenCmsbillXlstemp info = new InoutGenCmsbillXlstemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Empid = res["empid"].ToString().Trim();
                    info.Gengoods = res["gengoods"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Planprc = res["planprc"].ToString().Trim();
                    info.Plancount = res["plancount"].ToString().Trim();
                    info.Pmark = res["pmark"].ToString().Trim();
                    info.Amark = res["amark"].ToString().Trim();
                    info.Genaddresscode = res["genaddresscode"].ToString().Trim();
                    info.Cmsaddresscode = res["cmsaddresscode"].ToString().Trim();
                    info.Checkstate = res["checkstate"].ToString().Trim();
                    info.Checkmsg = res["checkmsg"].ToString().Trim();
                    info.Importstate = res["importstate"].ToString().Trim();
                    info.Importmsg = res["importstate"].ToString().Trim();
                    info.Goodtype = res["goodtype"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Prcresultcode = res["prcresultcode"].ToString().Trim();
                    info.Prcmsg = res["prcmsg"].ToString().Trim();
                    info.Quotation = res["quotation"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.Importprc = res["importprc"].ToString().Trim();
                    //---------2018-10-22-----新增字段
                    info.GoodName = res["goodname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.BizownQty = res["bizownqty"].ToString().Trim();
                    info.AllowQty = res["allowqty"].ToString().Trim();
                    info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                    info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                    info.ColdChain = res["coldchain"].ToString().Trim();
                    info.LastSoTime = res["lastsotime"].ToString().Trim();
                    info.ChooseFlag = res["chooseflag"].ToString().Trim();
                    info.PromotionFlag = res["promotionflag"].ToString().Trim();

                    //---------2018-12-26-----新增字段
                    info.InvoiceType = res["invoicetypename"].ToString().Trim();
                    info.AttchInv = res["attchinvname"].ToString().Trim();
                    info.InvPostFlag = res["invpostflagname"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();

                    infoList.Add(info);
                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infoList;
        }

        //校验勾选数据的重复性
        public void CheckFos(string batchid,SPRetInfo retinfo) {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try {
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_CHECK_DEPEAT";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Int64.Parse(batchid);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[1].Value.ToString().Trim();
                retinfo.msg = parameters[2].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();

            } finally {

                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
            }
        }


        //获取下拉框默认ID
        public CbId GetCbId(string cstid, SPRetInfo retinfo)
        {
            CbId infos = new CbId();
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_MENU_DEFAULT";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_SALEDEPTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_PAYID",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_TRANSID",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_KDFS",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[2].Value = Int64.Parse(SessionDto.Empdeptid);
                parameters[3].Value = Int64.Parse(cstid);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[4].Size = 2048;
                parameters[5].Size = 2048;
                parameters[6].Size = 2048;
                parameters[7].Size = 8;
                parameters[8].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                infos.Payid = parameters[4].Value.ToString().Trim();
                infos.Transid = parameters[5].Value.ToString().Trim();
                infos.Kdfs = parameters[6].Value.ToString().Trim();
                retinfo.num = parameters[7].Value.ToString().Trim();
                retinfo.msg = parameters[8].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
            }
            return infos;
        }
        //获取开单方式下拉框
        public Dictionary<string, string> getBillingType() {

            Dictionary<string, string> dic = new Dictionary<string, string>();
            string sql = "select * from V_MENU_KDFS";
            OracleCommand cmd = null;
            try {

                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.CommandText = sql.ToString();
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    string code = res["kindid"].ToString().Trim();
                    string value = res["name"].ToString().Trim();
                    dic.Add(code, value);
                }
                res.Close();
                res = null;
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");

            } finally {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return dic;
        }

        //获取支付方式下拉框
        public Dictionary<string, string> getPayType()
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();
            string sql = "select * from V_MENU_PAYMODE";
            OracleCommand cmd = null;
            try
            {

                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.CommandText = sql.ToString();
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    string code = res["id"].ToString().Trim();
                    string value = res["name"].ToString().Trim();
                    dic.Add(code, value);
                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return dic;
        }
        //获取发运方式下拉框
        public Dictionary<string, string> getTransType()
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();
            string sql = "select * from V_MENU_TRANSMODE";
            OracleCommand cmd = null;
            try
            {

                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.CommandText = sql.ToString();
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    string code = res["id"].ToString().Trim();
                    string value = res["name"].ToString().Trim();
                    dic.Add(code, value);
                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return dic;
        }
        //获取送货地址下拉框
        public Dictionary<string, string> GetSendAdr(string cstid) {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string sql = "SELECT STOREID,ADDR_LINE_1 FROM v_cms_address  where CSTID =:cstid order by priseq desc";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("cstid", cstid);
                cmd.CommandText = sql.ToString();
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    string code = res["STOREID"].ToString().Trim();
                    string value = res["ADDR_LINE_1"].ToString().Trim();
                    dic.Add(code, value);
                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return dic;
        }


        //更改发运方式
        public void UpTrans(string batchid, string key, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_TRANSMODE_UPDATE";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_TRANSID",OracleDbType.Varchar2,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(batchid);
                parameters[1].Value = key;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[2].Size = 8;
                parameters[3].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[2].Value.ToString().Trim();
                retinfo.msg = parameters[3].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
            }
        }

        //更改开单方式
        public void UpBillings(string batchid, string key, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_KDFS_UPDATE";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_KINDID",OracleDbType.Varchar2,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(batchid);
                parameters[1].Value = key;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[2].Size = 8;
                parameters[3].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[2].Value.ToString().Trim();
                retinfo.msg = parameters[3].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
            }
        }

        //更改支付方式
        public void UpPayType(string batchid, string key, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_PAYMODE_UPDATE";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_PAYID",OracleDbType.Varchar2,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(batchid);
                parameters[1].Value = key;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[2].Size = 8;
                parameters[3].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[2].Value.ToString().Trim();
                retinfo.msg = parameters[3].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
            }
        }


        //生成校验参数字符串
        public List<CheckInfo> GetInpara(string batchid, SPRetInfo retinfos) {
            List<CheckInfo> infoList = new List<CheckInfo>();
            string selSql = "select batchid, excel_seqid,check_para from INOUT_GEN_CMSBILL_XLSTEMP where BATCHID =:batchid AND CHOOSEFLAG = 1";
            OracleCommand spCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_CHECK_PARA";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(batchid);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfos.num = parameters[1].Value.ToString();
                retinfos.msg = parameters[2].Value.ToString();
                spCmd.Dispose();
                spCmd = null;

                //再次查询中间表
                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.Parameters.Add("batchid", batchid);
                seCmd.CommandTimeout = 3600;

                OracleDataReader ress = seCmd.ExecuteReader();
                while (ress.Read()) {
                    CheckInfo ins = new CheckInfo();
                    ins.Batchid = ress["batchid"].ToString().Trim();
                    ins.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    ins.CheckPara = ress["check_para"].ToString().Trim();
                    infoList.Add(ins);
                }

                ress.Close();
                ress.Dispose();

                seCmd.Dispose();
                seCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
                if (seCmd != null)
                    seCmd.Dispose();
            }
            return infoList;
        }

        //更新校验结果到数据库中
        public void UpdateCheckMsg(string batchid, string excqlsqlid, int ret, string retmsg) {
            string sql = "update INOUT_GEN_CMSBILL_XLSTEMP set CHECK_RET=:ret,IMPORTMSG=:retmsg where batchid=:batchid and excel_seqid =:excel_seqid ";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("ret", ret);
                cmd.Parameters.Add("retmsg", retmsg);
                cmd.Parameters.Add("batchid", batchid);
                cmd.Parameters.Add("excel_seqid", excqlsqlid);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }

        }
        //查询校验完成数据
        public SortableBindingList<ContractInfo> GetCheckedInfo(string batchid) {
            SortableBindingList<ContractInfo> infolist = new SortableBindingList<ContractInfo>();
            string sql = "select * from INOUT_GEN_CMSBILL_XLSTEMP where batchid=:batchid and chooseflag=1";
            OracleCommand cmd = null;
            try {

                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("batchid", batchid);
                cmd.CommandText = sql.ToString();
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    ContractInfo info = new ContractInfo();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.GoodName = res["goodname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.Importprc = res["importprc"].ToString().Trim();
                    info.Plancount = res["plancount"].ToString().Trim();
                    info.Importmsg = res["importmsg"].ToString().Trim();
                    info.Importstate = res["importstate"].ToString().Trim();
                    info.Pmark = res["pmark"].ToString().Trim();
                    info.Amark = res["amark"].ToString().Trim();
                    infolist.Add(info);
                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infolist;
        }

        //生成提交CMS参数字符串
        public List<CommitInfo> GetCommit(string batchid, SPRetInfo retinfos)
        {
            List<CommitInfo> infoList = new List<CommitInfo>();
            string selSql = "select batchid, excel_seqid,import_para1,CHECK_PARA,import_para2 from INOUT_GEN_CMSBILL_XLSTEMP where BATCHID =:batchid AND CHOOSEFLAG = 1 and check_ret = 1";
            OracleCommand spCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_GEN_CMSBILL.P_IMPORT_PARA";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(batchid);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfos.num = parameters[1].Value.ToString();
                retinfos.msg = parameters[2].Value.ToString();
                spCmd.Dispose();
                spCmd = null;

                //再次查询中间表
                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.Parameters.Add("batchid", batchid);
                seCmd.CommandTimeout = 3600;

                OracleDataReader ress = seCmd.ExecuteReader();
                while (ress.Read())
                {
                    CommitInfo ins = new CommitInfo();
                    ins.Batchid = ress["batchid"].ToString().Trim();
                    ins.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    ins.ImportParas = ress["import_para1"].ToString().Trim();
                    ins.CheckPara = ress["check_para"].ToString().Trim();
                    ins.ImportParass = ress["import_para2"].ToString().Trim();
                    infoList.Add(ins);
                }

                ress.Close();
                ress.Dispose();

                seCmd.Dispose();
                seCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
                if (seCmd != null)
                    seCmd.Dispose();
            }
            return infoList;
        }

        //更新提交结果到数据库中
        public void UpdateImpoprtMsg(string batchid, string excqlsqlid, int ret, string retmsg)
        {
            string sql = "update INOUT_GEN_CMSBILL_XLSTEMP set import_RET=:ret,IMPORTMSG=:retmsg where batchid=:batchid and excel_seqid =:excel_seqid ";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("ret", ret);
                cmd.Parameters.Add("retmsg", retmsg);
                cmd.Parameters.Add("batchid", batchid);
                cmd.Parameters.Add("excel_seqid", excqlsqlid);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }

        }
        //更新Importstate(单条)
        public void UpImportstate(string batchid, string excelsqlid) {
            string sql = "update INOUT_GEN_CMSBILL_XLSTEMP set importstate ='失败' where batchid=:batchid and excel_seqid =:excel_seqid";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("batchid", batchid);
                cmd.Parameters.Add("excel_seqid", excelsqlid);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
        }

        //更新Importstate(批次)
        public void updateBatchImport(string batchid)
        {
            string sql = "update INOUT_GEN_CMSBILL_XLSTEMP set importstate ='成功' where batchid=:batchid and CHOOSEFLAG = 1 and check_ret = 1";
            OracleCommand cmd = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("batchid", batchid);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
        }

        //2018-12-29 合同处理控制进入 
        public SortableBindingList<InoutGenCmsbillXlstemp> GetDataSource(string batchid)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> infoList = new SortableBindingList<InoutGenCmsbillXlstemp>();

            string sql = "SELECT * FROM INOUT_GEN_CMSBILL_XLSTEMP WHERE batchid =:batchid and checkstate='成功' and (IMPORTSTATE is null or IMPORTSTATE!= '成功')";

            OracleCommand cmd = null;
            try
            {

                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("batchid", batchid);

                cmd.CommandText = sql.ToString();
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    InoutGenCmsbillXlstemp info = new InoutGenCmsbillXlstemp();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Empid = res["empid"].ToString().Trim();
                    info.Gengoods = res["gengoods"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Planprc = res["planprc"].ToString().Trim();
                    info.Plancount = res["plancount"].ToString().Trim();
                    info.Pmark = res["pmark"].ToString().Trim();
                    info.Amark = res["amark"].ToString().Trim();
                    info.Genaddresscode = res["genaddresscode"].ToString().Trim();
                    info.Cmsaddresscode = res["cmsaddresscode"].ToString().Trim();
                    info.Checkstate = res["checkstate"].ToString().Trim();
                    info.Checkmsg = res["checkmsg"].ToString().Trim();
                    info.Importstate = res["importstate"].ToString().Trim();
                    info.Importmsg = res["importmsg"].ToString().Trim();
                    info.Goodtype = res["goodtype"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Prcresultcode = res["prcresultcode"].ToString().Trim();
                    info.Prcmsg = res["prcmsg"].ToString().Trim();
                    info.Quotation = res["quotation"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.Importprc = res["importprc"].ToString().Trim();
                    //---------2018-10-22-----新增字段
                    info.GoodName = res["goodname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.BizownQty = res["bizownqty"].ToString().Trim();
                    info.AllowQty = res["allowqty"].ToString().Trim();
                    info.AllowBatNum = res["allowbatnum"].ToString().Trim();
                    info.GoodSaleCheck = res["goodsalecheck"].ToString().Trim();
                    info.ColdChain = res["coldchain"].ToString().Trim();
                    info.LastSoTime = res["lastsotime"].ToString().Trim();
                    info.ChooseFlag = res["chooseflag"].ToString().Trim();
                    info.PromotionFlag = res["promotionflag"].ToString().Trim();

                    //---------2018-12-26-----新增字段
                    info.InvoiceType = res["invoicetypename"].ToString().Trim();
                    info.AttchInv = res["attchinvname"].ToString().Trim();
                    info.InvPostFlag = res["invpostflagname"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();

                    infoList.Add(info);
                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
            return infoList;
        }

        //库存对接  查询
        public SortableBindingList<StockDockInfo> SelStockDockInfo(string accountCode, Dictionary<string, string> sqlkeydict)
        {
            SortableBindingList<StockDockInfo> infoList = new SortableBindingList<StockDockInfo>();
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            string sql = "";
            if (accountCode == "9999")
            {
                sql = "select *  from V_ABUTJOIN_GOODS_CONF where 1=1 $";
            }
            else {

                sql = "select *  from V_ABUTJOIN_GOODS_CONF where accountcode=:accountcode $";
            }
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;


                string whereStr = "";
                //遍历Dictionary中的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");

                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                        }

                    }

                }
                sql = sql.Replace("$", whereStr);
                cmd.Parameters.Add("accountcode", accountCode);
                cmd.CommandText = sql.ToString();
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.Add(kv.Key, kv.Value);
                        }
                    }
                }
                string s = sql;
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    StockDockInfo infos = new StockDockInfo();
                    infos.Goods = res["goods"].ToString().Trim();
                    infos.Name = res["name"].ToString().Trim();
                    infos.Spec = res["spec"].ToString().Trim();
                    infos.Producer = res["producer"].ToString().Trim();
                    infos.BizOwnQty = res["bizownqty"].ToString().Trim();
                    infos.AbutJoinQty = res["abutjoinqty"].ToString().Trim();
                    infos.TopLimit = res["toplimit"].ToString().Trim();
                    infos.LowerLimit = res["lowerlimit"].ToString().Trim();
                    infos.ModifyTime = res["modifytime"].ToString().Trim();
                    infoList.Add(infos);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                if (trans != null)
                    trans.Dispose();

            }
            return infoList;
        }


        #region  系统库存对接
        //查询主数据
        public List<MasterDataInfo> GetMasterData() {

            List<MasterDataInfo> list = new List<MasterDataInfo>();
            string sql = "SELECT *  FROM V_ABUTJOIN_CST";
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;

                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    MasterDataInfo infos = new MasterDataInfo();
                    infos.AccountId = res["accountid"].ToString().Trim();
                    infos.AccountName = res["accountname"].ToString().Trim();
                    infos.AccountPercent = res["accountpercent"].ToString().Trim();
                    list.Add(infos);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());

            } finally {
                if (cmd != null)
                    cmd.Dispose();

                if (trans != null)
                    trans.Dispose();
            }
            return list;
        }

        //更新平台库存百分比
        public void UpdateAccountPercent(string accountPercent,string accountId,SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                trans = Conn_datacenter_cmszh.BeginTransaction();
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN_STOCK.P_ACCOUNT_EDITPERCENT";
                spCmd.CommandTimeout = 36000;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_ACCOUNTPERCENT",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_ACCOUNTID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = accountPercent;
                parameters[1].Value = Int64.Parse(accountId);
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[2].Size = 8;
                parameters[3].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[2].Value.ToString().Trim();
                retinfo.msg = parameters[3].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
        }

        //查询仓库数据
        public List<DepotInfo> GetDepotInfo()
        {
            List<DepotInfo> list = new List<DepotInfo>();
            string sql = "SELECT *  FROM V_ABUTJOIN_CST_DEPOT WHERE OWNERID=:OWNERIDS";
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("OWNERIDS", Properties.Settings.Default.OWNERID);

                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    DepotInfo infos = new DepotInfo();
                    infos.OwnerId = res["ownerid"].ToString().Trim();
                    infos.DeptCode = res["deptcode"].ToString().Trim();
                    infos.DeptName = res["deptname"].ToString().Trim();
                    infos.Percent = res["percent"].ToString().Trim();
                    infos.StorageId = res["storageid"].ToString().Trim();
                    list.Add(infos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                if (trans != null)
                    trans.Dispose();
            }
            return list;
        }

        //查询特殊配置数据
        public List<ConfigInfo> GetConfigInfo(Dictionary<string,string> sqlkeydict)
        {
            List<ConfigInfo> list = new List<ConfigInfo>();
            string sql = "SELECT * FROM V_ABUTJOIN_CST_DEPOTSPECIAL WHERE OWNERID=:OWNERIDS $";
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;
                string whereStr = "";
                //遍历Dictionary中的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");

                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                        }

                    }

                }
                sql = sql.Replace("$", whereStr);
                cmd.Parameters.Add("OWNERIDS", Properties.Settings.Default.OWNERID);
                cmd.CommandText = sql.ToString();
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.Add(kv.Key, kv.Value);
                        }
                    }
                }
                string s = sql;
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    ConfigInfo infos = new ConfigInfo();
                    infos.SpecialId = res["specialid"].ToString().Trim();
                    infos.OwnerId = res["ownerid"].ToString().Trim();
                    infos.StorageId = res["storageid"].ToString().Trim();
                    infos.DeptCode = res["deptcode"].ToString().Trim();
                    infos.DeptName = res["deptname"].ToString().Trim();
                    infos.Goods = res["goods"].ToString().Trim();
                    infos.GoodId = res["goodid"].ToString().Trim();
                    infos.Name = res["name"].ToString().Trim();
                    infos.Spec = res["spec"].ToString().Trim();
                    infos.Producer = res["producer"].ToString().Trim();
                    infos.Percent = res["percent"].ToString().Trim();
                    list.Add(infos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                if (trans != null)
                    trans.Dispose();
            }
            return list;
        }

        //查询商品数据
        public List<GoodsInfo> GetGoodsInfo(Dictionary<string, string> sqlkeydict,string accountids)
        {
            List<GoodsInfo> list = new List<GoodsInfo>();
            string sql = "SELECT * FROM V_ABUTJOIN_CST_GOODS WHERE Accountid=:accountid and Ownerid=:ownerid$";
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("accountid", accountids);
                cmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);
                string whereStr = "";
                //遍历Dictionary中的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");

                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                        }

                    }

                }
                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.Add(kv.Key, kv.Value);
                        }
                    }
                }
                string s = sql;
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    GoodsInfo infos = new GoodsInfo();
                    infos.AccountId = res["accountid"].ToString().Trim();
                    infos.Goods = res["goods"].ToString().Trim();
                    infos.GoodId = res["goodid"].ToString().Trim();
                    infos.Name = res["name"].ToString().Trim();
                    infos.Spec = res["spec"].ToString().Trim();
                    infos.Producer = res["producer"].ToString().Trim();
                    infos.CurQty = res["curqty"].ToString().Trim();
                    list.Add(infos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                if (trans != null)
                    trans.Dispose();
            }
            return list;
        }
        //查询仓库
        public List<Dept> GetDepts(Dictionary<string, string> sqlkeydict)
        {
            List<Dept> list = new List<Dept>();
            string sql = "SELECT * FROM  V_PUB_DEPT_STORAGE WHERE 1=1 $";
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;
                string whereStr = "";
                //遍历Dictionary中的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");

                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                        }

                    }

                }
                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.Add(kv.Key, kv.Value);
                        }
                    }
                }
                string s = sql;
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    Dept infos = new Dept();
                    infos.DeptId = res["deptid"].ToString().Trim();
                    infos.DeptCode = res["deptcode"].ToString().Trim();
                    infos.DeptName = res["deptname"].ToString().Trim();
                    list.Add(infos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                if (trans != null)
                    trans.Dispose();
            }
            return list;
        }

        //新增仓库
        public void AddDepts(string storageId,string percent,SPRetInfo retinfo) {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                trans = Conn_datacenter_cmszh.BeginTransaction();
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN_STOCK.P_DEPOT_ADD";
                spCmd.CommandTimeout = 36000;
                OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_STORAGEID",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_PERCENT",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[2].Value = storageId;
                parameters[3].Value = Int64.Parse(percent);
                parameters[4].Value = Int64.Parse(SessionDto.Empid);
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[5].Size = 8;
                parameters[6].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[5].Value.ToString().Trim();
                retinfo.msg = parameters[6].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
        }

        //删除仓库
        public void DelDepot(DepotInfo info,SPRetInfo retinfo) {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                //执行存储过程,进行删除
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN_STOCK.P_DEPOT_DEL";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_STORAGEID",OracleDbType.Varchar2,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
            };
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[2].Value = info.StorageId;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[3].Size = 8;
                parameters[4].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString();
                retinfo.msg = parameters[4].Value.ToString();
                spCmd.Dispose();
                spCmd = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }
            finally {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }

        }
        //更新仓库百分比
        public void UpdateDepotPercent(string Storage, string Percent,SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                trans = Conn_datacenter_cmszh.BeginTransaction();
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN_STOCK.P_DEPOT_UPDATE";
                spCmd.CommandTimeout = 36000;
                OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_STORAGEID",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_PERCENT",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[2].Value = Storage;
                parameters[3].Value = Int64.Parse(Percent);
                parameters[4].Value = Int64.Parse(SessionDto.Empid);
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[5].Size = 8;
                parameters[6].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[5].Value.ToString().Trim();
                retinfo.msg = parameters[6].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
        }

        //更新特殊配置百分比
        public void UpdateConfigPercent(string SpecialId, string Percent, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                trans = Conn_datacenter_cmszh.BeginTransaction();
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN_STOCK.P_DEPOTSPECIAL_MODIFY";
                spCmd.CommandTimeout = 36000;
                OracleParameter[] parameters ={
                    new OracleParameter("IN_SPECIALID",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_PERCENT",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = SpecialId;
                parameters[1].Value = Int64.Parse(Percent);
                parameters[2].Value = Int64.Parse(SessionDto.Empid);
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[3].Size = 8;
                parameters[4].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                retinfo.msg = parameters[4].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
        }
        //删除特殊配置
        public int DeleteConfig(List<DelSystemDock> List, SPRetInfo retinfo)
        {
            string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            string insertSql = "insert into ABUTJOIN_DEPOSPECIAL_DELTEMP(BATCHID,SPECIALID)values(:Batchid,:Relatid)";//插入中间表

            OracleCommand selCmd = null;
            OracleCommand insertCmd = null;
            OracleCommand spCmd = null;
            OracleTransaction trans = null;

            int retCode = -1;
            string sqlID = "0";

            try
            {
                //查询函数获取批次号
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    sqlID = res["SEQ"].ToString().Trim();
                res.Close();
                res.Dispose();
                selCmd.Dispose();
                selCmd = null;

                trans = Conn_datacenter_cmszh.BeginTransaction();
                //插入中间表
                insertCmd = Conn_datacenter_cmszh.CreateCommand();
                insertCmd.Connection = Conn_datacenter_cmszh;
                insertCmd.CommandType = System.Data.CommandType.Text;
                insertCmd.CommandText = insertSql.ToString();
                insertCmd.CommandTimeout = 3600;
                insertCmd.Transaction = trans;
                int recCount = 0;

                foreach (DelSystemDock selected in List)
                {
                    recCount++;

                    insertCmd.Parameters.Clear();

                    insertCmd.Parameters.Add("Batchid", int.Parse(sqlID));
                    insertCmd.Parameters.Add("Relatid", selected.Relatid);

                    insertCmd.ExecuteNonQuery();
                }
                trans.Commit();
                trans.Dispose();
                trans = null;

                insertCmd.Dispose();
                insertCmd = null;

                if (recCount < 1)
                {
                    retinfo.num = "0";
                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return 0;
                }

                //执行存储过程,进行删除
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN_STOCK.P_DEPOTSPECIAL_DEL";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
            };
                parameters[0].Value = Int64.Parse(sqlID);

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[1].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[2].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[2].Value.ToString().Trim();
                }
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
                if (trans != null)
                {
                    trans.Rollback();
                    trans.Dispose();
                    trans = null;
                }
            }
            return retCode;
        }

        //查询配置 商品
        public List<ConfigGood> GetConfigGoods(Dictionary<string, string> sqlkeydict)
        {
            List<ConfigGood> list = new List<ConfigGood>();
            string sql = "SELECT * FROM  V_PUB_WAREDICTS WHERE 1=1 $";
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 3600;
                string whereStr = "";
                //遍历Dictionary中的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + "and" + kv.Key.Replace("%", "") + "=" + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like:" + kv.Key.Replace("%", "");

                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "=:" + kv.Key;
                        }

                    }

                }
                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.Add(kv.Key, kv.Value);
                        }
                    }
                }
                string s = sql;
                OracleDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    ConfigGood infos = new ConfigGood();
                    infos.GoodId = res["goodid"].ToString().Trim();
                    infos.Goods = res["goods"].ToString().Trim();
                    infos.Name = res["name"].ToString().Trim();
                    infos.Spec = res["spec"].ToString().Trim();
                    infos.Producer = res["producer"].ToString().Trim();
                    list.Add(infos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                if (trans != null)
                    trans.Dispose();
            }
            return list;
        }
        //新增特殊配置
        public void AddConfigs(string storageId, string goodId, string percent, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                trans = Conn_datacenter_cmszh.BeginTransaction();
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN_STOCK.P_DEPOTSPECIAL_ADD";
                spCmd.CommandTimeout = 36000;
                OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_STORAGEID",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_GOODID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_PERCENT",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[2].Value = storageId;
                parameters[3].Value = Int64.Parse(goodId);
                parameters[4].Value = Int64.Parse(percent);
                parameters[5].Value = Int64.Parse(SessionDto.Empid);
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[6].Size = 8;
                parameters[7].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[6].Value.ToString().Trim();
                retinfo.msg = parameters[7].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
        }

        //删除商品
        public int DeleteGoodsd(List<DelSystemDock> List,string AccountId,SPRetInfo retinfo)
        {
            string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            string insertSql = "insert into ABUTJOIN_GOODS_DELTEMP(BATCHID,COMPID,OWNERID,ACCOUNTID,GOODID)values(:batchid,:compid,:ownerid,:accountid,:goodid)";//插入中间表

            OracleCommand selCmd = null;
            OracleCommand insertCmd = null;
            OracleCommand spCmd = null;
            OracleTransaction trans = null;

            int retCode = -1;
            string sqlID = "0";

            try
            {
                //查询函数获取批次号
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 3600;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    sqlID = res["SEQ"].ToString().Trim();
                res.Close();
                res.Dispose();
                selCmd.Dispose();
                selCmd = null;

                trans = Conn_datacenter_cmszh.BeginTransaction();
                //插入中间表
                insertCmd = Conn_datacenter_cmszh.CreateCommand();
                insertCmd.Connection = Conn_datacenter_cmszh;
                insertCmd.CommandType = System.Data.CommandType.Text;
                insertCmd.CommandText = insertSql.ToString();
                insertCmd.CommandTimeout = 3600;
                insertCmd.Transaction = trans;
                int recCount = 0;

                foreach (DelSystemDock selected in List)
                {
                    recCount++;

                    insertCmd.Parameters.Clear();

                    insertCmd.Parameters.Add("batchid", int.Parse(sqlID));
                    insertCmd.Parameters.Add("compid", Properties.Settings.Default.COMPID);
                    insertCmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);
                    insertCmd.Parameters.Add("accountid",Int64.Parse(AccountId));
                    insertCmd.Parameters.Add("goodid", selected.Relatid);
                    insertCmd.ExecuteNonQuery();
                }
                trans.Commit();
                trans.Dispose();
                trans = null;

                insertCmd.Dispose();
                insertCmd = null;

                if (recCount < 1)
                {
                    retinfo.num = "0";
                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return 0;
                }

                //执行存储过程,进行删除
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN_STOCK.P_GOODS_DEL";
                spCmd.CommandTimeout = 3600;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)
            };
                parameters[0].Value = Int64.Parse(sqlID);

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;

                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[1].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[2].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[2].Value.ToString().Trim();
                }
                spCmd.Dispose();
                spCmd = null;
                retCode = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                retCode = -1;
            }
            finally
            {
                if (spCmd != null)
                    spCmd.Dispose();
                if (trans != null)
                {
                    trans.Rollback();
                    trans.Dispose();
                    trans = null;
                }
            }
            return retCode;
        }

        //新增商品
        public void AddGoodsd(string accountId,string goodId,SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                trans = Conn_datacenter_cmszh.BeginTransaction();
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN_STOCK.P_GOODS_ADD";
                spCmd.CommandTimeout = 36000;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_ACCOUNTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_GOODID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = Int64.Parse(accountId);
                parameters[1].Value = Properties.Settings.Default.COMPID;
                parameters[2].Value = Properties.Settings.Default.OWNERID;
                parameters[3].Value = Int64.Parse(goodId);
                parameters[4].Value = Int64.Parse(SessionDto.Empid);
                //parameters[1].Value = userinfo.empid.ToString();
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[5].Size = 8;
                parameters[6].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[5].Value.ToString().Trim();
                retinfo.msg = parameters[6].Value.ToString().Trim();
                //retinfo.result = parameters[1].Value.ToString().Trim();
                spCmd.Dispose();
                spCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
        }


        //excel导入 
        public SortableBindingList<SysDockXlsInfo> GetSysDockData(SortableBindingList<SysDockXlsInfo> ClientList, SPRetInfo retinfo)
        {
            SortableBindingList<SysDockXlsInfo> infoList = new SortableBindingList<SysDockXlsInfo>();

            string selectSql = "SELECT F_GET_SEQ('BATCHID') as SEQ FROM dual";
            string insertSql = "INSERT INTO ABUTJOIN_GOODS_XLSTEMP(batchid,excel_seqid,goods,createuser,accountid,compid,ownerid)VALUES(:Batchid,:Excel_seqid,:Goods,:Createuser,:Accountid,:Compid,:Ownerid)";

            string selSql = "SELECT * FROM ABUTJOIN_GOODS_XLSTEMP  WHERE batchid =:batchid";

            OracleCommand spCmd = null;
            OracleCommand selCmd = null;
            OracleCommand insetCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            string seqID = "0";

            //查询批次号
            try
            {
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selectSql.ToString();
                selCmd.CommandTimeout = 36000;

                OracleDataReader res = selCmd.ExecuteReader();
                if (res.Read())
                    seqID = res["SEQ"].ToString().Trim();

                retinfo.result = seqID;

                res.Close();
                res.Dispose();

                selCmd.Dispose();
                selCmd = null;

                //将信息插入中间表
                trans = Conn_datacenter_cmszh.BeginTransaction();

                insetCmd = Conn_datacenter_cmszh.CreateCommand();
                insetCmd.Connection = Conn_datacenter_cmszh;
                insetCmd.CommandType = System.Data.CommandType.Text;
                insetCmd.CommandText = insertSql.ToString();
                insetCmd.CommandTimeout = 36000;

                insetCmd.Transaction = trans;
                int recCount = 0;
                foreach (SysDockXlsInfo selected in ClientList)
                {
                    recCount++;

                    insetCmd.Parameters.Clear();

                    insetCmd.Parameters.Add("Batchid", Int32.Parse(seqID));
                    insetCmd.Parameters.Add("Excel_seqid", Int32.Parse(selected.ExcelSeqid));

                    if (selected.Goods != "")
                    {
                        insetCmd.Parameters.Add("Goods", selected.Goods);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("Goods", "");
                    }
                    insetCmd.Parameters.Add("Createuser", Int64.Parse(selected.CreateUser));
                    insetCmd.Parameters.Add("Accountid", Int64.Parse(selected.AccountId));
                    insetCmd.Parameters.Add("Compid", Int32.Parse(selected.Compid));
                    insetCmd.Parameters.Add("Ownerid", Int32.Parse(selected.Ownerid));
                  
                    //MessageBox.Show("=="+ insetCmd.Parameters[0].Value);
                    insetCmd.ExecuteNonQuery();
                }

                trans.Commit();
                trans.Dispose();
                trans = null;

                insetCmd.Dispose();
                insetCmd = null;

                //再次查询此表
                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.Parameters.Add("batchid", seqID);
                seCmd.CommandTimeout = 36000;

                OracleDataReader ress = seCmd.ExecuteReader();
                foreach (SysDockXlsInfo info in ClientList)
                {
                    if (ress.Read())
                    info.Batchid = ress["batchid"].ToString().Trim();
                    info.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    info.Compid = ress["compid"].ToString().Trim();
                    info.Ownerid = ress["ownerid"].ToString().Trim();
                    info.AccountId = ress["accountid"].ToString().Trim();
                    info.Goods = ress["goods"].ToString().Trim();
                    info.GoodId = ress["goodid"].ToString().Trim();
                    info.CreateUser= ress["createuser"].ToString().Trim();
                    infoList.Add(info);
                }

                ress.Close();
                ress.Dispose();

                seCmd.Dispose();
                seCmd = null;


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();

            }
            finally
            {

                if (selCmd != null)
                    selCmd.Dispose();

                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();

                if (insetCmd != null)
                    insetCmd.Dispose();

                if (seCmd != null)
                    seCmd.Dispose();

            }

            return infoList;
        }


        //检查导入
        public SortableBindingList<SysDockXlsInfo> chackSysDockInfo(SortableBindingList<SysDockXlsInfo> list, string batchid)
        {
            SortableBindingList<SysDockXlsInfo> infolist = new SortableBindingList<SysDockXlsInfo>();
            string selSql = "SELECT * FROM ABUTJOIN_GOODS_XLSTEMP  WHERE batchid =:batchid";
            OracleCommand spCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN_STOCK.P_GOODS_XLSCHECK";
                spCmd.CommandTimeout = 36000;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(batchid);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                spCmd.Dispose();
                spCmd = null;

                //再次查询中间表
                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.Parameters.Add("batchid", batchid);
                seCmd.CommandTimeout = 36000;

                OracleDataReader ress = seCmd.ExecuteReader();
                foreach (SysDockXlsInfo info in list)
                {
                    if (ress.Read())
                    info.Batchid = ress["batchid"].ToString().Trim();
                    info.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    info.Compid = ress["compid"].ToString().Trim();
                    info.Ownerid = ress["ownerid"].ToString().Trim();
                    info.AccountId = ress["accountid"].ToString().Trim();
                    info.Goods = ress["goods"].ToString().Trim();
                    info.GoodId = ress["goodid"].ToString().Trim();
                    info.CreateUser = ress["createuser"].ToString().Trim();
                    info.CheckState = ress["checkstate"].ToString().Trim();
                    info.CheckMsg = ress["checkmsg"].ToString().Trim();
                    infolist.Add(info);
                }

                ress.Close();
                ress.Dispose();

                seCmd.Dispose();
                seCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
                if (seCmd != null)
                    seCmd.Dispose();
            }

            return infolist;
        }

        //导入
        public SortableBindingList<SysDockXlsInfo> insertSysDockInfo(SortableBindingList<SysDockXlsInfo> list, string batchid)
        {
            SortableBindingList<SysDockXlsInfo> infolist = new SortableBindingList<SysDockXlsInfo>();
            string selSql = "SELECT * FROM ABUTJOIN_GOODS_XLSTEMP  WHERE batchid =:batchid";
            OracleCommand spCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_ABUTJOIN_STOCK.P_GOODS_XLSIMPORT";
                spCmd.CommandTimeout = 36000;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Int64.Parse(batchid);
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[1].Size = 8;
                parameters[2].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                spCmd.Dispose();
                spCmd = null;

                //再次查询中间表
                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.Parameters.Add("batchid", batchid);
                seCmd.CommandTimeout = 36000;

                OracleDataReader ress = seCmd.ExecuteReader();
                foreach (SysDockXlsInfo info in list)
                {
                    if (ress.Read())
                    info.Batchid = ress["batchid"].ToString().Trim();
                    info.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    info.Compid = ress["compid"].ToString().Trim();
                    info.Ownerid = ress["ownerid"].ToString().Trim();
                    info.AccountId = ress["accountid"].ToString().Trim();
                    info.Goods = ress["goods"].ToString().Trim();
                    info.GoodId = ress["goodid"].ToString().Trim();
                    info.CreateUser = ress["createuser"].ToString().Trim();
                    info.CheckState = ress["checkstate"].ToString().Trim();
                    info.CheckMsg = ress["checkmsg"].ToString().Trim();
                    info.ImportState = ress["importstate"].ToString().Trim();
                    info.ImportMsg = ress["importmsg"].ToString().Trim();
                    infolist.Add(info);
                }

                ress.Close();
                ress.Dispose();

                seCmd.Dispose();
                seCmd = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
                if (seCmd != null)
                    seCmd.Dispose();
            }

            return infolist;
        }


        #endregion
    }

}
