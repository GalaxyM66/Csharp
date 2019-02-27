using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
namespace PriceManager
{
    class APDao_ClientGroup : MySQLHelper
    {
        private object dao;
        #region ClientGroup
        //查询客户组信息(18-07-02)
        public  SortableBindingList<PubConfigureInfo> GetPubConfigureInfoList()
        {

            SortableBindingList<PubConfigureInfo> infoList = new SortableBindingList<PubConfigureInfo>();
            string sql = "SELECT a.compid,a.ownerid,a.saledeptid,a.detailtypemenu,a.grouptypeprior FROM pub_configure a";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 118000;
                cmd.CommandText = sql.ToString();
                //cmd.Parameters.Add("whsecode", whse);
                //cmd.Parameters.Add("boxbillno", boxbillno);
                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    PubConfigureInfo info = new PubConfigureInfo();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Detailtypemenu = res["detailtypemenu"].ToString().Trim();
                    info.Grouptypeprior = res["grouptypeprior"].ToString().Trim();

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


        //查询定价客户组主表
        public SortableBindingList<CstGroupHdr> GetCstGroupHdrList(Dictionary<string, string> sqlkeydict)
        {

            SortableBindingList<CstGroupHdr> infoList = new SortableBindingList<CstGroupHdr>();
            string sql = "SELECT a.ownerid, a.deptname, a.code, a.name, a.detailtypename, a.grouptypename,"
                +"a.synctypename, a.attachcode, a.deliveryfeerate, a.STOPNAME, a.mark, a.detailtype, a.grouptype, "
            + "a.synctype, a.stopflag, a.createuser, a.createusername, a.createdate, a.modifyuser, a.modifyusername, a.modifydate, "
            + "a.compid, a.hdrid,a.saledeptid,a.saledeptname FROM v_sel_cst_group_hdr AS a where a.ownerid=@ownerid "
            + "and a.compid=@compid $";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 118000;
                //cmd.CommandText = sql.ToString();
                
                String whereStr = "";

                //遍历Dictionary的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + " and " + kv.Key.Replace("%", "") + "= " + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like @" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "= @" + kv.Key;
                        }
                    }
                }

                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                
                // 遍历Dictionary的Values值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.AddWithValue(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(kv.Key, kv.Value);
                        }
                    }

                }
                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    CstGroupHdr info = new CstGroupHdr();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Deptname = res["deptname"].ToString().Trim();
                    info.Code = res["code"].ToString().Trim();
                    info.Name = res["name"].ToString().Trim();
                    info.Detailtypename = res["detailtypename"].ToString().Trim();
                    info.Grouptypename = res["grouptypename"].ToString().Trim();
                    info.Synctypename = res["synctypename"].ToString().Trim();
                    info.Attachcode = res["attachcode"].ToString().Trim();
                    info.Deliveryfeerate = res["deliveryfeerate"].ToString().Trim();
                    info.STOPNAME = res["STOPNAME"].ToString().Trim();
                    info.Mark = res["mark"].ToString().Trim();
                    info.Detailtype = res["detailtype"].ToString().Trim();
                    info.Grouptype = res["grouptype"].ToString().Trim();
                    info.Synctype = res["synctype"].ToString().Trim();
                    info.Stopflag = res["stopflag"].ToString().Trim();
                    info.Createuser = res["createuser"].ToString().Trim();
                    info.Createusername = res["createusername"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Modifyusername = res["modifyusername"].ToString().Trim();
                    info.Modifydate = res["modifydate"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Hdrid = res["hdrid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Saledeptname = res["saledeptname"].ToString().Trim();
                    
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

        //查询客户组明细表信息
        public SortableBindingList<CstGroupDtl> GetCstGroupDtlList(string hdrid)
        {

            SortableBindingList<CstGroupDtl> infoList = new SortableBindingList<CstGroupDtl>();
            string sql = "SELECT cstcode, CSTNAME, region, STOPNAME, CREATENAME, MODIFYNAME, hdrid, dtlid,"
            + "compid, ownerid, cstid, modifyprctype, modprcname, modifyprcdate, modifynum, stopflag,"
            + "createuser, createdate, modifyuser, modifydate, batchid, saledeptid, saledeptname, clienttypename, "
            + "cmsclienttypename FROM v_sel_cst_group_dtl AS a where a.hdrid=@hdrid and a.ownerid=@ownerid";

            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 118000;
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("hdrid", hdrid);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);

                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    CstGroupDtl info = new CstGroupDtl();
                    info.Cstcode = res["cstcode"].ToString().Trim();
                    info.CSTNAME = res["CSTNAME"].ToString().Trim();
                    info.Region = res["region"].ToString().Trim();
                    info.STOPNAME = res["STOPNAME"].ToString().Trim();
                    info.CREATENAME = res["CREATENAME"].ToString().Trim();
                    info.MODIFYNAME = res["MODIFYNAME"].ToString().Trim();
                    info.Hdrid = res["hdrid"].ToString().Trim();
                    info.Dtlid = res["dtlid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Modifyprctype = res["modifyprctype"].ToString().Trim();
                    info.Modprcname = res["modprcname"].ToString().Trim();
                    info.Modifyprcdate = res["modifyprcdate"].ToString().Trim();
                    info.Modifynum = res["modifynum"].ToString().Trim();
                    info.Stopflag = res["stopflag"].ToString().Trim();
                    info.Createuser = res["createuser"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Modifyuser = res["modifyuser"].ToString().Trim();
                    info.Modifydate = res["modifydate"].ToString().Trim();
                    info.Batchid = res["batchid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Saledeptname = res["saledeptname"].ToString().Trim();
                    info.Clienttypename = res["clienttypename"].ToString().Trim();
                    info.Cmsclienttypename = res["clienttypename"].ToString().Trim();
                    info.UseFlag = "00";

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



        //查询客户组定价商品明细信息
        public SortableBindingList<GoodPrc> GetGoodPrcDtlList(string hdrid)
        {

            SortableBindingList<GoodPrc> infoList = new SortableBindingList<GoodPrc>();
            string sql = "SELECT saledeptid, goodtapid, compid, goodid, goods, name, spec,"
            + "producer, sprdug, hdrid, ownerid, prc, price, bottomprc, bottomprice, costprc,"
            + "costprice, grouptype, synctype, begindate, enddate, costrate "
            + "FROM v_sel_good_prc AS a where a.hdrid=@hdrid and a.ownerid=@ownerid";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 118000;
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("hdrid", hdrid);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);

                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    GoodPrc info = new GoodPrc();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Goodtapid = res["goodtapid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Name = res["name"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.Sprdug = res["sprdug"].ToString().Trim();
                    info.Hdrid = res["hdrid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Begindate = res["begindate"].ToString().Trim();
                    info.Enddate = res["enddate"].ToString().Trim();
                    info.Costrate = res["costrate"].ToString().Trim();
                    info.Grouptype = res["grouptype"].ToString().Trim();
                    info.Synctype = res["synctype"].ToString().Trim();

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

        //查询归属部门
        public SortableBindingList<PubDept> GetPubDept()
        {
            SortableBindingList<PubDept> infoList = new SortableBindingList<PubDept>();
            string sql = "SELECT a.SALEDEPTID,CONCAT(a.SALEDEPTID,'|',a.DEPTNAME) deptname FROM pub_dept AS a where "
            +"a.ownerid=@ownerid";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 118000;
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);

                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    PubDept info = new PubDept();
                    info.Saledeptid = res["SALEDEPTID"].ToString().Trim();
                    info.Deptname = res["deptname"].ToString().Trim();

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

        //查询新新增客户信息(18-05-31)
        public SortableBindingList<CstGroupDtl> GetSelClientsList(Dictionary<string, string> sqlkeydict, int Hdrid)
        {

            SortableBindingList<CstGroupDtl> infoList = new SortableBindingList<CstGroupDtl>();
            string sql = "SELECT cstid, cstcode, cstname, region, cmsclienttypename, clienttypename FROM v_sel_clients_owner "
            + "AS a where a.compid=@compid and a.ownerid=@ownerid $";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 118000;
                //cmd.CommandText = sql.ToString();

                String whereStr = "";

                //遍历Dictionary的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + " and " + kv.Key.Replace("%", "") + "= " + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like @" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "= @" + kv.Key;
                        }
                    }
                }

                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                //cmd.Parameters.AddWithValue("hdrid", hdrid);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
                // 遍历Dictionary的Values值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.AddWithValue(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(kv.Key, kv.Value);
                        }
                    }

                }

                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    CstGroupDtl info = new CstGroupDtl();

                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Cstcode = res["cstcode"].ToString().Trim();
                    info.CSTNAME = res["cstname"].ToString().Trim();
                    info.Region = res["region"].ToString().Trim();

                    info.STOPNAME = "正常";
                    info.CREATENAME = SessionDto.Empname;
                    info.MODIFYNAME = SessionDto.Empname;
                    info.Hdrid = Hdrid.ToString();
                    //info.Dtlid = res["dtlid"].ToString().Trim();
                    info.Compid = Properties.Settings.Default.COMPID;
                    info.Ownerid = Properties.Settings.Default.OWNERID;
                    info.Modifyprctype = "10";
                    info.Modprcname = "否";
                    //info.Modifyprcdate = res["modifyprcdate"].ToString().Trim();
                    //info.Modifynum = res["modifynum"].ToString().Trim();
                    info.Stopflag = "00";
                    info.Createuser = SessionDto.Empcode;
                    info.Createdate = DateTime.Now.ToString();
                    info.Modifyuser = SessionDto.Empcode;
                    info.Modifydate = DateTime.Now.ToString();
                    info.Batchid = "";


                    info.Saledeptid = PubOwnerConfigureDto.Saledeptid;
                    //info.Saledeptname = PubOwnerConfigureDto.Saledepttype;
                    info.Cmsclienttypename = res["cmsclienttypename"].ToString().Trim();
                    info.Clienttypename = res["clienttypename"].ToString().Trim();

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

        //获取客户组ID，批次号
        //查询客户组信息
        public int GetSeqNum(int i)
        {

            int n = -1;
            string sql = "";
            string sqlH = "SELECT seq_nextval('seq_clientsgroup_hdrid') as a";//调函数获取客户组ID
            string sqlB = "SELECT seq_nextval('seq_bacthid') as a";//调函数获取操作批次号ID
            if (i == 1)
            { sql = sqlH; }
            else
            { sql = sqlB; }

            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 118000;
                cmd.CommandText = sql.ToString();
                //cmd.Parameters.Add("whsecode", whse);
                //cmd.Parameters.Add("boxbillno", boxbillno);
                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    if (i == 1)
                    {
                        n = int.Parse(res["a"].ToString().Trim());
                    }
                    else
                    {
                        n = int.Parse(res["a"].ToString().Trim());
                    }

                    
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

            return n;
        }



        /// <summary>
        /// 新增客户组,无明细
        /// </summary>
        /// <param name="List"></param>
        /// <param name="retinfo"></param>
        /// <returns></returns>
        public int PCstGroupInfo(SortableBindingList<ScmClientsGroupTemp> List, SPRetInfo retinfo)
        {
            string selectsql = "SELECT seq_nextval('seq_bacthid') as seqid";//调函数获取操作批次号ID

            string insertSql = "insert into scm_clients_group_temp(batchid,hdrid,compid,ownerid,"
            + "saledeptid,code,name,detailtype,grouptype,synctype,attachcode,deliveryfeerate,mark,"
            + "hdrstopflag,dtlid,cstid,modifyprctype,dtlstopflag,empid,operatype,createdate,"
            + "hdr_saledeptid,dtl_saledeptid) VALUES(@batchid, @hdrid, @compid, @ownerid, @saledeptid, @code, "
            + "@name, @detailtype, @grouptype, @synctype, @attachcode, @deliveryfeerate, @mark, @hdrstopflag, @dtlid, "
            + "@cstid, @modifyprctype, @dtlstopflag, @empid, @operatype, @createdate, @hdr_saledeptid, @dtl_saledeptid)";


            MySqlCommand selcmd = null;
            MySqlCommand insetCmd = null;
            MySqlCommand spCmd = null;
            MySqlTransaction trans = null;
            int retCode = -1;

            string seqID = "0";
            try
            {
                selcmd = connection.CreateCommand();
                selcmd.Connection = connection;
                selcmd.CommandType = System.Data.CommandType.Text;
                selcmd.CommandTimeout = 118000;
                selcmd.CommandText = selectsql.ToString();

                MySqlDataReader res = selcmd.ExecuteReader();
                if (res.Read())
                    seqID = res["seqid"].ToString().Trim();
                res.Close();
                res.Dispose();
                selcmd.Dispose();
                selcmd = null;

                trans = connection.BeginTransaction();

                insetCmd = connection.CreateCommand();
                insetCmd.Connection = connection;
                insetCmd.CommandType = System.Data.CommandType.Text;
                insetCmd.CommandText = insertSql.ToString();
                insetCmd.CommandTimeout = 118000;

                insetCmd.Transaction = trans;
                int recCount = 0;
                foreach (ScmClientsGroupTemp selected in List)
                {
                    recCount++;

                    insetCmd.Parameters.Clear();
                    insetCmd.Parameters.AddWithValue("batchid", Int32.Parse(seqID));
                    insetCmd.Parameters.AddWithValue("hdrid", Int32.Parse(selected.Hdrid));
                    insetCmd.Parameters.AddWithValue("compid", Int32.Parse(selected.Compid));
                    insetCmd.Parameters.AddWithValue("ownerid", Int32.Parse(selected.Ownerid));
                    if (!string.IsNullOrEmpty(selected.Saledeptid))
                    {
                        insetCmd.Parameters.AddWithValue("saledeptid", Int32.Parse(selected.Saledeptid));
                    }
                    else
                    {
                        insetCmd.Parameters.AddWithValue("saledeptid", null);
                    }
                    insetCmd.Parameters.AddWithValue("code", selected.Code);
                    insetCmd.Parameters.AddWithValue("name", selected.Name);
                    insetCmd.Parameters.AddWithValue("detailtype", selected.Detailtype);
                    insetCmd.Parameters.AddWithValue("grouptype", selected.Grouptype);
                    insetCmd.Parameters.AddWithValue("synctype", selected.Synctype);
                    insetCmd.Parameters.AddWithValue("attachcode", null);
                    insetCmd.Parameters.AddWithValue("deliveryfeerate", double.Parse(selected.Deliveryfeerate));
                    insetCmd.Parameters.AddWithValue("mark", selected.Mark);
                    insetCmd.Parameters.AddWithValue("hdrstopflag", selected.Hdrstopflag);
                    insetCmd.Parameters.AddWithValue("dtlid", null);
                    if (!string.IsNullOrEmpty(selected.Cstid))
                    {
                        insetCmd.Parameters.AddWithValue("cstid", Int32.Parse(selected.Cstid));
                    }
                    else
                    {
                        insetCmd.Parameters.AddWithValue("cstid", null);
                    }
                    insetCmd.Parameters.AddWithValue("modifyprctype", selected.Modifyprctype);
                    insetCmd.Parameters.AddWithValue("dtlstopflag", selected.Dtlstopflag);
                    insetCmd.Parameters.AddWithValue("empid", Int32.Parse(selected.Empid));
                    insetCmd.Parameters.AddWithValue("operatype", Int32.Parse(selected.Operatype));
                    insetCmd.Parameters.AddWithValue("createdate", DateTime.Now);
                    if (!string.IsNullOrEmpty(selected.Saledeptid))
                    {
                        insetCmd.Parameters.AddWithValue("hdr_saledeptid", Int32.Parse(selected.Saledeptid));
                    }
                    else
                    {
                        insetCmd.Parameters.AddWithValue("hdr_saledeptid", null);
                    }
                    if (!string.IsNullOrEmpty(selected.Saledeptid))
                    {
                        insetCmd.Parameters.AddWithValue("dtl_saledeptid", Int32.Parse(selected.Saledeptid));
                    }
                    else
                    {
                        insetCmd.Parameters.AddWithValue("dtl_saledeptid", null);
                    }


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



                spCmd = connection.CreateCommand();
                spCmd.Connection = connection;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "P_CST_GROUP_INFO";
                spCmd.CommandTimeout = 600;
                MySqlParameter[] parameters ={
                    new MySqlParameter("@AN_BATCHID",MySqlDbType.Int64),
                    new MySqlParameter("@AN_OWNERID",MySqlDbType.Int64),
                    new MySqlParameter("@AN_EMPID",MySqlDbType.Int64),
                    //new MySqlParameter("@in_goodid",MySqlDbType.Int64),
                    //new MySqlParameter("@in_prc",MySqlDbType.Double),
                    //new MySqlParameter("@in_price",MySqlDbType.Double),
                    //new MySqlParameter("@in_empid",MySqlDbType.Int64),
                    //new MySqlParameter("@in_costprc",MySqlDbType.Double),
                    //new MySqlParameter("@in_costprice",MySqlDbType.Double),
                    //new MySqlParameter("@in_costrate",MySqlDbType.Double),
                    //new MySqlParameter("@in_begindate",MySqlDbType.VarChar),
                    //new MySqlParameter("@in_enddate",MySqlDbType.VarChar),
                    //new MySqlParameter("@in_bottomprc",MySqlDbType.Double),
                    //new MySqlParameter("@in_bottomprice",MySqlDbType.Double),
                    //new MySqlParameter("@in_source",MySqlDbType.VarChar),
                    new MySqlParameter("@AS_RTD",MySqlDbType.Int64),
                    new MySqlParameter("@AS_COUNT",MySqlDbType.Int64),
                    new MySqlParameter("@AS_ARMSG",MySqlDbType.VarChar),
                    new MySqlParameter("@AS_RESULT",MySqlDbType.VarChar),
                    new MySqlParameter("@AS_SELFLAG",MySqlDbType.Int64),
                                              };

                parameters[0].Value = seqID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = SessionDto.Empid;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Size = 8;
                parameters[3].Direction = ParameterDirection.Output;
                parameters[4].Size = 2048;
                parameters[4].Direction = ParameterDirection.Output;
                parameters[5].Size = 2048;
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6].Size = 2048;
                parameters[6].Direction = ParameterDirection.Output;
                parameters[7].Size = 2048;
                parameters[7].Direction = ParameterDirection.Output;

                foreach (MySqlParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                if (retinfo.num == "1")
                {
                    retinfo.count = parameters[4].Value.ToString().Trim();
                    retinfo.msg = parameters[5].Value.ToString().Trim();
                }
                else
                {
                    retinfo.count = parameters[4].Value.ToString().Trim();
                    retinfo.msg = parameters[5].Value.ToString().Trim();
                }
                retinfo.result = parameters[6].Value.ToString().Trim();
                retinfo.selflag = parameters[7].Value.ToString().Trim();

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



        //查询定价客户组主表
        public SortableBindingList<CstGroupHdr> GetCstGroupClientGroupPriceList(string dept,Dictionary<string, string> sqlkeydict)
        {

            SortableBindingList<CstGroupHdr> infoList = new SortableBindingList<CstGroupHdr>();
            string sql = "SELECT a.ownerid, a.deptname, a.code, a.name, a.detailtypename, a.grouptypename,"
                + "a.synctypename, a.attachcode, a.deliveryfeerate, a.STOPNAME, a.mark, a.detailtype, a.grouptype, "
            + "a.synctype, a.stopflag, a.createuser, a.createusername, a.createdate, a.modifyuser, a.modifyusername, a.modifydate, "
            + "a.compid, a.hdrid,a.saledeptid,a.saledeptname FROM v_sel_cst_group_hdr AS a where a.ownerid=@ownerid "
            + "and a.compid=@compid and (a.saledeptid=@dept or a.saledeptid is null) $";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 118000;
                //cmd.CommandText = sql.ToString();

                String whereStr = "";

                //遍历Dictionary的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + " and " + kv.Key.Replace("%", "") + "= " + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like @" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "= @" + kv.Key;
                        }
                    }
                }

                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.AddWithValue("dept", dept);
                // 遍历Dictionary的Values值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.AddWithValue(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(kv.Key, kv.Value);
                        }
                    }

                }
                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    CstGroupHdr info = new CstGroupHdr();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Deptname = res["deptname"].ToString().Trim();
                    info.Code = res["code"].ToString().Trim();
                    info.Name = res["name"].ToString().Trim();
                    info.Detailtypename = res["detailtypename"].ToString().Trim();
                    info.Grouptypename = res["grouptypename"].ToString().Trim();
                    info.Synctypename = res["synctypename"].ToString().Trim();
                    info.Attachcode = res["attachcode"].ToString().Trim();
                    info.Deliveryfeerate = res["deliveryfeerate"].ToString().Trim();
                    info.STOPNAME = res["STOPNAME"].ToString().Trim();
                    info.Mark = res["mark"].ToString().Trim();
                    info.Detailtype = res["detailtype"].ToString().Trim();
                    info.Grouptype = res["grouptype"].ToString().Trim();
                    info.Synctype = res["synctype"].ToString().Trim();
                    info.Stopflag = res["stopflag"].ToString().Trim();
                    info.Createuser = res["createuser"].ToString().Trim();
                    info.Createusername = res["createusername"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Modifyusername = res["modifyusername"].ToString().Trim();
                    info.Modifydate = res["modifydate"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Hdrid = res["hdrid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Saledeptname = res["saledeptname"].ToString().Trim();

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

        public int PCstGroupInfoDtl(SortableBindingList<ScmClientsGroupTemp> GroupTempList,
             SortableBindingList<ScmPriceExe> ScmPriceExeList, SPRetInfo retinfo)
        {

            string selectsql = "SELECT seq_nextval('seq_bacthid') as seqid";//调函数获取操作批次号ID


            string insertSql1 = "insert into scm_clients_group_temp(batchid,hdrid,compid,ownerid,"
            + "saledeptid,code,name,detailtype,grouptype,synctype,attachcode,deliveryfeerate,mark,"
            + "hdrstopflag,dtlid,cstid,modifyprctype,dtlstopflag,empid,operatype,createdate,"
            + "hdr_saledeptid,dtl_saledeptid) VALUES(@batchid, @hdrid, @compid, @ownerid, @saledeptid, @code, "
            + "@name, @detailtype, @grouptype, @synctype, @attachcode, @deliveryfeerate, @mark, @hdrstopflag, @dtlid, "
            + "@cstid, @modifyprctype, @dtlstopflag, @empid, @operatype, @createdate, @hdr_saledeptid, @dtl_saledeptid)";

            string insertSql2 = "insert into scm_price_exe_temp (batchid, compid, ownerid, saledeptid,"
            + "cstid, hdrid, goodid, prc, price, bottomprc, bottomprice, costprc, costprice, begindate,"
            + "enddate, goodtapid, source, type, synctype, grouptype, stopflag, costrate,prc_pri) VALUES (@batchid, @compid, "
            + "@ownerid, @saledeptid, @cstid, @hdrid, @goodid, @prc, @price, @bottomprc, @bottomprice, "
            + "@costprc, @costprice, @begindate, @enddate, @goodtapid, @source, @type, @synctype, @grouptype, @stopflag, "
            + "@costrate,f_grouptypecode_topriory(@prcpri))";

            MySqlCommand selcmd = null;
            MySqlCommand inset1Cmd = null;
            MySqlCommand inset2Cmd = null;
            MySqlCommand spCmd = null;
            MySqlTransaction trans1 = null;
            MySqlTransaction trans2 = null;
            int retCode = -1;

            string seqID = "0";
            try
            {

                selcmd = connection.CreateCommand();
                selcmd.Connection = connection;
                selcmd.CommandType = System.Data.CommandType.Text;
                selcmd.CommandTimeout = 118000;
                selcmd.CommandText = selectsql.ToString();

                MySqlDataReader res = selcmd.ExecuteReader();
                if (res.Read())
                    seqID = res["seqid"].ToString().Trim();
                res.Close();
                res.Dispose();
                selcmd.Dispose();
                selcmd = null;

                trans1 = connection.BeginTransaction();

                inset1Cmd = connection.CreateCommand();
                inset1Cmd.Connection = connection;
                inset1Cmd.CommandType = System.Data.CommandType.Text;
                inset1Cmd.CommandText = insertSql1.ToString();
                inset1Cmd.CommandTimeout = 118000;

                inset1Cmd.Transaction = trans1;

                int recCount = 0;
                foreach (ScmClientsGroupTemp selected in GroupTempList)
                {
                    recCount++;

                    inset1Cmd.Parameters.Clear();
                    inset1Cmd.Parameters.AddWithValue("batchid", Int32.Parse(seqID));
                    inset1Cmd.Parameters.AddWithValue("hdrid", Int32.Parse(selected.Hdrid));
                    inset1Cmd.Parameters.AddWithValue("compid", Int32.Parse(selected.Compid));
                    inset1Cmd.Parameters.AddWithValue("ownerid", Int32.Parse(selected.Ownerid));
                    if (!string.IsNullOrEmpty(selected.Saledeptid))
                    {
                        inset1Cmd.Parameters.AddWithValue("saledeptid", Int32.Parse(selected.Saledeptid));
                    }
                    else
                    {
                        inset1Cmd.Parameters.AddWithValue("saledeptid", null);
                    }
                    inset1Cmd.Parameters.AddWithValue("code", selected.Code);
                    inset1Cmd.Parameters.AddWithValue("name", selected.Name);
                    inset1Cmd.Parameters.AddWithValue("detailtype", selected.Detailtype);
                    inset1Cmd.Parameters.AddWithValue("grouptype", selected.Grouptype);
                    inset1Cmd.Parameters.AddWithValue("synctype", selected.Synctype);
                    inset1Cmd.Parameters.AddWithValue("attachcode", null);
                    if (!string.IsNullOrEmpty(selected.Deliveryfeerate))
                    {
                        inset1Cmd.Parameters.AddWithValue("deliveryfeerate", double.Parse(selected.Deliveryfeerate));
                    }
                    else
                    {
                        inset1Cmd.Parameters.AddWithValue("deliveryfeerate", null);
                    }
                    inset1Cmd.Parameters.AddWithValue("mark", selected.Mark);
                    inset1Cmd.Parameters.AddWithValue("hdrstopflag", selected.Hdrstopflag);
                    inset1Cmd.Parameters.AddWithValue("dtlid", null);
                    if (!string.IsNullOrEmpty(selected.Cstid))
                    {
                        inset1Cmd.Parameters.AddWithValue("cstid", Int32.Parse(selected.Cstid));
                    }
                    else
                    {
                        inset1Cmd.Parameters.AddWithValue("cstid", null);
                    }
                    inset1Cmd.Parameters.AddWithValue("modifyprctype", selected.Modifyprctype);
                    inset1Cmd.Parameters.AddWithValue("dtlstopflag", selected.Dtlstopflag);
                    inset1Cmd.Parameters.AddWithValue("empid", Int32.Parse(selected.Empid));
                    inset1Cmd.Parameters.AddWithValue("operatype", Int32.Parse(selected.Operatype));
                    inset1Cmd.Parameters.AddWithValue("createdate", DateTime.Now);
                    if (!string.IsNullOrEmpty(selected.Saledeptid))
                    {
                        inset1Cmd.Parameters.AddWithValue("hdr_saledeptid", Int32.Parse(selected.Saledeptid));
                    }
                    else
                    {
                        inset1Cmd.Parameters.AddWithValue("hdr_saledeptid", null);
                    }
                    if (!string.IsNullOrEmpty(selected.Saledeptid))
                    {
                        inset1Cmd.Parameters.AddWithValue("dtl_saledeptid", Int32.Parse(selected.Saledeptid));
                    }
                    else
                    {
                        inset1Cmd.Parameters.AddWithValue("dtl_saledeptid", null);
                    }


                    inset1Cmd.ExecuteNonQuery();
                }

                trans1.Commit();
                trans1.Dispose();
                trans1 = null;

                inset1Cmd.Dispose();
                inset1Cmd = null;

                trans2 = connection.BeginTransaction();

                inset2Cmd = connection.CreateCommand();
                inset2Cmd.Connection = connection;
                inset2Cmd.CommandType = System.Data.CommandType.Text;
                inset2Cmd.CommandText = insertSql2.ToString();
                inset2Cmd.CommandTimeout = 118000;

                inset2Cmd.Transaction = trans2;

                int recCount1 = 0;
                foreach (ScmPriceExe selected in ScmPriceExeList)
                {
                    recCount1++;
                    inset2Cmd.Parameters.Clear();
                    inset2Cmd.Parameters.AddWithValue("batchid", Int32.Parse(seqID));
                    inset2Cmd.Parameters.AddWithValue("compid", Int32.Parse(Properties.Settings.Default.COMPID));
                    inset2Cmd.Parameters.AddWithValue("ownerid", Int32.Parse(Properties.Settings.Default.OWNERID));
                    if (!string.IsNullOrEmpty(selected.Saledeptid))
                    {
                        inset2Cmd.Parameters.AddWithValue("saledeptid", Int32.Parse(selected.Saledeptid));
                    }
                    else
                    {
                        inset2Cmd.Parameters.AddWithValue("saledeptid", null);
                    }
                    inset2Cmd.Parameters.AddWithValue("cstid", selected.Cstid);
                    inset2Cmd.Parameters.AddWithValue("hdrid", selected.Hdrid);
                    inset2Cmd.Parameters.AddWithValue("goodid", selected.Goodid);
                    inset2Cmd.Parameters.AddWithValue("prc", decimal.Parse(selected.Prc));
                    inset2Cmd.Parameters.AddWithValue("price", decimal.Parse(selected.Price));
                    inset2Cmd.Parameters.AddWithValue("bottomprc", decimal.Parse(selected.Bottomprc));
                    inset2Cmd.Parameters.AddWithValue("bottomprice", decimal.Parse(selected.Bottomprice));
                    inset2Cmd.Parameters.AddWithValue("costprc", decimal.Parse(selected.Costprc));
                    inset2Cmd.Parameters.AddWithValue("costprice", decimal.Parse(selected.Costprice));
                    inset2Cmd.Parameters.AddWithValue("begindate", DateTime.Parse(selected.Begindate));
                    inset2Cmd.Parameters.AddWithValue("enddate", DateTime.Parse(selected.Enddate));
                    inset2Cmd.Parameters.AddWithValue("goodtapid", Int32.Parse(selected.Goodtapid));
                    inset2Cmd.Parameters.AddWithValue("source", selected.Source);
                    inset2Cmd.Parameters.AddWithValue("type", Int32.Parse(selected.Type));
                    inset2Cmd.Parameters.AddWithValue("synctype", selected.Synctype);
                    inset2Cmd.Parameters.AddWithValue("grouptype", selected.Grouptype);
                    inset2Cmd.Parameters.AddWithValue("stopflag", selected.Stopflag);
                    inset2Cmd.Parameters.AddWithValue("costrate", double.Parse(selected.Costrate));
                    inset2Cmd.Parameters.AddWithValue("prcpri", selected.PrcPri.ToString());

                    inset2Cmd.ExecuteNonQuery();
                }

                trans2.Commit();
                trans2.Dispose();
                trans2 = null;

                inset2Cmd.Dispose();
                inset2Cmd = null;
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

                if (recCount1 < 1)
                {
                    retinfo.num = "0";
                    retinfo.msg = "无数据";
                    retinfo.result = "没调用!";
                    return 0;
                }
                spCmd = connection.CreateCommand();
                spCmd.Connection = connection;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "P_CST_GROUP_INFO";
                spCmd.CommandTimeout = 600;
                MySqlParameter[] parameters ={
                    new MySqlParameter("@AN_BATCHID",MySqlDbType.Int64),
                    new MySqlParameter("@AN_OWNERID",MySqlDbType.Int64),
                    new MySqlParameter("@AN_EMPID",MySqlDbType.Int64),
                    //new MySqlParameter("@in_goodid",MySqlDbType.Int64),
                    //new MySqlParameter("@in_prc",MySqlDbType.Double),
                    //new MySqlParameter("@in_price",MySqlDbType.Double),
                    //new MySqlParameter("@in_empid",MySqlDbType.Int64),
                    //new MySqlParameter("@in_costprc",MySqlDbType.Double),
                    //new MySqlParameter("@in_costprice",MySqlDbType.Double),
                    //new MySqlParameter("@in_costrate",MySqlDbType.Double),
                    //new MySqlParameter("@in_begindate",MySqlDbType.VarChar),
                    //new MySqlParameter("@in_enddate",MySqlDbType.VarChar),
                    //new MySqlParameter("@in_bottomprc",MySqlDbType.Double),
                    //new MySqlParameter("@in_bottomprice",MySqlDbType.Double),
                    //new MySqlParameter("@in_source",MySqlDbType.VarChar),
                    new MySqlParameter("@AS_RTD",MySqlDbType.Int64),
                    new MySqlParameter("@AS_COUNT",MySqlDbType.Int64),
                    new MySqlParameter("@AS_ARMSG",MySqlDbType.VarChar),
                    new MySqlParameter("@AS_RESULT",MySqlDbType.VarChar),
                    new MySqlParameter("@AS_SELFLAG",MySqlDbType.Int64),
                                              };

                parameters[0].Value = seqID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = SessionDto.Empid;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Size = 8;
                parameters[3].Direction = ParameterDirection.Output;
                parameters[4].Size = 2048;
                parameters[4].Direction = ParameterDirection.Output;
                parameters[5].Size = 2048;
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6].Size = 2048;
                parameters[6].Direction = ParameterDirection.Output;
                parameters[7].Size = 2048;
                parameters[7].Direction = ParameterDirection.Output;

                foreach (MySqlParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                if (retinfo.num == "1")
                {
                    retinfo.count = parameters[4].Value.ToString().Trim();
                    retinfo.msg = parameters[5].Value.ToString().Trim();
                }
                else
                {
                    retinfo.count = parameters[4].Value.ToString().Trim();
                    retinfo.msg = parameters[5].Value.ToString().Trim();
                }
                retinfo.result = parameters[6].Value.ToString().Trim();
                retinfo.selflag = parameters[7].Value.ToString().Trim();

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

                if (trans1 != null)
                    trans1.Dispose();

                if (trans2 != null)
                    trans2.Dispose();
                if (trans1 != null)
                {
                    trans1.Rollback();
                    trans1.Dispose();
                    trans1 = null;
                }
                if (trans2 != null)
                {
                    trans2.Rollback();
                    trans2.Dispose();
                    trans2 = null;
                }

            }

            return retCode;
        }


        //修改客户组
        public int PCstGroupUpdate(int Hdrid, string Code, string Name, decimal Deliveryfeerate,
            string Mark, string Stopflag, SPRetInfo retinfo)
        {
            MySqlCommand spCmd = null;
            int retCode = -1;
            try
            {
                spCmd = connection.CreateCommand();
                spCmd.Connection = connection;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "P_CST_GROUP_UPDATE";
                spCmd.CommandTimeout = 600;
                MySqlParameter[] parameters ={
                    new MySqlParameter("@IN_hdrid",MySqlDbType.Int64),
                    new MySqlParameter("@IN_code",MySqlDbType.VarChar),
                    new MySqlParameter("@IN_name",MySqlDbType.VarChar),
                    new MySqlParameter("@IN_deliveryfeerate",MySqlDbType.Decimal),
                    new MySqlParameter("@IN_mark",MySqlDbType.VarChar),
                    new MySqlParameter("@IN_stopflag",MySqlDbType.VarChar),
                    new MySqlParameter("@IN_empid",MySqlDbType.Int64),

                    new MySqlParameter("@AS_RTD",MySqlDbType.Int64),
                    new MySqlParameter("@AS_COUNT",MySqlDbType.Int64),
                    new MySqlParameter("@AS_ARMSG",MySqlDbType.VarChar),
                    new MySqlParameter("@AS_RESULT",MySqlDbType.VarChar),
                    //new MySqlParameter("@in_enddate",MySqlDbType.VarChar),
                    //new MySqlParameter("@in_bottomprc",MySqlDbType.Double),
                    //new MySqlParameter("@in_bottomprice",MySqlDbType.Double),
                    //new MySqlParameter("@in_source",MySqlDbType.VarChar),
                    //new MySqlParameter("@out_resultcode",MySqlDbType.VarChar),
                    //new MySqlParameter("@out_resultmsg",MySqlDbType.VarChar),

                                              };

                parameters[0].Value = Hdrid;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Code;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = Name;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = Deliveryfeerate;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = Mark;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Value = Stopflag;
                parameters[5].Direction = ParameterDirection.Input;
                parameters[6].Value = SessionDto.Empid;
                parameters[6].Direction = ParameterDirection.Input;

                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[7].Size = 8;
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8].Size = 2048;
                parameters[8].Direction = ParameterDirection.Output;
                parameters[9].Size = 2048;
                parameters[9].Direction = ParameterDirection.Output;
                parameters[10].Size = 2048;
                parameters[10].Direction = ParameterDirection.Output;

                foreach (MySqlParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[7].Value.ToString().Trim();
                if (retinfo.num == "1")
                {
                    retinfo.count = parameters[8].Value.ToString().Trim();
                    retinfo.msg = parameters[9].Value.ToString().Trim();
                }
                else
                {
                    retinfo.count = parameters[8].Value.ToString().Trim();
                    retinfo.msg = parameters[9].Value.ToString().Trim();
                }
                retinfo.result = parameters[10].Value.ToString().Trim();


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
        //-------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------
        //查询限禁销渠道信息(18-06-29)
        public SortableBindingList<ScmPriceForbidsale> GetScmPriceForbidsaleList(Dictionary<string, string> sqlkeydict)
        {

            SortableBindingList<ScmPriceForbidsale> infoList = new SortableBindingList<ScmPriceForbidsale>();
            string sql = "SELECT a.cstid, a.cstcode, a.cstname, a.region, a.goodid, a.goods,"
            + "a.name, a.spec, a.producer, a.outrate, a.sprdug, a.id, a.compid, a.ownerid,"
            + "a.saleflag, a.saleflagname, a.begindate, a.enddate, a.hdrcode, a.hdrname, a.createuser,"
            + "a.createusername, a.createdate, a.modifyuser, a.modifyusername,a.cmsclienttypename,a.clienttypename,"
            + "a.modifydate,a.mark FROM v_scm_price_forbidsale AS a where a.compid=@compid and a.ownerid =@ownerid $";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 118000;
                //cmd.CommandText = sql.ToString();
                String whereStr = "";

                //遍历Dictionary的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + " and " + kv.Key.Replace("%", "") + "= " + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like @" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "= @" + kv.Key;
                        }
                    }
                }

                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
                //cmd.Parameters.AddWithValue("hdrid", hdrid);
                // 遍历Dictionary的Values值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.AddWithValue(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(kv.Key, kv.Value);
                        }
                    }

                }

                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    ScmPriceForbidsale info = new ScmPriceForbidsale();

                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Name = res["name"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();

                    info.Producer = res["producer"].ToString().Trim();
                    info.Cstcode = res["cstcode"].ToString().Trim();
                    info.Cstname = res["cstname"].ToString().Trim();
                    info.Hdrcode = res["hdrcode"].ToString().Trim();
                    info.Hdrname = res["hdrname"].ToString().Trim();
                    info.Region = res["region"].ToString().Trim();
                    info.Saleflag = res["saleflag"].ToString().Trim();
                    info.Saleflagname = res["saleflagname"].ToString().Trim();
                    info.Createuser = res["createuser"].ToString().Trim();
                    info.Createusername = res["createusername"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Begindate = res["begindate"].ToString().Trim();
                    info.Enddate = res["enddate"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Cmsclienttypename = res["cmsclienttypename"].ToString().Trim();
                    info.Clienttypename = res["clienttypename"].ToString().Trim();
                    info.Mark = res["mark"].ToString().Trim();
                    //info.Batchid = "";
                    //info.Saledeptid = PubOwnerConfigureDto.Saledeptid;
                    //info.Saledeptname = PubOwnerConfigureDto.Saledepttype;

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

        //限禁销维护操作存储过程(18-06-08)
        public int PPrcForbitsaleInf(SortableBindingList<ScmPriceForbidsaleTemp> List, SPRetInfo retinfo)
        {
            string selectsql = "SELECT seq_nextval('seq_bacthid') as seqid";//调函数获取操作批次号ID

            string insertSql = "insert into scm_price_forbidsale_temp(batchid, id, compid, ownerid, cstid,"
            + "goodid, saleflag, operatetype, audflag, audstatus, lastaudtime, begindate, enddate, source,mark) VALUES"
            + "(@batchid, @id, @compid, @ownerid, @cstid, @goodid, @saleflag, @operatetype, @audflag, @audstatus,"
            + "@lastaudtime, @begindate, @enddate, @source, @mark)";


            MySqlCommand selcmd = null;
            MySqlCommand insetCmd = null;
            MySqlCommand spCmd = null;
            MySqlTransaction trans = null;
            int retCode = -1;

            string seqID = "0";
            try
            {
                selcmd = connection.CreateCommand();
                selcmd.Connection = connection;
                selcmd.CommandType = System.Data.CommandType.Text;
                selcmd.CommandTimeout = 118000;
                selcmd.CommandText = selectsql.ToString();

                MySqlDataReader res = selcmd.ExecuteReader();
                if (res.Read())
                    seqID = res["seqid"].ToString().Trim();
                res.Close();
                res.Dispose();
                selcmd.Dispose();
                selcmd = null;

                trans = connection.BeginTransaction();

                insetCmd = connection.CreateCommand();
                insetCmd.Connection = connection;
                insetCmd.CommandType = System.Data.CommandType.Text;
                insetCmd.CommandText = insertSql.ToString();
                insetCmd.CommandTimeout = 118000;

                insetCmd.Transaction = trans;
                int recCount = 0;
                foreach (ScmPriceForbidsaleTemp selected in List)
                {
                    recCount++;

                    insetCmd.Parameters.Clear();
                    //, , , , , , , , , , , , , 
                    insetCmd.Parameters.AddWithValue("batchid", Int32.Parse(seqID));
                    insetCmd.Parameters.AddWithValue("id", null);
                    insetCmd.Parameters.AddWithValue("compid", Int32.Parse(Properties.Settings.Default.COMPID));
                    insetCmd.Parameters.AddWithValue("ownerid", Int32.Parse(Properties.Settings.Default.OWNERID));
                    insetCmd.Parameters.AddWithValue("cstid", Int32.Parse(selected.Cstid));
                    insetCmd.Parameters.AddWithValue("goodid", Int32.Parse(selected.Goodid));
                    insetCmd.Parameters.AddWithValue("saleflag", selected.Saleflag);
                    insetCmd.Parameters.AddWithValue("operatetype", Int32.Parse(selected.Operatetype));
                    insetCmd.Parameters.AddWithValue("audflag", null);
                    insetCmd.Parameters.AddWithValue("audstatus", null);
                    insetCmd.Parameters.AddWithValue("lastaudtime", null);
                    insetCmd.Parameters.AddWithValue("begindate", DateTime.Parse(selected.Begindate));
                    insetCmd.Parameters.AddWithValue("enddate", DateTime.Parse(selected.Enddate));
                    insetCmd.Parameters.AddWithValue("source", selected.Source);
                    insetCmd.Parameters.AddWithValue("mark", selected.Mark);


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



                spCmd = connection.CreateCommand();
                spCmd.Connection = connection;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "P_PRC_FORBIDSALE_INF";
                spCmd.CommandTimeout = 600;
                MySqlParameter[] parameters ={
                    new MySqlParameter("@AN_BATCHID",MySqlDbType.Int64),
                    new MySqlParameter("@AN_OWNERID",MySqlDbType.Int64),
                    new MySqlParameter("@AN_EMPID",MySqlDbType.Int64),
                    //new MySqlParameter("@in_goodid",MySqlDbType.Int64),
                    //new MySqlParameter("@in_prc",MySqlDbType.Double),
                    //new MySqlParameter("@in_price",MySqlDbType.Double),
                    //new MySqlParameter("@in_empid",MySqlDbType.Int64),
                    //new MySqlParameter("@in_costprc",MySqlDbType.Double),
                    //new MySqlParameter("@in_costprice",MySqlDbType.Double),
                    //new MySqlParameter("@in_costrate",MySqlDbType.Double),
                    //new MySqlParameter("@in_begindate",MySqlDbType.VarChar),
                    //new MySqlParameter("@in_enddate",MySqlDbType.VarChar),
                    //new MySqlParameter("@in_bottomprc",MySqlDbType.Double),
                    //new MySqlParameter("@in_bottomprice",MySqlDbType.Double),
                    //new MySqlParameter("@in_source",MySqlDbType.VarChar),
                    new MySqlParameter("@AS_RTD",MySqlDbType.Int64),
                    new MySqlParameter("@AS_ARMSG",MySqlDbType.VarChar),
                    new MySqlParameter("@AS_RESULT",MySqlDbType.VarChar),

                                              };

                parameters[0].Value = seqID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = SessionDto.Empid;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Size = 8;
                parameters[3].Direction = ParameterDirection.Output;
                parameters[4].Size = 2048;
                parameters[4].Direction = ParameterDirection.Output;
                parameters[5].Size = 2048;
                parameters[5].Direction = ParameterDirection.Output;


                foreach (MySqlParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[3].Value.ToString().Trim();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[4].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[4].Value.ToString().Trim();
                }
                retinfo.result = parameters[5].Value.ToString().Trim();


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

        //查询限禁销新增商品信息(18-06-29)
        public SortableBindingList<SelWaredict> GetSelWaredictList(Dictionary<string, string> sqlkeydict)
        {

            SortableBindingList<SelWaredict> infoList = new SortableBindingList<SelWaredict>();
            string sql = "SELECT a.goodid, a.compid, a.goods, a.name, a.spec, a.producer, a.islimit,"
            + "a.islimitname, a.bargain, a.bargainname, a.outrate, a.stopflag, a.stopflagname, a.createuser,"
            + "a.createdate, a.sprdug, a.wdrcode, a.wdrname FROM v_sel_waredict_owner AS a where a.compid=@compid "
            + "and a.ownerid=@ownerid $";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 1800;
                //cmd.CommandText = sql.ToString();

                String whereStr = "";

                //遍历Dictionary的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + " and " + kv.Key.Replace("%", "") + "= " + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like @" + kv.Key.Replace("%", "");
                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "= @" + kv.Key;
                        }
                    }
                }

                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                //cmd.Parameters.AddWithValue("hdrid", hdrid);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
                // 遍历Dictionary的Values值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.AddWithValue(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(kv.Key, kv.Value);
                        }
                    }

                }

                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    SelWaredict info = new SelWaredict();

                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Name = res["name"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Stopflag = res["stopflag"].ToString().Trim();
                    info.Stopflagname = res["stopflagname"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.Wdrcode = res["wdrcode"].ToString().Trim();
                    info.Wdrname = res["wdrname"].ToString().Trim();
                    //info.Spec = res["spec"].ToString().Trim();


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


        //查询限禁销新增客户信息(18-06-08)
        public SortableBindingList<SelClientsGroup> GetSelClientsGroupList(Dictionary<string, string> sqlkeydict)
        {

            SortableBindingList<SelClientsGroup> infoList = new SortableBindingList<SelClientsGroup>();
            string sql = "SELECT a.compid, a.ownerid, a.cstid, a.cstcode, a.cstname, a.province, a.provincename,"
            + "a.city, a.cityname, a.area, a.areaname, a.region, a.paytype, a.paytypename, a.stopflag, a.stopflagname,"
            + "a.hdrcode, a.hdrname, a.createuser, a.createusercode, a.createusername, a.createdate, a.modifyuser,"
            + "a.modifyusercode, a.modifyusername, a.cmsclienttypename, a.clienttypename,"
            + "a.modifydate FROM v_sel_clients_owner AS a where a.compid=@compid and a.ownerid=@ownerid $";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 118000;
                //cmd.CommandText = sql.ToString();

                String whereStr = "";

                //遍历Dictionary的key值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (kv.Key.Equals(null) || kv.Key.Equals(""))
                    {
                        whereStr = whereStr + " and " + kv.Key.Replace("%", "") + "= " + kv.Key.Replace("%", "");
                    }
                    else
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("%", "") + " like @" + kv.Key.Replace("%", "");
                        }
                        else if (kv.Key.IndexOf("#") > -1)
                        {
                            whereStr = whereStr + " and " + kv.Key.Replace("#", "") + " not like @" + kv.Key.Replace("#", "");
                        }
                        else
                        {
                            whereStr = whereStr + " and " + kv.Key + "= @" + kv.Key;
                        }
                    }
                }

                sql = sql.Replace("$", whereStr);
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                //cmd.Parameters.AddWithValue("hdrid", hdrid);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
                // 遍历Dictionary的Values值
                foreach (KeyValuePair<string, string> kv in sqlkeydict)
                {
                    if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                    {
                        if (kv.Key.IndexOf("%") > -1)
                        {
                            cmd.Parameters.AddWithValue(kv.Key.Replace("%", ""), kv.Value);
                        }
                        else if (kv.Key.IndexOf("#") > -1)
                        {
                            cmd.Parameters.AddWithValue(kv.Key.Replace("#", ""), kv.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(kv.Key, kv.Value);
                        }
                    }

                }

                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    SelClientsGroup info = new SelClientsGroup();

                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Cstcode = res["cstcode"].ToString().Trim();
                    info.Cstname = res["cstname"].ToString().Trim();
                    info.Region = res["region"].ToString().Trim();

                    info.Hdrcode = res["hdrcode"].ToString().Trim();
                    info.Hdrname = res["hdrname"].ToString().Trim();
                    info.Stopflag = res["stopflag"].ToString().Trim();
                    info.Stopflagname = res["stopflagname"].ToString().Trim();
                    info.Cmsclienttypename = res["cmsclienttypename"].ToString().Trim();
                    info.Clienttypename = res["clienttypename"].ToString().Trim();

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

        #endregion
    }
}
