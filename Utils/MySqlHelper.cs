using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace PriceManager
{
    class MySQLHelper : DatabaseHelper
    {
        protected static string Conn = "Database='" + DB + "';Data Source='" + DBIP + "';User Id='" + DBUN + "';Password='" + DBPW + "';Use Procedure Bodies=false; charset='utf8';Allow User Variables=true;pooling=true";

        protected static MySqlConnection connection = null;

        //--2019-1-7-----mysql从库
        protected static string Conns = "Database='" + DBS + "';Data Source='" + DBIPS + "';User Id='" + DBUNS + "';Password='" + DBPWS + "';Use Procedure Bodies=false; charset='utf8';Allow User Variables=true;pooling=true";

        protected static MySqlConnection connections = null;

        //---------------------------------------------------
        //Oracle测试库
        //protected static string Conn_cmszh = "Database='" + DB_cmszh + "';Data Source='" + DBIP_cmszh + "';User Id='" + DBUN_cmszh + "';Password='" + DBPW_cmszh + "';Use Procedure Bodies=false; charset='utf8';pooling=true";
        public static string DBname_datacenter_cmszh = DBUNbilltool;
        protected static string ConnString_datacenter_cmszh = string.Format("User ID={0};Password=" + DBPWbilltool + ";"
            + "Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = " + DBIPbilltool + ")"
            + "(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME ="+ DBbilltool+")))", DBname_datacenter_cmszh);
        protected static OracleConnection Conn_datacenter_cmszh = null;


        //---------------------------------------------------

        public int ConnectionTest()
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                GetConnection();
                if (connection.State == ConnectionState.Open)
                    return 1;
                else
                    return -1;
            }
            else
            {
                return 1;
            }

            
        }
        //--2019-1-7-----
        public int ConnectionTests()
        {
            if (connections == null || connections.State != ConnectionState.Open)
            {
                GetConnections();
                if (connections.State == ConnectionState.Open)
                    return 1;
                else
                    return -1;
            }
            else
            {
                return 1;
            }


        }

        public int ConnectionTest_cmszh()
        {
            //------------------------------------------------
            //Oracle测试库
            if (Conn_datacenter_cmszh == null || Conn_datacenter_cmszh.State != ConnectionState.Open)
            {
                GetConnection_cmszh();
                if (Conn_datacenter_cmszh.State == ConnectionState.Open)
                    return 1;
                else
                    return -1;
            }
            else
            {
                return 1;
            }
            //------------------------------------------------
        }

        public static void GetConnection()
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                try
                {
                    connection = new MySqlConnection(Conn);
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
        //--2019-1-7-----
        public static void GetConnections()
        {
            if (connections == null || connections.State != ConnectionState.Open)
            {
                try
                {
                    connections = new MySqlConnection(Conns);
                    connections.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        public static void GetConnection_cmszh()
        {
            //--------------------------------------------------
            //Oracle测试库
            if (Conn_datacenter_cmszh == null || Conn_datacenter_cmszh.State != ConnectionState.Open)
            {
                try
                {
                    Conn_datacenter_cmszh = new OracleConnection(ConnString_datacenter_cmszh);
                    Conn_datacenter_cmszh.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //---------------------------------------------------
        }

        /// <summary>
        /// 用假设参数执行一个sql命令
        /// </summary>
        /// <param name="cmdText">sql命令语句</param>
        /// <param name="parMap">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, Dictionary<string, object> parMap)
        {

            MySqlCommand cmd = null;
            int result = 0;
            try
            {
                cmd = new MySqlCommand()
                {
                    CommandTimeout = 3000
                };
                PrepareCommand(cmd, null, CommandType.Text, cmdText, parMap, null);
                result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
                //if (connection != null)
                //{
                //    connection.Close();
                //    connection.Dispose();
                //    connection = null;
                //}
            }
            return result;
        }
        /// <summary>
        /// 用假设参数执行一个sql命令
        /// </summary>
        /// <param name="cmdText">sql命令语句</param>
        /// <param name="parMap">执行命令所用参数的集合</param>
        /// <returns>执行命令所返回集合</returns>
        public static DataTable ExecuteDataTable(string cmdText, Dictionary<string, object> parMap)
        {

            MySqlCommand cmd = null;
            MySqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                cmd = new MySqlCommand()
                {
                    CommandTimeout = 3000
                };
                PrepareCommand(cmd, null, CommandType.Text, cmdText, parMap, null);
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
                //if (connection != null)
                //{
                //    connection.Close();
                //    connection.Dispose();
                //    connection = null;
                //}
            }
            return dt;
        }
        /// <summary>
        /// 用假设参数执行一个存储过程
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行存储过程返回参数</returns>
        public static string[] ExecProcWith2Out(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = null;
            string[] retMsg = new string[2];
            try
            {
                cmd = new MySqlCommand();
                PrepareCommand(cmd, null, CommandType.StoredProcedure, cmdText, null, commandParameters);
                cmd.CommandTimeout = 3000;
                commandParameters[commandParameters.Length - 2].Size = 2048;
                commandParameters[commandParameters.Length - 1].Size = 2048;
                int result = cmd.ExecuteNonQuery();
                retMsg[0] = commandParameters[commandParameters.Length - 2].Value.ToString().Trim();
                retMsg[1] = commandParameters[commandParameters.Length - 1].Value.ToString().Trim();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                retMsg[0] = "0";
                retMsg[1] = ex.Message.ToString();
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
                //if (connection != null)
                //{
                //    connection.Close();
                //    connection.Dispose();
                //    connection = null;
                //}
            }
            return retMsg;
        }


        /// <summary>
        /// 用假设参数执行一个存储过程
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行存储过程返回参数</returns>
        public static void ExecProcWithPar(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = null;
            try
            {
                cmd = new MySqlCommand();
                PrepareCommand(cmd, null, CommandType.StoredProcedure, cmdText, null, commandParameters);
                cmd.CommandTimeout = 3000;
                int result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                commandParameters[commandParameters.Length - 2].Value = "0";
                commandParameters[commandParameters.Length - 1].Value = ex.Message.ToString();
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
                //if (connection != null)
                //{
                //    connection.Close();
                //    connection.Dispose();
                //    connection = null;
                //}
            }

        }

        /// <summary>
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">sql命令</param>
        /// <param name="conn">OleDb连接</param>
        /// <param name="trans">OleDb事务</param>
        /// <param name="cmdType">命令类型例如 存储过程或者文本</param>
        /// <param name="cmdText">命令文本,例如:Select * from Products</param>
        /// <param name="parMap">执行命令的参数</param>
        /// <param name="cmdParms">执行命令的参数</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:检查 SQL 查询是否存在安全漏洞")]
        private static void PrepareCommand(MySqlCommand cmd, MySqlTransaction trans,
            CommandType cmdType, string cmdText, Dictionary<string, object> parMap, params MySqlParameter[] cmdParms)
        {
            GetConnection();

            cmd.Connection = connection;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (parMap != null)
            {
                cmd.Parameters.Clear();
                foreach (var par in parMap)
                    cmd.Parameters.AddWithValue(par.Key, par.Value);
            }

            if (cmdParms != null)
            {
                cmd.Parameters.Clear();
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        public static void PingDB()
        {
            string cmdText = "select 1 from dual";
            ExecuteNonQuery(cmdText, null);
        }
        public static void CloseDB()
        {
            try
            {
                connection.Close();
            }
            catch (Exception)
            {
            }
        }

        public static void CloseDBS()
        {
            try
            {
                connections.Close();
            }
            catch (Exception)
            {
            }
        }

        public static int pingAllDB()
        {

            if (pingDB(Conn_datacenter_cmszh) != 0)
            {
                try
                {
                    Conn_datacenter_cmszh.Open();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("CMSZH" + ex.Message.ToString());
                    return -3;
                }
            }

            return 0;
        }
        protected static int pingDB(OracleConnection conn)
        {
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select 1 from dual";
                OracleDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    break;
                }
                res.Close();
                cmd.Dispose();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return -1;
            }
        }

        public static void closeAllDB()
        {
            try
            {

                Conn_datacenter_cmszh.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
