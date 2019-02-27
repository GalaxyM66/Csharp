using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Thrift.Protocol;
using Thrift.Transport;

namespace PriceManager
{
    class APDao_Agreement : MySQLHelper
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
        //下拉框加载
        public Dictionary<string, string> getCbContent(string typecode) {
            Dictionary<string, string> info = new Dictionary<string, string>();
            string sql = "select * from SYS_CODE where typecode=:typecode and ownerid=:ownerid order by name desc";
            OracleCommand selCmd = null;
            OracleTransaction trans = null;
            try {
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = sql.ToString();
                selCmd.CommandTimeout = 3000;
                selCmd.Parameters.Add("typecode", typecode);
                //-------2019-2-12-
                selCmd.Parameters.Add("ownerid",Properties.Settings.Default.OWNERID);
                OracleDataReader res = selCmd.ExecuteReader();
                while (res.Read())
                {
                    string Name = res["NAME"].ToString().Trim();
                    string Code = res["CODE"].ToString().Trim();
                    info.Add(Code, Name);
                }
            } catch (Exception ex) {

                MessageBox.Show(ex.ToString(), "错误信息");

            } finally {
                if (selCmd != null)
                    selCmd.Dispose();

                if (trans != null)
                    trans.Dispose();
            }

            return info;
        }

        //修改或者新增下拉框加载
        public Dictionary<string, string> getCbContents(string typecode)
        {
            Dictionary<string, string> info = new Dictionary<string, string>();
            string sql = "select * from SYS_CODE where typecode=:typecode and ownerid=:ownerid and name is not null order by name desc";
            OracleCommand selCmd = null;
            OracleTransaction trans = null;
            try
            {
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = sql.ToString();
                selCmd.CommandTimeout = 1800;
                selCmd.Parameters.Add("typecode", typecode);
            //---2019-2-12-----------
                selCmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);

                OracleDataReader res = selCmd.ExecuteReader();
                while (res.Read())
                {
                    string Name = res["NAME"].ToString().Trim();
                    string Code = res["CODE"].ToString().Trim();
                    info.Add(Code, Name);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "错误信息");

            }
            finally
            {
                if (selCmd != null)
                    selCmd.Dispose();

                if (trans != null)
                    trans.Dispose();
            }

