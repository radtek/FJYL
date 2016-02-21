using FJYL.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace FJYL.Web.DbManager
{
    public static class UserManager
    {
        private const string User_Json_Path = "~/App_Data/users.json";

        public static bool SignIn(string username, string password)
        {
            var isAuthenticate = Authenticate(username, password);

            if (isAuthenticate)
            {
                SetAuthenticationCookie(username, false, null);
            }

            return isAuthenticate;
        }

        public static void SignOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
        }

        public static bool Authenticate(string username, string password)
        {
            var isAuthenticate = false;

            var user = GetUser(username);

            if (user != null && user.Password == password)
            {
                isAuthenticate = true;
            }

            return isAuthenticate;
        }

        public static User GetUser(string username)
        {
            var user = GetUsers().Where(u => u.Username.ToLower() == username.ToLower()).FirstOrDefault();
            return user;
        }

        public static List<User> GetUsers()
        {
            var fileName = HttpContext.Current.Server.MapPath(User_Json_Path);
            var users = JsonUtil.GetJsonData<List<User>>(fileName);
            return users;
        }

        public static void SetAuthenticationCookie(string username, bool isPersistent = false, int? timeout = null)
        {
            var authConfig = (AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication");

            DateTime issueDate = DateTime.Now;
            DateTime expiration;
            if (timeout.HasValue) expiration = issueDate.AddMinutes(timeout.Value);
            else expiration = issueDate.Add(authConfig.Forms.Timeout);
            var ticket = new FormsAuthenticationTicket(2, username.ToLower(), issueDate, expiration, isPersistent, username);

            var cookie = new HttpCookie(authConfig.Forms.Name, FormsAuthentication.Encrypt(ticket));
            cookie.Domain = authConfig.Forms.Domain;
            if (isPersistent) cookie.Expires = expiration;
            cookie.HttpOnly = true;
            cookie.Path = authConfig.Forms.Path;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}