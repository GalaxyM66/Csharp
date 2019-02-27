using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
 
 
 
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace PriceManager
{
    class PMPriceDao : MySQLHelper
    {
        private object dao;
        #region Group
        public DataTable GetCliensGroupsByCbo(string Value)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_scm_clients_group_hdr where compid = @compid and ownerid = @owenerid ";

            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("owenerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(Value))
            {
                cmdText += " and( code like @value or groupname like @value or mark like @value )";
                parmap.Add("value", "%" + Value + "%");
            }
            cmdText += " and stopflag = '00' limit 50";
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetGoodsGroupsByCbo(string Value)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from scm_waredict_group_hdr where compid = @compid and ownerid = @owenerid ";

            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("owenerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(Value))
            {
                cmdText += " and( code like @value or groupname like @value or mark like @value )";
                parmap.Add("value", "%" + Value + "%");
            }
            cmdText += " and stopflag = '00' limit 50";
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetCliensGroups(string groupCode, string groupName, string stopFlag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_scm_clients_group_hdr where compid = @compid and ownerid = @owenerid ";

            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("owenerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(groupCode))
            {
                cmdText += " and code like @code ";
                parmap.Add("code", "%" + groupCode + "%");
            }
            if (StringUtils.IsNotNull(groupName))
            {
                cmdText += " and groupname like @name ";
                parmap.Add("name", "%" + groupName + "%");
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag ";
                parmap.Add("stopflag", stopFlag);
            }
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetGoodsGroups(string groupCode, string groupName, string stopFlag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_scm_waredict_group_hdr where compid = @compid and ownerid = @owenerid ";

            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("owenerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(groupCode))
            {
                cmdText += " and code like @code ";
                parmap.Add("code", "%" + groupCode + "%");
            }

            if (StringUtils.IsNotNull(groupName))
            {
                cmdText += " and groupname like @name ";
                parmap.Add("name", "%" + groupName + "%");
            }

            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag ";
                parmap.Add("stopflag", stopFlag);
            }
            return ExecuteDataTable(cmdText, parmap);
        }

        public string[] NewGoodsGroup(string groupCode, string groupName, string mark, string groupType, string stopFlag)
        {
            string cmdText = "p_scm_waredict_group_hdr_add";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_compId",MySqlDbType.VarChar),
                new MySqlParameter("@in_ownerId",MySqlDbType.VarChar),
                new MySqlParameter("@in_groupCode",MySqlDbType.VarChar),
                new MySqlParameter("@in_groupName",MySqlDbType.VarChar),
                new MySqlParameter("@in_mark",MySqlDbType.VarChar),
                new MySqlParameter("@in_groupType",MySqlDbType.VarChar),
                new MySqlParameter("@in_stopFlag",MySqlDbType.VarChar),
                new MySqlParameter("@in_createUser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = groupCode;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = groupName;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = mark;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Value = groupType;
                parameters[5].Direction = ParameterDirection.Input;
                parameters[6].Value = stopFlag;
                parameters[6].Direction = ParameterDirection.Input;
                parameters[7].Value = SessionDto.Empid;
                parameters[7].Direction = ParameterDirection.Input;
                parameters[8].Direction = ParameterDirection.Output;
                parameters[9].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public string[] EditGoodsGroup(string groupId, string groupCode, string groupName, string mark, string stopFlag)
        {
            string cmdText = "p_scm_waredict_group_hdr_modify";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_id",MySqlDbType.VarChar),
                new MySqlParameter("@in_groupCode",MySqlDbType.VarChar),
                new MySqlParameter("@in_groupName",MySqlDbType.VarChar),
                new MySqlParameter("@in_mark",MySqlDbType.VarChar),
                new MySqlParameter("@in_stopFlag",MySqlDbType.VarChar),
                new MySqlParameter("@in_modifyUser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = groupId;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = groupCode;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = groupName;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = mark;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = stopFlag;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Value = SessionDto.Empid;
                parameters[5].Direction = ParameterDirection.Input;
                parameters[6].Direction = ParameterDirection.Output;
                parameters[7].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public string[] AddGoodsGroupDetails(string groupId, string goodsId)
        {
            string cmdText = "p_scm_waredict_group_dtl_add";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_compId",MySqlDbType.VarChar),
                new MySqlParameter("@in_ownerId",MySqlDbType.VarChar),
                new MySqlParameter("@in_hdrId",MySqlDbType.VarChar),
                new MySqlParameter("@in_goodId",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = groupId;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = goodsId;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Direction = ParameterDirection.Output;
                parameters[5].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public string[] RemoveGoodsGroupDetails(string groupId, string goodsId)
        {
            string cmdText = "p_scm_waredict_group_dtl_del";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_compId",MySqlDbType.VarChar),
                new MySqlParameter("@in_ownerId",MySqlDbType.VarChar),
                new MySqlParameter("@in_hdrId",MySqlDbType.VarChar),
                new MySqlParameter("@in_goodId",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = groupId;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = goodsId;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Direction = ParameterDirection.Output;
                parameters[5].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public string[] NewClientsGroup(string groupCode, string groupName, string mark, string groupType, string stopFlag)
        {
            string cmdText = "p_scm_clients_group_hdr_add";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_compId",MySqlDbType.VarChar),
                new MySqlParameter("@in_ownerId",MySqlDbType.VarChar),
                new MySqlParameter("@in_groupCode",MySqlDbType.VarChar),
                new MySqlParameter("@in_groupName",MySqlDbType.VarChar),
                new MySqlParameter("@in_mark",MySqlDbType.VarChar),
                new MySqlParameter("@in_groupType",MySqlDbType.VarChar),
                new MySqlParameter("@in_stopFlag",MySqlDbType.VarChar),
                new MySqlParameter("@in_createUser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = groupCode;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = groupName;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = mark;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Value = groupType;
                parameters[5].Direction = ParameterDirection.Input;
                parameters[6].Value = stopFlag;
                parameters[6].Direction = ParameterDirection.Input;
                parameters[7].Value = SessionDto.Empid;
                parameters[7].Direction = ParameterDirection.Input;
                parameters[8].Direction = ParameterDirection.Output;
                parameters[9].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public string[] EditClientsGroup(string groupId, string groupCode, string groupName, string mark, string stopFlag)
        {
            string cmdText = "p_scm_clients_group_hdr_modify";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_id",MySqlDbType.VarChar),
                new MySqlParameter("@in_groupCode",MySqlDbType.VarChar),
                new MySqlParameter("@in_groupName",MySqlDbType.VarChar),
                new MySqlParameter("@in_mark",MySqlDbType.VarChar),
                new MySqlParameter("@in_stopFlag",MySqlDbType.VarChar),
                new MySqlParameter("@in_modifyUser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = groupId;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = groupCode;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = groupName;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = mark;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = stopFlag;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Value = SessionDto.Empid;
                parameters[5].Direction = ParameterDirection.Input;
                parameters[6].Direction = ParameterDirection.Output;
                parameters[7].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public string[] AddClientsGroupDetails(string groupId, string clientsId)
        {
            string cmdText = "p_scm_clients_group_dtl_add";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_compId",MySqlDbType.VarChar),
                new MySqlParameter("@in_ownerId",MySqlDbType.VarChar),
                new MySqlParameter("@in_hdrId",MySqlDbType.VarChar),
                new MySqlParameter("@in_clientsId",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = groupId;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = clientsId;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Direction = ParameterDirection.Output;
                parameters[5].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public string[] RemoveClientsGroupDetails(string groupId, string clientsId)
        {
            string cmdText = "p_scm_clients_group_dtl_del";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_compId",MySqlDbType.VarChar),
                new MySqlParameter("@in_ownerId",MySqlDbType.VarChar),
                new MySqlParameter("@in_hdrId",MySqlDbType.VarChar),
                new MySqlParameter("@in_clientsId",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = groupId;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = clientsId;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Direction = ParameterDirection.Output;
                parameters[5].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        #endregion
        #region Price
        public DataTable GetPriceByTable(string cstCode, string cstName, string goods, string goodsName, string spec, string producer, string stopFlag, string tableName, string beginDateBegin, string beginDateEnd,string buyerCode)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from " + tableName + " where compid = @compid and ownerid = @ownerid ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(buyerCode))
            {
                cmdText += " and buyercode = @buyercode";
                parmap.Add("buyercode", buyerCode);
                SessionDto.Empdeptid = "-1";
            }
            if (!"-1".Equals(SessionDto.Empdeptid))
            {
                cmdText += " and saledeptid = @saledeptid";
                parmap.Add("saledeptid", SessionDto.Empdeptid);
            }
            if (StringUtils.IsNotNull(cstCode))
            {
                cmdText += " and cstcode like @cstcode";
                parmap.Add("cstcode", "%" + cstCode + "%");
            }
            if (StringUtils.IsNotNull(cstName))
            {
                cmdText += " and cstname like @cstname";
                parmap.Add("cstname", "%" + cstName + "%");
            }
            if (StringUtils.IsNotNull(goods))
            {
                cmdText += " and goods like @goods";
                parmap.Add("goods", "%" + goods + "%");
            }
            if (StringUtils.IsNotNull(goodsName))
            {
                cmdText += " and name like @name";
                parmap.Add("name", "%" + goodsName + "%");
            }
            if (StringUtils.IsNotNull(spec))
            {
                cmdText += " and spec like @spec";
                parmap.Add("spec", "%" + spec + "%");
            }
            if (StringUtils.IsNotNull(producer))
            {
                cmdText += " and producer like @producer";
                parmap.Add("producer", "%" + producer + "%");
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            if (StringUtils.IsNotNull(beginDateBegin))
            {
                cmdText += " and begindate >= @begindatebegin";
                parmap.Add("begindatebegin", beginDateBegin);
            }
            if (StringUtils.IsNotNull(beginDateEnd))
            {
                cmdText += " and begindate <= @begindateend";
                parmap.Add("begindateend", beginDateEnd);
            }
            cmdText += " and f_judge_dept_cst(ifnull(compid, 0), ifnull(ownerid, 0), ifnull(saledeptid, 0), ifnull(cstid, 0)) order by modifydate desc  limit 0,100000 ";
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetPriceDraftAndExec(string cstId, string goodId, string stopFlag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "and compid = @compid and ownerid = @ownerid ";

            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            if (!"-1".Equals(SessionDto.Empdeptid))
            {
                cmdText += " and saledeptid = @saledeptid ";
                parmap.Add("saledeptid", SessionDto.Empdeptid);
            }
            if (StringUtils.IsNotNull(cstId))
            {
                cmdText += " and cstid = @cstid";
                parmap.Add("cstid", cstId);
            }
            if (StringUtils.IsNotNull(goodId))
            {
                cmdText += " and goodid = @goodid";
                parmap.Add("goodid", goodId);
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            string sortable = " order by modifydate desc";

            return ExecuteDataTable(SqlStr.PriceSql(cmdText, sortable), parmap);
        }

        public DataTable GetPriceByClientsId(string clientsId, string goodsCode, string goodsName, string goodsGroupId, string goodsGroupType, string stopFlag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "and compid = @compid and ownerid = @ownerid and cstid in (" + clientsId + ") ";
            string cmdText_1 = "";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            if (!"-1".Equals(SessionDto.Empdeptid))
            {
                cmdText += " and saledeptid = @saledeptid ";
                parmap.Add("saledeptid", SessionDto.Empdeptid);
            }
            if (StringUtils.IsNotNull(goodsCode))
            {
                cmdText_1 += " and goods like @goods";
                parmap.Add("goods", "%" + goodsCode + "%");
            }
            if (StringUtils.IsNotNull(goodsName))
            {
                cmdText_1 += " and name like @name";
                parmap.Add("name", "%" + goodsName + "%");
            }
            if (StringUtils.IsNotNull(goodsGroupId))
            {
                if ("10".Equals(goodsGroupType))
                {
                    cmdText += " and goodid in (select goodid from v_scm_clients_group_dtl where id = @id) ";
                    parmap.Add("id", goodsGroupId);
                }
                else if ("20".Equals(goodsGroupType))
                {
                    string[] result = PMBaseDao.GetDynamicSql(goodsGroupId, "goodid", "goods");
                    cmdText += " and goodid in (" + result[1] + ")";
                }
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            cmdText_1 += " order by modifydate desc";
           
            return ExecuteDataTable(SqlStr.PriceSql(cmdText, cmdText_1), parmap);
        }

        public DataTable GetPriceByGoodsId(string goodsId, string clientsCode, string clientsName, string clientsGroupId, string clientsGroupType, string stopFlag,string area)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = " and compid = @compid and ownerid = @ownerid and goodid in (" + goodsId + ") ";
            string cmdText_1 = "";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            if (!"-1".Equals(SessionDto.Empdeptid))
            {
                cmdText += " and saledeptid = @saledeptid ";
                parmap.Add("saledeptid", SessionDto.Empdeptid);
            }
            if (StringUtils.IsNotNull(clientsCode))
            {
                cmdText_1 += " and cstcode like @cstcode";
                parmap.Add("cstcode", "%" + clientsCode + "%");
            }
            if (StringUtils.IsNotNull(clientsName))
            {
                cmdText_1 += " and cstname like @cstname";
                parmap.Add("cstname", "%" + clientsName + "%");
            }
            if (StringUtils.IsNotNull(area))
            {
                cmdText_1 += " and concat( `f_region_codetoname` (`pc`.`province`), '-', `f_region_codetoname` (`pc`.`city`), '-', `f_region_codetoname` (`pc`.`area`)) like @area ";
                parmap.Add("area", "%" + area + "%");
            }
            if (StringUtils.IsNotNull(clientsGroupId))
            {
                if ("10".Equals(clientsGroupType))
                {
                    cmdText += " and id = @id";
                    parmap.Add("id", clientsGroupId);
                }
                else if ("20".Equals(clientsGroupId))
                {
                    string[] result = PMBaseDao.GetDynamicSql(clientsGroupId, "cstid", "clients");
                    cmdText += " and cstid in (" + result[1] + ")";
                }
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            cmdText_1 += " order by modifydate desc";
            return ExecuteDataTable(SqlStr.PriceSql(cmdText, cmdText_1), parmap);
        }

        public string[] SavePrice(string str, string status, string tableType)
        {
            string cmdText = "p_scm_data_import";
            if ("excel".Equals(status))
                cmdText = "p_scm_excel_entry";
         
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_user",MySqlDbType.VarChar),
                new MySqlParameter("@in_table_type",MySqlDbType.VarChar),
                new MySqlParameter("@in_field_values",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = SessionDto.Empid;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = tableType;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = str;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Direction = ParameterDirection.Output;
                parameters[4].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public string[] SavePriceWithAddrate(ChannelPrice price, decimal addrate)
        {
            string[] retMsg = { "0", price.Cstcode + "，" + price.Cstname + "/" + price.Goods + "，" + price.Name };
            if ("00".Equals(price.Bargain))
            {
                retMsg[1] = "不可议价：" + retMsg[1];
                return retMsg;
            }
            decimal costrate = StringUtils.ToDecimal(price.Costrate) + addrate;
            if (!(costrate < 1 || costrate >= 0))
            {
                retMsg[1] = "毛利率错误：" + retMsg[1];
                return retMsg;
            }
            decimal prc = StringUtils.ToDecimal(price.Costprc) / (1 - (costrate));
            if(prc< StringUtils.ToDecimal(price.Bottomprc))
            {
                retMsg[1] = "低于底价：" + retMsg[1];
                return retMsg;
            }
            price.Prc = prc.ToString();
            price.Price = (Math.Round(prc / (1 + StringUtils.ToDecimal(price.Outrate)), 6)).ToString();
            price.Costrate = costrate.ToString();
            string[] result = SavePrice(price.GetString(""), "", "executed");
            if (!"1".Equals(result[0]))
            {
                retMsg[1] = result[1] + retMsg[1];
                return retMsg;
            }
            retMsg[0] = "1";
            retMsg[1] = "成功";
            return retMsg;
        }
        public string[] CopyPrice(string copyId, string targetId, string status,string procedure)
        {
            //string cmdText = "p_scm_exec_acopytob";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_compId",MySqlDbType.VarChar),
                new MySqlParameter("@in_ownerId",MySqlDbType.VarChar),
                new MySqlParameter("@in_copyId",MySqlDbType.VarChar),
                new MySqlParameter("@in_targetId",MySqlDbType.VarChar),
                new MySqlParameter("@in_coverType",MySqlDbType.VarChar),
                new MySqlParameter("@in_conctrlUser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = copyId;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = targetId;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = status;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Value = SessionDto.Empid;
                parameters[5].Direction = ParameterDirection.Input;
                parameters[6].Direction = ParameterDirection.Output;
                parameters[7].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(procedure, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public DataTable GetChangeConfirmPrice(string goodId, string clientTypeGroup, string clientsId, string clientsCode, string clientsName, string region)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = " and f_clientstype_to_group(pc.clienttype,'code') = @clienttypegroup";
            string saleValue = "";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            parmap.Add("goodid", goodId);
            parmap.Add("clienttypegroup", clientTypeGroup);
            if (!"-1".Equals(SessionDto.Empdeptid))
            {

                cmdText += " and cs.subid = @saledeptid";
                saleValue += " and d.saledeptid = @saledeptid ";
                parmap.Add("saledeptid", SessionDto.Empdeptid);
            }
            if ("all".Equals(clientTypeGroup))
            {
                cmdText = "";
                parmap.Add("cstid", clientsId);
            }
            if (StringUtils.IsNotNull(clientsId))
            {
                cmdText += " and pc.cstid = @cstid";
                parmap.Add("cstid", clientsId);
            }
            if (StringUtils.IsNotNull(clientsCode))
            {
                cmdText += " and pc.cstcode like @cstcode";
                parmap.Add("cstcode", "%" + clientsCode + "%");
            }
            if (StringUtils.IsNotNull(clientsName))
            {
                cmdText += " and pc.cstname like @cstname";
                parmap.Add("cstname", "%" + clientsName + "%");
            }
            if (StringUtils.IsNotNull(region))
            {
                cmdText += " and pc.region like @region";
                parmap.Add("region", "%" + region + "%");
            }
            string sortable = " order by origin desc,personal desc";

            return ExecuteDataTable(SqlStr.ChangeConfirmSql(saleValue, cmdText, sortable), parmap);
        }

        public string[] SetDisplay(ChannelPrice price, string isDisplay)
        {
            string cmdText = "p_scm_exec_b2bdisplay_update";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_id",MySqlDbType.VarChar),
                new MySqlParameter("@in_origin",MySqlDbType.VarChar),
                new MySqlParameter("@in_b2bdisplay",MySqlDbType.VarChar),
                new MySqlParameter("@in_modifyUser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = price.Id;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = price.Origin;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = isDisplay;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = SessionDto.Empid;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Direction = ParameterDirection.Output;
                parameters[5].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public DataTable GetExceptionPrice(string exceptionType, string handleFlag, string buyerCode,string goodsCode,string clientsCode)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText_value = "and compid = @compid and ownerid = @ownerid ";

            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(buyerCode))
            {
                cmdText_value += " and buyercode = @buyercode";
                parmap.Add("buyercode", buyerCode);
            }
            if (StringUtils.IsNotNull(exceptionType))
            {
                cmdText_value += " and outliertype = @outliertype";
                parmap.Add("outliertype", exceptionType);
            }
            if (StringUtils.IsNotNull(clientsCode))
            {
                cmdText_value += " and cstcode like @cstcode";
                parmap.Add("cstcode", "%"+clientsCode+ "%");
            }
            if (StringUtils.IsNotNull(goodsCode))
            {
                cmdText_value += " and goods like @goods";
                parmap.Add("goods", "%" + goodsCode+ "%");
            }
            if (StringUtils.IsNotNull(handleFlag))
            {
                cmdText_value += " and handleflag = @handleflag";
                parmap.Add("handleflag", handleFlag);
            }
            string sortableText = " order by outliercreatedate desc ";

            string cmdText = string.Format("(select * from v_scm_price_outlier_draft_detail where  1=1 {0} limit 0,5000)" +
                " union (select * from v_scm_price_outlier_executed_detail  where 1=1 {1}  limit 0,5000) {2}", cmdText_value, cmdText_value, sortableText);

            return ExecuteDataTable(cmdText, parmap);
        }

       public string[] ExceptionHandle(string outlierId)
        {
            string cmdText = "p_scm_outlier_handleflag";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_id",MySqlDbType.VarChar),
                new MySqlParameter("@in_modifyUser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = outlierId;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = SessionDto.Empid;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Direction = ParameterDirection.Output;
                parameters[3].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        #endregion
        #region SalePrice
        public DataTable GetSalePrice(string cstCode, string goods, string stopFlag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_scm_price_off_executed where compid = @compid and ownerid = @owenerid ";

            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("owenerid", Properties.Settings.Default.OWNERID);
            if (!"-1".Equals(SessionDto.Empdeptid))
            {
                cmdText += " and saledeptid = @saledeptid";
                parmap.Add("saledeptid", SessionDto.Empdeptid);
            }
            if (StringUtils.IsNotNull(cstCode))
            {
                cmdText += " and cstcode like @cstcode";
                parmap.Add("cstcode", "%" + cstCode + "%");
            }
            if (StringUtils.IsNotNull(goods))
            {
                cmdText += " and goods like @goods";
                parmap.Add("goods", "%" + goods + "%");
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            cmdText += " order by modifydate desc";
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetSalePriceByClientsId(string clientsId, string goodsCode, string goodsName, string goodsGroupId, string goodsGroupType, string stopFlag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_scm_price_off_draftunionexec_detail where compid = @compid and ownerid = @ownerid and cstid = @cstid ";

            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            parmap.Add("cstid", clientsId);
            if (!"-1".Equals(SessionDto.Empdeptid))
            {
                cmdText += " and saledeptid = @saledeptid";
                parmap.Add("saledeptid", SessionDto.Empdeptid);
            }
            if (StringUtils.IsNotNull(goodsCode))
            {
                cmdText += " and goods like @goods";
                parmap.Add("goods", "%" + goodsCode + "%");
            }
            if (StringUtils.IsNotNull(goodsName))
            {
                cmdText += " and name like @name";
                parmap.Add("name", "%" + goodsName + "%");
            }
            if (StringUtils.IsNotNull(goodsGroupId))
            {
                if ("10".Equals(goodsGroupType))
                {
                    cmdText += "and goodid in  (select goodid from v_scm_waredict_group_dtl where compid = @compid and ownerid = @ownerid  and id = @id) ";
                    parmap.Add("id", goodsGroupId);
                }
                else if ("20".Equals(goodsGroupType))
                {
                    string[] result = PMBaseDao.GetDynamicSql(goodsGroupId, "goodid", "goods");
                    cmdText += " and goodid in (" + result[1] + ")";
                }
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            cmdText += " order by modifydate desc";
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetSalePriceByGoodsId(string goodsId, string clientsCode, string clientsName, string clientsGroupId, string clientsGroupType, string stopFlag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_scm_price_off_draftunionexec_detail where compid = @compid and ownerid = @ownerid and goodid = @goodid ";

            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            parmap.Add("goodid", goodsId);
            if (!"-1".Equals(SessionDto.Empdeptid))
            {
                cmdText += " and saledeptid = @saledeptid";
                parmap.Add("saledeptid", SessionDto.Empdeptid);
            }
            if (StringUtils.IsNotNull(clientsCode))
            {
                cmdText += " and cstcode like @cstcode";
                parmap.Add("cstcode", "%" + clientsCode + "%");
            }
            if (StringUtils.IsNotNull(clientsName))
            {
                cmdText += " and cstname like @cstname";
                parmap.Add("cstname", "%" + clientsName + "%");
            }
            if (StringUtils.IsNotNull(clientsGroupId))
            {
                if ("10".Equals(clientsGroupType))
                {
                    cmdText += "  and cstid in (select cstid from v_scm_clients_group_dtl where compid = @compid  and ownerid = @ownerid   and id = @id)";
                    parmap.Add("id", clientsGroupId);
                }
                else if ("20".Equals(clientsGroupId))
                {
                    string[] result = PMBaseDao.GetDynamicSql(clientsGroupId, "cstid", "clients");
                    cmdText += " and cstid in (" + result[1] + ")";
                }
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            cmdText += " order by modifydate desc";
            return ExecuteDataTable(cmdText, parmap);
        }
        #endregion
        #region LowPrice
        public DataTable GetLowPrice(string goodsId, string goodsCode, string goodsName, string spec, string producer, string stopFlag, string clientTypeGroup, string isModifyExec,string buyerCode)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_scm_price_bottom where compid = @compid and ownerid = @owenerid ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("owenerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(buyerCode))
            {
                cmdText += " and buyercode = @buyercode";
                parmap.Add("buyercode", buyerCode);
            }
            if (StringUtils.IsNotNull(goodsId))
            {
                cmdText += " and goodid = @goodid";
                parmap.Add("goodid", goodsId);
            }
            if (StringUtils.IsNotNull(goodsCode))
            {
                cmdText += " and goods like @goods";
                parmap.Add("goods", "%" + goodsCode + "%");
            }
            if (StringUtils.IsNotNull(goodsName))
            {
                cmdText += " and name like @name";
                parmap.Add("name", "%" + goodsName + "%");
            }
            if (StringUtils.IsNotNull(spec))
            {
                cmdText += " and spec like @spec";
                parmap.Add("spec", "%" + spec + "%");
            }
            if (StringUtils.IsNotNull(producer))
            {
                cmdText += " and producer like @producer";
                parmap.Add("producer", "%" + producer + "%");
            }
            if (StringUtils.IsNotNull(clientTypeGroup))
            {
                cmdText += " and clienttypegroup = @clienttypegroup";
                parmap.Add("clienttypegroup", clientTypeGroup);
            }
            if (StringUtils.IsNotNull(isModifyExec))
            {
                cmdText += " and ismodifyexec = @ismodifyexec";
                parmap.Add("ismodifyexec", isModifyExec);
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            cmdText += " order by modifydate desc";
            return ExecuteDataTable(cmdText, parmap);
        }

        public LowPrice GetSuggestValue(string goodsId, string clientTypeGroup)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            DataTable dt = null;
            LowPrice lowPrice = new LowPrice();
            string cmdText = "select Suggestexecprc, Suggestexecprice, prc, price, costprc, costprice, costrate,ismodifyexecname from v_scm_price_bottom where compid = @compid and ownerid = @owenerid ";

            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("owenerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(goodsId))
            {
                cmdText += " and goodid = @goodid";
                parmap.Add("goodid", goodsId);
            }
            if (StringUtils.IsNotNull(clientTypeGroup))
            {
                cmdText += " and clienttypegroup = @clienttypegroup";
                parmap.Add("clienttypegroup", clientTypeGroup);
            }
            cmdText += " and stopflag = '00'";
            dt = ExecuteDataTable(cmdText, parmap);

            if (dt == null || dt.Rows.Count != 1)
                return lowPrice;
            else
                return lowPrice.SetValue(dt);
        }
        #endregion
        #region public
        public static string[] CheckData(string str, string tableType)
        {
            string cmdText = "p_scm_data_check";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_table_type",MySqlDbType.VarChar),
                new MySqlParameter("@in_field_values",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = tableType;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = str;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Direction = ParameterDirection.Output;
                parameters[3].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public string[] ClientsPriceControl(string cstId, string procedureName)
        {
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_compId",MySqlDbType.VarChar),
                new MySqlParameter("@in_ownerId",MySqlDbType.VarChar),
                new MySqlParameter("@in_cstId",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = cstId;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Direction = ParameterDirection.Output;
                parameters[4].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(procedureName, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public DataTable GetClientPriceExpand(string cstId)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = " ";
            string sortable = " ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            parmap.Add("cstid", cstId);

            return ExecuteDataTable(SqlStr.PriceExpandSql(cmdText, sortable), parmap);
        }
        #endregion
    }
}
