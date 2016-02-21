using FJYL.Web.DbManager;
using FJYL.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FJYL.Web.Api
{
    public class MessageBoardController : ApiController
    {
        private const int Page_Size = 10;

        [HttpPost]
        public ResponseResult Save(MessageBoard message)
        {
            ResponseResult result = new ResponseResult();

            try
            {
                result.Success = new MessageBoardManager().SaveMessage(message);

                if (!result.Success)
                {
                    result.Message = "系统错误, 请稍后重试!";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        [HttpGet]
        public ResponseResult Get(int id)
        {
            ResponseResult result = new ResponseResult();

            try
            {
                var pageCount = 0;
                var recordCount = 0;
                var messages = new MessageBoardManager().GetMessageBoard(id, Page_Size, ref pageCount, ref recordCount);

                var data = new
                {
                    Messages = messages,
                    PageCount = pageCount,
                    RecordCount = recordCount
                };

                result.Data = data;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
