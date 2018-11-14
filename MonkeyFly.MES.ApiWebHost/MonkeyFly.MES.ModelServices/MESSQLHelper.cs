using System;
using System.Data;
using System.Data.SqlClient;
using MonkeyFly.Utils;
using System.Collections;
using MonkeyFly.AspNet;
using MonkeyFly.Core;
using System.Web;
using MonkeyFly.MES.BasicService;

namespace MonkeyFly.MES.ModelServices
{
    public class MESSQLHelper
    {

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string mfcconn = ConfigHelper.GetConnectionString("MES").ToString();

        /// <summary>
        /// 返回对应的类型, int, decima, string, data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static T getValue<T>(string sql, CommandType type = CommandType.Text)
        {
            DataLogerService.writeSQL(sql);
            SqlConnection conn = dbConnection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = type;

                beginTransaction(cmd);
                var obj = cmd.ExecuteScalar();

                if (obj == null) return default(T);
                Type t = typeof(T);
                if (t == typeof(int))
                    return (T)Convert.ChangeType(obj.toInt32(), t);
                else if (t == typeof(decimal))
                    return (T)Convert.ChangeType(obj.toDecimal(), t);
                else if (t == typeof(string))
                    return (T)Convert.ChangeType(obj.toStr(), t);
                else if (t == typeof(DateTime))
                    return (T)Convert.ChangeType(obj.toDatetime(), t);

                throw new Exception("不可识别类型:" + t);
            }
            catch (Exception err)
            {
                throw new Exception("getvalue:" + sql + "," + err.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        ///  执行非查询ExecuteNonQuery返回受影响的行数，增加，修改，删除
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectstring"></param>
        /// <returns></returns>
        public static int exec(string commandText, string connectstring = "")
        {
            DataLogerService.writeSQL(commandText);
            if (connectstring == "") connectstring = mfcconn;
            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.Text;
                    int result = cmd.ExecuteNonQuery();
                    DataLogerService.writeSQLData(result.ToString());
                    return result;
                }
            }
        }


