using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
 

namespace PriceManager
{
    class PMSystemDao : MySQLHelper
    {
        #region emp
        public string[] CheckLoginUser(string version,string user, string passwords, string deptId)
        {
            string cmdText = "price.p_pub_emp_logincheck";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_empcode",MySqlDbType.VarChar),
                new MySqlParameter("@in_pwd",MySqlDbType.VarChar),
                new MySqlParameter("@in_deptid",MySqlDbType.VarChar),
                new MySqlParameter("@in_version",MySqlDbType.VarChar),

                new MySqlParameter("@out_empid",MySqlDbType.VarChar),
                new MySqlParameter("@out_empname",MySqlDbType.VarChar),
                new MySqlParameter("@out_roleid",MySqlDbType.VarChar),
                new MySqlParameter("@out_rolename",MySqlDbType.VarChar),
                new MySqlParameter("@out_deptname",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = user;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = passwords;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = deptId;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = version;
                parameters[3].Direction = ParameterDirection.Input;

                parameters[4].Direction = ParameterDirection.Output;
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6].Direction = ParameterDirection.Output;
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8].Direction = ParameterDirection.Output;
                parameters[9].Direction = ParameterDirection.Output;
                parameters[10].Direction = ParameterDirection.Output;

                ExecProcWithPar(cmdText, parameters);
                retMsg[0] = parameters[parameters.Length - 2].Value.ToString().Trim();
                retMsg[1] = parameters[parameters.Length - 1].Value.ToString().Trim();
                string a = retMsg[1];
                if (retMsg[0].CompareTo("1") == 0)
                {

                    //--记录登陆状态
                    //retMsg[2] = parameters[4].Value.ToString().Trim();
                    //retMsg[3] = parameters[5].Value.ToString().Trim();
                    //retMsg[4] = parameters[6].Value.ToString().Trim();
                    //retMsg[5] = parameters[7].Value.ToString().Trim();
                    //retMsg[6] = parameters[8].Value.ToString().Trim();
                    //--记录登陆状态
                    SessionDto.Empid = parameters[4].Value.ToString().Trim();
                    SessionDto.Empcode = user;
                    SessionDto.Empname = parameters[5].Value.ToString().Trim();
                    SessionDto.Emproleid = parameters[6].Value.ToString().Trim();
                    SessionDto.Emprolename = parameters[7].Value.ToString().Trim();
                    SessionDto.Empdeptid = deptId;
                    SessionDto.Empdeptname = parameters[8].Value.ToString().Trim();
                    
                    //-- 保存账号密码
                    Properties.Settings.Default.D_USER = user;
                    Properties.Settings.Default.D_PWD = passwords;
                    Properties.Settings.Default.D_DEPT = parameters[8].Value.ToString().Trim();
                    Properties.Settings.Default.Save();
                    //--获取货主
                    SessionDto.Ownername = GetOwner();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            return retMsg;
        }

        private string GetOwner()
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from pub_owner where ownerid = @ownerid";
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            DataTable dt= ExecuteDataTable(cmdText, parmap);
            if (dt.Rows.Count == 0)
                return null;
            else
                return dt.Rows[0]["ownername"].ToString();
        }