            return info;
        }
        //查询所有的信息
        public SortableBindingList<AgreeProducerInfo> GetAgreeProdInfo(Dictionary<string, string> sqlkeydict) {
            SortableBindingList<AgreeProducerInfo> infoList = new SortableBindingList<AgreeProducerInfo>();
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            string sql = "select * from V_AGREEMENT_PRODUCER_INFO where 1=1 and ownerid=:ownerid $";
            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 1800;
                //----2019-2-12---------
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
                while (res.Read()) {
                    AgreeProducerInfo infos = new AgreeProducerInfo();
                    infos.ProdId = res["PROD_ID"].ToString().Trim();
                    infos.ProdCode = res["PROD_CODE"].ToString().Trim();
                    infos.ProdName = res["PROD_NAME"].ToString().Trim();
                    infos.Import = res["IMPORTNAME"].ToString().Trim();
                    infos.BuyerName = res["BUYERNAMENAME"].ToString().Trim();
                    infos.Manager = res["MANAGERNAME"].ToString().Trim();
                    infos.MiddleMan = res["MIDDLEMAN"].ToString().Trim();
                    infos.AgreeType = res["AGREETYPENAME"].ToString().Trim();
                    infos.CreateUser = res["CREATEUSER"].ToString().Trim();
                    infos.CreateTime = res["CREATETIME"].ToString().Trim();
                    infos.ModifyUser = res["MODIFYUSER"].ToString().Trim();
                    infos.ModifyTime = res["MODIFYTIME"].ToString().Trim();
                    infos.BeginDate = res["BEGINDATE"].ToString().Trim();
                    //----2019-1-29 新增字段
                    infos.CompId = res["COMPID"].ToString().Trim();
                    infos.OwnerId = res["OWNERID"].ToString().Trim();
                    infoList.Add(infos);
                }

            } catch (Exception ex) {

                MessageBox.Show(ex.ToString());

            } finally {
                if (cmd != null)
                    cmd.Dispose();

                if (trans != null)
                    trans.Dispose();

            }
            return infoList;
        }
        //新增
        public int AddAgrProd(string ProdCode, string ProdName,
          string import, string buyer, string manager, string MiddleMan,
          string agreeType, string beginDate, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_AGR_PRODINFO.P_AGR_PRODINFO_ADD";
                spCmd.CommandTimeout = 1800;
                //向存储过程中添加输入输出
                OracleParameter[] parameters ={
                    new OracleParameter("IN_PROD_CODE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_PROD_NAME",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_IMPORT",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_BUYERNAME",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_MANAGER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_MIDDLEMAN",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_AGREETYPE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_BEGINDATE",OracleDbType.Date,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = ProdCode;
                parameters[1].Value = ProdName;
                parameters[2].Value = import;
                parameters[3].Value = buyer;
                parameters[4].Value = manager;
                parameters[5].Value = MiddleMan;
                parameters[6].Value = agreeType;
                parameters[7].Value = DateTime.ParseExact(beginDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                parameters[8].Value = SessionDto.Empid;
                parameters[9].Value = Int64.Parse(Properties.Settings.Default.COMPID);
                parameters[10].Value = Int64.Parse(Properties.Settings.Default.OWNERID);

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[11].Size = 8;
                parameters[12].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[11].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[12].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[12].Value.ToString().Trim();
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

        //修改
        public int UpdateAgrProd(string Prodid, string ProdCode, string ProdName,
          string import, string buyer, string manager, string MiddleMan,
          string agreeType, string beginDate,string compid,string ownerid, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_AGR_PRODINFO.P_AGR_PRODINFO_MODIFY";
                spCmd.CommandTimeout = 1800;
                //向存储过程中添加输入输出
                OracleParameter[] parameters ={
                    new OracleParameter("IN_PROD_ID",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_PROD_CODE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_PROD_NAME",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_IMPORT",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_BUYERNAME",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_MANAGER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_MIDDLEMAN",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_AGREETYPE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_BEGINDATE",OracleDbType.Date,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = Prodid;
                parameters[1].Value = ProdCode;
                parameters[2].Value = ProdName;
                parameters[3].Value = import;
                parameters[4].Value = buyer;
                parameters[5].Value = manager;
                parameters[6].Value = MiddleMan;
                parameters[7].Value = agreeType;
                parameters[8].Value = DateTime.ParseExact(beginDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                parameters[9].Value = Int64.Parse(SessionDto.Empid);
                parameters[10].Value = compid;
                parameters[11].Value = ownerid;

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[12].Size = 8;
                parameters[13].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[12].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[13].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[13].Value.ToString().Trim();
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
        //删除厂家信息
        public int DeleteAgrProd(SortableBindingList<DelTemp> List, SPRetInfo retinfo)
        {
            string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            string insertSql = "insert into AGREEMENT_PRODUCER_DELTEMP(BATCHID,PROD_ID)values(:Batchid,:ProdId)";//插入中间表

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
                selCmd.CommandTimeout = 1800;

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
                insertCmd.CommandTimeout = 1800;
                insertCmd.Transaction = trans;
                int recCount = 0;

                foreach (DelTemp selected in List)
                {
                    recCount++;

                    insertCmd.Parameters.Clear();

                    insertCmd.Parameters.Add("Batchid", int.Parse(sqlID));
                    insertCmd.Parameters.Add("ProdId", selected.RelateId);

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
                spCmd.CommandText = "PG_AGR_PRODINFO.P_AGR_PRODINFO_DEL";
                spCmd.CommandTimeout = 1800;

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

        //协议客户信息查询
        public SortableBindingList<AgreeClient> GetAgreeClientInfo(Dictionary<string, string> sqlkeydict) {
            SortableBindingList<AgreeClient> infolist = new SortableBindingList<AgreeClient>();
            string sql = "";
            if (SessionDto.Emproleid == "108"|| SessionDto.Emproleid == "113") {
                sql = "select * from V_AGREEMENT_CLIENT_INFO where pibuyernamename=:buyernamename  and ownerid=:ownerid $";
            }
            if (SessionDto.Emproleid == "109" || SessionDto.Emproleid == "114") {
                sql = "select * from V_AGREEMENT_CLIENT_INFO where pimanagername=:managername and ownerid=:ownerid $";
            }
            if (SessionDto.Emproleid == "99" || SessionDto.Emproleid == "115" || SessionDto.Emproleid == "121") {
                sql = "select * from V_AGREEMENT_CLIENT_INFO where 1=1 and ownerid=:ownerid $";
            }
            OracleCommand cmd = null;
            OracleTransaction trans = null;

            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                if (SessionDto.Emproleid == "108" || SessionDto.Emproleid == "113")
                {
                    cmd.Parameters.Add("pibuyernamename", SessionDto.Empname);
                }
                if (SessionDto.Emproleid == "109" || SessionDto.Emproleid == "114")
                {
                    cmd.Parameters.Add("pimanagername", SessionDto.Empname);
                }
                cmd.CommandTimeout = 1800;
                //------2019-2-12---------
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
                    AgreeClient infos = new AgreeClient();
                    infos.AgreementId = res["AGREEMENT_ID"].ToString().Trim();
                    infos.ProdId = res["PROD_ID"].ToString().Trim();
                    infos.ProdCode = res["PROD_CODE"].ToString().Trim();
                    infos.ProdName = res["PROD_NAME"].ToString().Trim();
                    infos.Import = res["IMPORTNAME"].ToString().Trim();
                    infos.BuyerName = res["BUYERNAMENAME"].ToString().Trim();
                    infos.Manager = res["MANAGERNAME"].ToString().Trim();
                    infos.MiddleMan = res["MIDDLEMAN"].ToString().Trim();
                    infos.AgreeType = res["AGREETYPENAME"].ToString().Trim();
                    infos.YearNum = res["YEARNUM"].ToString().Trim();
                    infos.CstId = res["CSTID"].ToString().Trim();
                    infos.CstCode = res["CSTCODE"].ToString().Trim();
                    infos.CstName = res["CSTNAME"].ToString().Trim();
                    infos.Saller = res["SALLER"].ToString().Trim();
                    infos.SallManager = res["SALLMANAGER"].ToString().Trim();
                    infos.SallLeader = res["SALLLEADER"].ToString().Trim();
                    infos.AgreeLevel = res["AGREELEVELNAME"].ToString().Trim();
                    infos.LastValues = res["LASTVALUES"].ToString().Trim();
                    infos.ForecastValues = res["FORECASTVALUES"].ToString().Trim();
                    infos.LastUpStream = res["LASTUPSTREAMNAME"].ToString().Trim();
                    infos.TarGet = res["TARGETNAME"].ToString().Trim();
                    infos.CstIntention = res["CSTINTENTIONNAME"].ToString().Trim();
                    infos.CstIntentionTime = res["CSTINTENTIONTIME"].ToString().Trim();
                    infos.ProdIntention = res["PRODINTENTIONNAME"].ToString().Trim();
                    infos.Mark = res["MARK"].ToString().Trim();
                    infos.Dynamics = res["DYNAMICSNAME"].ToString().Trim();
                    infos.FinalChannel = res["FINALCHANNELNAME"].ToString().Trim();
                    infos.ThisYearValues = res["THISYEARVALUES"].ToString().Trim();
                    infos.HopeValues = res["HOPEVALUES"].ToString().Trim();
                    infos.Seal = res["SEALNAME"].ToString().Trim();
                    infos.Onfile = res["ONFILENAME"].ToString().Trim();
                    infos.FinalValues = res["FINALVALUES"].ToString().Trim();
                    infos.BeginDate = res["BEGINDATE"].ToString().Trim();
                    infos.ModifySallerTime = res["MODIFYSALLERTIME"].ToString().Trim();
                    //----2019-1-29 新增字段
                    infos.CompId = res["COMPID"].ToString().Trim();
                    infos.OwnerId = res["OWNERID"].ToString().Trim();
                    infolist.Add(infos);
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

            return infolist;
        }
        //销售反馈 查询
        public SortableBindingList<SaleFeed> GetSaleFeedBackInfo(Dictionary<string, string> sqlkeydict) {
            SortableBindingList<SaleFeed> infolist = new SortableBindingList<SaleFeed>();
            string sql = "";
            if (SessionDto.Emproleid == "117" || SessionDto.Emproleid == "104")
            {
                sql = "select * from V_AGREEMENT_CLIENT_INFO where 1=1 and saller=:saller and compid=:compid and ownerid=:ownerid $";
            }
            if (SessionDto.Emproleid == "118" || SessionDto.Emproleid == "123")
            {
                sql = "select * from V_AGREEMENT_CLIENT_INFO where (sallmanager=:sallmanager or sallmanager is null or saller is null or sallleader is null) and compid=:compid and ownerid=:ownerid $";
            }
            if (SessionDto.Emproleid == "119")
            {
                sql = "select * from V_AGREEMENT_CLIENT_INFO where 1=1 and sallLeader=:sallLeader and compid=:compid and ownerid=:ownerid $";
            }
            if (SessionDto.Emproleid == "99" || SessionDto.Emproleid == "120" || SessionDto.Emproleid == "121")
            {
                sql = "select * from V_AGREEMENT_CLIENT_INFO where compid=:compid and ownerid=:ownerid $";
            }
            OracleCommand cmd = null;
            OracleTransaction trans = null;

            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                if (SessionDto.Emproleid == "117" || SessionDto.Emproleid == "104")
                {
                    cmd.Parameters.Add("saller", SessionDto.Empname);
                }
                if (SessionDto.Emproleid == "118" || SessionDto.Emproleid == "123")
                {
                    cmd.Parameters.Add("sallmanager", SessionDto.Empname);
                }
                if (SessionDto.Emproleid == "119")
                {
                    cmd.Parameters.Add("sallLeader", SessionDto.Empname);
                }
                cmd.Parameters.Add("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);
                cmd.CommandTimeout = 1800;

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
                    SaleFeed infos = new SaleFeed();
                    infos.YearNum = res["YEARNUM"].ToString().Trim();
                    infos.AgreementId = res["AGREEMENT_ID"].ToString().Trim();
                    infos.ProdId = res["PROD_ID"].ToString().Trim();
                    infos.ProdCode = res["PROD_CODE"].ToString().Trim();
                    infos.ProdName = res["PROD_NAME"].ToString().Trim();
                    infos.TarGet = res["TARGETNAME"].ToString().Trim();
                    infos.CstCode = res["CSTCODE"].ToString().Trim();
                    infos.CstName = res["CSTNAME"].ToString().Trim();
                    infos.SallLeader = res["SALLLEADER"].ToString().Trim();
                    infos.SallManager = res["SALLMANAGER"].ToString().Trim();
                    infos.Saller = res["SALLER"].ToString().Trim();
                    infos.CstIntention = res["CSTINTENTIONNAME"].ToString().Trim();
                    infos.ProdIntention = res["PRODINTENTIONNAME"].ToString().Trim();
                    infos.Mark = res["MARK"].ToString().Trim();
                    infos.Dynamics = res["DYNAMICSNAME"].ToString().Trim();
                    infos.FinalChannel = res["FINALCHANNELNAME"].ToString().Trim();
                    infos.ThisYearValues = res["THISYEARVALUES"].ToString().Trim();
                    infos.HopeValues = res["HOPEVALUES"].ToString().Trim();
                    infos.Import = res["IMPORTNAME"].ToString().Trim();
                    infos.BuyerName = res["BUYERNAMENAME"].ToString().Trim();
                    infos.Manager = res["MANAGERNAME"].ToString().Trim();
                    infos.AgreeType = res["AGREETYPENAME"].ToString().Trim();
                    infos.MiddleMan = res["MIDDLEMAN"].ToString().Trim();
                    infos.AgreeLevel = res["AGREELEVELNAME"].ToString().Trim();
                    infos.LastValues = res["LASTVALUES"].ToString().Trim();
                    infos.LastUpStream = res["LASTUPSTREAMNAME"].ToString().Trim();
                    infos.ForecastValues = res["FORECASTVALUES"].ToString().Trim();
                    infos.BeginDate = res["BEGINDATE"].ToString().Trim();
                    infos.Seal = res["SEALNAME"].ToString().Trim();
                    infos.Onfile = res["ONFILENAME"].ToString().Trim();
                    infos.FinalValues = res["FINALVALUES"].ToString().Trim();
                    infos.ModifyBuyerTime = res["MODIFYBUYERTIME"].ToString().Trim();

                    infolist.Add(infos);
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
            return infolist;
        }

        //销售反馈 查询(勾选时间)
        public SortableBindingList<SaleFeed> GetSaleFeedBackInfos(Dictionary<string, string> sqlkeydict,string time)
        {
            SortableBindingList<SaleFeed> infolist = new SortableBindingList<SaleFeed>();
            string sql = "";
            if (SessionDto.Emproleid == "117" || SessionDto.Emproleid == "104")
            {
                sql = "select * from V_AGREEMENT_CLIENT_INFO where 1=1 and saller=:saller and begindate=:begindate and compid=:compid and ownerid=:ownerid $";
            }
            if (SessionDto.Emproleid == "118" || SessionDto.Emproleid == "123")
            {
                sql = "select * from V_AGREEMENT_CLIENT_INFO where (sallmanager=:sallmanager or sallmanager is null or saller is null or sallleader is null) and begindate=:begindate and compid=:compid and ownerid=:ownerid $";
            }
            if (SessionDto.Emproleid == "119")
            {
                sql = "select * from V_AGREEMENT_CLIENT_INFO where 1=1 and sallLeader=:sallLeader and begindate=:begindate and compid=:compid and ownerid=:ownerid $";
            }
            if (SessionDto.Emproleid == "99" || SessionDto.Emproleid == "120" || SessionDto.Emproleid == "121")
            {
                sql = "select * from V_AGREEMENT_CLIENT_INFO where 1=1 and begindate=:begindate and compid=:compid and ownerid=:ownerid $";
            }
            OracleCommand cmd = null;
            OracleTransaction trans = null;

            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                if (SessionDto.Emproleid == "117" || SessionDto.Emproleid == "104")
                {
                    cmd.Parameters.Add("saller", SessionDto.Empname);
                }
                if (SessionDto.Emproleid == "118" || SessionDto.Emproleid == "123")
                {
                    cmd.Parameters.Add("sallmanager", SessionDto.Empname);
                }
                if (SessionDto.Emproleid == "119")
                {
                    cmd.Parameters.Add("sallLeader", SessionDto.Empname);
                }
                cmd.Parameters.Add("begindate", DateTime.ParseExact(time, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture));
                cmd.Parameters.Add("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);
                cmd.CommandTimeout = 1800;

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
                    SaleFeed infos = new SaleFeed();
                    infos.YearNum = res["YEARNUM"].ToString().Trim();
                    infos.AgreementId = res["AGREEMENT_ID"].ToString().Trim();
                    infos.ProdId = res["PROD_ID"].ToString().Trim();
                    infos.ProdCode = res["PROD_CODE"].ToString().Trim();
                    infos.ProdName = res["PROD_NAME"].ToString().Trim();
                    infos.TarGet = res["TARGETNAME"].ToString().Trim();
                    infos.CstCode = res["CSTCODE"].ToString().Trim();
                    infos.CstName = res["CSTNAME"].ToString().Trim();
                    infos.SallLeader = res["SALLLEADER"].ToString().Trim();
                    infos.SallManager = res["SALLMANAGER"].ToString().Trim();
                    infos.Saller = res["SALLER"].ToString().Trim();
                    infos.CstIntention = res["CSTINTENTIONNAME"].ToString().Trim();
                    infos.ProdIntention = res["PRODINTENTIONNAME"].ToString().Trim();
                    infos.Mark = res["MARK"].ToString().Trim();
                    infos.Dynamics = res["DYNAMICSNAME"].ToString().Trim();
                    infos.FinalChannel = res["FINALCHANNELNAME"].ToString().Trim();
                    infos.ThisYearValues = res["THISYEARVALUES"].ToString().Trim();
                    infos.HopeValues = res["HOPEVALUES"].ToString().Trim();
                    infos.Import = res["IMPORTNAME"].ToString().Trim();
                    infos.BuyerName = res["BUYERNAMENAME"].ToString().Trim();
                    infos.Manager = res["MANAGERNAME"].ToString().Trim();
                    infos.AgreeType = res["AGREETYPENAME"].ToString().Trim();
                    infos.MiddleMan = res["MIDDLEMAN"].ToString().Trim();
                    infos.AgreeLevel = res["AGREELEVELNAME"].ToString().Trim();
                    infos.LastValues = res["LASTVALUES"].ToString().Trim();
                    infos.LastUpStream = res["LASTUPSTREAMNAME"].ToString().Trim();
                    infos.ForecastValues = res["FORECASTVALUES"].ToString().Trim();
                    infos.BeginDate = res["BEGINDATE"].ToString().Trim();
                    infos.Seal = res["SEALNAME"].ToString().Trim();
                    infos.Onfile = res["ONFILENAME"].ToString().Trim();
                    infos.FinalValues = res["FINALVALUES"].ToString().Trim();
                    infos.ModifyBuyerTime = res["MODIFYBUYERTIME"].ToString().Trim();

                    infolist.Add(infos);
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
            return infolist;
        }

        #region  协议维护模块
        //查询客户 协议维护 
        public SortableBindingList<SearchClientInfo> GetSearchClientInfo(Dictionary<string, string> sqlkeydict, SPRetInfo retInfo)
        {
            SortableBindingList<SearchClientInfo> infoList = new SortableBindingList<SearchClientInfo>();
            string sql = "select * from v_pub_clients where 1=1 $";
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            try {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 1800;

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
                    SearchClientInfo info = new SearchClientInfo();
                    info.CstId = res["CSTID"].ToString().Trim();
                    info.CstCode = res["CSTCODE"].ToString().Trim();
                    info.CstName = res["DNAME"].ToString().Trim();
                    infoList.Add(info);
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                trans.Rollback();
            } finally {
                if (cmd != null) {
                    cmd.Dispose();
                }
                if (trans != null) {
                    trans.Dispose();
                }
            }
            return infoList;
        }

        //查询客户详细 协议维护 
        public SearchClientInfo SelDetail(string CstId)
        {
            SearchClientInfo info = new SearchClientInfo();
            string sql = "select * from  V_SCM_SELLERCST_STRUC where compid=:compid and ownerid=:ownerid and saledeptid=:saledeptid and cstid=:cstid";
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.CommandTimeout = 1800;
                cmd.Parameters.Add("compid", Int64.Parse(Properties.Settings.Default.COMPID));
                cmd.Parameters.Add("ownerid", Int64.Parse(Properties.Settings.Default.OWNERID));
                cmd.Parameters.Add("saledeptid", Int64.Parse(SessionDto.Empdeptid));
                cmd.Parameters.Add("cstid", Int64.Parse(CstId));

                string s = sql;
                OracleDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    info.Saller = res["sellername"].ToString().Trim();
                    info.SallManager = res["managername"].ToString().Trim();
                    info.SallLeader = res["leadername"].ToString().Trim();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                trans.Rollback();
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
            return info;
        }

        //新增 协议维护
        public int AddAgrClient(string ProdId, string ProdCode, string ProdName,
         string import, string buyer, string manager, string MiddleMan, string agreeType,
         string YearNum, string cstId, string cstCode, string cstName, string saller, string sallManager, string sallLeader, string agreeLevel,
         string lastValues, string foreCastValues, string lastUpStream, string tarGet, string beginDate, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_AGR_CSTINFO.P_AGR_CSTINFO_ADD";
                spCmd.CommandTimeout = 1800;
                //向存储过程中添加输入输出
                OracleParameter[] parameters ={
                    new OracleParameter("IN_PROD_ID",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_PROD_CODE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_PROD_NAME",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_IMPORT",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_BUYERNAME",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_MANAGER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_MIDDLEMAN",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_AGREETYPE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_YEARNUM",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTCODE",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_CSTNAME",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_SALLER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_SALLMANAGER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_SALLLEADER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_AGREELEVEL",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_LASTVALUES",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("IN_FORECASTVALUES",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("IN_LASTUPSTREAM",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_TARGET",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_BEGINDATE",OracleDbType.Date,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = ProdId;
                parameters[1].Value = ProdCode;
                parameters[2].Value = ProdName;
                parameters[3].Value = import;
                parameters[4].Value = buyer;
                parameters[5].Value = manager;
                parameters[6].Value = MiddleMan;
                parameters[7].Value = agreeType;

                parameters[8].Value = Int64.Parse(YearNum);
                parameters[9].Value = Int64.Parse(cstId);
                parameters[10].Value = cstCode;
                parameters[11].Value = cstName;
                parameters[12].Value = saller;
                parameters[13].Value = sallManager;
                parameters[14].Value = sallLeader;
                parameters[15].Value = agreeLevel;
                parameters[16].Value = Double.Parse(lastValues);
                parameters[17].Value = Double.Parse(foreCastValues);
                parameters[18].Value = lastUpStream;
                parameters[19].Value = tarGet;
                parameters[20].Value = DateTime.ParseExact(beginDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                parameters[21].Value = SessionDto.Empid;
                parameters[22].Value = Int64.Parse(Properties.Settings.Default.COMPID);
                parameters[23].Value = Int64.Parse(Properties.Settings.Default.OWNERID);

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[24].Size = 8;
                parameters[25].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[24].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[25].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[25].Value.ToString().Trim();
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

        //修改 协议维护
        public int UpdateAgrClient(string agreeId, string ProdId, string ProdCode, string ProdName,
         string import, string buyer, string manager, string MiddleMan, string agreeType,
         string YearNum, string cstId, string cstCode, string cstName, string saller, string sallManager, string sallLeader, string agreeLevel,
         string lastValues, string foreCastValues, string lastUpStream, string tarGet, string beginDate,string compid,string ownerid, SPRetInfo retinfo)
        {
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            int retCode = -1;
            try
            {
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_AGR_CSTINFO.P_AGR_BUYER_MODIFY";
                spCmd.CommandTimeout = 1800;
                //向存储过程中添加输入输出
                OracleParameter[] parameters ={
                    new OracleParameter("IN_AGREEMENT_ID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_PROD_ID",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_PROD_CODE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_PROD_NAME",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_IMPORT",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_BUYERNAME",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_MANAGER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_MIDDLEMAN",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_AGREETYPE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_YEARNUM",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTCODE",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_CSTNAME",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_SALLER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_SALLMANAGER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_SALLLEADER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_AGREELEVEL",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_LASTVALUES",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("IN_FORECASTVALUES",OracleDbType.Double,ParameterDirection.Input),
                    new OracleParameter("IN_LASTUPSTREAM",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_TARGET",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_BEGINDATE",OracleDbType.Date,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = agreeId;
                parameters[1].Value = ProdId;
                parameters[2].Value = ProdCode;
                parameters[3].Value = ProdName;
                parameters[4].Value = import;
                parameters[5].Value = buyer;
                parameters[6].Value = manager;
                parameters[7].Value = MiddleMan;
                parameters[8].Value = agreeType;

                parameters[9].Value = Int64.Parse(YearNum);
                parameters[10].Value = Int64.Parse(cstId);
                parameters[11].Value = cstCode;
                parameters[12].Value = cstName;
                parameters[13].Value = saller;
                parameters[14].Value = sallManager;
                parameters[15].Value = sallLeader;
                parameters[16].Value = agreeLevel;
                parameters[17].Value = Double.Parse(lastValues);
                parameters[18].Value = Double.Parse(foreCastValues);
                parameters[19].Value = lastUpStream;
                parameters[20].Value = tarGet;
                parameters[21].Value = DateTime.ParseExact(beginDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                parameters[22].Value = SessionDto.Empid;
                parameters[23].Value = compid;
                parameters[24].Value = ownerid;

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[25].Size = 8;
                parameters[26].Size = 2048;
                foreach (OracleParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[25].Value.ToString();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[26].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[26].Value.ToString().Trim();
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

        //删除 协议维护
        public int DelClient(SortableBindingList<DelTemp> List, SPRetInfo retinfo)
        {
            string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            string insertSql = "insert into AGREEMENT_CLIENT_BATCHTEMP(BATCHID,AGREEMENT_ID)values(:Batchid,:agreementId)";//插入中间表

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
                selCmd.CommandTimeout = 1800;

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
                insertCmd.CommandTimeout = 1800;
                insertCmd.Transaction = trans;
                int recCount = 0;

                foreach (DelTemp selected in List)
                {
                    recCount++;

                    insertCmd.Parameters.Clear();

                    insertCmd.Parameters.Add("Batchid", int.Parse(sqlID));
                    insertCmd.Parameters.Add("agreementId", selected.RelateId);

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
                spCmd.CommandText = "PG_AGR_CSTINFO.P_AGR_BUYER_DEL";
                spCmd.CommandTimeout = 1800;

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

        //批量修改  协议维护
        public int BatchUpClient(SortableBindingList<AgreeClient> BatchList, string ProdCode, string ProdName,
         string import, string buyer, string manager, string MiddleMan,
         string agreeType, string beginDate, SPRetInfo retinfo)
        {
            string selectSql = "select F_GET_SEQ('BATCHID') as SEQ from dual";//获取批次号
            string insertSql = "insert into AGREEMENT_CLIENT_BATCHTEMP(BATCHID,AGREEMENT_ID)values(:Batchid,:agreementId)";

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
                selCmd.CommandTimeout = 1800;

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
                insertCmd.CommandTimeout = 1800;
                insertCmd.Transaction = trans;
                int recCount = 0;

                foreach (AgreeClient selected in BatchList)
                {
                    recCount++;

                    insertCmd.Parameters.Clear();

                    insertCmd.Parameters.Add("Batchid", int.Parse(sqlID));
                    insertCmd.Parameters.Add("agreementId", int.Parse(selected.AgreementId));

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
                spCmd.CommandText = "PG_AGR_CSTINFO.P_AGR_BUYER_BATCHMODIFY";
                spCmd.CommandTimeout = 1800;
                //向存储过程中添加输入输出
                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_PROD_CODE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_PROD_NAME",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_IMPORT",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_BUYERNAME",OracleDbType.Varchar2 ,ParameterDirection.Input),
                    new OracleParameter("IN_MANAGER",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_MIDDLEMAN",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_AGREETYPE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_BEGINDATE",OracleDbType.Date,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
                parameters[0].Value = sqlID;
                parameters[1].Value = ProdCode;
                parameters[2].Value = ProdName;
                parameters[3].Value = import;
                parameters[4].Value = buyer;
                parameters[5].Value = manager;
                parameters[6].Value = MiddleMan;
                parameters[7].Value = agreeType;
                parameters[8].Value = DateTime.ParseExact(beginDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                parameters[9].Value = Int64.Parse(SessionDto.Empid);

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
                if (selCmd != null)
                    selCmd.Dispose();
                if (insertCmd != null)
                    insertCmd.Dispose();
                if (spCmd != null)
                    spCmd.Dispose();
            }
            return retCode;
        }

        //excel导入 协议维护
        public SortableBindingList<ClientXlsInfo> GetClientData(SortableBindingList<ClientXlsInfo> ClientList, SPRetInfo retinfo) {
            SortableBindingList<ClientXlsInfo> infoList = new SortableBindingList<ClientXlsInfo>();

            string selectSql = "SELECT F_GET_SEQ('BATCHID') as SEQ FROM dual";
            string insertSql = "INSERT INTO AGREEMENT_CLIENT_INFO_XLSTEMP(batchid,excel_seqid,compid,ownerid,saledeptid,empid,"
                + "yearnum,prod_id,agreelevelname,cstcode,cstname,lastupstreamname,forecastvalues,targetname,createtime,"
                + "lastvalues)VALUES(:Batchid,:Excel_seqid,:Compid,:Ownerid,:Saledeptid,:Empid,:YearNum,:ProdId,"
                + ":AgreeLevel,:CstCode,:CstName,:LastUpStream,:ForeCastValues,:TarGet,:CreateTime,:LastValues)";

            string selSql = "SELECT * FROM AGREEMENT_CLIENT_INFO_XLSTEMP  WHERE batchid =:batchid";

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
                selCmd.CommandTimeout = 1800;

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
                insetCmd.CommandTimeout = 1800;

                insetCmd.Transaction = trans;
                int recCount = 0;
                foreach (ClientXlsInfo selected in ClientList)
                {
                    recCount++;

                    insetCmd.Parameters.Clear();

                    insetCmd.Parameters.Add("Batchid", Int32.Parse(seqID));
                    insetCmd.Parameters.Add("Excel_seqid", Int32.Parse(selected.ExcelSeqid));
                    insetCmd.Parameters.Add("Compid", Int32.Parse(selected.Compid));
                    insetCmd.Parameters.Add("Ownerid", Int32.Parse(selected.Ownerid));
                    insetCmd.Parameters.Add("Saledeptid", Int64.Parse(selected.SaleDeptid));
                    insetCmd.Parameters.Add("Empid", Int64.Parse(selected.Empid));

                    if (selected.YearNum != "")
                    {
                        insetCmd.Parameters.Add("YearNum", Int64.Parse(selected.YearNum));
                    }
                    else
                    {
                        insetCmd.Parameters.Add("YearNum", Int64.Parse(""));
                    }
                    if (selected.ProdId != "")
                    {
                        insetCmd.Parameters.Add("ProdCode", selected.ProdId);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("ProdCode", "");
                    }
                    if (selected.AgreeLevel != "")
                    {
                        insetCmd.Parameters.Add("AgreeLevel", selected.AgreeLevel);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("AgreeLevel", "");
                    }
                    if (selected.CstCode != "")
                    {
                        insetCmd.Parameters.Add("CstCode", selected.CstCode);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("CstCode", "");
                    }
                    if (selected.CstName != "")
                    {
                        insetCmd.Parameters.Add("CstName", selected.CstName);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("CstName", "");
                    }
                    if (selected.LastUpStream != "")
                    {
                        insetCmd.Parameters.Add("LastUpStream", selected.LastUpStream);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("LastUpStream", "");
                    }
                    if (selected.ForeCastValues != "")
                    {
                        insetCmd.Parameters.Add("ForeCastValues", selected.ForeCastValues);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("ForeCastValues", "");
                    }
                    if (selected.TarGet != "")
                    {
                        insetCmd.Parameters.Add("TarGet", selected.TarGet);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("TarGet", "");
                    }

                    insetCmd.Parameters.Add("CreateTime", System.DateTime.Now);

                    if (selected.LastValues != "")
                    {
                        insetCmd.Parameters.Add("LastValues", selected.LastValues);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("LastValues", "");
                    }

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
                seCmd.CommandTimeout = 1800;

                OracleDataReader ress = seCmd.ExecuteReader();
                foreach (ClientXlsInfo info in ClientList)
                {
                    if (ress.Read())
                        info.Batchid = ress["batchid"].ToString().Trim();
                    info.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    info.Compid = ress["compid"].ToString().Trim();
                    info.Ownerid = ress["ownerid"].ToString().Trim();
                    info.SaleDeptid = ress["saledeptid"].ToString().Trim();
                    info.Empid = ress["empid"].ToString().Trim();
                    info.YearNum = ress["yearnum"].ToString().Trim();
                    info.ProdId = ress["prod_id"].ToString().Trim();
                    info.AgreeLevel = ress["agreelevelname"].ToString().Trim();
                    info.CstCode = ress["cstcode"].ToString().Trim();
                    info.CstName = ress["cstname"].ToString().Trim();
                    info.LastUpStream = ress["lastupstreamname"].ToString().Trim();
                    info.ForeCastValues = ress["forecastvalues"].ToString().Trim();
                    info.TarGet = ress["targetname"].ToString().Trim();
                    info.LastValues = ress["lastvalues"].ToString().Trim();
                    info.CreateTime = ress["createtime"].ToString().Trim();
                    info.Saller = ress["saller"].ToString().Trim();
                    info.SallManager = ress["sallmanager"].ToString().Trim();
                    info.SallLeader = ress["sallleader"].ToString().Trim();
                    infoList.Add(info);
                }

                ress.Close();
                ress.Dispose();

                seCmd.Dispose();
                seCmd = null;


            } catch (Exception ex) {

                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();

            } finally {

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

        //检查导入客户信息 协议维护
        public SortableBindingList<ClientXlsInfo> chackClientInfo(SortableBindingList<ClientXlsInfo> list, string batchid) {
            SortableBindingList<ClientXlsInfo> infolist = new SortableBindingList<ClientXlsInfo>();
            string selSql = "SELECT * FROM AGREEMENT_CLIENT_INFO_XLSTEMP  WHERE batchid =:batchid";
            OracleCommand spCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            try {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_AGR_CSTINFO.P_AGR_INFO_XLSCHECK";
                spCmd.CommandTimeout = 1800;

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
                seCmd.CommandTimeout = 1800;

                OracleDataReader ress = seCmd.ExecuteReader();
                foreach (ClientXlsInfo info in list)
                {
                    if (ress.Read())
                        info.Batchid = ress["batchid"].ToString().Trim();
                    info.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    info.Compid = ress["compid"].ToString().Trim();
                    info.Ownerid = ress["ownerid"].ToString().Trim();
                    info.SaleDeptid = ress["saledeptid"].ToString().Trim();
                    info.Empid = ress["empid"].ToString().Trim();
                    info.YearNum = ress["yearnum"].ToString().Trim();
                    info.ProdId = ress["prod_id"].ToString().Trim();
                    info.AgreeLevel = ress["agreelevelname"].ToString().Trim();
                    info.CstCode = ress["cstcode"].ToString().Trim();
                    info.CstName = ress["cstname"].ToString().Trim();
                    info.LastUpStream = ress["lastupstreamname"].ToString().Trim();
                    info.ForeCastValues = ress["forecastvalues"].ToString().Trim();
                    info.TarGet = ress["targetname"].ToString().Trim();
                    info.LastValues = ress["lastvalues"].ToString().Trim();
                    info.CreateTime = ress["createtime"].ToString().Trim();
                    info.Saller = ress["saller"].ToString().Trim();
                    info.SallManager = ress["sallmanager"].ToString().Trim();
                    info.SallLeader = ress["sallleader"].ToString().Trim();
                    info.CheckState = ress["checkstate"].ToString().Trim();
                    info.CheckMsg = ress["checkmsg"].ToString().Trim();
                    infolist.Add(info);
                }

                ress.Close();
                ress.Dispose();

                seCmd.Dispose();
                seCmd = null;

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            } finally {
                if (spCmd != null) {
                    spCmd.Dispose();
                }
                if (seCmd != null)
                    seCmd.Dispose();
            }

            return infolist;
        }

        //导入客户信息 协议维护
        public SortableBindingList<ClientXlsInfo> insertClientInfo(SortableBindingList<ClientXlsInfo> list, string batchid)
        {
            SortableBindingList<ClientXlsInfo> infolist = new SortableBindingList<ClientXlsInfo>();
            string selSql = "SELECT * FROM AGREEMENT_CLIENT_INFO_XLSTEMP  WHERE batchid =:batchid";
            OracleCommand spCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_AGR_CSTINFO.P_AGR_INFO_XLSIMPORT";
                spCmd.CommandTimeout = 1800;

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
                seCmd.CommandTimeout = 1800;

                OracleDataReader ress = seCmd.ExecuteReader();
                foreach (ClientXlsInfo info in list)
                {
                    if (ress.Read())
                        info.Batchid = ress["batchid"].ToString().Trim();
                    info.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    info.Compid = ress["compid"].ToString().Trim();
                    info.Ownerid = ress["ownerid"].ToString().Trim();
                    info.SaleDeptid = ress["saledeptid"].ToString().Trim();
                    info.Empid = ress["empid"].ToString().Trim();
                    info.YearNum = ress["yearnum"].ToString().Trim();
                    info.ProdId = ress["prod_id"].ToString().Trim();
                    info.AgreeLevel = ress["agreelevelname"].ToString().Trim();
                    info.CstCode = ress["cstcode"].ToString().Trim();
                    info.CstName = ress["cstname"].ToString().Trim();
                    info.LastUpStream = ress["lastupstreamname"].ToString().Trim();
                    info.ForeCastValues = ress["forecastvalues"].ToString().Trim();
                    info.TarGet = ress["targetname"].ToString().Trim();
                    info.LastValues = ress["lastvalues"].ToString().Trim();
                    info.CreateTime = ress["createtime"].ToString().Trim();
                    info.Saller = ress["saller"].ToString().Trim();
                    info.SallManager = ress["sallmanager"].ToString().Trim();
                    info.SallLeader = ress["sallleader"].ToString().Trim();
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

        //导出信息  协议维护
        public SortableBindingList<ClientXlsInfo> ImportCheckInfo(SortableBindingList<ClientXlsInfo> list, string batchid)
        {
            SortableBindingList<ClientXlsInfo> infolist = new SortableBindingList<ClientXlsInfo>();
            string selSql = "SELECT * FROM AGREEMENT_CLIENT_INFO_XLSTEMP  WHERE batchid =:batchid";
            OracleCommand spCmd = null;
            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            try
            {
                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_AGR_CSTINFO.P_AGR_INFO_XLSIMPORT";
                spCmd.CommandTimeout = 1800;

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
                seCmd.CommandTimeout = 1800;

                OracleDataReader ress = seCmd.ExecuteReader();
                foreach (ClientXlsInfo info in list)
                {
                    if (ress.Read())
                        info.Batchid = ress["batchid"].ToString().Trim();
                    info.ExcelSeqid = ress["excel_seqid"].ToString().Trim();
                    info.Compid = ress["compid"].ToString().Trim();
                    info.Ownerid = ress["ownerid"].ToString().Trim();
                    info.SaleDeptid = ress["saledeptid"].ToString().Trim();
                    info.Empid = ress["empid"].ToString().Trim();
                    info.YearNum = ress["yearnum"].ToString().Trim();
                    info.ProdId = ress["prod_id"].ToString().Trim();
                    info.AgreeLevel = ress["agreelevelname"].ToString().Trim();
                    info.CstCode = ress["cstcode"].ToString().Trim();
                    info.CstName = ress["cstname"].ToString().Trim();
                    info.LastUpStream = ress["lastupstreamname"].ToString().Trim();
                    info.ForeCastValues = ress["forecastvalues"].ToString().Trim();
                    info.TarGet = ress["targetname"].ToString().Trim();
                    info.LastValues = ress["lastvalues"].ToString().Trim();
                    info.CreateTime = ress["createtime"].ToString().Trim();
                    info.Saller = ress["saller"].ToString().Trim();
                    info.SallManager = ress["sallmanager"].ToString().Trim();
                    info.SallLeader = ress["sallleader"].ToString().Trim();
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

        //客户名称匹配
        public int DealWithMatch(SortableBindingList<CstNameMatch> List, SPRetInfo retinfo) {
            string selectSql = "SELECT F_GET_SEQ('BATCHID') as SEQ FROM dual";
            string insertSql = "INSERT INTO AGREEMENT_CLIENT_XLSTEMP(batchid,excel_seqid,"
                + "compid,ownerid,saledeptid,empid,"
                + "cstname)VALUES(:Batchid,:Excel_seqid,:Compid,:Ownerid,:Saledeptid,:Empid,:CstName)";

            OracleCommand selCmd = null;
            OracleCommand insetCmd = null;
            OracleCommand spCmd = null;
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
                selCmd.CommandTimeout = 1800;

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
                insetCmd.CommandTimeout = 1800;

                insetCmd.Transaction = trans;
                int recCount = 0;
                foreach (CstNameMatch selected in List)
                {
                    recCount++;

                    insetCmd.Parameters.Clear();

                    insetCmd.Parameters.Add("Batchid", Int32.Parse(seqID));
                    insetCmd.Parameters.Add("Excel_seqid", Int32.Parse(selected.ExcelSeqid));
                    insetCmd.Parameters.Add("Compid", Int32.Parse(selected.Compid));
                    insetCmd.Parameters.Add("Ownerid", Int32.Parse(selected.Ownerid));
                    insetCmd.Parameters.Add("Saledeptid", Int64.Parse(selected.SaleDeptid));
                    insetCmd.Parameters.Add("Empid", Int64.Parse(selected.Empid));

                    if (selected.CstName != "")
                    {
                        insetCmd.Parameters.Add("CstName", selected.CstName);
                    }
                    else
                    {
                        insetCmd.Parameters.Add("CstName", "");
                    }

                    //MessageBox.Show("=="+ insetCmd.Parameters[0].Value);
                    insetCmd.ExecuteNonQuery();
                }

                trans.Commit();
                trans.Dispose();
                trans = null;

                insetCmd.Dispose();
                insetCmd = null;

                //调用存储过程
                spCmd = Conn_datacenter_cmszh.CreateCommand();
                spCmd.Connection = Conn_datacenter_cmszh;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "PG_AGR_CSTINFO.P_AGR_CLIENT_XLS";
                spCmd.CommandTimeout = 1800;

                OracleParameter[] parameters ={
                    new OracleParameter("IN_BATCHID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };

                //将前台数据一一对应，传进存储过程
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
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally {
                if (selCmd != null) {
                    selCmd.Dispose();
                }
                if (insetCmd != null)
                {
                    insetCmd.Dispose();
                }
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }
            }
            return retCode;
        }
        //获取客户名称匹配信息
        public SortableBindingList<CstNameMatch> GetCstNameList(string batchid) {

            SortableBindingList<CstNameMatch> infolist = new SortableBindingList<CstNameMatch>();
            string selSql = "select * from AGREEMENT_CLIENT_XLSTEMP where batchid=:batchid";

            OracleCommand seCmd = null;
            OracleTransaction trans = null;
            try {

                seCmd = Conn_datacenter_cmszh.CreateCommand();
                seCmd.Connection = Conn_datacenter_cmszh;
                seCmd.CommandType = System.Data.CommandType.Text;
                seCmd.CommandText = selSql.ToString();
                seCmd.Parameters.Add("batchid", batchid);
                seCmd.CommandTimeout = 1800;

                OracleDataReader res = seCmd.ExecuteReader();
                while (res.Read())
                {
                    CstNameMatch info = new CstNameMatch();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.ExcelSeqid = res["excel_seqid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Empid = res["empid"].ToString().Trim();
                    info.SaleDeptid = res["saledeptid"].ToString().Trim();
                    info.CstName = res["cstname"].ToString().Trim();
                    info.CstCode = res["cstcode"].ToString().Trim();
                    info.CheckMsg = res["checkmsg"].ToString().Trim();

                    infolist.Add(info);
                }
                res.Close();
                res = null;

                seCmd.Dispose();
                seCmd = null;


            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();

            } finally {
                if (seCmd != null)
                {
                    seCmd.Dispose();
                }
            }
            return infolist;
        }

        #endregion
        #region 客服存档模块
        // 查询 客服存档
        public SortableBindingList<AgreeClient> SelCustomerInfo(Dictionary<string, string> sqlkeydict)
        {
            SortableBindingList<AgreeClient> infolist = new SortableBindingList<AgreeClient>();
            string sql = "select * from V_AGREEMENT_CLIENT_INFO where compid=:compid and ownerid=:ownerid $ ";

            OracleCommand cmd = null;
            OracleTransaction trans = null;

            try
            {
                cmd = Conn_datacenter_cmszh.CreateCommand();
                cmd.Connection = Conn_datacenter_cmszh;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.Parameters.Add("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);
                cmd.CommandTimeout = 1800;

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
                    AgreeClient infos = new AgreeClient();
                    infos.AgreementId = res["AGREEMENT_ID"].ToString().Trim();
                    infos.ProdId = res["PROD_ID"].ToString().Trim();
                    //infos.ProdCode = res["PROD_CODE"].ToString().Trim();
                    infos.ProdName = res["PROD_NAME"].ToString().Trim();
                    infos.Import = res["IMPORTNAME"].ToString().Trim();
                    infos.BuyerName = res["BUYERNAMENAME"].ToString().Trim();
                    infos.Manager = res["MANAGERNAME"].ToString().Trim();
                    infos.MiddleMan = res["MIDDLEMAN"].ToString().Trim();
                    infos.AgreeType = res["AGREETYPENAME"].ToString().Trim();
                    infos.YearNum = res["YEARNUM"].ToString().Trim();
                    infos.CstId = res["CSTID"].ToString().Trim();
                    infos.CstCode = res["CSTCODE"].ToString().Trim();
                    infos.CstName = res["CSTNAME"].ToString().Trim();
                    infos.Saller = res["SALLER"].ToString().Trim();
                    infos.SallManager = res["SALLMANAGER"].ToString().Trim();
                    infos.SallLeader = res["SALLLEADER"].ToString().Trim();
                    //infos.AgreeLevel = res["AGREELEVELNAME"].ToString().Trim();
                    //infos.LastValues = res["LASTVALUES"].ToString().Trim();
                    //infos.ForecastValues = res["FORECASTVALUES"].ToString().Trim();
                    //infos.LastUpStream = res["LASTUPSTREAMNAME"].ToString().Trim();
                    //infos.TarGet = res["TARGETNAME"].ToString().Trim();
                    //infos.CstIntention = res["CSTINTENTIONNAME"].ToString().Trim();
                    //infos.CstIntentionTime = res["CSTINTENTIONTIME"].ToString().Trim();
                    //infos.ProdIntention = res["PRODINTENTIONNAME"].ToString().Trim();
                    //infos.Dynamics = res["DYNAMICSNAME"].ToString().Trim();
                    //infos.FinalChannel = res["FINALCHANNELNAME"].ToString().Trim();
                    //infos.ThisYearValues = res["THISYEARVALUES"].ToString().Trim();
                    //infos.HopeValues = res["HOPEVALUES"].ToString().Trim();
                    infos.Seal = res["SEALNAME"].ToString().Trim();
                    infos.Onfile = res["ONFILENAME"].ToString().Trim();
                    infos.FinalValues = res["FINALVALUES"].ToString().Trim();
                    //infos.BeginDate = res["BEGINDATE"].ToString().Trim();
                    infolist.Add(infos);
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

            return infolist;
        }
        #endregion
        // 单个字段修改 
        public int UpdateColValues(string colName, string colValue, string agreementId, int roleType, SPRetInfo retinfo)
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
                spCmd.CommandText = "PG_AGR_CSTINFO.P_AGR_COLUPDATE";
                spCmd.CommandTimeout = 1800;
                OracleParameter[] parameters ={

                    new OracleParameter("IN_COLNAME",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_COLVALUE",OracleDbType.Varchar2,ParameterDirection.Input),
                    new OracleParameter("IN_AGREEMENT_ID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_EMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_ROLETYPE",OracleDbType.Int64,ParameterDirection.Input),


                    new OracleParameter("OUT_CODE",OracleDbType.Int64,ParameterDirection.Output),
                    new OracleParameter("OUT_MSG",OracleDbType.Varchar2,ParameterDirection.Output)

                };
                parameters[0].Value = colName;
                parameters[1].Value = colValue;
                parameters[2].Value = Int64.Parse(agreementId);
                parameters[3].Value = SessionDto.Empid;
                parameters[4].Value = roleType;
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

        #region  协议报表模块
        public SortableBindingList<AgreeReport> GetAgreeReport(Dictionary<string, string> sqlkeydict, Dictionary<string, string> dimension) {
            SortableBindingList<AgreeReport> infoList = new SortableBindingList<AgreeReport>();
            //权限控制
            string selSql = "";
            if (SessionDto.Emproleid == "99" || SessionDto.Emproleid == "115"|| SessionDto.Emproleid == "120" || SessionDto.Emproleid == "121") {
                selSql = "select #COUNT(CASE I.TARGET WHEN '01' THEN 1 END) BIBAO,COUNT(CASE I.TARGET WHEN '02' THEN 1 END) ZHENGQU,COUNT(CASE I.TARGET WHEN '03' THEN 1 END) QITA,COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END) BBZQ,SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END) XSFK,COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END) YXHX,COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END) BBYXHX,COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END) QDHX,F_LPAD_ZERO(ROUND(SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' FKL,F_LPAD_ZERO(ROUND(COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' YXL,F_LPAD_ZERO(ROUND(COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 END)+0.00001),4)*100)||'%' BBQDL,F_LPAD_ZERO(ROUND(COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' QDL,COUNT(CASE WHEN I.TARGET = '02' AND I.CSTINTENTION = '01'  THEN 1 END) ZQHX,F_LPAD_ZERO(ROUND(COUNT(CASE WHEN I.TARGET = '02' AND I.CSTINTENTION = '01'  THEN 1 END) /(COUNT(CASE I.TARGET WHEN '02' THEN 1 END) + 0.00001), 4) * 100) || '%' ZQQDL " +
                "from V_AGREEMENT_CLIENT_INFO I where 1=1 and compid=:compid and ownerid=:ownerid $ " +
                "group by @";
            }
            if (SessionDto.Emproleid == "109" || SessionDto.Emproleid == "114") {
                selSql = "select #COUNT(CASE I.TARGET WHEN '01' THEN 1 END) BIBAO,COUNT(CASE I.TARGET WHEN '02' THEN 1 END) ZHENGQU,COUNT(CASE I.TARGET WHEN '03' THEN 1 END) QITA,COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END) BBZQ,SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END) XSFK,COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END) YXHX,COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END) BBYXHX,COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END) QDHX,F_LPAD_ZERO(ROUND(SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' FKL,F_LPAD_ZERO(ROUND(COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' YXL,F_LPAD_ZERO(ROUND(COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 END)+0.00001),4)*100)||'%' BBQDL,F_LPAD_ZERO(ROUND(COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' QDL,COUNT(CASE WHEN I.TARGET = '02' AND I.CSTINTENTION = '01'  THEN 1 END) ZQHX,F_LPAD_ZERO(ROUND(COUNT(CASE WHEN I.TARGET = '02' AND I.CSTINTENTION = '01'  THEN 1 END) /(COUNT(CASE I.TARGET WHEN '02' THEN 1 END) + 0.00001), 4) * 100) || '%' ZQQDL " +
               "from V_AGREEMENT_CLIENT_INFO I where 1=1 and (managername=:managername or managername is null) and compid=:compid and ownerid=:ownerid $ " +
               "group by @";

            }
            if (SessionDto.Emproleid == "108" || SessionDto.Emproleid == "113")
            {
                selSql = "select #COUNT(CASE I.TARGET WHEN '01' THEN 1 END) BIBAO,COUNT(CASE I.TARGET WHEN '02' THEN 1 END) ZHENGQU,COUNT(CASE I.TARGET WHEN '03' THEN 1 END) QITA,COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END) BBZQ,SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END) XSFK,COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END) YXHX,COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END) BBYXHX,COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END) QDHX,F_LPAD_ZERO(ROUND(SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' FKL,F_LPAD_ZERO(ROUND(COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' YXL,F_LPAD_ZERO(ROUND(COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 END)+0.00001),4)*100)||'%' BBQDL,F_LPAD_ZERO(ROUND(COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' QDL,COUNT(CASE WHEN I.TARGET = '02' AND I.CSTINTENTION = '01'  THEN 1 END) ZQHX,F_LPAD_ZERO(ROUND(COUNT(CASE WHEN I.TARGET = '02' AND I.CSTINTENTION = '01'  THEN 1 END) /(COUNT(CASE I.TARGET WHEN '02' THEN 1 END) + 0.00001), 4) * 100 || '%' ZQQDL " +
               "from V_AGREEMENT_CLIENT_INFO I where 1=1 and (buyernamename=:buyername or buyernamename is null) and compid=:compid and ownerid=:ownerid $ " +
               "group by @";

            }
            if (SessionDto.Emproleid == "119")
            {
                selSql = "select #COUNT(CASE I.TARGET WHEN '01' THEN 1 END) BIBAO,COUNT(CASE I.TARGET WHEN '02' THEN 1 END) ZHENGQU,COUNT(CASE I.TARGET WHEN '03' THEN 1 END) QITA,COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END) BBZQ,SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END) XSFK,COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END) YXHX,COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END) BBYXHX,COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END) QDHX,F_LPAD_ZERO(ROUND(SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' FKL,F_LPAD_ZERO(ROUND(COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' YXL,F_LPAD_ZERO(ROUND(COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 END)+0.00001),4)*100)||'%' BBQDL,F_LPAD_ZERO(ROUND(COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' QDL,COUNT(CASE WHEN I.TARGET = '02' AND I.CSTINTENTION = '01'  THEN 1 END) ZQHX,F_LPAD_ZERO(ROUND(COUNT(CASE WHEN I.TARGET = '02' AND I.CSTINTENTION = '01'  THEN 1 END) /(COUNT(CASE I.TARGET WHEN '02' THEN 1 END) + 0.00001), 4) * 100) || '%' ZQQDL " +
               "from V_AGREEMENT_CLIENT_INFO I where 1=1 and (sallleader=:sallleader or sallleader is null) and compid=:compid and ownerid=:ownerid $ " +
               "group by @";

            }
            if (SessionDto.Emproleid == "118")
            {
                selSql = "select #COUNT(CASE I.TARGET WHEN '01' THEN 1 END) BIBAO,COUNT(CASE I.TARGET WHEN '02' THEN 1 END) ZHENGQU,COUNT(CASE I.TARGET WHEN '03' THEN 1 END) QITA,COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END) BBZQ,SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END) XSFK,COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END) YXHX,COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END) BBYXHX,COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END) QDHX,F_LPAD_ZERO(ROUND(SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' FKL,F_LPAD_ZERO(ROUND(COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' YXL,F_LPAD_ZERO(ROUND(COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 END)+0.00001),4)*100)||'%' BBQDL,F_LPAD_ZERO(ROUND(COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' QDL,COUNT(CASE WHEN I.TARGET = '02' AND I.CSTINTENTION = '01'  THEN 1 END) ZQHX,F_LPAD_ZERO(ROUND(COUNT(CASE WHEN I.TARGET = '02' AND I.CSTINTENTION = '01'  THEN 1 END) /(COUNT(CASE I.TARGET WHEN '02' THEN 1 END) + 0.00001), 4) * 100 )|| '%' ZQQDL " +
               "from V_AGREEMENT_CLIENT_INFO I where 1=1 and (sallmanager=:sallmanager or sallmanager is null) and compid=:compid and ownerid=:ownerid $ " +
               "group by @";

            }
            if (SessionDto.Emproleid == "117" || SessionDto.Emproleid == "104")
            {
                selSql = "select #COUNT(CASE I.TARGET WHEN '01' THEN 1 END) BIBAO,COUNT(CASE I.TARGET WHEN '02' THEN 1 END) ZHENGQU,COUNT(CASE I.TARGET WHEN '03' THEN 1 END) QITA,COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END) BBZQ,SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END) XSFK,COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END) YXHX,COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END) BBYXHX,COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END) QDHX,F_LPAD_ZERO(ROUND(SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' FKL,F_LPAD_ZERO(ROUND(COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' YXL,F_LPAD_ZERO(ROUND(COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 END)+0.00001),4)*100)||'%' BBQDL,F_LPAD_ZERO(ROUND(COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100)||'%' QDL,COUNT(CASE WHEN I.TARGET = '02' AND I.CSTINTENTION = '01'  THEN 1 END) ZQHX,F_LPAD_ZERO(ROUND(COUNT(CASE WHEN I.TARGET = '02' AND I.CSTINTENTION = '01'  THEN 1 END) /(COUNT(CASE I.TARGET WHEN '02' THEN 1 END) + 0.00001), 4) * 100 )|| '%' ZQQDL " +
               "from V_AGREEMENT_CLIENT_INFO I where 1=1 and saller=:saller or saller is null and compid=:compid and ownerid=:ownerid $ " +
               "group by @";

            }
            //string selSql = "select #COUNT(CASE I.TARGET WHEN '01' THEN 1 END) BIBAO,COUNT(CASE I.TARGET WHEN '02' THEN 1 END) ZHENGQU,COUNT(CASE I.TARGET WHEN '03' THEN 1 END) QITA,COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END) BBZQ,SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END) XSFK,COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END) YXHX,COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END) BBYXHX,COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END) QDHX,ROUND(SUM(CASE WHEN NVL(I.CSTINTENTION,'0000') = '0000' THEN 0 ELSE 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100||'%' FKL,ROUND(COUNT(CASE I.CSTINTENTION WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100||'%' YXL,ROUND(COUNT(CASE  WHEN I.TARGET = '01' AND I.CSTINTENTION = '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 END)+0.00001),4)*100||'%' BBQDL,ROUND(COUNT(CASE I.FINALCHANNEL WHEN '01' THEN 1 END)/(COUNT(CASE I.TARGET WHEN '01' THEN 1 WHEN '02' THEN 1 END)+0.00001),4)*100||'%' QDL " +
            //    "from V_AGREEMENT_CLIENT_INFO I where 1=1$ " +
            //    "group by @";
            OracleCommand selCmd = null;
            OracleTransaction trans = null;
            try {
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selSql.ToString();
                if (SessionDto.Emproleid == "108" || SessionDto.Emproleid == "113")
                {
                    selCmd.Parameters.Add("buyernamename", SessionDto.Empname);
                }
                if (SessionDto.Emproleid == "109" || SessionDto.Emproleid == "114")
                {
                    selCmd.Parameters.Add("managername", SessionDto.Empname);
                }
                if (SessionDto.Emproleid == "119")
                {
                    selCmd.Parameters.Add("sallleader", SessionDto.Empname);
                }
                if (SessionDto.Emproleid == "118" || SessionDto.Emproleid == "123")
                {
                    selCmd.Parameters.Add("sallmanager", SessionDto.Empname);
                }
                if (SessionDto.Emproleid == "117" || SessionDto.Emproleid == "104")
                {
                    selCmd.Parameters.Add("saller", SessionDto.Empname);
                }
                selCmd.Parameters.Add("compid", Properties.Settings.Default.COMPID);
                selCmd.Parameters.Add("ownerid", Properties.Settings.Default.OWNERID);
                selCmd.CommandTimeout = 1800;

                string whereCondi = "";
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in dimension)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereCondi = whereCondi + " ";
                    }
                    else
                    {
                        whereCondi = whereCondi + kv.Value + ",";                    
                    }

                }
                selSql = selSql.Replace("#", whereCondi);
                
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
                selSql = selSql.Replace("$", whereStr);
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            selCmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            selCmd.Parameters.Add(kv.Key, kv.Value);
                        }
                    }
                }
                //拼接分组
                string whereGruop = "";
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in dimension)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereGruop = whereGruop + " ";
                    }
                    else
                    {
                        whereGruop = whereGruop + kv.Value + ",";
                    }

                }
                selSql = selSql.Replace("@", whereGruop);
                selSql = selSql.Substring(0, selSql.Length - 1);

                string s = selSql;
                selCmd.CommandText = selSql.ToString();
                OracleDataReader res = selCmd.ExecuteReader();
                while (res.Read()) {
                    AgreeReport infos = new AgreeReport();
                    //遍历Dictionary中的value值
                    foreach (KeyValuePair<string, string> kv in dimension)
                    {
                        if (StringUtils.IsNotNull(kv.Key)) {
                            if (kv.Key == "yearnum")
                            {
                                infos.YearNum = res["YEARNUM"].ToString().Trim();
                            }
                            if (kv.Key == "prod_name")
                            {
                                infos.ProdName = res["PROD_NAME"].ToString().Trim();
                            }
                            if (kv.Key == "sallmanager")
                            {
                                infos.SallManager = res["SALLMANAGER"].ToString().Trim();
                            }
                            if (kv.Key == "saller")
                            {
                                infos.Saller = res["SALLER"].ToString().Trim();
                            }
                            if (kv.Key == "middle")
                            {
                                infos.MiddleMan = res["MIDDLEMAN"].ToString().Trim();
                            }
                            if (kv.Key == "sallleader")
                            {
                                infos.SallLeader = res["SALLLEADER"].ToString().Trim();
                            }
                            if (kv.Key == "buyername")
                            {
                                infos.BuyerName = res["PIBUYERNAMENAME"].ToString().Trim();
                            }
                            if (kv.Key == "agreetype")
                            {
                                infos.AgreeType = res["AGREETYPENAME"].ToString().Trim();
                            }
                            if (kv.Key == "begindate")
                            {
                                infos.BeginDate = res["BEGINDATE"].ToString().Trim();
                            }
                        }
                    }
                    infos.BiBao = res["BIBAO"].ToString().Trim();
                    infos.ZhengQu = res["ZHENGQU"].ToString().Trim();
                    infos.QiTa  = res["QITA"].ToString().Trim();
                    infos.BbZq  = res["BBZQ"].ToString().Trim();
                    infos.Xsfk  = res["XSFK"].ToString().Trim();
                    infos.YxHx  = res["YXHX"].ToString().Trim();
                    infos.BbYxHx  = res["BBYXHX"].ToString().Trim();
                    infos.QdHx  = res["QDHX"].ToString().Trim();
                    infos.Fkl  = res["FKL"].ToString().Trim();
                    infos.Yxl  = res["YXL"].ToString().Trim();
                    infos.BbQdl  = res["BBQDL"].ToString().Trim();
                    infos.Qdl  = res["QDL"].ToString().Trim();
                    infos.ZqHx = res["ZQHX"].ToString().Trim();
                    infos.ZqQdl = res["ZQQDL"].ToString().Trim();
                    infoList.Add(infos);
                }

            } catch (Exception ex) {

                MessageBox.Show(ex.ToString());
                if (trans!=null) {
                    trans.Rollback();
                }
            } finally {
                if (selCmd!=null) {
                    selCmd.Dispose();
                }
            }
            return infoList;
        }

        #endregion
        //获取员工信息
        public SortableBindingList<EmpInfos> GetEmpInfo(Dictionary<string,string>sqlkeydict) {

            SortableBindingList<EmpInfos> infoList = new SortableBindingList<EmpInfos>();
            string selSql = "select * from v_pub_emp where 1=1$";
            OracleCommand selCmd = null;
            OracleTransaction trans = null;
            try {
                selCmd = Conn_datacenter_cmszh.CreateCommand();
                selCmd.Connection = Conn_datacenter_cmszh;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandText = selSql.ToString();

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
                selSql = selSql.Replace("$", whereStr);

                selCmd.CommandText = selSql.ToString();
                //遍历Dictionary中的value值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            selCmd.Parameters.Add(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            selCmd.Parameters.Add(kv.Key, kv.Value);
                        }
                    }
                }
                string s = selSql;
                OracleDataReader res = selCmd.ExecuteReader();
                while (res.Read())
                {
                    EmpInfos infos = new EmpInfos();
                    infos.EmpCode = res["EMPCODE"].ToString().Trim();
                    infos.EmpName = res["EMPNAME"].ToString().Trim();
                    infos.EmpId = res["EMPID"].ToString().Trim();
                    //infos.BeginDate = res["BEGINDATE"].ToString().Trim();
                    infoList.Add(infos);
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(),"错误信息");
                if (trans!=null) {
                    trans.Rollback();
                }
            } finally {
                if (selCmd != null)
                {
                    selCmd.Dispose();
                }
            }
            return infoList;
        }


}
}
