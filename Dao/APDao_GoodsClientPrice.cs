using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using Oracle.DataAccess.Client;

namespace PriceManager
{
    class APDao_GoodsClientPrice : MySQLHelper
    {
        private object dao;
        #region ClientGroup
        //查询商品信息
        public SortableBindingList<SelPubWaredict> GetPubWaredictAllInfoList(Dictionary<string, string> sqlkeydict)
        {
            SortableBindingList<SelPubWaredict> infoList = new SortableBindingList<SelPubWaredict>();
            string sql = "SELECT a.compid, a.goodid, a.goods, a.name, a.spec, a.producer, a.islimit, a.islimitname, a.bargain, a.bargainname, a.outrate, a.stopflag, a.stopflagname, a.createuser,a.createdate, a.sprdug FROM v_sel_waredict AS a where a.compid=@compid $  ";
            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                

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

                    SelPubWaredict info = new SelPubWaredict();

                    info.Compid = res["compid"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Name = res["name"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.Islimit = res["islimit"].ToString().Trim();
                    info.Islimitname = res["islimitname"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.Bargainname = res["bargainname"].ToString().Trim();
                    info.Outrate = res["outrate"].ToString().Trim();
                    info.Stopflag = res["stopflag"].ToString().Trim();
                    info.Stopflagname = res["stopflagname"].ToString().Trim();
                    info.Createuser = res["createuser"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Sprdug = res["sprdug"].ToString().Trim();

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
        //底价维护模块------2018-10-12
        public SortableBindingList<SelPubWaredicts> GetPubWaredictAllInfoLists(Dictionary<string, string> sqlkeydict)
        {

            SortableBindingList<SelPubWaredicts> infoList = new SortableBindingList<SelPubWaredicts>();
            string sql = "";
            if (SessionDto.Emproleid=="99"|| SessionDto.Emproleid == "109") {
               sql = "SELECT a.compid, a.goodid, a.goods, a.name, a.spec, a.producer, a.islimit, a.islimitname, a.bargain, a.bargainname, a.outrate, a.stopflag, a.stopflagname, a.createuser,a.createdate, a.sprdug,a.buyercode,a.buyername FROM v_sel_waredict_owner AS a where a.compid=@compid and a.ownerid = @ownerid$  ";
            }
            else {
                 sql = "SELECT a.compid, a.goodid, a.goods, a.name, a.spec, a.producer, a.islimit, a.islimitname, a.bargain, a.bargainname, a.outrate, a.stopflag, a.stopflagname, a.createuser,a.createdate, a.sprdug,a.buyercode,a.buyername FROM v_sel_waredict_owner AS a where a.compid=@compid and a.ownerid = @ownerid and(a.buyercode=@loginid OR a.buyercode is null)$  ";
            }
            


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;


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
                cmd.Parameters.AddWithValue("loginid", SessionDto.Empcode);
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

                    SelPubWaredicts info = new SelPubWaredicts();

                    info.Compid = res["compid"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Name = res["name"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.Islimit = res["islimit"].ToString().Trim();
                    info.Islimitname = res["islimitname"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.Bargainname = res["bargainname"].ToString().Trim();
                    info.Outrate = res["outrate"].ToString().Trim();
                    info.Stopflag = res["stopflag"].ToString().Trim();
                    info.Stopflagname = res["stopflagname"].ToString().Trim();
                    info.Createuser = res["createuser"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Sprdug = res["sprdug"].ToString().Trim();
                    info.Buyercode = res["buyercode"].ToString().Trim();
                    info.BuyerName = res["buyername"].ToString().Trim();

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


        //查询商品统一价格信息
        public SortableBindingList<ScmPriceGoodunify> GetScmPriceGoodunifyList(Dictionary<string, string> sqlkeydict)
        {

            SortableBindingList<ScmPriceGoodunify> infoList = new SortableBindingList<ScmPriceGoodunify>();
            string sql = "SELECT a.id, a.compid, a.ownerid, a.saledeptid, a.goodid, a.prc, a.price, a.stopflag, a.stopflagname, a.createuser, a.createusername, a.createdate, a.modifyuser, a.modifyusername, a.modifydate, a.audflag, a.audflagname, a.audstatus, a.audstatusname, a.lastaudtime, a.costprc, a.costprice, a.bargain, a.iscredit, a.costrate, a.begindate, a.enddate, a.bottomprc, a.bottomprice, a.oriprc, a.lastprc, a.source, a.b2bdisplay, a.b2bdisplayname, a.goods, a.name, a.outrate, a.producer, a.spec, a.sprdug FROM v_scm_price_goodunify AS a where a.compid=@compid  and a.ownerid=@ownerid  $  ";


            MySqlCommand cmd = null;
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                if (ConnectionTests() == -1)//打开mysql从库连接
                {
                    MessageBox.Show("连接从库失败!");
                    Environment.Exit(0);
                }
                //从库 ----2019-1-7---
                cmd = connections.CreateCommand();
                cmd.Connection = connections;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;


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

                    ScmPriceGoodunify info = new ScmPriceGoodunify();

                    info.Id = res["id"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    //info.Defaultdeptid = res["defaultdeptid"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Stopflag = res["stopflag"].ToString().Trim();
                    info.Stopflagname = res["stopflagname"].ToString().Trim();
                    info.Createuser = res["createuser"].ToString().Trim();
                    info.Createusername = res["createusername"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Modifyuser = res["modifyuser"].ToString().Trim();
                    info.Modifyusername = res["modifyusername"].ToString().Trim();
                    info.Modifydate = res["modifydate"].ToString().Trim();
                    info.Audflag = res["audflag"].ToString().Trim();
                    info.Audflagname = res["audflagname"].ToString().Trim();
                    info.Audstatus = res["audstatus"].ToString().Trim();
                    info.Audstatusname = res["audstatusname"].ToString().Trim();
                    info.Lastaudtime = res["lastaudtime"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.Iscredit = res["iscredit"].ToString().Trim();
                    info.Costrate = res["costrate"].ToString().Trim();
                    try
                    {
                        info.Begindate = Convert.ToDateTime(res["begindate"]).ToString("yyyy-MM-dd").Trim();
                        info.Enddate = Convert.ToDateTime(res["enddate"]).ToString("yyyy-MM-dd").Trim();
                    }
                    catch { }
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Oriprc = res["oriprc"].ToString().Trim();
                    info.Lastprc = res["lastprc"].ToString().Trim();
                    info.Source = res["source"].ToString().Trim();
                    info.B2bdisplay = res["b2bdisplay"].ToString().Trim();
                    info.B2bdisplayname = res["b2bdisplayname"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Name = res["name"].ToString().Trim();
                    info.Outrate = res["outrate"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Sprdug = res["sprdug"].ToString().Trim();

                    //调用存储过程
                    spCmd = Conn_datacenter_cmszh.CreateCommand();
                    spCmd.Connection = Conn_datacenter_cmszh;
                    spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    spCmd.CommandText = "CMS_TEL.P_B2BPRC_PURPRC";
                    spCmd.CommandTimeout = 600;

                    OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_GOODID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_LASTPURPRC",OracleDbType.Long,ParameterDirection.Output),
                    new OracleParameter("OUT_LASTPURDATE",OracleDbType.Varchar2,ParameterDirection.Output)
                };

                    parameters[0].Value = Int64.Parse(info.Compid);
                    parameters[1].Value = Int64.Parse(info.Ownerid);
                    parameters[2].Value = Int64.Parse(info.Goodid);
                    /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/

                    parameters[3].Size = 2048;
                    parameters[4].Size = 2048;
                    foreach (OracleParameter parameter in parameters)
                    {
                        spCmd.Parameters.Add(parameter);
                    }
                    spCmd.ExecuteNonQuery();
                    info.Lastpurprc = parameters[3].Value.ToString().Trim();
                    info.Lastpurdate = parameters[4].Value.ToString().Trim();

                    spCmd.Dispose();
                    spCmd = null;


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

                if (trans != null)
                    trans.Dispose();


                if (spCmd != null)
                    spCmd.Dispose();

            }

            return infoList;
        }

        //商品客户组定价视图
        public SortableBindingList<ScmPriceGoodtap> GetScmPriceGoodtapList(Dictionary<string, string> sqlkeydict)
        {

            SortableBindingList<ScmPriceGoodtap> infoList = new SortableBindingList<ScmPriceGoodtap>();
            string sql = "SELECT a.bacthid, a.goodtapid, a.compid, a.ownerid, a.saledeptid, a.goodid, a.hdrid, a.ismodifyexec, a.ismodifyexecname, a.pgdeliveryfeerate, a.prc, a.price, a.stopflag, a.stopflagname, a.createuser, a.createusername, a.createdate, a.modifyuser, a.modifyusername, a.modifydate, a.audflag, a.audflagname, a.audstatus, a.audstatusname, a.lastaudtime, a.costprctype, a.costprctypename, a.costprc, a.costprice, a.costrate, a.oriprc, a.lastprc, a.bottomprc, a.bottomprice, a.begindate, a.enddate, a.code, a.name, a.ghdeliveryfeerate, a.detailtype, a.detailtypename, a.grouptype, a.grouptypename, a.synctype, a.synctypename,a.goods FROM v_scm_price_goodtap AS a  where a.compid=@compid  and a.ownerid=@ownerid $  ";


            MySqlCommand cmd = null;
            try
            {
                if (ConnectionTests() == -1)//打开mysql从库连接
                {
                    MessageBox.Show("连接从库失败!");
                    Environment.Exit(0);
                }
                //从库 ----2019-1-7---
                cmd = connections.CreateCommand();
                cmd.Connection = connections;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;


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
                //cmd.Parameters.AddWithValue("saledeptid", SessionDto.Empdeptid);
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

                    ScmPriceGoodtap info = new ScmPriceGoodtap();
                    info.Bacthid = res["bacthid"].ToString().Trim();
                    info.Goodtapid = res["goodtapid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Hdrid = res["hdrid"].ToString().Trim();
                    info.Ismodifyexec = res["ismodifyexec"].ToString().Trim();
                    info.Ismodifyexecname = res["ismodifyexecname"].ToString().Trim();
                    info.Pgdeliveryfeerate = res["pgdeliveryfeerate"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Stopflag = res["stopflag"].ToString().Trim();
                    info.Stopflagname = res["stopflagname"].ToString().Trim();
                    info.Createuser = res["createuser"].ToString().Trim();
                    info.Createusername = res["createusername"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Modifyuser = res["modifyuser"].ToString().Trim();
                    info.Modifyusername = res["modifyusername"].ToString().Trim();
                    info.Modifydate = res["modifydate"].ToString().Trim();
                    info.Audflag = res["audflag"].ToString().Trim();
                    info.Audflagname = res["audflagname"].ToString().Trim();
                    info.Audstatus = res["audstatus"].ToString().Trim();
                    info.Audstatusname = res["audstatusname"].ToString().Trim();
                    info.Lastaudtime = res["lastaudtime"].ToString().Trim();
                    info.Costprctype = res["costprctype"].ToString().Trim();
                    info.Costprctypename = res["costprctypename"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Costrate = res["costrate"].ToString().Trim();
                    info.Oriprc = res["oriprc"].ToString().Trim();
                    info.Lastprc = res["lastprc"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Begindate = res["begindate"].ToString().Trim();
                    info.Enddate = res["enddate"].ToString().Trim();
                    info.Code = res["code"].ToString().Trim();
                    info.Name = res["name"].ToString().Trim();
                    info.Ghdeliveryfeerate = res["ghdeliveryfeerate"].ToString().Trim();
                    info.Detailtype = res["detailtype"].ToString().Trim();
                    info.Detailtypename = res["detailtypename"].ToString().Trim();
                    info.Grouptype = res["grouptype"].ToString().Trim();
                    info.Grouptypename = res["grouptypename"].ToString().Trim();
                    info.Synctype = res["synctype"].ToString().Trim();
                    info.Synctypename = res["synctypename"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();

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

        //-----2018-11-20----
        //商品客户组定价视图
        public SortableBindingList<ScmPriceGoodtap> GetScmPriceGoodtapLists(Dictionary<string, string> sqlkeydict)
        {

            SortableBindingList<ScmPriceGoodtap> infoList = new SortableBindingList<ScmPriceGoodtap>();
            string sql = "SELECT a.bacthid, a.goodtapid, a.compid, a.ownerid, a.saledeptid, a.goodid, a.hdrid, a.ismodifyexec, a.ismodifyexecname, a.pgdeliveryfeerate, a.prc, a.price, a.stopflag, a.stopflagname, a.createuser, a.createusername, a.createdate, a.modifyuser, a.modifyusername, a.modifydate, a.audflag, a.audflagname, a.audstatus, a.audstatusname, a.lastaudtime, a.costprctype, a.costprctypename, a.costprc, a.costprice, a.costrate, a.oriprc, a.lastprc, a.bottomprc, a.bottomprice, a.begindate, a.enddate, a.code, a.name, a.ghdeliveryfeerate, a.detailtype, a.detailtypename, a.grouptype, a.grouptypename, a.synctype, a.synctypename,a.goods FROM v_scm_price_goodtap AS a  where a.compid=@compid  and a.ownerid=@ownerid and a.stopflag = '00' and ((a.begindate < NOW() and a.enddate > NOW()) or costprctype = '10') $";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;


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
                //cmd.Parameters.AddWithValue("saledeptid", SessionDto.Empdeptid);
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

                    ScmPriceGoodtap info = new ScmPriceGoodtap();
                    info.Bacthid = res["bacthid"].ToString().Trim();
                    info.Goodtapid = res["goodtapid"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Hdrid = res["hdrid"].ToString().Trim();
                    info.Ismodifyexec = res["ismodifyexec"].ToString().Trim();
                    info.Ismodifyexecname = res["ismodifyexecname"].ToString().Trim();
                    info.Pgdeliveryfeerate = res["pgdeliveryfeerate"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Stopflag = res["stopflag"].ToString().Trim();
                    info.Stopflagname = res["stopflagname"].ToString().Trim();
                    info.Createuser = res["createuser"].ToString().Trim();
                    info.Createusername = res["createusername"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Modifyuser = res["modifyuser"].ToString().Trim();
                    info.Modifyusername = res["modifyusername"].ToString().Trim();
                    info.Modifydate = res["modifydate"].ToString().Trim();
                    info.Audflag = res["audflag"].ToString().Trim();
                    info.Audflagname = res["audflagname"].ToString().Trim();
                    info.Audstatus = res["audstatus"].ToString().Trim();
                    info.Audstatusname = res["audstatusname"].ToString().Trim();
                    info.Lastaudtime = res["lastaudtime"].ToString().Trim();
                    info.Costprctype = res["costprctype"].ToString().Trim();
                    info.Costprctypename = res["costprctypename"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Costrate = res["costrate"].ToString().Trim();
                    info.Oriprc = res["oriprc"].ToString().Trim();
                    info.Lastprc = res["lastprc"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Begindate = res["begindate"].ToString().Trim();
                    info.Enddate = res["enddate"].ToString().Trim();
                    info.Code = res["code"].ToString().Trim();
                    info.Name = res["name"].ToString().Trim();
                    info.Ghdeliveryfeerate = res["ghdeliveryfeerate"].ToString().Trim();
                    info.Detailtype = res["detailtype"].ToString().Trim();
                    info.Detailtypename = res["detailtypename"].ToString().Trim();
                    info.Grouptype = res["grouptype"].ToString().Trim();
                    info.Grouptypename = res["grouptypename"].ToString().Trim();
                    info.Synctype = res["synctype"].ToString().Trim();
                    info.Synctypename = res["synctypename"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();

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


        //商品客户明细价格视图       --2018-8-22--------
        public SortableBindingList<ScmPriceExe> GetScmPriceExeList(Dictionary<string, string> sqlkeydict)
        {

            SortableBindingList<ScmPriceExe> infoList = new SortableBindingList<ScmPriceExe>();
            string sql = "SELECT a.code, a.name, a.province, a.provincename, a.city, a.cityname, a.area, a.areaname, a.region, a.cstcode, a.cstname, a.id, a.compid, a.ownerid, a.saledeptid, a.cstid, a.hdrid, a.goodid, a.prc, a.price, a.bottomprc, a.bottomprice, a.costprc, a.costprice, a.costrate, a.begindate, a.enddate, a.goodtapid, a.source, a.type, a.synctype, a.stopflag, a.stopflagname, a.createuser, a.createusername, a.createdate, a.modifyuser, a.modifyusername, a.modifydate, a.audflag, a.audflagname, a.audstatus, a.audstatusname, a.lastaudtime, a.bargain, a.iscredit, a.oriprc, a.lastprc, a.b2bdisplay, a.b2bdisplayname, a.syn_date,a.goods, a.goodsname, a.outrate, a.producer, a.spec, a.sprdug,a.detailtype,a.detailtypename, a.grouptype, a.grouptypename, a.ghsynctype, a.ghsynctypename,a.cmsclienttype,a.cmsclienttypename FROM v_scm_price_exe AS a   where a.compid=@compid  and a.ownerid=@ownerid  $  ";


            MySqlCommand cmd = null;
            
            try
            {
                if (ConnectionTests() == -1)//打开mysql从库连接
                {
                    MessageBox.Show("连接从库失败!");
                    Environment.Exit(0);
                }
                //从库 ----2019-1-7---
                cmd = connections.CreateCommand();
                cmd.Connection = connections;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;


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
                //cmd.Parameters.AddWithValue("saledeptid", SessionDto.Empdeptid);
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

                    ScmPriceExe info = new ScmPriceExe();

                    info.Code = res["code"].ToString().Trim();
                    info.Name = res["name"].ToString().Trim();
                    info.Province = res["province"].ToString().Trim();
                    info.Provincename = res["provincename"].ToString().Trim();
                    info.City = res["city"].ToString().Trim();
                    info.Cityname = res["cityname"].ToString().Trim();
                    info.Area = res["area"].ToString().Trim();
                    info.Areaname = res["areaname"].ToString().Trim();
                    info.Region = res["region"].ToString().Trim();
                    info.Cstcode = res["cstcode"].ToString().Trim();
                    info.Cstname = res["cstname"].ToString().Trim();
                    info.Id = res["id"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Hdrid = res["hdrid"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Costrate = res["costrate"].ToString().Trim();
                    info.Begindate = res["begindate"].ToString().Trim();
                    info.Enddate = res["enddate"].ToString().Trim();
                    info.Goodtapid = res["goodtapid"].ToString().Trim();
                    info.Source = res["source"].ToString().Trim();
                    info.Type = res["type"].ToString().Trim();
                    info.Synctype = res["synctype"].ToString().Trim();
                    info.Stopflag = res["stopflag"].ToString().Trim();
                    info.Stopflagname = res["stopflagname"].ToString().Trim();
                    info.Createuser = res["createuser"].ToString().Trim();
                    info.Createusername = res["createusername"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Modifyuser = res["modifyuser"].ToString().Trim();
                    info.Modifyusername = res["modifyusername"].ToString().Trim();
                    info.Modifydate = res["modifydate"].ToString().Trim();
                    info.Audflag = res["audflag"].ToString().Trim();
                    info.Audflagname = res["audflagname"].ToString().Trim();
                    info.Audstatus = res["audstatus"].ToString().Trim();
                    info.Audstatusname = res["audstatusname"].ToString().Trim();
                    info.Lastaudtime = res["lastaudtime"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.Iscredit = res["iscredit"].ToString().Trim();
                    info.Oriprc = res["oriprc"].ToString().Trim();
                    info.Lastprc = res["lastprc"].ToString().Trim();
                    info.B2bdisplay = res["b2bdisplay"].ToString().Trim();
                    info.B2bdisplayname = res["b2bdisplayname"].ToString().Trim();
                    info.SynDate = res["syn_date"].ToString().Trim();
                    info.GoodsCode = res["goods"].ToString().Trim();
                    info.GoodsName = res["goodsname"].ToString().Trim();
                    info.Outrate = res["outrate"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Sprdug = res["sprdug"].ToString().Trim();
                    info.Detailtype = res["detailtype"].ToString().Trim();
                    info.Detailtypename = res["detailtypename"].ToString().Trim();
                    info.Grouptype = res["grouptype"].ToString().Trim();
                    info.Grouptypename = res["grouptypename"].ToString().Trim();
                    info.Ghsynctype = res["ghsynctype"].ToString().Trim();
                    info.Ghsynctypename = res["ghsynctypename"].ToString().Trim();
                    info.Cmsclienttype = res["cmsclienttype"].ToString().Trim();
                    info.Cmsclienttypename = res["cmsclienttypename"].ToString().Trim();

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

        //获取新增界面默认部门
        public string FGetGoodunifyDefaultdept(int compid,int ownerid, int goodid)
        {
            string defaultdept = "";
            string sql = "select f_get_goodunify_defaultdept(@compid,@ownerid,@goodid)as defaultdept";

            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
                cmd.Parameters.AddWithValue("goodid", goodid);

                MySqlDataReader res = cmd.ExecuteReader();
                if (res.Read())
                {
                    defaultdept = res["defaultdept"].ToString().Trim();
                }
                else
                {
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
            return defaultdept;
        }

        //新增统一价
        public int PScmGoodunifyInsert(int compid, int ownerid, int saledeptid, int goodid, double prc, double price, int empid, double costprc, double costprice, double costrate, string begindate, string enddate, double bottomprc, double bottomprice, string source, SPRetInfo retinfo)
        {

            MySqlCommand spCmd = null;
            int retCode = -1;
            try
            {
                spCmd = connection.CreateCommand();
                spCmd.Connection = connection;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "P_SCM_GOODUNIFY_INSERT";
                spCmd.CommandTimeout = 600;
                MySqlParameter[] parameters ={
                    new MySqlParameter("@in_compid",MySqlDbType.Int64),
                    new MySqlParameter("@in_ownerid",MySqlDbType.Int64),
                    new MySqlParameter("@in_saledeptid",MySqlDbType.Int64),
                    new MySqlParameter("@in_goodid",MySqlDbType.Int64),
                    new MySqlParameter("@in_prc",MySqlDbType.Double),
                    new MySqlParameter("@in_price",MySqlDbType.Double),
                    new MySqlParameter("@in_empid",MySqlDbType.Int64),
                    new MySqlParameter("@in_costprc",MySqlDbType.Double),
                    new MySqlParameter("@in_costprice",MySqlDbType.Double),
                    new MySqlParameter("@in_costrate",MySqlDbType.Double),
                    new MySqlParameter("@in_begindate",MySqlDbType.VarChar),
                    new MySqlParameter("@in_enddate",MySqlDbType.VarChar),
                    new MySqlParameter("@in_bottomprc",MySqlDbType.Double),
                    new MySqlParameter("@in_bottomprice",MySqlDbType.Double),
                    new MySqlParameter("@in_source",MySqlDbType.VarChar),
                    new MySqlParameter("@out_resultcode",MySqlDbType.VarChar),
                    new MySqlParameter("@out_resultmsg",MySqlDbType.VarChar),

                                              };

                parameters[0].Value = compid;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = ownerid;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = saledeptid;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = goodid;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = prc;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Value = price;
                parameters[5].Direction = ParameterDirection.Input;
                parameters[6].Value = empid;
                parameters[6].Direction = ParameterDirection.Input;
                parameters[7].Value = costprc;
                parameters[7].Direction = ParameterDirection.Input;
                parameters[8].Value = costprice;
                parameters[8].Direction = ParameterDirection.Input;
                parameters[9].Value = costrate;
                parameters[9].Direction = ParameterDirection.Input;
                parameters[10].Value = begindate;
                parameters[10].Direction = ParameterDirection.Input;
                parameters[11].Value = enddate;
                parameters[11].Direction = ParameterDirection.Input;
                parameters[12].Value = bottomprc;
                parameters[12].Direction = ParameterDirection.Input;
                parameters[13].Value = bottomprice;
                parameters[13].Direction = ParameterDirection.Input;
                parameters[14].Value = source;
                parameters[14].Direction = ParameterDirection.Input;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[15].Size = 8;
                parameters[15].Direction = ParameterDirection.Output;
                parameters[16].Size = 2048;
                parameters[16].Direction = ParameterDirection.Output;

                foreach (MySqlParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[15].Value.ToString().Trim();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[16].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[16].Value.ToString().Trim();
                }
                retinfo.result = parameters[15].Value.ToString().Trim();


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

        //修改统一价
        public int PScmGoodunifyUpdate(int compid, int ownerid, int saledeptid, int goodid, double prc, double price,string stopflag, int empid, double costprc, double costprice, double costrate, string begindate, string enddate, double bottomprc, double bottomprice, string source, SPRetInfo retinfo)
        {

            MySqlCommand spCmd = null;
            int retCode = -1;
            try
            {
                spCmd = connection.CreateCommand();
                spCmd.Connection = connection;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "P_SCM_GOODUNIFY_UPDATE";
                spCmd.CommandTimeout = 600;
                MySqlParameter[] parameters ={
                    new MySqlParameter("@in_compid",MySqlDbType.Int64),
                    new MySqlParameter("@in_ownerid",MySqlDbType.Int64),
                    new MySqlParameter("@in_saledeptid",MySqlDbType.Int64),
                    new MySqlParameter("@in_goodid",MySqlDbType.Int64),
                    new MySqlParameter("@in_prc",MySqlDbType.Double),
                    new MySqlParameter("@in_price",MySqlDbType.Double),
                    new MySqlParameter("@in_stopflag",MySqlDbType.VarChar),
                    new MySqlParameter("@in_empid",MySqlDbType.Int64),
                    new MySqlParameter("@in_costprc",MySqlDbType.Double),
                    new MySqlParameter("@in_costprice",MySqlDbType.Double),
                    new MySqlParameter("@in_costrate",MySqlDbType.Double),
                    new MySqlParameter("@in_begindate",MySqlDbType.VarChar),
                    new MySqlParameter("@in_enddate",MySqlDbType.VarChar),
                    new MySqlParameter("@in_bottomprc",MySqlDbType.Double),
                    new MySqlParameter("@in_bottomprice",MySqlDbType.Double),
                    new MySqlParameter("@in_source",MySqlDbType.VarChar),
                    new MySqlParameter("@out_resultcode",MySqlDbType.VarChar),
                    new MySqlParameter("@out_resultmsg",MySqlDbType.VarChar),

                                              };

                parameters[0].Value = compid;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = ownerid;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = saledeptid;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = goodid;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = prc;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Value = price;
                parameters[5].Direction = ParameterDirection.Input;
                parameters[6].Value = stopflag;
                parameters[6].Direction = ParameterDirection.Input;
                parameters[7].Value = empid;
                parameters[7].Direction = ParameterDirection.Input;
                parameters[8].Value = costprc;
                parameters[8].Direction = ParameterDirection.Input;
                parameters[9].Value = costprice;
                parameters[9].Direction = ParameterDirection.Input;
                parameters[10].Value = costrate;
                parameters[10].Direction = ParameterDirection.Input;
                parameters[11].Value = begindate;
                parameters[11].Direction = ParameterDirection.Input;
                parameters[12].Value = enddate;
                parameters[12].Direction = ParameterDirection.Input;
                parameters[13].Value = bottomprc;
                parameters[13].Direction = ParameterDirection.Input;
                parameters[14].Value = bottomprice;
                parameters[14].Direction = ParameterDirection.Input;
                parameters[15].Value = source;
                parameters[15].Direction = ParameterDirection.Input;
                /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/
                parameters[16].Size = 8;
                parameters[16].Direction = ParameterDirection.Output;
                parameters[17].Size = 2048;
                parameters[17].Direction = ParameterDirection.Output;

                foreach (MySqlParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }


                int ret = spCmd.ExecuteNonQuery();
                retinfo.num = parameters[16].Value.ToString().Trim();
                if (retinfo.num == "1")
                {
                    retinfo.msg = parameters[17].Value.ToString().Trim();
                }
                else
                {
                    retinfo.msg = parameters[17].Value.ToString().Trim();
                }
                retinfo.result = parameters[16].Value.ToString().Trim();


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

        //部门下拉视图
        public SortableBindingList<GoodunifyDeptmenu> GetGoodunifyDeptmenuList(int compid,int ownerid)
        {

            SortableBindingList<GoodunifyDeptmenu> infoList = new SortableBindingList<GoodunifyDeptmenu>();
            string sql = "SELECT a.compid, a.ownerid, a.SALEDEPTID, a.deptname FROM v_goodunify_deptmenu AS a  where a.compid=@compid  and a.ownerid=@ownerid";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;

                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
               

                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    GoodunifyDeptmenu info = new GoodunifyDeptmenu();

                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
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

        //新增同步客户组价格客户(operatype:1新增商品客户组定价和客户定价,2修改商品客户组定价，3新增客户明细定价，4，停用客户明细定价,5，修改客户明细定价(有客户组定价信息)，6，修改客户明细定价(没有客户组定价信息)
        public int PPrcGoodDtapInf(SortableBindingList<ScmPriceGoodtap> ScmPriceGoodtapList, SortableBindingList<ScmPriceExe> ScmPriceExeList, string deptid,string operatype, SPRetInfo retinfo)
        {

            string selectsql1 = "SELECT seq_nextval('seq_bacthid') as seqid";//调函数获取操作批次号ID

            string selectsql2 = "SELECT seq_nextval('goodtapid') as goodtapid";//调函数获取goodtapid

            string insertSql1 = "insert into scm_price_goodtap_temp (batchid,goodtapid, compid, ownerid, saledeptid, goodid, hdrid, deliveryfeerate, prc, price, createuser,costprctype, costprc, costprice, costrate, bottomprc, bottomprice, begindate,enddate, synctype, grouptype, operatype,pre_goodtapid,stopflag) VALUES (@batchid,@goodtapid, @compid, @ownerid, @saledeptid, @goodid, @hdrid, @deliveryfeerate, @prc, @price, @createuser,@costprctype, @costprc, @costprice, @costrate, @bottomprc, @bottomprice,@begindate, @enddate, @synctype, @grouptype, @operatype,@pregoodtapid,@stopflag)";

            string insertSql2 = "insert into scm_price_exe_temp (batchid, compid, ownerid, saledeptid, cstid, hdrid, goodid, prc, price, bottomprc, bottomprice, costprc, costprice, begindate, enddate,goodtapid, source, type, synctype, grouptype, costrate,prc_pri,stopflag) VALUES (@batchid, @compid, @ownerid, @saledeptid, @cstid, @hdrid, @goodid, @prc, @price, @bottomprc, @bottomprice, @costprc, @costprice, @begindate, @enddate,@goodtapid, @source, @type, @synctype, @grouptype, @costrate,f_get_hdr_priory(@hdrid1),@stopflag)";

            MySqlCommand sel1cmd = null;
            MySqlCommand sel2cmd = null;
            MySqlCommand inset1Cmd = null;
            MySqlCommand inset2Cmd = null;
            MySqlCommand sp1Cmd = null;
            MySqlCommand sp2Cmd = null;
            MySqlTransaction trans1 = null;
            //MySqlTransaction trans2 = null;
            int retCode = -1;

            string seqID = "0";
            string goodtapID = "0";
            try
            {

                sel1cmd = connection.CreateCommand();
                sel1cmd.Connection = connection;
                sel1cmd.CommandType = System.Data.CommandType.Text;
                sel1cmd.CommandTimeout = 3600;
                sel1cmd.CommandText = selectsql1.ToString();

                MySqlDataReader res1 = sel1cmd.ExecuteReader();
                if (res1.Read())
                    seqID = res1["seqid"].ToString().Trim();
                res1.Close();
                res1.Dispose();
                sel1cmd.Dispose();
                sel1cmd = null;

                if (operatype == "1" || operatype == "6")
                {
                    sel2cmd = connection.CreateCommand();
                    sel2cmd.Connection = connection;
                    sel2cmd.CommandType = System.Data.CommandType.Text;
                    sel2cmd.CommandTimeout = 3600;
                    sel2cmd.CommandText = selectsql2.ToString();

                    MySqlDataReader res2 = sel2cmd.ExecuteReader();
                    if (res2.Read())
                        goodtapID = res2["goodtapid"].ToString().Trim();
                    res2.Close();
                    res2.Dispose();
                    sel2cmd.Dispose();
                    sel2cmd = null;
                }


                trans1 = connection.BeginTransaction();
                int recCount = 0;
                if (operatype == "1" || operatype == "2" || operatype == "5" || operatype == "6")
                {
                    inset1Cmd = connection.CreateCommand();
                    inset1Cmd.Connection = connection;
                    inset1Cmd.CommandType = System.Data.CommandType.Text;
                    inset1Cmd.CommandText = insertSql1.ToString();
                    inset1Cmd.CommandTimeout = 3600;

                    inset1Cmd.Transaction = trans1;
                    
                    foreach (ScmPriceGoodtap selected in ScmPriceGoodtapList)
                    {
                        recCount++;

                        inset1Cmd.Parameters.Clear();
                        inset1Cmd.Parameters.AddWithValue("batchid", Int32.Parse(seqID));
                        if (operatype == "2" || operatype == "5")
                        {
                            inset1Cmd.Parameters.AddWithValue("goodtapid",Int32.Parse(selected.Goodtapid));
                        }
                        else
                        {
                            inset1Cmd.Parameters.AddWithValue("goodtapid", Int32.Parse(goodtapID));
                        }
                        inset1Cmd.Parameters.AddWithValue("compid", Int32.Parse(Properties.Settings.Default.COMPID));
                        inset1Cmd.Parameters.AddWithValue("ownerid", Int32.Parse(Properties.Settings.Default.OWNERID));
                        inset1Cmd.Parameters.AddWithValue("saledeptid", Int32.Parse(deptid));
                        inset1Cmd.Parameters.AddWithValue("goodid", selected.Goodid);
                        inset1Cmd.Parameters.AddWithValue("hdrid", selected.Hdrid);
                        if (selected.Pgdeliveryfeerate == "" || selected.Pgdeliveryfeerate == null)
                        {
                            inset1Cmd.Parameters.AddWithValue("deliveryfeerate", null);
                        }
                        else
                        {
                            inset1Cmd.Parameters.AddWithValue("deliveryfeerate", selected.Pgdeliveryfeerate);
                        }

                        if (selected.Prc == "" || selected.Prc == null)
                        {
                            inset1Cmd.Parameters.AddWithValue("prc", null);
                        }
                        else
                        {
                            inset1Cmd.Parameters.AddWithValue("prc", double.Parse(selected.Prc));
                        }
                        if (selected.Price == "" || selected.Price == null)
                        {
                            inset1Cmd.Parameters.AddWithValue("price", null);
                        }
                        else {
                            inset1Cmd.Parameters.AddWithValue("price", double.Parse(selected.Price));
                        }

                        
                        inset1Cmd.Parameters.AddWithValue("createuser", Int32.Parse(SessionDto.Empid));
                        inset1Cmd.Parameters.AddWithValue("costprctype", selected.Costprctype);
                        if (selected.Costprc == "" || selected.Costprc == null)
                        {
                            inset1Cmd.Parameters.AddWithValue("costprc", null);
                        }
                        else
                        {
                            inset1Cmd.Parameters.AddWithValue("costprc", double.Parse(selected.Costprc));
                        }
                        if (selected.Costprice == "" || selected.Costprice == null)
                        {
                            inset1Cmd.Parameters.AddWithValue("costprice", null);
                        }
                        else
                        {
                            inset1Cmd.Parameters.AddWithValue("costprice", double.Parse(selected.Costprice));
                        }
                        if (selected.Costrate == "" || selected.Costrate == null)
                        {
                            inset1Cmd.Parameters.AddWithValue("costrate", null);
                        }
                        else
                        {
                            inset1Cmd.Parameters.AddWithValue("costrate", double.Parse(selected.Costrate));
                        }
                        if (selected.Bottomprc == "" || selected.Bottomprc == null)
                        {
                            inset1Cmd.Parameters.AddWithValue("bottomprc", null);
                        }
                        else
                        {
                            inset1Cmd.Parameters.AddWithValue("bottomprc", double.Parse(selected.Bottomprc));
                        }
                        if (selected.Bottomprice == "" || selected.Bottomprice == null)
                        {
                            inset1Cmd.Parameters.AddWithValue("bottomprice", null);
                        }
                        else
                        {
                            inset1Cmd.Parameters.AddWithValue("bottomprice", double.Parse(selected.Bottomprice));
                        }
                        if (selected.Begindate == "" || selected.Begindate == null)
                        {
                            inset1Cmd.Parameters.AddWithValue("begindate", null);
                        }
                        else {
                            inset1Cmd.Parameters.AddWithValue("begindate", selected.Begindate);
                        }
                        if (selected.Enddate == "" || selected.Enddate == null)
                        {
                            inset1Cmd.Parameters.AddWithValue("enddate", null);
                        }
                        else
                        {
                            inset1Cmd.Parameters.AddWithValue("enddate", selected.Enddate);
                        }

                        inset1Cmd.Parameters.AddWithValue("synctype", selected.Synctype);
                        inset1Cmd.Parameters.AddWithValue("grouptype", selected.Grouptype);
                        if (operatype == "1")
                        {
                            inset1Cmd.Parameters.AddWithValue("operatype", "1");
                            inset1Cmd.Parameters.AddWithValue("stopflag", "00");
                        }
                        if (operatype == "2")
                        {
                            inset1Cmd.Parameters.AddWithValue("operatype", "2");
                            inset1Cmd.Parameters.AddWithValue("stopflag", selected.Stopflag);
                        }
                        if (operatype == "5" || operatype == "6")
                        {
                            inset1Cmd.Parameters.AddWithValue("operatype", "3");
                            inset1Cmd.Parameters.AddWithValue("stopflag", "00");
                            inset1Cmd.Parameters.AddWithValue("pregoodtapid", selected.PreGoodtapid);
                            
                        }
                        else
                        {
                            inset1Cmd.Parameters.AddWithValue("pregoodtapid", 0);
                        }
                        inset1Cmd.ExecuteNonQuery();
                    }

                    inset1Cmd.Dispose();
                    inset1Cmd = null;
                }
                //trans2 = connection.BeginTransaction();
                int recCount1 = 0;
                if (operatype == "1" || operatype == "2" || operatype == "3" || operatype == "4" || operatype == "5" || operatype == "6")
                {
                    inset2Cmd = connection.CreateCommand();
                    inset2Cmd.Connection = connection;
                    inset2Cmd.CommandType = System.Data.CommandType.Text;
                    inset2Cmd.CommandText = insertSql2.ToString();
                    inset2Cmd.CommandTimeout = 3600;

                    inset2Cmd.Transaction = trans1;

                    
                    foreach (ScmPriceExe selected in ScmPriceExeList)
                    {
                        recCount1++;
                        inset2Cmd.Parameters.Clear();
                        inset2Cmd.Parameters.AddWithValue("batchid", Int32.Parse(seqID));
                        inset2Cmd.Parameters.AddWithValue("compid", Int32.Parse(Properties.Settings.Default.COMPID));
                        inset2Cmd.Parameters.AddWithValue("ownerid", Int32.Parse(Properties.Settings.Default.OWNERID));
                        inset2Cmd.Parameters.AddWithValue("saledeptid", Int32.Parse(deptid));
                        inset2Cmd.Parameters.AddWithValue("cstid", selected.Cstid);
                        inset2Cmd.Parameters.AddWithValue("hdrid", selected.Hdrid);
                        inset2Cmd.Parameters.AddWithValue("goodid", selected.Goodid);
                        inset2Cmd.Parameters.AddWithValue("prc", double.Parse(selected.Prc));
                        inset2Cmd.Parameters.AddWithValue("price", double.Parse(selected.Price));
                        inset2Cmd.Parameters.AddWithValue("bottomprc", double.Parse(selected.Bottomprc));
                        inset2Cmd.Parameters.AddWithValue("bottomprice", double.Parse(selected.Bottomprice));
                        inset2Cmd.Parameters.AddWithValue("costprc", double.Parse(selected.Costprc));
                        inset2Cmd.Parameters.AddWithValue("costprice", double.Parse(selected.Costprice));
                        inset2Cmd.Parameters.AddWithValue("begindate", selected.Begindate);
                        inset2Cmd.Parameters.AddWithValue("enddate", selected.Enddate);
                        if (operatype == "2" || operatype == "3" || operatype == "4" || operatype == "5")
                        {
                            inset2Cmd.Parameters.AddWithValue("goodtapid", Int32.Parse(selected.Goodtapid));
                        }
                        else
                        {
                            inset2Cmd.Parameters.AddWithValue("goodtapid", Int32.Parse(goodtapID));

                        }
                        inset2Cmd.Parameters.AddWithValue("source", selected.Source);
                        inset2Cmd.Parameters.AddWithValue("type", selected.Type);
                        inset2Cmd.Parameters.AddWithValue("synctype", selected.Synctype);
                        inset2Cmd.Parameters.AddWithValue("grouptype", selected.Grouptype);
                        inset2Cmd.Parameters.AddWithValue("costrate", double.Parse(selected.Costrate));
                        inset2Cmd.Parameters.AddWithValue("hdrid1", double.Parse(selected.Hdrid));
                        if (operatype == "4")
                        {
                            inset2Cmd.Parameters.AddWithValue("stopflag", "99");
                        }
                        else {
                            inset2Cmd.Parameters.AddWithValue("stopflag", "00");
                        }
                        inset2Cmd.ExecuteNonQuery();
                    }
                }
                    trans1.Commit();
                    trans1.Dispose();
                    trans1 = null;

                    inset2Cmd.Dispose();
                    inset2Cmd = null;
                
                /*
                 * AI_SEQ_ID NUMBER, --程序前台ID
                   --AI_BARCODE_TYPE NUMBER; --条码类型
                   OUT_RTD    OUT NUMBER, --返回值，可以判断执行是否成功
                   OUT_ARMSG  OUT VARCHAR2, --返回执行说明
                   OUT_RESULT OUT VARCHAR2 --返回执行代码
                */
                if (operatype == "1" || operatype == "2")
                {
                    if (recCount < 1)
                    {
                        retinfo.num = "0";
                        retinfo.msg = "无数据";
                        retinfo.result = "没调用!";
                        return 0;
                    }
                }
                if (operatype == "2"||operatype == "3" ||operatype == "4")
                {
                    if (recCount1 < 1)
                    {
                        retinfo.num = "0";
                        retinfo.msg = "无数据";
                        retinfo.result = "没调用!";
                        return 0;
                    }
                }
                if (operatype == "1" || operatype == "2" || operatype == "5" || operatype == "6")
                {
                    sp1Cmd = connection.CreateCommand();
                    sp1Cmd.Connection = connection;
                    sp1Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sp1Cmd.CommandText = "P_PRC_GOODTAP_INF";
                    sp1Cmd.CommandTimeout = 600;
                    MySqlParameter[] parameters ={
                    new MySqlParameter("@in_batchid",MySqlDbType.Int64),
                    new MySqlParameter("@in_ownerid",MySqlDbType.Int64),
                    new MySqlParameter("@in_empid",MySqlDbType.Int64),
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
                    parameters[7].Size = 8;
                    parameters[7].Direction = ParameterDirection.Output;


                    foreach (MySqlParameter parameter in parameters)
                    {
                        sp1Cmd.Parameters.Add(parameter);
                    }


                    int ret = sp1Cmd.ExecuteNonQuery();
                    retinfo.num = parameters[3].Value.ToString().Trim();
                    if (retinfo.num == "1")
                    {
                        retinfo.count = parameters[4].Value.ToString().Trim();
                        retinfo.msg = parameters[5].Value.ToString().Trim();
                        retinfo.selflag = parameters[7].Value.ToString().Trim();
                    }
                    else
                    {
                        retinfo.count = parameters[4].Value.ToString().Trim();
                        retinfo.msg = parameters[5].Value.ToString().Trim();
                        retinfo.selflag = parameters[7].Value.ToString().Trim();
                    }
                    retinfo.result = parameters[6].Value.ToString().Trim();


                    sp1Cmd.Dispose();
                    sp1Cmd = null;
                    retCode = 0;
                }


                if (operatype == "3" || operatype == "4")
                {
                    sp2Cmd = connection.CreateCommand();
                    sp2Cmd.Connection = connection;
                    sp2Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sp2Cmd.CommandText = "P_PRC_EXE_INF_NEW";
                    sp2Cmd.CommandTimeout = 600;
                    MySqlParameter[] parameters ={
                    new MySqlParameter("@AN_BATCHID",MySqlDbType.Int64),
                    new MySqlParameter("@AN_OWNERID",MySqlDbType.Int64),
                    new MySqlParameter("@AN_EMPID",MySqlDbType.Int64),
                    new MySqlParameter("@AS_RTD",MySqlDbType.Int64),
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
                    parameters[6].Size = 8;
                    parameters[6].Direction = ParameterDirection.Output;


                    foreach (MySqlParameter parameter in parameters)
                    {
                        sp2Cmd.Parameters.Add(parameter);
                    }


                    int ret = sp2Cmd.ExecuteNonQuery();
                    retinfo.num = parameters[3].Value.ToString().Trim();
                    if (retinfo.num == "1")
                    {
                        retinfo.count = parameters[3].Value.ToString().Trim();
                        retinfo.msg = parameters[4].Value.ToString().Trim();
                        retinfo.selflag = parameters[6].Value.ToString().Trim();
                    }
                    else
                    {
                        retinfo.count = parameters[3].Value.ToString().Trim();
                        retinfo.msg = parameters[4].Value.ToString().Trim();
                        retinfo.selflag = parameters[6].Value.ToString().Trim();
                    }
                    retinfo.result = parameters[5].Value.ToString().Trim();

                    
                    sp2Cmd.Dispose();
                    sp2Cmd = null;
                    retCode = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");

                retCode = -1;

                if (sp1Cmd != null)
                {
                    sp1Cmd.Dispose();
                    sp1Cmd = null;
                }
                if (sp2Cmd != null)
                {
                    sp2Cmd.Dispose();
                    sp2Cmd = null;
                }
                //if (trans2 != null)
                //    trans2.Dispose();

            }
            finally 
            {
                if (sp1Cmd != null)
                {
                    sp1Cmd.Dispose();
                    sp1Cmd = null;
                }
                if (sp2Cmd != null)
                {
                    sp2Cmd.Dispose();
                    sp2Cmd = null;
                }
                //if (trans2 != null)
                //    trans2.Dispose();

            }

            return retCode;
        }


        //查询新新增客户信息
        public SortableBindingList<CstGroupDtl> GetSelClientsList(Dictionary<string, string> sqlkeydict)
        {

            SortableBindingList<CstGroupDtl> infoList = new SortableBindingList<CstGroupDtl>();
            string sql = "SELECT cstid, cstcode, cstname, region FROM v_sel_clients AS a where a.compid=@compid $";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.CommandText = sql.ToString();

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


        //查询商品价格异常信息
        public SortableBindingList<ScmPriceExeWait> GetScmPriceExeWaitList(Dictionary<string, string> sqlkeydict)
        {

            SortableBindingList<ScmPriceExeWait> infoList = new SortableBindingList<ScmPriceExeWait>();
            string sql = "SELECT hdrcode, hdrname, province, provincename, city, cityname, area, areaname, region, cstcode, cstname, goods, goodsname, spec, producer, outrate, sprdug, id, compid, ownerid, saledeptid, cstid, hdrid, goodid, prc, price, bottomprc, bottomprice, costprc, costprice, costrate, begindate, enddate, goodtapid, source, type, synctype, stopflag, stopflagname, createuser, createusername, createdate, modifyuser, modifyusername, modifydate, audflag, audflagname, audstatus, audstatusname, lastaudtime, bargain, iscredit, oriprc, lastprc, b2bdisplay, b2bdisplayname, syn_date, type_flag, type_flagname, origin, originname, ownchgid, beneficiate, affirm_flag, affirm_flagname, affirm_batchid,buyercode,buyername FROM v_scm_price_exe_wait where compid=@compid and ownerid=@ownerid and saledeptid=@saledeptid  $  LIMIT 0,360000";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;


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
                cmd.Parameters.AddWithValue("saledeptid", SessionDto.Empdeptid);
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

                    ScmPriceExeWait info = new ScmPriceExeWait();
                    //, ,  , , , , , , , , , , , , , , , , , , , , , , , 
                    info.Hdrcode = res["hdrcode"].ToString().Trim();
                    info.Hdrname = res["hdrname"].ToString().Trim();
                    info.Province = res["province"].ToString().Trim();
                    info.Provincename = res["provincename"].ToString().Trim();
                    info.City = res["city"].ToString().Trim();
                    info.Cityname = res["cityname"].ToString().Trim();
                    info.Area = res["area"].ToString().Trim();
                    info.Areaname = res["areaname"].ToString().Trim();
                    info.Region = res["region"].ToString().Trim();
                    info.Cstcode = res["cstcode"].ToString().Trim();
                    info.Cstname = res["cstname"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.Goodsname = res["goodsname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.Outrate = res["outrate"].ToString().Trim();
                    info.Sprdug = res["sprdug"].ToString().Trim();
                    info.Id = res["id"].ToString().Trim();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Cstid = res["cstid"].ToString().Trim();
                    info.Hdrid = res["hdrid"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim();
                    info.Prc = res["prc"].ToString().Trim();
                    info.Price = res["price"].ToString().Trim();
                    info.Bottomprc = res["bottomprc"].ToString().Trim();
                    info.Bottomprice = res["bottomprice"].ToString().Trim();
                    info.Costprc = res["costprc"].ToString().Trim();
                    info.Costprice = res["costprice"].ToString().Trim();
                    info.Costrate = res["costrate"].ToString().Trim();
                    info.Begindate = res["begindate"].ToString().Trim();
                    info.Enddate = res["enddate"].ToString().Trim();
                    info.Goodtapid = res["goodtapid"].ToString().Trim();
                    info.Source = res["source"].ToString().Trim();
                    info.Type = res["type"].ToString().Trim();
                    info.Synctype = res["synctype"].ToString().Trim();
                    info.Stopflag = res["stopflag"].ToString().Trim();
                    info.Stopflagname = res["stopflagname"].ToString().Trim();
                    info.Createuser = res["createuser"].ToString().Trim();
                    info.Createusername = res["createusername"].ToString().Trim();
                    info.Createdate = res["createdate"].ToString().Trim();
                    info.Modifyuser = res["modifyuser"].ToString().Trim();
                    info.Modifyusername = res["modifyusername"].ToString().Trim();
                    info.Modifydate = res["modifydate"].ToString().Trim();
                    info.Audflag = res["audflag"].ToString().Trim();
                    info.Audflagname = res["audflagname"].ToString().Trim();
                    info.Audstatusname = res["audstatusname"].ToString().Trim();
                    info.Lastaudtime = res["lastaudtime"].ToString().Trim();
                    info.Bargain = res["bargain"].ToString().Trim();
                    info.Iscredit = res["iscredit"].ToString().Trim();
                    info.Oriprc = res["oriprc"].ToString().Trim();
                    info.Lastprc = res["lastprc"].ToString().Trim();
                    info.B2bdisplay = res["b2bdisplay"].ToString().Trim();
                    info.B2bdisplayname = res["b2bdisplayname"].ToString().Trim();
                    info.SynDate = res["syn_date"].ToString().Trim();
                    info.TypeFlag = res["type_flag"].ToString().Trim();
                    info.TypeFlagname = res["type_flagname"].ToString().Trim();
                    info.Origin = res["origin"].ToString().Trim();
                    info.Originname = res["originname"].ToString().Trim();
                    info.Ownchgid = res["ownchgid"].ToString().Trim();
                    info.Beneficiate = res["beneficiate"].ToString().Trim();
                    info.AffirmFlag = res["affirm_flag"].ToString().Trim();
                    info.AffirmFlagname = res["affirm_flagname"].ToString().Trim();
                    info.AffirmBatchid = res["affirm_batchid"].ToString().Trim();
                    info.UnFlag = "00";
                    info.Buyercode = res["buyercode"].ToString().Trim();
                    info.Buyername = res["buyername"].ToString().Trim();

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


        //异常处理
        public int PPrcWaitInf(SortableBindingList<ScmPriceExeWait> ScmPriceExeWaitOKList, SortableBindingList<ScmPriceExeWait> ScmPriceExeWaitCancelList, SPRetInfo retinfo)
        {

            string selectsql1 = "SELECT seq_nextval('seq_bacthid') as seqid";//调函数获取操作批次号ID

            string updateSql1 = "UPDATE scm_price_exe_wait  set affirm_flag=@affirmflag, affirm_batchid=@affirmbathid WHERE id=@id";


            MySqlCommand sel1cmd = null;
            MySqlCommand update1Cmd = null;
            MySqlCommand update2Cmd = null;
            MySqlCommand spCmd = null;
            MySqlTransaction trans1 = null;
            //MySqlTransaction trans2 = null;
            int retCode = -1;

            string seqID = "0";
            try
            {

                sel1cmd = connection.CreateCommand();
                sel1cmd.Connection = connection;
                sel1cmd.CommandType = System.Data.CommandType.Text;
                sel1cmd.CommandTimeout = 3600;
                sel1cmd.CommandText = selectsql1.ToString();

                MySqlDataReader res1 = sel1cmd.ExecuteReader();
                if (res1.Read())
                    seqID = res1["seqid"].ToString().Trim();
                res1.Close();
                res1.Dispose();
                sel1cmd.Dispose();
                sel1cmd = null;


                trans1 = connection.BeginTransaction();
                int recCount = 0;
                if (ScmPriceExeWaitOKList.Count>0)
                {
                    update1Cmd = connection.CreateCommand();
                    update1Cmd.Connection = connection;
                    update1Cmd.CommandType = System.Data.CommandType.Text;
                    update1Cmd.CommandText = updateSql1.ToString();
                    update1Cmd.CommandTimeout = 3600;

                    update1Cmd.Transaction = trans1;


                    foreach (ScmPriceExeWait selected in ScmPriceExeWaitOKList)
                    {
                        recCount++;

                        update1Cmd.Parameters.Clear();
                        update1Cmd.Parameters.AddWithValue("affirmflag", 1);
                        update1Cmd.Parameters.AddWithValue("affirmbathid", Int32.Parse(seqID));
                        update1Cmd.Parameters.AddWithValue("id", Int32.Parse(selected.Id));

                        update1Cmd.ExecuteNonQuery();
                    }



                    update1Cmd.Dispose();
                    update1Cmd = null;
                }

                int recCount1 = 0;
                if (ScmPriceExeWaitCancelList.Count > 0)
                {
                    update2Cmd = connection.CreateCommand();
                    update2Cmd.Connection = connection;
                    update2Cmd.CommandType = System.Data.CommandType.Text;
                    update2Cmd.CommandText = updateSql1.ToString();
                    update2Cmd.CommandTimeout = 3600;

                    update2Cmd.Transaction = trans1;


                    foreach (ScmPriceExeWait selected in ScmPriceExeWaitCancelList)
                    {
                        recCount1++;

                        update2Cmd.Parameters.Clear();
                        update2Cmd.Parameters.AddWithValue("affirmflag", 2);
                        update2Cmd.Parameters.AddWithValue("affirmbathid", Int32.Parse(seqID));
                        update2Cmd.Parameters.AddWithValue("id", Int32.Parse(selected.Id));

                        update2Cmd.ExecuteNonQuery();
                    }



                    update2Cmd.Dispose();
                    update2Cmd = null;
                }

                trans1.Commit();
                trans1.Dispose();
                trans1 = null;

                
                /*
                 * AI_SEQ_ID NUMBER, --程序前台ID
                   --AI_BARCODE_TYPE NUMBER; --条码类型
                   OUT_RTD    OUT NUMBER, --返回值，可以判断执行是否成功
                   OUT_ARMSG  OUT VARCHAR2, --返回执行说明
                   OUT_RESULT OUT VARCHAR2 --返回执行代码
                */
                if (ScmPriceExeWaitOKList.Count > 0)
                {
                    if (recCount < 1)
                    {
                        retinfo.num = "0";
                        retinfo.msg = "无数据";
                        retinfo.result = "没调用!";
                        return 0;
                    }
                }
                if (ScmPriceExeWaitCancelList.Count > 0)
                {
                    if (recCount1 < 1)
                    {
                        retinfo.num = "0";
                        retinfo.msg = "无数据";
                        retinfo.result = "没调用!";
                        return 0;
                    }
                }
                spCmd = connection.CreateCommand();
                spCmd.Connection = connection;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "P_PRC_WAIT_INF";
                spCmd.CommandTimeout = 600;
                MySqlParameter[] parameters ={
                    new MySqlParameter("@AN_BATCHID",MySqlDbType.Int64),
                    new MySqlParameter("@AN_OWNERID",MySqlDbType.Int64),
                    new MySqlParameter("@AN_EMPID",MySqlDbType.Int64),
                    new MySqlParameter("@AS_RTD",MySqlDbType.Int64),
                    new MySqlParameter("@AS_COUNT",MySqlDbType.Int64),
                    new MySqlParameter("@AS_ARMSG",MySqlDbType.VarChar),
                    new MySqlParameter("@AS_RESULT",MySqlDbType.VarChar),
                                              };

                parameters[0].Value = Int32.Parse(seqID);
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Int32.Parse(Properties.Settings.Default.OWNERID);
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = Int32.Parse(SessionDto.Empid);
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Size = 8;
                parameters[3].Direction = ParameterDirection.Output;
                parameters[4].Size = 2048;
                parameters[4].Direction = ParameterDirection.Output;
                parameters[5].Size = 2048;
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6].Size = 2048;
                parameters[6].Direction = ParameterDirection.Output;


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


                spCmd.Dispose();
                spCmd = null;
                retCode = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");

                retCode = -1;

                if (spCmd != null)
                    spCmd.Dispose();
                //if (trans2 != null)
                //    trans2.Dispose();
                if (update1Cmd != null)
                    update1Cmd.Dispose();
                if (update2Cmd!=null)
                update2Cmd.Dispose();


            }
            finally
            {
                if (trans1 != null)
                {
                    trans1.Rollback();
                    trans1.Dispose();
                    trans1 = null;
                }
                if (spCmd != null)
                    spCmd.Dispose();
                

                //if (trans2 != null)
                //    trans2.Dispose();

            }

            return retCode;
        }


        //查询责任采购人员信息
        public SortableBindingList<PubEmpBuyer> GetPubEmpBuyerList()
        {

            SortableBindingList<PubEmpBuyer> infoList = new SortableBindingList<PubEmpBuyer>();
            string sql = "SELECT  compid,ownerid,empid,empcode,empname,roleid FROM v_pub_emp_buyer where compid=@compid and ownerid=@ownerid ";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;


                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
               
                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    PubEmpBuyer info = new PubEmpBuyer();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Empid = res["empid"].ToString().Trim();
                    info.Empcode = res["empcode"].ToString().Trim();
                    info.Empname = res["empname"].ToString().Trim();
                    info.Roleid = res["roleid"].ToString().Trim();
                    

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

        //------------------------2018-8-22------------------
        //销售价格视图
        public SortableBindingList<SalePriceEx> GetSalePriceExeList(Dictionary<string, string> sqlkeydict)
        {

            SortableBindingList<SalePriceEx> infoList = new SortableBindingList<SalePriceEx>();
            string sql = "SELECT a.compid,a.ownerid,a.saledeptid, a.cstid,a.cstcode,a.cstname,a.goodid,a.goods,a.goodsname,a.spec," +
                "a.producer,a.packnum," +
                "a.ratifier FROM v_scm_price_exe_sale AS a where a.compid=@compid  and a.ownerid=@ownerid  $ limit 0,10000";

            MySqlCommand cmd = null;
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;


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
                //cmd.Parameters.AddWithValue("saledeptid", SessionDto.Empdeptid);
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

                    SalePriceEx info = new SalePriceEx();

                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();//
                    info.Cstid = res["cstid"].ToString().Trim();   //
                    info.Cstcode = res["cstcode"].ToString().Trim();
                    info.Cstname = res["cstname"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim(); //
                    info.Goods = res["goods"].ToString().Trim();
                    info.GoodsName = res["goodsname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.Packnum = res["packnum"].ToString().Trim();
                    info.Ratifier = res["ratifier"].ToString().Trim();

                    
                    
                    //调用存储过程
                    spCmd = Conn_datacenter_cmszh.CreateCommand();
                    spCmd.Connection = Conn_datacenter_cmszh;
                    spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    spCmd.CommandText = "CMS_TEL.P_PRICE_SALE";
                    spCmd.CommandTimeout = 600;

                    OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_SALEDEPTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_GOODID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_LASTSOPRC",OracleDbType.Long,ParameterDirection.Output),
                    new OracleParameter("OUT_BUYER",OracleDbType.Varchar2,ParameterDirection.Output),
                    new OracleParameter("OUT_PLANBUYER",OracleDbType.Varchar2,ParameterDirection.Output)
                    };

                    parameters[0].Value = Int64.Parse(info.Compid);
                    parameters[1].Value = Int64.Parse(info.Ownerid);
                    parameters[2].Value = Int64.Parse(info.Saledeptid);
                    parameters[3].Value = Int64.Parse(info.Cstid);
                    parameters[4].Value = Int64.Parse(info.Goodid);
                    /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/

                    parameters[5].Size = 2048;
                    parameters[6].Size = 2048;
                    parameters[7].Size = 2048;
                    foreach (OracleParameter parameter in parameters)
                    {
                        spCmd.Parameters.Add(parameter);
                    }
                    spCmd.ExecuteNonQuery();
                    info.Lastsoprc= parameters[5].Value.ToString().Trim();
                    info.Buyer= parameters[6].Value.ToString().Trim();
                    info.Planbuyer= parameters[7].Value.ToString().Trim();

                    spCmd.Dispose();
                    spCmd = null;

                  
                    //info.Prc = parameters[6].Value.ToString().Trim();
                    //info.Price = parameters[7].Value.ToString().Trim();
                    //info.Costrate = parameters[12].Value.ToString().Trim();
                    //info.Msg = parameters[17].Value.ToString().Trim();


                    infoList.Add(info);
                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                cmd = null;

                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
            return infoList;
        }
        //查询是否存在商品代码
        public string getGoodsCounts(string goods) {
            string sql = "select goods from pub_waredict where GOODS =@goods";
            string str = "";
            MySqlCommand cmd = null;
            try {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;

                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("goods", goods);

                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read()) {
                    str = res["goods"].ToString().Trim();
                }
                res.Close();
                res = null;
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(),"错误信息");

            } finally {
                if (cmd!=null) {
                    cmd.Dispose();
                }
            }
            return str;
        }
        //查询是否存在客户代码
        public string getCodeCounts(string clientCode)
        {
            string sql = "select cstcode from pub_clients where cstcode =@cstcode";
            string str = "";
            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;

                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("cstcode", clientCode);

                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    str = res["cstcode"].ToString().Trim();
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
                {
                    cmd.Dispose();
                }
            }
            return str;
        }
        //销售价格视图
        public SortableBindingList<SalePriceEx> GetSalePriceExeLists(Dictionary<string, string> sqlkeydict,string goods)
        {

            SortableBindingList<SalePriceEx> infoList = new SortableBindingList<SalePriceEx>();
            string sql = "SELECT e.compid,e.ownerid,e.saledeptid,`c`.`cstid` AS `cstid`,`c`.`cstcode` AS `cstcode`,`c`.`cstname` AS `cstname`,`c`.`hdrcode` AS `hdrcode`,`c`.`hdrname` AS `hdrname`,`c`.`region` AS `region`,`w`.`goodid` AS `goodid`,`w`.`goods` AS `goods`,`w`.`name` AS `goodsname`,`w`.`spec` AS `spec`,`w`.`producer` AS `producer`,`w`.`packnum` AS `packnum`,`w`.`ratifier` AS `ratifier` FROM scm_price_exe e,v_sel_clients_owner C,v_sel_waredict_owner W WHERE e.compid = C.compid AND e.ownerid = C.ownerid AND e.cstid = C.cstid AND e.compid = W.compid AND e.ownerid = e.ownerid AND e.GOODID = W.GOODID AND e.compid =@compid AND e.ownerid =@ownerid AND W.goods=@goods $";

            MySqlCommand cmd = null;
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                if (ConnectionTests() == -1)//打开mysql从库连接
                {
                    MessageBox.Show("连接从库失败!");
                    Environment.Exit(0);
                }
                //从库 ----2019-1-7---
                cmd = connections.CreateCommand();
                cmd.Connection = connections;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;


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
                cmd.Parameters.AddWithValue("goods", goods);
                //cmd.Parameters.AddWithValue("saledeptid", SessionDto.Empdeptid);
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

                    SalePriceEx info = new SalePriceEx();

                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();//
                    info.Cstid = res["cstid"].ToString().Trim();   //
                    info.Cstcode = res["cstcode"].ToString().Trim();
                    info.Cstname = res["cstname"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim(); //
                    info.Goods = res["goods"].ToString().Trim();
                    info.GoodsName = res["goodsname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.Packnum = res["packnum"].ToString().Trim();
                    info.Ratifier = res["ratifier"].ToString().Trim();



                    //调用存储过程
                    spCmd = Conn_datacenter_cmszh.CreateCommand();
                    spCmd.Connection = Conn_datacenter_cmszh;
                    spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    spCmd.CommandText = "CMS_TEL.P_PRICE_SALE";
                    spCmd.CommandTimeout = 600;

                    OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_SALEDEPTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_GOODID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_LASTSOPRC",OracleDbType.Long,ParameterDirection.Output),
                    new OracleParameter("OUT_BUYER",OracleDbType.Varchar2,ParameterDirection.Output),
                    new OracleParameter("OUT_PLANBUYER",OracleDbType.Varchar2,ParameterDirection.Output)
                    };

                    parameters[0].Value = Int64.Parse(info.Compid);
                    parameters[1].Value = Int64.Parse(info.Ownerid);
                    parameters[2].Value = Int64.Parse(info.Saledeptid);
                    parameters[3].Value = Int64.Parse(info.Cstid);
                    parameters[4].Value = Int64.Parse(info.Goodid);
                    /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/

                    parameters[5].Size = 2048;
                    parameters[6].Size = 2048;
                    parameters[7].Size = 2048;
                    foreach (OracleParameter parameter in parameters)
                    {
                        spCmd.Parameters.Add(parameter);
                    }
                    spCmd.ExecuteNonQuery();
                    info.Lastsoprc = parameters[5].Value.ToString().Trim();
                    info.Buyer = parameters[6].Value.ToString().Trim();
                    info.Planbuyer = parameters[7].Value.ToString().Trim();

                    spCmd.Dispose();
                    spCmd = null;


                    //info.Prc = parameters[6].Value.ToString().Trim();
                    //info.Price = parameters[7].Value.ToString().Trim();
                    //info.Costrate = parameters[12].Value.ToString().Trim();
                    //info.Msg = parameters[17].Value.ToString().Trim();


                    infoList.Add(info);
                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                cmd = null;

                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
            return infoList;
        }

        //销售价格视图
        public SortableBindingList<SalePriceEx> GetSalePriceExeListss(Dictionary<string, string> sqlkeydict,string cstcode)
        {

            SortableBindingList<SalePriceEx> infoList = new SortableBindingList<SalePriceEx>();
            //string sql = "SELECT T.COMPID,T.OWNERID,T.SALEDEPTID,c.cstid,c.cstcode,c.cstname,c.hdrcode,c.hdrname,c.region,w.goodid,w.goods,w.name,w.spec,w.producer,w.packnum,w.ratifier FROM(SELECT e.compid,e.ownerid,e.saledeptid,e.cstid,e.goodid FROM scm_price_exe e WHERE e.cstid = (SELECT CSTID FROM pub_clients PC WHERE PC.cstcode =@cstcode) AND COMPID =@compid AND OWNERID =@ownerid UNION SELECT g.compid,g.ownerid,`f_get_goodunify_defaultdept` (`g`.`compid`,`g`.`ownerid`,`g`.`goodid`) AS `saledeptid`,(SELECT CSTID FROM pub_clients PC WHERE PC.cstcode =@cstid) AS cstid, g.goodid FROM scm_price_goodunify g WHERE g.stopflag = '00' AND compid = @compid AND OWNERID = @ownerid) T,v_sel_clients_owner C,v_sel_waredict_owner W WHERE T.compid = C.compid AND T.ownerid = C.ownerid AND T.cstid = C.cstid AND T.compid = W.compid AND T.ownerid = W.ownerid AND T.GOODID = W.GOODID $ ";
            string sql = "SELECT T.COMPID,T.OWNERID,T.SALEDEPTID,`c`.`cstid` AS `cstid`,`c`.`cstcode` AS `cstcode`,`c`.`cstname` AS `cstname`,`c`.`hdrcode` AS `hdrcode`,`c`.`hdrname` AS `hdrname`,`c`.`region` AS `region`,`w`.`goodid` AS `goodid`,`w`.`goods` AS `goods`,`w`.`name` AS `goodsname`,`w`.`spec` AS `spec`,`w`.`producer` AS `producer`,`w`.`packnum` AS `packnum`,`w`.`ratifier` AS `ratifier` FROM(SELECT e.compid,e.ownerid,e.saledeptid,e.cstid,e.goodid FROM scm_price_exe e WHERE e.cstid = (SELECT CSTID FROM pub_clients PC WHERE PC.cstcode =@cstcode) AND COMPID =@compid AND OWNERID =@ownerid UNION SELECT g.compid,g.ownerid,`f_get_goodunify_defaultdept` (`g`.`compid`,`g`.`ownerid`,`g`.`goodid`) AS `saledeptid`,(SELECT CSTID FROM pub_clients PC WHERE PC.cstcode =@cstcode) AS cstid, g.goodid FROM scm_price_goodunify g WHERE g.stopflag = '00' AND compid = @compid AND OWNERID = @ownerid) T,v_sel_clients_owner C,v_sel_waredict_owner W WHERE T.compid = C.compid AND T.ownerid = C.ownerid AND T.cstid = C.cstid AND T.compid = W.compid AND T.ownerid = W.ownerid AND T.GOODID = W.GOODID $ ";

            MySqlCommand cmd = null;
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                if (ConnectionTests() == -1)//打开mysql从库连接
                {
                    MessageBox.Show("连接从库失败!");
                    Environment.Exit(0);
                }

                //从库 ----2019-1-7---
                cmd = connections.CreateCommand();
                cmd.Connection = connections;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;


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
                cmd.Parameters.AddWithValue("cstcode", cstcode);
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
                //cmd.Parameters.AddWithValue("saledeptid", SessionDto.Empdeptid);
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

                    SalePriceEx info = new SalePriceEx();

                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();//
                    info.Cstid = res["cstid"].ToString().Trim();   //
                    info.Cstcode = res["cstcode"].ToString().Trim();
                    info.Cstname = res["cstname"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim(); //
                    info.Goods = res["goods"].ToString().Trim();
                    info.GoodsName = res["goodsname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.Packnum = res["packnum"].ToString().Trim();
                    info.Ratifier = res["ratifier"].ToString().Trim();



                    //调用存储过程
                    spCmd = Conn_datacenter_cmszh.CreateCommand();
                    spCmd.Connection = Conn_datacenter_cmszh;
                    spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    spCmd.CommandText = "CMS_TEL.P_PRICE_SALE";
                    spCmd.CommandTimeout = 600;

                    OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_SALEDEPTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_GOODID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_LASTSOPRC",OracleDbType.Long,ParameterDirection.Output),
                    new OracleParameter("OUT_BUYER",OracleDbType.Varchar2,ParameterDirection.Output),
                    new OracleParameter("OUT_PLANBUYER",OracleDbType.Varchar2,ParameterDirection.Output)
                    };

                    parameters[0].Value = Int64.Parse(info.Compid);
                    parameters[1].Value = Int64.Parse(info.Ownerid);
                    parameters[2].Value = Int64.Parse(info.Saledeptid);
                    parameters[3].Value = Int64.Parse(info.Cstid);
                    parameters[4].Value = Int64.Parse(info.Goodid);
                    /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/

                    parameters[5].Size = 2048;
                    parameters[6].Size = 2048;
                    parameters[7].Size = 2048;
                    foreach (OracleParameter parameter in parameters)
                    {
                        spCmd.Parameters.Add(parameter);
                    }
                    spCmd.ExecuteNonQuery();
                    info.Lastsoprc = parameters[5].Value.ToString().Trim();
                    info.Buyer = parameters[6].Value.ToString().Trim();
                    info.Planbuyer = parameters[7].Value.ToString().Trim();

                    spCmd.Dispose();
                    spCmd = null;


                    //info.Prc = parameters[6].Value.ToString().Trim();
                    //info.Price = parameters[7].Value.ToString().Trim();
                    //info.Costrate = parameters[12].Value.ToString().Trim();
                    //info.Msg = parameters[17].Value.ToString().Trim();


                    infoList.Add(info);
                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                cmd = null;

                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
            return infoList;
        }
        //销售价格视图
        public SortableBindingList<SalePriceEx> GetSalePriceExeListsss(Dictionary<string, string> sqlkeydict, string cstcode,string goods)
        {

            SortableBindingList<SalePriceEx> infoList = new SortableBindingList<SalePriceEx>();
            string sql = "SELECT T.COMPID,T.OWNERID,T.SALEDEPTID,`c`.`cstid` AS `cstid`,`c`.`cstcode` AS `cstcode`,`c`.`cstname` AS `cstname`,`c`.`hdrcode` AS `hdrcode`,`c`.`hdrname` AS `hdrname`,`c`.`region` AS `region`,`w`.`goodid` AS `goodid`,`w`.`goods` AS `goods`,`w`.`name` AS `goodsname`,`w`.`spec` AS `spec`,`w`.`producer` AS `producer`,`w`.`packnum` AS `packnum`,`w`.`ratifier` AS `ratifier` FROM(SELECT e.compid,e.ownerid,e.saledeptid,e.cstid,e.goodid FROM scm_price_exe e WHERE e.cstid = (SELECT CSTID FROM pub_clients PC WHERE PC.cstcode =@cstcode)" +
                " AND GOODID = ( SELECT GOODID FROM pub_waredict WW WHERE WW.GOODS =@goods)" +
                " AND COMPID =@compid AND OWNERID =@ownerid UNION SELECT g.compid,g.ownerid,`f_get_goodunify_defaultdept` (`g`.`compid`,`g`.`ownerid`,`g`.`goodid`) AS `saledeptid`,(SELECT CSTID FROM pub_clients PC WHERE PC.cstcode =@cstcode) AS cstid, g.goodid FROM scm_price_goodunify g WHERE g.stopflag = '00' " +
                " AND GOODID = (SELECT GOODID FROM pub_waredict WW WHERE WW.GOODS =@goods)" +
                " AND compid = @compid AND OWNERID = @ownerid) T,v_sel_clients_owner C,v_sel_waredict_owner W WHERE T.compid = C.compid AND T.ownerid = C.ownerid AND T.cstid = C.cstid AND T.compid = W.compid AND T.ownerid = W.ownerid AND T.GOODID = W.GOODID $ ";

            MySqlCommand cmd = null;
            OracleCommand spCmd = null;
            OracleTransaction trans = null;
            try
            {
                if (ConnectionTests() == -1)//打开mysql从库连接
                {
                    MessageBox.Show("连接从库失败!");
                    Environment.Exit(0);
                }
                //从库 ----2019-1-7---
                cmd = connections.CreateCommand();
                cmd.Connection = connections;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 3600;


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
                cmd.Parameters.AddWithValue("goods", goods);
                cmd.Parameters.AddWithValue("cstcode", cstcode);
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
                //cmd.Parameters.AddWithValue("saledeptid", SessionDto.Empdeptid);
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

                    SalePriceEx info = new SalePriceEx();

                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();//
                    info.Cstid = res["cstid"].ToString().Trim();   //
                    info.Cstcode = res["cstcode"].ToString().Trim();
                    info.Cstname = res["cstname"].ToString().Trim();
                    info.Goodid = res["goodid"].ToString().Trim(); //
                    info.Goods = res["goods"].ToString().Trim();
                    info.GoodsName = res["goodsname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.Packnum = res["packnum"].ToString().Trim();
                    info.Ratifier = res["ratifier"].ToString().Trim();



                    //调用存储过程
                    spCmd = Conn_datacenter_cmszh.CreateCommand();
                    spCmd.Connection = Conn_datacenter_cmszh;
                    spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    spCmd.CommandText = "CMS_TEL.P_PRICE_SALE";
                    spCmd.CommandTimeout = 600;

                    OracleParameter[] parameters ={
                    new OracleParameter("IN_COMPID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_OWNERID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_SALEDEPTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_CSTID",OracleDbType.Int64,ParameterDirection.Input),
                    new OracleParameter("IN_GOODID",OracleDbType.Int64,ParameterDirection.Input),

                    new OracleParameter("OUT_LASTSOPRC",OracleDbType.Long,ParameterDirection.Output),
                    new OracleParameter("OUT_BUYER",OracleDbType.Varchar2,ParameterDirection.Output),
                    new OracleParameter("OUT_PLANBUYER",OracleDbType.Varchar2,ParameterDirection.Output)
                    };

                    parameters[0].Value = Int64.Parse(info.Compid);
                    parameters[1].Value = Int64.Parse(info.Ownerid);
                    parameters[2].Value = Int64.Parse(info.Saledeptid);
                    parameters[3].Value = Int64.Parse(info.Cstid);
                    parameters[4].Value = Int64.Parse(info.Goodid);
                    /*Oracle For .Net has Bug: OUTPUT parameter must set size like the following satetment!!!!!*/

                    parameters[5].Size = 2048;
                    parameters[6].Size = 2048;
                    parameters[7].Size = 2048;
                    foreach (OracleParameter parameter in parameters)
                    {
                        spCmd.Parameters.Add(parameter);
                    }
                    spCmd.ExecuteNonQuery();
                    info.Lastsoprc = parameters[5].Value.ToString().Trim();
                    info.Buyer = parameters[6].Value.ToString().Trim();
                    info.Planbuyer = parameters[7].Value.ToString().Trim();

                    spCmd.Dispose();
                    spCmd = null;


                    //info.Prc = parameters[6].Value.ToString().Trim();
                    //info.Price = parameters[7].Value.ToString().Trim();
                    //info.Costrate = parameters[12].Value.ToString().Trim();
                    //info.Msg = parameters[17].Value.ToString().Trim();


                    infoList.Add(info);
                }
                res.Close();
                res = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误信息");
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                cmd = null;

                if (trans != null)
                    trans.Dispose();

                if (spCmd != null)
                    spCmd.Dispose();
            }
            return infoList;
        }
        public SortableBindingList<SalePriceEx> GetSaleExe(SortableBindingList<SalePriceEx> ScmPriceExeList)
        {
            MySqlCommand spCmd = null;
            SortableBindingList<SalePriceEx> infoList = new SortableBindingList<SalePriceEx>();
            foreach (SalePriceEx info in ScmPriceExeList) {
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
                    spCmd.CommandTimeout = 600;
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

                    parameters[0].Value = Int64.Parse(info.Compid);
                    parameters[0].Direction = ParameterDirection.Input;
                    parameters[1].Value = Int64.Parse(info.Ownerid);
                    parameters[1].Direction = ParameterDirection.Input;
                    parameters[2].Value = Int64.Parse(info.Saledeptid);
                    parameters[2].Direction = ParameterDirection.Input;
                    parameters[3].Value = Int64.Parse(info.Cstid);
                    parameters[3].Direction = ParameterDirection.Input;
                    parameters[4].Value = Int64.Parse(info.Goodid);
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
                    info.Prcresultcode = parameters[16].Value.ToString().Trim();
                    if (info.Prcresultcode == "1")
                    {
                        info.Prc = parameters[6].Value.ToString().Trim();
                        info.Price = parameters[7].Value.ToString().Trim();
                        info.Costrate = parameters[12].Value.ToString().Trim();
                        info.Prcresultcode = parameters[16].Value.ToString().Trim();
                        info.Msg = parameters[17].Value.ToString().Trim();
                    }
                    else
                    {
                        info.Prcresultcode = parameters[16].Value.ToString().Trim();
                        info.Msg = parameters[17].Value.ToString().Trim();
                    }

                    
                    spCmd.Dispose();
                    spCmd = null;

                    infoList.Add(info);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "错误信息");
                }
                finally
                {
                    if (spCmd != null)
                        spCmd.Dispose();
                }
            }
            return infoList;
        }

        //--------线上品种屏蔽-------------------2018-11-20-------------
        public List<PriceShield> GetPriceShields() {
            List<PriceShield> infolist = new List<PriceShield>();
            string selSql = "";
            if (SessionDto.Emproleid == "108")
            {
                selSql = "select id,prodname,goods,goodsname,spec,producer,createusercode,createusername,createtime from v_scm_b2bgoods_offline where createusercode=@createusercode";
            }
            else {
                selSql = "select id,prodname,goods,goodsname,spec,producer,createusercode,createusername,createtime from v_scm_b2bgoods_offline";
            }

            MySqlCommand cmd = null;
            try {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 400;

                cmd.CommandText = selSql.ToString();
                if (SessionDto.Emproleid == "108") {
                    cmd.Parameters.AddWithValue("createusercode", SessionDto.Empcode);
                }             
                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    PriceShield info = new PriceShield();

                    info.Id = res["id"].ToString().Trim();
                    info.ProdName = res["prodname"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.GoodsName = res["goodsname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.CreateUserCode = res["createusercode"].ToString().Trim();
                    info.CreateUserName = res["createusername"].ToString().Trim();
                    info.CreateTime = res["createtime"].ToString().Trim();
                    infolist.Add(info);
                }
                res.Close();
                res = null;
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(),"错误信息");

            } finally {
                if (cmd!=null) {
                    cmd.Dispose();
                }

            }
            return infolist;
        }

        //--------线上品种屏蔽-------------------2018-11-20-------------
        public List<PriceShield> GetPriceShield(Dictionary<string,string>sqlkeydict)
        {
            List<PriceShield> infolist = new List<PriceShield>();
            string selSql = "";
            if (SessionDto.Emproleid == "108")
            {
                 selSql = "select id,prodname,goods,goodsname,spec,producer,createusercode,createusername,createtime from v_scm_b2bgoods_offline where createusercode=@createusercode$";
            }
            else {
                selSql = "select id,prodname,goods,goodsname,spec,producer,createusercode,createusername,createtime from v_scm_b2bgoods_offline where 1=1$";

            }
            
            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 400;

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

                selSql = selSql.Replace("$", whereStr);
                cmd.CommandText = selSql.ToString();
                if (SessionDto.Emproleid == "108")
                {
                    cmd.Parameters.AddWithValue("createusercode", SessionDto.Empcode);
                }
                //cmd.Parameters.AddWithValue("saledeptid", SessionDto.Empdeptid);
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

                cmd.CommandText = selSql.ToString();
                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {

                    PriceShield info = new PriceShield();

                    info.Id = res["id"].ToString().Trim();
                    info.ProdName = res["prodname"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.GoodsName = res["goodsname"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    info.CreateUserCode = res["createusercode"].ToString().Trim();
                    info.CreateUserName = res["createusername"].ToString().Trim();
                    info.CreateTime = res["createtime"].ToString().Trim();
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
                {
                    cmd.Dispose();
                }

            }
            return infolist;
        }

        //查询商品
        public List<GoodsDetails> GetGoodsShield(Dictionary<string,string>sqlkeydict) {
            List<GoodsDetails> infolist = new List<GoodsDetails>();
            string selSql = "select goodid,goods,name,spec,producer from v_sel_waredict where 1=1$";
            MySqlCommand cmd = null;
            try {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 400;

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

                selSql = selSql.Replace("$", whereStr);
                cmd.CommandText = selSql.ToString();
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

                cmd.CommandText = selSql.ToString();
                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    GoodsDetails info = new GoodsDetails();
                    info.GoodId = res["goodid"].ToString().Trim();
                    info.Goods = res["goods"].ToString().Trim();
                    info.GoodsName = res["name"].ToString().Trim();
                    info.Spec = res["spec"].ToString().Trim();
                    info.Producer = res["producer"].ToString().Trim();
                    infolist.Add(info);
                }
                res.Close();
                res = null;

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(),"错误信息");
            } finally {

                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            return infolist;
        }

        //新增
        public int AddBtbPriceShieldInfo(string goodid, string prodname, string goods, string goodsname, string spec, string producer, SPRetInfo retinfo) {

            MySqlCommand spCmd = null;
            int retCode = -1;
            try {
                spCmd = connection.CreateCommand();
                spCmd.Connection = connection;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "p_scm_offline_insert";
                spCmd.CommandTimeout = 600;
                MySqlParameter[] parameters ={
                    new MySqlParameter("@in_compid",MySqlDbType.Int64),
                    new MySqlParameter("@in_ownerid",MySqlDbType.Int64),
                    new MySqlParameter("@in_saledeptid",MySqlDbType.Int64),
                    new MySqlParameter("@in_goodid",MySqlDbType.Int64),
                    new MySqlParameter("@in_prodname",MySqlDbType.VarChar),
                    new MySqlParameter("@in_goods",MySqlDbType.VarChar),
                    new MySqlParameter("@in_goodsname",MySqlDbType.VarChar),
                    new MySqlParameter("@in_spec",MySqlDbType.VarChar),
                    new MySqlParameter("@in_producer",MySqlDbType.VarChar),
                    new MySqlParameter("@createuserid",MySqlDbType.Int64),
                    new MySqlParameter("@createusercode",MySqlDbType.VarChar),
                    new MySqlParameter("@createusername",MySqlDbType.VarChar),

                    new MySqlParameter("@out_resultcode",MySqlDbType.Int64),
                    new MySqlParameter("@out_resultmsg",MySqlDbType.VarChar)

                };

                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = Int64.Parse(SessionDto.Empdeptid);
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = Int64.Parse(goodid);
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = prodname;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Value = goods;
                parameters[5].Direction = ParameterDirection.Input;
                parameters[6].Value = goodsname;
                parameters[6].Direction = ParameterDirection.Input;
                parameters[7].Value = spec;
                parameters[7].Direction = ParameterDirection.Input;
                parameters[8].Value = producer;
                parameters[8].Direction = ParameterDirection.Input;
                parameters[9].Value =Int64.Parse(SessionDto.Empid);
                parameters[9].Direction = ParameterDirection.Input;
                parameters[10].Value = SessionDto.Empcode;
                parameters[10].Direction = ParameterDirection.Input;
                parameters[11].Value = SessionDto.Empname;
                parameters[11].Direction = ParameterDirection.Input;

                parameters[12].Size = 200;
                parameters[12].Direction = ParameterDirection.Output;
                parameters[13].Size = 200;//
                parameters[13].Direction = ParameterDirection.Output;

                foreach (MySqlParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();

                retinfo.num = parameters[12].Value.ToString().Trim();
                retinfo.msg = parameters[13].Value.ToString().Trim();

                spCmd.Dispose();
                spCmd = null;


            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(),"错误信息");

            } finally {
                if (spCmd!=null) {
                    spCmd.Dispose();
                }

            }
            return retCode;
        }
        //删除
        public int DelPriceShield(SortableBindingList<DelTemp> DelClientList,SPRetInfo retinfo) {

            string selectSql = "SELECT seq_nextval('seq_bacthid') as seqid";//调函数获取操作批次号ID
            string insertSql = "insert into scm_b2bgoods_offline_deltemp(batchid,id)VALUES(@batchid,@id)";//插入临时表
        
            MySqlCommand selCmd = null;
            MySqlCommand insCmd = null;
            MySqlCommand spCmd = null;
            MySqlTransaction trans = null;

            string seqID = "0";
            int retCode = 0;

            try {
                //1.读取批次id
                selCmd = connection.CreateCommand();
                selCmd.Connection = connection;
                selCmd.CommandType = System.Data.CommandType.Text;
                selCmd.CommandTimeout = 3600;
                selCmd.CommandText = selectSql.ToString();

                MySqlDataReader res1 = selCmd.ExecuteReader();
                if (res1.Read())
                    seqID = res1["seqid"].ToString().Trim();
                res1.Close();
                res1.Dispose();
                selCmd.Dispose();
                selCmd = null;

                //2.插表
                trans = connection.BeginTransaction();
                int recCount = 0;
                insCmd = connection.CreateCommand();
                insCmd.Connection = connection;
                insCmd.CommandType = System.Data.CommandType.Text;
                insCmd.CommandText = insertSql.ToString();
                insCmd.CommandTimeout = 3600;

                insCmd.Transaction = trans;

                foreach (DelTemp selected in DelClientList)
                {
                    recCount++;

                    insCmd.Parameters.Clear();
                    insCmd.Parameters.AddWithValue("batchid", Int32.Parse(seqID));
                    insCmd.Parameters.AddWithValue("id", Int32.Parse(selected.RelateId));

                    insCmd.ExecuteNonQuery();
                }
                trans.Commit();
                trans.Dispose();
                trans = null;

                insCmd.Dispose();
                insCmd = null;

                //3.调动存储过程
                spCmd = connection.CreateCommand();
                spCmd.Connection = connection;
                spCmd.CommandType = System.Data.CommandType.StoredProcedure;
                spCmd.CommandText = "p_scm_offline_del";
                spCmd.CommandTimeout = 600;
                MySqlParameter[] parameters ={
                    new MySqlParameter("@in_batchid",MySqlDbType.Int64),

                    new MySqlParameter("@out_resultcode",MySqlDbType.Int64),
                    new MySqlParameter("@out_resultmsg",MySqlDbType.VarChar)

                };

                parameters[0].Value = Int64.Parse(seqID);
                parameters[0].Direction = ParameterDirection.Input;

                parameters[1].Size = 200;
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2].Size = 200;//
                parameters[2].Direction = ParameterDirection.Output;


                foreach (MySqlParameter parameter in parameters)
                {
                    spCmd.Parameters.Add(parameter);
                }

                int ret = spCmd.ExecuteNonQuery();

                retinfo.num = parameters[1].Value.ToString().Trim();
                retinfo.msg = parameters[2].Value.ToString().Trim();

                spCmd.Dispose();
                spCmd = null;



            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(),"错误信息");

            } finally {
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (selCmd != null)
                {
                    selCmd.Dispose();
                }
                if (insCmd != null)
                {
                    insCmd.Dispose();
                }
                if (spCmd != null)
                {
                    spCmd.Dispose();
                }

            }
            return retCode;
        }


    }
}
