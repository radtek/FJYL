using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FJYL.Web.Models
{
    public class ResponseResult
    {
        public ResponseResult()
        {
            this.Success = true;
        }
            
        public ResponseResult(bool success)
        {
            this.Success = success;
        }

        public ResponseResult(bool success, string message, object data)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }// 要返回的数据
    }
}