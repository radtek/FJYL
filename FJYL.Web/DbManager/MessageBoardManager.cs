using FJYL.Web.DbManager;
using FJYL.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FJYL.Web.DbManager
{
    public class MessageBoardManager
    {
        #region SaveMessage
        public bool SaveMessage(MessageBoard message)
        {
            OleDbParameter[] parameters = GetMessageBoardParams(message);

            string sql = "insert into MessageBoard ([Name],[Homepage],[QQ],[Phone],[Message],[CreateTime]) values (@Name,@Homepage,@QQ,@Phone,@Message,@CreateTime)";

            bool success = new AccessDbUtil().ExeSQL(sql, parameters);

            return success;
        }

        private OleDbParameter[] GetMessageBoardParams(MessageBoard message)
        {
            OleDbParameter name = new OleDbParameter("Name", OleDbType.VarWChar);
            name.Value = AccessSqlUtil.GetStringValue(message.Name);

            OleDbParameter homepage = new OleDbParameter("Homepage", OleDbType.VarWChar);
            homepage.Value = AccessSqlUtil.GetStringValue(message.Homepage);

            OleDbParameter qq = new OleDbParameter("QQ", OleDbType.VarWChar);
            qq.Value = AccessSqlUtil.GetStringValue(message.QQ);

            OleDbParameter phone = new OleDbParameter("Phone", OleDbType.VarWChar);
            phone.Value = AccessSqlUtil.GetStringValue(message.Phone);

            OleDbParameter msg = new OleDbParameter("Message", OleDbType.VarWChar);
            msg.Value = AccessSqlUtil.GetStringValue(message.Message);

            OleDbParameter createTime = new OleDbParameter("CreateTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            //createTime.Value = DateTime.Now;

            OleDbParameter[] parameters = { 
                                              name, 
                                              homepage,
                                              qq,
                                              phone,
                                              msg,
                                              createTime
                                          };
            return parameters;
        }

        #endregion

        #region Get Message

        public List<MessageBoard> GetMessageBoard(int index, int size, ref int pageCount, ref int recordCount)
        {
            var key = "ID";
            var fields = "[Name],[Homepage],[QQ],[Phone],[Message],[CreateTime]";
            var tables = "MessageBoard";
            var where = "";
            var orderBy = "ID DESC";

            var dataTable = new AccessDbUtil().ExecutePager(index, size, key, fields, tables, where, orderBy, ref pageCount, ref recordCount);

            var messages = new List<MessageBoard>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                messages.Add(new MessageBoard
                {
                    Name = GetRowString(dataTable.Rows[i]["Name"]),
                    Homepage = GetRowString(dataTable.Rows[i]["Homepage"]),
                    QQ = GetRowString(dataTable.Rows[i]["QQ"]),
                    Phone = GetRowString(dataTable.Rows[i]["Phone"]),
                    Message = GetRowString(dataTable.Rows[i]["Message"]),
                    CreateTime = Convert.ToDateTime(dataTable.Rows[i]["CreateTime"])
                });
            }

            return messages;
        }

        private string GetRowString(object data)
        {
            return data != null ? Convert.ToString(data) : "";
        }

        #endregion
    }
}