        /// <summary>
        ///  执行非查询ExecuteNonQuery返回受影响的行数，增加，修改，删除
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static int exec(string commandText)
        {
            DataLogerService.writeSQL(commandText);
            SqlConnection conn = dbConnection();
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.Text;
                beginTransaction(cmd);
                int result = cmd.ExecuteNonQuery();
                DataLogerService.writeSQLData(result.ToString());
                return result;
            }
            catch
            {
                //conn.Close();
                throw new Exception("executeNonQuery:" + commandText);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 单独给外部的方法，不存在事务
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static DataSet getDataSet(string commandText, string connectstring = "")
        {
            DataLogerService.writeSQL(commandText);
            if (connectstring == "") connectstring = mfcconn;
            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();

                        // Fill the DataSet using default values for DataTable names, etc
                        da.Fill(ds);

                        // Detach the OracleParameters from the command object, so they can be used again
                        cmd.Parameters.Clear();

                        return ds;
                    }
                }
            }
        }

        /// <summary>
        /// 执行sql语句并返回dataset
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static DataSet getDataSet(string commandText)
        {
            DataLogerService.writeSQL(commandText);
            SqlConnection conn = dbConnection();
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.Text;
                beginTransaction(cmd);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();

                    // Fill the DataSet using default values for DataTable names, etc
                    da.Fill(ds);

                    // Detach the OracleParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();

                    return ds;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("executeNonQuery:" + commandText+"--->"+ ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///  执行sql语句并返回dataset(不写日志)
        ///  SAM 2016年2月18日17:27:30
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static DataSet getDataSetNoLog(string commandText)
        {
            SqlConnection conn = dbConnection();
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.Text;
                beginTransaction(cmd);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    cmd.Parameters.Clear();

                    return ds;
                }
            }
            catch
            {
                throw new Exception("executeNonQuery:" + commandText);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 输出一个table到页面,ds的第一个table
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="val"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable getDataTable(string sql)
        {
            DataLogerService.writeSQL(sql);
            DataTable dt = getDataSet(sql).Tables[0];
            DataLogerService.writeSQLDataTable(dt);
            //pb[val] = dt;
            return dt;
        }

        /// <summary>
        /// 不写日志
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable getDataTableNoLog(string sql)
        {
            DataTable dt = getDataSetNoLog(sql).Tables[0];
            return dt;
        }

        /// <summary>
        /// 返回所有rows
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataRowCollection getRows(string sql)
        {
            DataSet ds = getDataSet(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows;
            else
                return null;
        }


        public static DataRow getRow(string sql)
        {
            DataSet ds = getDataSet(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0];
            else
                return null;
        }


        /// <summary>
        /// 一次请求 就一完整的sql语句
        /// </summary>
        public static string session_connect = "session_connect";
        /// <summary>
        /// 获取连接字符串SqlConn进行数据判断，返回连接
        /// </summary>
        /// <param name="connstr">数据库连接字符串</param>
        /// <returns>返回数据连接字符串</returns>
        static SqlConnection dbConnection()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session[session_connect] != null)
            {
                SqlConnection conn = (SqlConnection)HttpContext.Current.Session[session_connect];
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                return conn;
            }
            else
            {
                SqlConnection conn = new SqlConnection(mfcconn);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                    HttpContext.Current.Session[session_connect] = conn;

                return conn;
            }


        }
        /// <summary>
        /// 当前用户的session
        /// </summary>
        public static string session_transaction = "session_transaction";
        public static string has_transaction = "has_transaction";

        static void beginTransaction(SqlCommand command)
        {
            //如果不存在，就不用事务了。
            if (HttpContext.Current.Session == null || HttpContext.Current.Session[has_transaction] == null) return;
            transAttribute tran = (transAttribute)HttpContext.Current.Session[has_transaction];
            var transaction = HttpContext.Current.Session[session_transaction];
            if (transaction != null)
            {
                SqlTransaction sqltransaction = (SqlTransaction)transaction;
                //if (sqltransaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = sqltransaction;
            }
            else
            {
                //todo:到底用什么锁？
                SqlTransaction sqltransaction = command.Connection.BeginTransaction(tran.iso);
                //sqltransaction.Connection = command.Connection;

                command.Transaction = sqltransaction;
                HttpContext.Current.Session[session_transaction] = sqltransaction;
            }
        }
        /// <summary>
        /// 关闭数据源
        /// </summary>
        public static void dbConnectionClose()
        {
            if (HttpContext.Current.Session[session_connect] != null)
            {
                SqlConnection conn = (SqlConnection)HttpContext.Current.Session[session_connect];
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public static void transactionCommit()
        {
            if (HttpContext.Current.Session[has_transaction].toStr().Length == 0)
            {
                dbConnectionClose();
                return;
            }
            var transaction = HttpContext.Current.Session[session_transaction];
            if (transaction != null)
            {
                SqlTransaction sqltransaction = (SqlTransaction)transaction;
                sqltransaction.Commit();

                HttpContext.Current.Session[session_transaction] = null;
            }
            dbConnectionClose();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public static void transactionRollback()
        {
            if (HttpContext.Current.Session[has_transaction].toStr().Length == 0)
            {
                dbConnectionClose();
                return;
            }
            var transaction = HttpContext.Current.Session[session_transaction];
            if (transaction != null)
            {
                SqlTransaction sqltransaction = (SqlTransaction)transaction;
                sqltransaction.Rollback();

                HttpContext.Current.Session[session_transaction] = null;
            }
            dbConnectionClose();
        }

        #region
        /// <summary>
        /// 参数设置
        /// </summary>
        /// <param name="command"></param>
        /// <param name="commandParameters"></param>
        private static void attachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
        /// <summary>
        /// 过滤非法字符
        /// </summary>
        static void addParams(IList lstParam, string strKey, SqlDbType sqlDbType, object objValue)
        {
            if (!strKey.StartsWith("@")) strKey = strKey.Insert(0, "@");

            SqlParameter sp = new SqlParameter(strKey, sqlDbType);
            sp.Value = objValue;
            lstParam.Add(sp);
        }


        //static string Const_CommondName = "PAGER_MSSQL";
        //static string Const_PageNow = "@pagenow";
        //static string Const_PageSize = "@pagesize";
        //static string Const_RecordCount = "@recordcount";
        /// <summary>
        /// 实现MSSQL分页 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="strtablename"></param>
        /// <param name="fileds"></param>
        /// <param name="strorderfileds"></param>
        /// <param name="ipageindex"></param>
        /// <param name="ipagesize"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        //public static pageinfo showPager(pagebase pb, string tableName, string fileds, string orderFileds, int ipageindex, int ipagesize, string where)
        //{
        //    //pageinfo pageinfo = new pageinfo();

        //    if (string.IsNullOrEmpty(orderFileds)) throw new Exception("必须定义排序字段!");

        //    IList lstParam = new ArrayList();
        //    addParams(lstParam, "tablename", SqlDbType.VarChar, tableName);
        //    addParams(lstParam, "columns", SqlDbType.VarChar, fileds);
        //    addParams(lstParam, "ordercolumns", SqlDbType.VarChar, orderFileds);
        //    addParams(lstParam, "where", SqlDbType.VarChar, where);

        //    int len = lstParam.Count;
        //    SqlParameter[] param = new SqlParameter[len + 3];

        //    if (len > 0) for (int i = 0; i < lstParam.Count; i++) param[i] = (SqlParameter)lstParam[i];

        //    ipagesize = (ipagesize == 0 ? pageinfo.pagesize : ipagesize);
        //    ipageindex = Math.Max(ipageindex, 1);

        //    param[len] = new SqlParameter(Const_PageNow, SqlDbType.Int, 4); param[len].Value = ipageindex;
        //    param[len + 1] = new SqlParameter(Const_PageSize, SqlDbType.Int, 4); param[len + 1].Value = ipagesize;
        //    param[len + 2] = new SqlParameter(Const_RecordCount, SqlDbType.Int, 4); param[len + 2].Direction = ParameterDirection.Output; param[len + 2].Value = 0;


        //    SqlConnection conn = dbConnection();

        //    SqlCommand cmd = new SqlCommand();

        //    cmd.Connection = conn;
        //    cmd.CommandText = Const_CommondName;
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    attachParameters(cmd, param);
        //    beginTransaction(cmd);
        //    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //    {
        //        DataSet ds = new DataSet();

        //        da.Fill(ds);
        //        cmd.Parameters.Clear();

        //        pageinfo.data = ds;
        //    }

        //    pageinfo.recordcount = (int)param[len + 2].Value;
        //    pageinfo.setfirstindex(ipageindex, ipagesize);

        //    //输出出去
        //    pageinfo.json(pb);

        //    //这里名称写死了。
        //    pb["pager"] = pageinfo;



        //    //pb.dopager();

        //    return pageinfo;
        //}


        #endregion

        /// <summary>
        /// 唯一性判断
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        /// <param name="keycolumn"></param>
        /// <param name="keyvalue"></param>
        /// <param name="where"></param>
        public static void Uniqueness(string table, string column, string value, string msg, string keycolumn = "", string keyvalue = "", string where = "")
        {
            string sql = "select count(1) from {0} where {1}='{2}' {3} {4}";

            if (where != "") where = " and " + where;

            if (string.IsNullOrEmpty(keycolumn) || string.IsNullOrEmpty(keyvalue))
                sql = string.Format(sql, _f(table), _f(column), value, "", where);
            else
                sql = string.Format(sql, _f(table), _f(column), value, " and " + keycolumn + "<>" + keyvalue, where);

            int count = getValue<int>(sql);
            if (count > 0) throw new Exception(msg);

        }



        static string _f(string k)
        {
            return "[" + k + "]";
        }

        /// <summary>
        /// 获取流水编号（此方法调用前提是SerialNumber表中已有所有表的初始化数据）
        /// SAM 2016年11月3日18:43:52
        /// </summary>
        /// <param name="tname">表名</param>
        /// <param name="amount">获取流水数量</param>
        /// <returns></returns>
        public static string[] getSNID(string tname, int amount)
        {
            string[] result = null;
            DataRow dr = null;
            int count = 0;                                 //用于更新成功后的返回计数

            SqlConnection conn = dbConnection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction transaction = conn.BeginTransaction();
                cmd.Transaction = transaction;
                cmd.CommandText = String.Format(@"select * from [SYS_SerialNumber] WITH(UPDLock) where [SystemID]='{0}' and [TableName]=N'{1}'", Framework.SystemID, tname);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dr = ds.Tables[0].Rows[0];
                String number = (String)dr["Number"];
                int id = (int)dr["ID"];
                count = Convert.ToInt32(number, 16);      //赋原值，更新成功后在原值上加计数

                number = String.Format("{0:X}", count + amount).PadLeft(6, '0');

                cmd.CommandText = String.Format(@"UPDATE [dbo].[SYS_SerialNumber] SET [Number]=N'{0}',[Modifier]='{1}',[ModifiedTime]='{2}' WHERE [ID]={3}", number, "adminy", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), id);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception err)
            {
                //DataLogerService.
                //TODO:写入日志
                DataLogerService.writeerrlog(err);
                //throw err;
            }
            finally
            {
                conn.Close();
            }

            result = new string[amount];
            string serial = (string)dr["FirstSN"] + dr["TablepropertyID"].ToString().PadLeft(3, '0') + DateTime.Now.ToString((string)dr["DateID"]);
            for (int i = 0; i < amount; i++)
            {
                result[i] = serial + String.Format("{0:X}", count + i + 1).PadLeft(6, '0');
            }
            return result;
        }
    }


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class transAttribute : Attribute
    {
        public IsolationLevel iso { get; set; }
        public transAttribute(IsolationLevel iso = IsolationLevel.Serializable)
        {
            this.iso = iso;
        }
    }



}