        public string[] ChangePassword(string empId, string oldPwd, string newPwd)
        {
            string cmdText = "p_pub_emp_modifypassword";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_empId",MySqlDbType.VarChar),
                new MySqlParameter("@in_old_password",MySqlDbType.VarChar),
                new MySqlParameter("@in_new_password",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = empId;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = oldPwd;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = newPwd;
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

        public DataTable GetEmp(string empCode, string empName, string stopFlag,string deptId)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_emp_deptrole where compid = @compid ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            if (StringUtils.IsNotNull(empCode))
            {
                cmdText += " and empcode like @empcode ";
                parmap.Add("empcode", "%" + empCode + "%");
            }
            if (StringUtils.IsNotNull(empName))
            {
                cmdText += " and empname like @empname ";
                parmap.Add("empname", "%" + empName + "%");
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag ";
                parmap.Add("stopflag", stopFlag);
            }
            if (StringUtils.IsNotNull(deptId))
            {
                cmdText += " and deptid = @deptid ";
                parmap.Add("deptid", deptId);
            }
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetBuyer()
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_emp_buyer where compid = @compid and ownerid = @ownerid ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("ownerid", Properties.Settings.Default.OWNERID);
            return ExecuteDataTable(cmdText, parmap);
        }

        public string[] SaveEmp(string empId, string empPwd, string roleId, string stopFlag, string allowLogin)
        {
            string cmdText = "p_pub_emp_modify";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_empId",MySqlDbType.VarChar),
                new MySqlParameter("@in_empPwd",MySqlDbType.VarChar),
                new MySqlParameter("@in_roleId",MySqlDbType.VarChar),
                new MySqlParameter("@in_stopFlag",MySqlDbType.VarChar),
                new MySqlParameter("@in_allowLogin",MySqlDbType.VarChar),
                new MySqlParameter("@in_modifyUser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = empId;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = empPwd;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = roleId;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = stopFlag;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = allowLogin;
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
        #endregion
        #region dept
        public DataTable GetAllDept(string flag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from pub_dept where compid = @compid and ownerid = @owenerid and stopflag = '00'";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("owenerid", Properties.Settings.Default.OWNERID);
            if (StringUtils.IsNotNull(flag))
            {
                cmdText += "  and  menuflag = 1 ";
            }
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetEmpDept(string empId)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_emp_dept where empid = @empid ";
            parmap.Add("empid", empId);
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetEmpNoDept(string empId)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from pub_dept where compid = @compid and ownerid = @owenerid and stopflag = '00' and saledeptid not in (select saledeptid from pub_emp_dept where empid = @empid) ";
            parmap.Add("compid", Properties.Settings.Default.COMPID);
            parmap.Add("owenerid", Properties.Settings.Default.OWNERID);
            parmap.Add("empid", empId);
            return ExecuteDataTable(cmdText, parmap);
        }

        public string[] SaveEmpDept(string deptIdList, string empId)
        {
            string cmdText = "p_pub_emp_dept";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_empId",MySqlDbType.VarChar),
                new MySqlParameter("@in_deptIdList",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = empId;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = deptIdList;
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

        //查询货主配置规则表
        public int GetPubOwnerConfigureInfo(string deptid)
        {
            int retnum = 0;
            string sql = "SELECT a.compid, a.ownerid, a.saledeptid,a.saledept_type, a.detailtypemenu, a.grouptypeprior, a.delivrate, a.delivraterange, a.delivratedefault, a.allowprctype, a.ban_on_sale,a.prc_dept,a.goodchoose FROM pub_owner_configure AS a  where  a.compid=@compid and a.ownerid=@ownerid and  a.saledeptid=@saledeptid";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 30;
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
                cmd.Parameters.AddWithValue("saledeptid", deptid);
                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    PubOwnerConfigureDto.Compid = res["compid"].ToString().Trim();
                    PubOwnerConfigureDto.Ownerid = res["ownerid"].ToString().Trim();
                    PubOwnerConfigureDto.Saledeptid = res["saledeptid"].ToString().Trim();
                    PubOwnerConfigureDto.Saledepttype = res["saledept_type"].ToString().Trim();
                    PubOwnerConfigureDto.Detailtypemenu = res["detailtypemenu"].ToString().Trim();
                    PubOwnerConfigureDto.Grouptypeprior = res["grouptypeprior"].ToString().Trim();
                    PubOwnerConfigureDto.Delivrate = res["delivrate"].ToString().Trim();
                    PubOwnerConfigureDto.Delivraterange = res["delivraterange"].ToString().Trim();
                    PubOwnerConfigureDto.Delivratedefault = res["delivratedefault"].ToString().Trim();
                    PubOwnerConfigureDto.Allowprctype = res["allowprctype"].ToString().Trim();
                    PubOwnerConfigureDto.BanOnSale = res["ban_on_sale"].ToString().Trim();
                    PubOwnerConfigureDto.PrcDept = res["prc_dept"].ToString().Trim();
                    PubOwnerConfigureDto.Goodchoose = res["goodchoose"].ToString().Trim();
                    retnum++;
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

            return retnum;
        }


        //查询部门配置规则表
        public int GetPubDeptConfigureInfo(string deptid)
        {
            int retnum = 0;
            string sql = "SELECT a.compid, a.ownerid, a.saledeptid,a.saledept_type, a.detailtypemenu, a.grouptypeprior, a.delivrate, a.delivraterange, a.delivratedefault, a.allowprctype, a.ban_on_sale,a.prc_pri,a.wait_flag,a.valid_days,a.default_enddate FROM pub_dept_configure AS a  where  a.compid=@compid and a.ownerid=@ownerid and  a.saledeptid=@saledeptid";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 30;
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
                cmd.Parameters.AddWithValue("saledeptid", deptid);
                MySqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    PubDeptConfigureDto.Compid = res["compid"].ToString().Trim();
                    PubDeptConfigureDto.Ownerid = res["ownerid"].ToString().Trim();
                    PubDeptConfigureDto.Saledeptid = res["saledeptid"].ToString().Trim();
                    PubDeptConfigureDto.Saledepttype = res["saledept_type"].ToString().Trim();
                    PubDeptConfigureDto.Detailtypemenu = res["detailtypemenu"].ToString().Trim();
                    PubDeptConfigureDto.Grouptypeprior = res["grouptypeprior"].ToString().Trim();
                    PubDeptConfigureDto.Delivrate = res["delivrate"].ToString().Trim();
                    PubDeptConfigureDto.Delivraterange = res["delivraterange"].ToString().Trim();
                    PubDeptConfigureDto.Delivratedefault = res["delivratedefault"].ToString().Trim();
                    PubDeptConfigureDto.Allowprctype = res["allowprctype"].ToString().Trim();
                    PubDeptConfigureDto.BanOnSale = res["ban_on_sale"].ToString().Trim();
                    PubDeptConfigureDto.PrcPri = res["prc_pri"].ToString().Trim();
                    PubDeptConfigureDto.WaitFlag = res["wait_flag"].ToString().Trim();
                    PubDeptConfigureDto.ValidDays = res["valid_days"].ToString().Trim();
                    PubDeptConfigureDto.DefaultEnddate = res["default_enddate"].ToString().Trim();

                    retnum++;
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

            return retnum;
        }


        //查询归属部门配置规则表
        public DeptConfigureDto GetDeptConfigureInfo(string deptid)
        {
            string sql = "SELECT a.compid, a.ownerid, a.saledeptid,a.saledept_type, a.detailtypemenu, a.grouptypeprior, a.delivrate, a.delivraterange, a.delivratedefault, a.allowprctype, a.ban_on_sale,a.prc_pri,a.wait_flag,a.valid_days,a.default_enddate FROM pub_dept_configure AS a  where  a.compid=@compid and a.ownerid=@ownerid and  a.saledeptid=@saledeptid";

            DeptConfigureDto info = new DeptConfigureDto();
            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 30;
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
                cmd.Parameters.AddWithValue("saledeptid", deptid);
                MySqlDataReader res = cmd.ExecuteReader();
                
                while (res.Read())
                {
                    
                    info.Compid = res["compid"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Saledeptid = res["saledeptid"].ToString().Trim();
                    info.Saledepttype = res["saledept_type"].ToString().Trim();
                    info.Detailtypemenu = res["detailtypemenu"].ToString().Trim();
                    info.Grouptypeprior = res["grouptypeprior"].ToString().Trim();
                    info.Delivrate = res["delivrate"].ToString().Trim();
                    info.Delivraterange = res["delivraterange"].ToString().Trim();
                    info.Delivratedefault = res["delivratedefault"].ToString().Trim();
                    info.Allowprctype = res["allowprctype"].ToString().Trim();
                    info.BanOnSale = res["ban_on_sale"].ToString().Trim();
                    info.PrcPri = res["prc_pri"].ToString().Trim();
                    info.WaitFlag = res["wait_flag"].ToString().Trim();
                    info.ValidDays = res["valid_days"].ToString().Trim();
                    info.DefaultEnddate = res["default_enddate"].ToString().Trim();
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

            return info;
        }

        //查询系统配置规则表
        public SortableBindingList<SysCode> GetSysCode(Dictionary<string, string> sqlkeydict)
        {
            SortableBindingList<SysCode> infoList = new SortableBindingList<SysCode>();
            string sql = "SELECT a.compid, a.id, a.typeid, a.typecode, a.typename, a.code, a.name,"
            + "a.mark, a.stopflag, a.createuser, a.createdate, a.modifyuser, a.modifydate, a.ownerid,"
            + "a.detailtypemenu FROM v_sel_sys_code a where a.ownerid=@ownerid $ ";


            MySqlCommand cmd = null;
            try
            {
                cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 30;
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
                cmd.Parameters.AddWithValue("ownerid", Properties.Settings.Default.OWNERID);
                //cmd.Parameters.AddWithValue("compid", Properties.Settings.Default.COMPID);

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
                    SysCode info = new SysCode();
                    info.Compid = res["compid"].ToString().Trim();
                    info.Id = res["id"].ToString().Trim();
                    info.Typeid = res["typeid"].ToString().Trim();
                    info.Typecode = res["typecode"].ToString().Trim();
                    info.Typename = res["typename"].ToString().Trim();
                    info.Code = res["code"].ToString().Trim();
                    info.Name = res["name"].ToString().Trim();
                    info.Mark = res["mark"].ToString().Trim();
                    info.Stopflag = res["stopflag"].ToString().Trim();
                    info.Createuser = res["createuser"].ToString().Trim();
                    info.Createdate = res["createuser"].ToString().Trim();
                    info.Modifyuser = res["modifyuser"].ToString().Trim();
                    info.Modifydate = res["modifydate"].ToString().Trim();
                    info.Ownerid = res["ownerid"].ToString().Trim();
                    info.Detailtypemenu = res["detailtypemenu"].ToString().Trim();

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
        #region menu
        public DataTable GetMenuByRoleId(string roleId)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_rolemenu where roleid = @roleid order by menucode asc";
            parmap.Add("roleid", roleId);
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetAllMenu(string stopFlag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_menulevel where 1=1 ";
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag =  @stopflag ";
                parmap.Add("stopflag", stopFlag);
            }
            cmdText += " order by menucode asc";
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetMenuById(string menuId)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_menulevel where menuid = @menuid";
            parmap.Add("menuid", menuId);
            return ExecuteDataTable(cmdText, parmap);
        }

        public DataTable GetParentMenu(string menuLevel)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from v_pub_menulevel where level = @menulevel ";
            parmap.Add("menulevel", menuLevel);
            cmdText += " order by menucode asc";
            return ExecuteDataTable(cmdText, parmap);
        }

        public string[] SaveMenu(string menuCode, string menuName, string parentsId, string formName, string stopFlag)
        {
            string cmdText = "p_pub_menu_add";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_menuCode",MySqlDbType.VarChar),
                new MySqlParameter("@in_menuName",MySqlDbType.VarChar),
                new MySqlParameter("@in_parentsId",MySqlDbType.VarChar),
                new MySqlParameter("@in_formName",MySqlDbType.VarChar),
                new MySqlParameter("@in_stopFlag",MySqlDbType.VarChar),
                new MySqlParameter("@in_createUser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = menuCode;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = menuName;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = parentsId;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = formName;
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

        public string[] SaveMenu(string menuId, string menuCode, string menuName, string parentsId, string formName, string stopFlag)
        {
            string cmdText = "p_pub_menu_modify";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_menuId",MySqlDbType.VarChar),
                new MySqlParameter("@in_menuCode",MySqlDbType.VarChar),
                new MySqlParameter("@in_menuName",MySqlDbType.VarChar),
                new MySqlParameter("@in_parentsId",MySqlDbType.VarChar),
                new MySqlParameter("@in_formName",MySqlDbType.VarChar),
                new MySqlParameter("@in_stopFlag",MySqlDbType.VarChar),
                new MySqlParameter("@in_modifyUser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = menuId;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = menuCode;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = menuName;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = parentsId;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = formName;
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
        #endregion
        #region role
        public DataTable GetAllRole()
        {
            string cmdText = "select * from pub_role where stopflag = '00'";
            return ExecuteDataTable(cmdText, null);
        }

        public DataTable GetRole(string roleName, string stopFlag)
        {
            Dictionary<string, object> parmap = new Dictionary<string, object>();
            string cmdText = "select * from pub_role where 1=1 ";
            if (StringUtils.IsNotNull(roleName))
            {
                cmdText += " and rolename = @rolename ";
                parmap.Add("rolename", roleName);
            }
            if (StringUtils.IsNotNull(stopFlag))
            {
                cmdText += " and stopflag = @stopflag";
                parmap.Add("stopflag", stopFlag);
            }
            return ExecuteDataTable(cmdText, parmap);
        }

        public string[] SaveRole(string roleId, string roleName, string mark, string stopFlag)
        {
            string cmdText = "p_pub_role_modify";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_roleId",MySqlDbType.VarChar),
                new MySqlParameter("@in_roleName",MySqlDbType.VarChar),
                new MySqlParameter("@in_mark",MySqlDbType.VarChar),
                new MySqlParameter("@in_stopFlag",MySqlDbType.VarChar),
                new MySqlParameter("@in_modifyUser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = roleId;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = roleName;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = mark;
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = stopFlag;
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = SessionDto.Empid;
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

        public string[] SaveRole(string roleName, string mark, string stopFlag)
        {
            string cmdText = "p_pub_role_add";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_roleName",MySqlDbType.VarChar),
                new MySqlParameter("@in_mark",MySqlDbType.VarChar),
                new MySqlParameter("@in_stopFlag",MySqlDbType.VarChar),
                new MySqlParameter("@in_createUser",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = roleName;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = mark;
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = stopFlag;
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

        public string[] SaveRoleMenu(string roleId, string menuIdList)
        {
            string cmdText = "p_pub_role_menu";
            string[] retMsg = { "", "" };
            MySqlParameter[] parameters = {
                new MySqlParameter("@in_roleId",MySqlDbType.VarChar),
                new MySqlParameter("@in_menuIdList",MySqlDbType.VarChar),
                new MySqlParameter("@out_resultcode", MySqlDbType.VarChar),
                new MySqlParameter("@out_resultmsg", MySqlDbType.VarChar)
            };
            try
            {
                parameters[0].Value = roleId;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = menuIdList;
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
