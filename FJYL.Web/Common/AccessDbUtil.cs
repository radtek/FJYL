using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FJYL.Web.DbManager
{
    public class AccessDbUtil
    {  /// <summary>
        /// 连接数据库字符串
        /// </summary>
        private string connectionString;

        /// <summary>
        /// 获得连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }

        /// <summary>
        /// 构造函数：数据库的默认连接
        /// </summary>
        public AccessDbUtil()
        {
            string dbPath = System.Configuration.ConfigurationManager.AppSettings["AccessDBPath"].ToString();
            dbPath = "Data Source=" + System.Web.HttpContext.Current.Server.MapPath(dbPath);
            string dbProvider = "Provider=Microsoft.Jet.OLEDB.4.0;";
            connectionString = dbProvider + dbPath + "Persist Security Info=False;";
        }

        /// <summary>
        /// 构造函数：带有参数的数据库连接
        /// </summary>
        /// <param name="newConnectionString"></param>
        public AccessDbUtil(string newConnectionString)
        {
            connectionString = newConnectionString;
        }

        /// <summary>
        /// 执行SQL语句没有返回结果，如：执行删除、更新、插入等操作
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>操作成功标志</returns>
        public bool ExeSQL(string strSQL)
        {
            bool resultState = false;

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                OleDbTransaction myTrans = conn.BeginTransaction();
                try
                {
                    OleDbCommand command = new OleDbCommand(strSQL, conn, myTrans);
                    command.ExecuteNonQuery();
                    myTrans.Commit();
                    resultState = true;
                }
                catch
                {
                    myTrans.Rollback();
                    resultState = false;
                }
            }

            return resultState;
        }

        /// <summary>
        /// 执行SQL语句没有返回结果，如：执行删除、更新、插入等操作
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>操作成功标志</returns>
        public bool ExeSQL(string strSQL, params OleDbParameter[] parameters)
        {
            bool resultState = false;

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                OleDbTransaction myTrans = conn.BeginTransaction();

                try
                {
                    OleDbCommand command = new OleDbCommand(strSQL, conn, myTrans);
                    if (parameters != null)
                    {
                        foreach (OleDbParameter parm in parameters)
                        {
                            command.Parameters.Add(parm);
                        }
                    }

                    command.ExecuteNonQuery();
                    myTrans.Commit();
                    resultState = true;
                }
                catch
                {
                    myTrans.Rollback();
                    resultState = false;
                }
            }

            return resultState;
        }

        /// <summary>
        /// 执行SQL语句返回结果到DataReader中
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>dataReader</returns>
        private OleDbDataReader ReturnDataReader(string strSQL)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, conn);
                OleDbDataReader dataReader = command.ExecuteReader();
                return dataReader;
            }
        }

        /// <summary>
        /// 执行SQL语句返回结果到DataSet中
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>DataSet</returns>
        public DataSet ReturnDataSet(string strSQL)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                OleDbDataAdapter OleDbDA = new OleDbDataAdapter(strSQL, conn);
                OleDbDA.Fill(dataSet, "objDataSet");
                return dataSet;
            }
        }

        /// <summary>
        /// 执行SQL语句返回结果到DataSet中
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>DataSet</returns>
        public DataSet ReturnDataSet(string strSQL, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                DataSet dataSet = new DataSet();

                try
                {
                    OleDbCommand command = new OleDbCommand(strSQL, conn);
                    if (parameters != null)
                    {
                        foreach (OleDbParameter parm in parameters)
                        {
                            command.Parameters.Add(parm);
                        }
                    }
                    OleDbDataAdapter OleDbDA = new OleDbDataAdapter(command);

                    OleDbDA.Fill(dataSet, "objDataSet");
                }
                catch
                {
                }

                return dataSet;
            }
        }

        /// <summary>
        /// 执行SQL语句返回结果到DataTable中
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>DataSet</returns>
        public DataTable ReturnDataTable(string strSQL)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                DataTable dataTable = new DataTable();
                OleDbDataAdapter OleDbDA = new OleDbDataAdapter(strSQL, conn);
                OleDbDA.Fill(dataTable);
                return dataTable;
            }
        }

        /// <summary>
        /// 执行SQL语句返回结果到DataTable中
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>DataSet</returns>
        public DataTable ReturnDataTable(string strSQL, params OleDbParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(strSQL, conn);
                    if (parameters != null)
                    {
                        foreach (OleDbParameter parm in parameters)
                        {
                            command.Parameters.Add(parm);
                        }
                    }
                    OleDbDataAdapter OleDbDA = new OleDbDataAdapter(command);

                    OleDbDA.Fill(dataTable);
                }
                catch //(Exception ex)
                {
                    //throw ex;
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 执行一查询语句，同时返回查询结果数目
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>sqlResultCount</returns>
        public int ReturnSqlResultCount(string strSQL)
        {
            int sqlResultCount = 0;

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(strSQL, conn);
                    OleDbDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        sqlResultCount++;
                    }
                    dataReader.Close();
                }
                catch
                {
                    sqlResultCount = 0;
                }
            }

            return sqlResultCount;
        }

        #region 分页
        /// <summary>
        /// ACCESS高效分页
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">分页容量</param>
        /// <param name="strKey">主键</param>
        /// <param name="showString">显示的字段</param>
        /// <param name="tables">查询表，支持联合查询</param>
        /// <param name="whereString">查询条件，若有条件限制则必须以where 开头</param>
        /// <param name="orderString">排序规则</param>
        /// <param name="pageCount">传出参数：总页数统计</param>
        /// <param name="recordCount">传出参数：总记录统计</param>
        /// <returns>装载记录的DataTable</returns>
        public DataTable ExecutePager(int pageIndex, int pageSize, string strKey, string showString, string tables, string whereString, string orderString, ref int pageCount, ref int recordCount)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (string.IsNullOrEmpty(showString)) showString = "*";
            if (string.IsNullOrEmpty(orderString)) orderString = strKey + " asc ";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string myVw = string.Format("( select * from {0} ) tempVw ", tables);
                OleDbCommand cmdCount = new OleDbCommand(string.Format(" select count(*) as recordCount from {0} {1}", myVw, whereString), conn);

                recordCount = Convert.ToInt32(cmdCount.ExecuteScalar());

                if ((recordCount % pageSize) > 0)
                    pageCount = recordCount / pageSize + 1;
                else
                    pageCount = recordCount / pageSize;
                OleDbCommand cmdRecord;
                if (pageIndex == 1)//第一页
                {
                    cmdRecord = new OleDbCommand(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, myVw, whereString, orderString), conn);
                }
                else if (pageIndex > pageCount)//超出总页数
                {
                    cmdRecord = new OleDbCommand(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, myVw, "where 1=2", orderString), conn);
                }
                else
                {
                    int pageLowerBound = pageSize * pageIndex;
                    int pageUpperBound = pageLowerBound - pageSize;
                    string recordIDs = GetRecordID(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageLowerBound, strKey, myVw, whereString, orderString), pageUpperBound);
                    cmdRecord = new OleDbCommand(string.Format("select {0} from {1} where {2} in ({3}) order by {4} ", showString, myVw, strKey, recordIDs, orderString), conn);

                }
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmdRecord);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                conn.Close();
                conn.Dispose();
                return dt;
            }
        }

        /// <summary>
        /// 分页使用
        /// </summary>
        /// <param name="query"></param>
        /// <param name="passCount"></param>
        /// <returns></returns>
        private string GetRecordID(string query, int passCount)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(query, conn);
                string result = string.Empty;
                using (OleDbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (passCount < 1)
                        {
                            result += "," + dr.GetInt32(0);
                        }
                        passCount--;
                    }
                }
                conn.Close();
                conn.Dispose();
                return result.Substring(1);
            }
        }
        #endregion
    }
}