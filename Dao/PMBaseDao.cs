using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace PriceManager
{
    class PMBaseDao : MySQLHelper
    {
        #region Goods
        public DataTable GetGoodsByCbo(string value)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_waredict where compid = @compid  and (ownerid = @ownerid or ownerid is null ) ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(value))
            {
                cmdText += " and( goods like @value or name like @value or spec like @value or producer like @value)";
                parmap.Add("value", "%" + value + "%");
            }
            cmdText += " and stopflag = '00' limit 50";
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetGoodss(string goodsCode, string goodsName, string spec, string producer,string area,string forbitarea, string limitcsttype,string stopFlag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_waredict where compid = @compid  and (ownerid = @ownerid or ownerid is null ) ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
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
            if (StringUtils.IsNotNull(area))
            {
                cmdText += " and area like @area";
                parmap.Add("area", "%" + area + "%");
            }
            if (StringUtils.IsNotNull(forbitarea))
            {
                cmdText += " and forbitarea like @forbitarea";
                parmap.Add("forbitarea", "%" + forbitarea + "%");
            }
            if (StringUtils.IsNotNull(limitcsttype))
            {
                cmdText += " and limitcsttype like @limitcsttype";
                parmap.Add("limitcsttype", "%" + limitcsttype + "%");
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            cmdText += " limit 10000";
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetGoods(string goodsCode, string goodsName, string spec, string producer,string stopFlag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_waredict where compid = @compid  and (ownerid = @ownerid or ownerid is null ) ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
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
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            cmdText += " limit 10000";
            return ExecuteDataTable(cmdText, parmap);
        }


        public DataTable GetAllGoods(string goodsCode, string goodsName, string spec, string producer, string stopFlag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_waredict_all  where compid = @compid";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
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
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetGoodsByGroupId(string goodsCodes, string goodsName, string groupId, string groupType, string clientTypeGroupName)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_waredict where compid = @compid and (ownerid = @ownerid or ownerid is null )";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(groupId))
            {
                if ("10".Equals(groupType))
                {
                    cmdText += " and goodid in  (select goodid from v_scm_waredict_group_dtl where compid = @compid and ownerid = @ownerid  and id = @id)";
                    parmap.Add("id", groupId);
                }
                else if ("20".Equals(groupType))
                {
                    string[] result = GetDynamicSql(groupId, "*", "goods");
                    cmdText = result[1] + " and compid = @compid and ownerid = @ownerid";
                }
            }
            if (StringUtils.IsNotNull(goodsCodes))
            {
                cmdText += " and goods in ("+ goodsCodes + ") ";
            }
            if (StringUtils.IsNotNull(goodsName))
            {
                cmdText += " and name like @name";
                parmap.Add("name", "%" + goodsName + "%");
            }
            if (StringUtils.IsNotNull(clientTypeGroupName))
            {
                cmdText += " and (limitcsttype like @limitcsttype or limitcsttype is null)";
                parmap.Add("limitcsttype", "%" + clientTypeGroupName + "%");
            }
            cmdText += " and stopflag = '00' limit 1000";
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetGoodsSub(string goodsId, string subType)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_waredict_sub where compid = @compid and ownerid = @ownerid ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(goodsId))
            {
                cmdText += " and goodid = @goodid";
                parmap.Add("goodid", goodsId);
            }
            if (StringUtils.IsNotNull(subType))
            {
                cmdText += " and subtype = @subtype";
                parmap.Add("subtype", subType);
            }
            return ExecuteDataTable(cmdText, parmap);
        }

        public string[] AddGoodsSub(string goodId, string subType, string subId)
        {
            string cmdText = "p_pub_waredict_sub_add";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_compId",MySqlDbType.VarChar),
                new MySqlParameter("@in_ownerId",MySqlDbType.VarChar),
                new MySqlParameter("@in_goodId",MySqlDbType.VarChar),
                new MySqlParameter("@in_subType",MySqlDbType.VarChar),
                new MySqlParameter("@in_subId",MySqlDbType.VarChar),
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
                parameters[2].Value = goodId;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = subType;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = subId;
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

        public string[] RemoveGoodsSub(string goodId, string subType, string subId)
        {
            string cmdText = "p_pub_waredict_sub_del";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_compId",MySqlDbType.VarChar),
                new MySqlParameter("@in_ownerId",MySqlDbType.VarChar),
                new MySqlParameter("@in_goodId",MySqlDbType.VarChar),
                new MySqlParameter("@in_subType",MySqlDbType.VarChar),
                new MySqlParameter("@in_subId",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = goodId;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = subType;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = subId;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }
        #endregion
        #region Clients
        public DataTable GetClientssByCbo(string value)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_clients where compid = @compid and ownerid = @ownerid ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(value))
            {
                cmdText += " and( cstcode like @value or cstname like @value or region like @value)";
                parmap.Add("value", "%" + value + "%");
            }
            cmdText += " and stopflag = '00' limit 50";
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetClients(string clientsCode, string clientsName, string areaName, string stopFlag, string payType, string clientsType)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_clients where compid = @compid and ownerid = @ownerid ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
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
            if (StringUtils.IsNotNull(areaName))
            {
                cmdText += " and region like @region";
                parmap.Add("region", "%" + areaName + "%");
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            if (StringUtils.IsNotNull(payType))
            {
                cmdText += " and paytype = @paytype";
                parmap.Add("paytype", payType);
            }
            if (StringUtils.IsNotNull(clientsType))
            {
                cmdText += " and clienttype = @clienttype";
                parmap.Add("clienttype", clientsType);
            }
            cmdText += " limit 10000 ";
            return ExecuteDataTable(cmdText, parmap);
        }

        public string[] CheckWaredictData(ImportGoodsSub importValue)
        {
            string cmdText = "p_pub_waredict_sub_excel_check";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_goods",MySqlDbType.VarChar),
                new MySqlParameter("@in_limitcsttypes",MySqlDbType.VarChar),
                new MySqlParameter("@in_areas",MySqlDbType.VarChar),
                new MySqlParameter("@in_forbitareas",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = importValue.Goods;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = importValue.Limitcsttype;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = importValue.Area;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = importValue.Forbitarea;
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

        public string[] ImportWaredictSub(ImportGoodsSub importValue)
        {
            string cmdText = "p_pub_waredict_sub_excel_to_data";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_modifyuser",MySqlDbType.VarChar),
                new MySqlParameter("@in_compid",MySqlDbType.VarChar),
                new MySqlParameter("@in_ownerid",MySqlDbType.VarChar),
                new MySqlParameter("@in_goods",MySqlDbType.VarChar),
                new MySqlParameter("@in_limitcsttypes",MySqlDbType.VarChar),
                new MySqlParameter("@in_areas",MySqlDbType.VarChar),
                new MySqlParameter("@in_forbitareas",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = SessionDto.Empid;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.COMPID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = Properties.Settings.Default.OWNERID;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = importValue.Goods;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = importValue.Limitcsttype;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Value = importValue.Area;
                parameters[5].Direction = ParameterDirection.Input;
                parameters[6].Value = importValue.Forbitarea;
                parameters[6].Direction = ParameterDirection.Input;
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public DataTable GetAllClients(string clientsCode, string clientsName, string areaName, string stopFlag, string payType, string clientsType)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from  v_pub_clients_all where compid = @compid and (ownerid=@ownerid or ownerid is null) ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
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
            if (StringUtils.IsNotNull(areaName))
            {
                cmdText += " and region like @region";
                parmap.Add("region", "%" + areaName + "%");
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            if (StringUtils.IsNotNull(payType))
            {
                cmdText += " and paytype = @paytype";
                parmap.Add("paytype", payType);
            }
            if (StringUtils.IsNotNull(clientsType))
            {
                cmdText += " and clienttype = @clienttype";
                parmap.Add("clienttype", clientsType);
            }
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetClientsByGroupId(string clientsCodes, string clientsName, string groupId, string groupType, string clientTypeGroup)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_clients where compid = @compid and ownerid = @ownerid ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(groupId))
            {
                if ("10".Equals(groupType))
                {
                    cmdText += " and cstid in (select cstid from v_scm_clients_group_dtl where compid = @compid  and ownerid = @ownerid and id = @id)";
                    parmap.Add("id", groupId);
                }
                else if ("20".Equals(groupType))
                {
                    string[] result = GetDynamicSql(groupId, "*", "clients");
                    cmdText = result[1] + " and compid = @compid and ownerid = @ownerid";
                }
            }
            if (StringUtils.IsNotNull(clientsCodes))
            {
                cmdText += " and cstcode in   (" + clientsCodes + ") ";
                //parmap.Add("cstcode", "(" + clientsCodes+")");
            }
            if (StringUtils.IsNotNull(clientsName))
            {
                cmdText += " and cstname like @cstname";
                parmap.Add("cstname", "%" + clientsName + "%");
            }
            if (StringUtils.IsNotNull(clientTypeGroup))
            {
                cmdText += " and clienttypegroup in (" + clientTypeGroup + ") ";
                //parmap.Add("clienttypegroup", clientTypeGroup);
            }
            cmdText += " and stopflag = '00' limit 1000";
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetClientsSub(string clientsId, string subType)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_clients_sub where compid = @compid and ownerid = @ownerid ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(clientsId))
            {
                cmdText += " and cstid = @cstid";
                parmap.Add("cstid", clientsId);
            }
            if (StringUtils.IsNotNull(subType))
            {
                cmdText += " and subtype = @subtype";
                parmap.Add("subtype", subType);
            }
            return ExecuteDataTable(cmdText, parmap);
        }

        public string[] AddClientsSub(string clientsId, string subType, string subId)
        {
            string cmdText = "p_pub_clients_sub_add";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_compId",MySqlDbType.VarChar),
                new MySqlParameter("@in_ownerId",MySqlDbType.VarChar),
                new MySqlParameter("@in_cstId",MySqlDbType.VarChar),
                new MySqlParameter("@in_subType",MySqlDbType.VarChar),
                new MySqlParameter("@in_subId",MySqlDbType.VarChar),
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
                parameters[2].Value = clientsId;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = subType;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = subId;
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

        public string[] RemoveClientsSub(string clientsId, string subType, string subId)
        {
            string cmdText = "p_pub_clients_sub_del";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_compId",MySqlDbType.VarChar),
                new MySqlParameter("@in_ownerId",MySqlDbType.VarChar),
                new MySqlParameter("@in_cstId",MySqlDbType.VarChar),
                new MySqlParameter("@in_subType",MySqlDbType.VarChar),
                new MySqlParameter("@in_subId",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = Properties.Settings.Default.COMPID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = Properties.Settings.Default.OWNERID;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = clientsId;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = subType;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = subId;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }
        #endregion
        #region Dictionary
        public DataTable GetDictionary(string typeCode, string typeName, string dictCode, string dictName, string stopFlag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_sys_code where compid = @compid ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            if (StringUtils.IsNotNull(typeCode))
            {
                cmdText += " and typecode like @typecode ";
                parmap.Add("typecode", "%" + typeCode + "%");
            }
            if (StringUtils.IsNotNull(typeName))
            {
                cmdText += " and typename like @typename ";
                parmap.Add("typename", "%" + typeName + "%");
            }
            if (StringUtils.IsNotNull(dictCode))
            {
                cmdText += " and code = @code ";
                parmap.Add("code", dictCode);
            }
            if (StringUtils.IsNotNull(dictName))
            {
                cmdText += " and name like @name ";
                parmap.Add("name", "%" + dictName + "%");
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag ";
                parmap.Add("stopflag", stopFlag);
            }
            cmdText += " order by typeid, code asc ";
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetDictionaryByTypeId(string typeId)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_sys_code where compid = @compid";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            if (StringUtils.IsNotNull(typeId))
            {
                cmdText += " and typeid = @typeid ";
                parmap.Add("typeid", typeId);
            }
            cmdText += " and stopflag = '00' ORDER BY PRIORY ASC";
            return ExecuteDataTable(cmdText, parmap);
        }

        public string[] NewDictionary(string typeCode,string typeName, string code, string name, string mark, string stopFlag)
        {
            string cmdText = "p_sys_code_add";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_typecode",MySqlDbType.VarChar),
                new MySqlParameter("@in_typename",MySqlDbType.VarChar),
                new MySqlParameter("@in_code",MySqlDbType.VarChar),
                new MySqlParameter("@in_name",MySqlDbType.VarChar),
                new MySqlParameter("@in_mark",MySqlDbType.VarChar),
                new MySqlParameter("@in_stopflag",MySqlDbType.VarChar),
                new MySqlParameter("@in_modifyuser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = typeCode;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = typeName;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = code;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = name;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = mark;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Value = stopFlag;
                parameters[5].Direction = ParameterDirection.Input;
                parameters[6].Value = SessionDto.Empid;
                parameters[6].Direction = ParameterDirection.Input;
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public string[] EditDictionary(string id, string typeId, string typeCode, string typeName, string code, string name, string mark, string stopFlag)
        {
            string cmdText = "p_sys_code_update";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_id",MySqlDbType.VarChar),
                new MySqlParameter("@in_typeid",MySqlDbType.VarChar),
                new MySqlParameter("@in_typecode",MySqlDbType.VarChar),
                new MySqlParameter("@in_typename",MySqlDbType.VarChar),
                new MySqlParameter("@in_code",MySqlDbType.VarChar),
                new MySqlParameter("@in_name",MySqlDbType.VarChar),
                new MySqlParameter("@in_mark",MySqlDbType.VarChar),
                new MySqlParameter("@in_stopflag",MySqlDbType.VarChar),
                new MySqlParameter("@in_modifyuser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = id;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = typeId;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = typeCode;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = typeName;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = code;
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Value = name;
                parameters[5].Direction = ParameterDirection.Input;
                parameters[6].Value = mark;
                parameters[6].Direction = ParameterDirection.Input;
                parameters[7].Value = stopFlag;
                parameters[7].Direction = ParameterDirection.Input;
                parameters[8].Value = SessionDto.Empid;
                parameters[8].Direction = ParameterDirection.Input;
                parameters[9].Direction = ParameterDirection.Output;
                parameters[10].Direction = ParameterDirection.Output;

                retMsg = ExecProcWith2Out(cmdText, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retMsg;
        }

        public DataTable GetAreaDictionary(string value, string type)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from pub_region_cms_area where 1=1 ";
            if (StringUtils.IsNotNull(value))
            {
                cmdText += " and( code like @value or name like @value or mark like @value or typename like @value) ";
                parmap.Add("value", "%" + value + "%");
            }
            if (StringUtils.IsNotNull(type))
            {
                cmdText += " and type = @type";
                parmap.Add("type", type);
            }
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetClientsTypeDictionary(string value, string type) {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from pub_clientstype_group where 1=1 ";
            if (StringUtils.IsNotNull(value))
            {
                cmdText += " and( code like @value or name like @value or mark like @value or typename like @value) ";
                parmap.Add("value", "%" + value + "%");
            }
            if (StringUtils.IsNotNull(type))
            {
                cmdText += " and type = @type";
                parmap.Add("type", type);
            }
            return ExecuteDataTable(cmdText, parmap);
        }
        #endregion
        #region public
        public static string[] GetDynamicSql(string groupId, string field, string status)
        {
            string cmdText = "";
            if ("clients".Equals(status))
                cmdText = "p_scm_clients_group_dynam";
            else
                cmdText = "p_scm_waredict_group_dynam";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_groupId",MySqlDbType.VarChar),
                new MySqlParameter("@in_field",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = groupId;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = field;
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
    }
}
