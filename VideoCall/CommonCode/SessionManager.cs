
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using VideoCall.Common.Configuration;
using VideoCall.DataAccess.Entities;



namespace VideoCall.CommonCode
{
    public  class SessionManager :  ControllerBase, ISessionManager
    {
        private readonly IHttpContextAccessor _contx;
        private string secret = AppConfigs.SecretKey;

        private readonly IConfiguration _configuration;
        public SessionManager(IHttpContextAccessor contx,  IConfiguration configuration)
        {
            this._contx = contx;
            this._configuration = configuration;
        }


        public User? GetLoginAdminFromSession()
        {

            User? user = new User();

            var Users = _contx != null && _contx.HttpContext != null && _contx.HttpContext.Session != null ? _contx.HttpContext.Session.GetString(AppConfigs.CurrentUserCK) : null;
            if (!String.IsNullOrEmpty(Users))
            {
                user = JsonConvert.DeserializeObject<User>(Users);

                return user;
            }
            else
            {
                return null;
            }

        }
        public User? GetLoginAdminFromSessionAdmin()
        {

            User? user = new User();

            var Users = _contx != null && _contx.HttpContext != null && _contx.HttpContext.Session != null ? _contx.HttpContext.Session.GetString(AppConfigs.CurrentUserAdmin) : null;
            if (!String.IsNullOrEmpty(Users))
            {
                user = JsonConvert.DeserializeObject<User>(Users);

                return user;
            }
            else
            {

                return null;
            }

        }
        public UserCall GetUserCall()
        {
            UserCall userg = new UserCall();

            if (_contx.HttpContext != null && _contx.HttpContext.Session != null)
            {
                var cookieValue = _contx.HttpContext.Request.Cookies[AppConfigs.CurrentUserCK];
                if (!string.IsNullOrEmpty(cookieValue))
                {
                    userg = JsonConvert.DeserializeObject<UserCall>(cookieValue);
                    if (userg != null)
                    {
                        userg.SesstionID = _contx.HttpContext.Session.Id;
                        userg.isLogined = true;
                    }
                }

                //return new UserCall
                //{
                //    UserId = 1,
                //    UserName = "Anonymus",
                //    isLogined = false,
                //    SesstionID = _contx.HttpContext.Session.Id
                //};
            }
            return userg;

            //return new UserCall
            //{
            //    UserId = 1,
            //    UserName = "Anonymus",
            //    isLogined = false,
            //    SesstionID = ""
            //};
        }

        public User? GetLoginTeacherFromSession()
        {

            User? user = new User();

            var Users = _contx != null && _contx.HttpContext != null && _contx.HttpContext.Session != null ? _contx.HttpContext.Session.GetString("Teacher") : null;
            if (Users != null && !String.IsNullOrEmpty(Users))
            {
                user = JsonConvert.DeserializeObject<User>(Users);

                return user;
            }
            else
            {

                return null;
            }
        }
        public User GetGostayUserFromSession()
        {

            User user = new User();
            if (_contx != null && _contx.HttpContext != null)
            {
                var cookieValue = _contx.HttpContext.Request.Cookies[AppConfigs.CurrentUserCK];
                if (!string.IsNullOrEmpty(cookieValue))
                {
                    user = JsonConvert.DeserializeObject<User>(cookieValue);
                    //var result = DecryptPassword(user.UserVerify);
                    //user.Id = Int32.Parse(result);
                }
            }
            return user;

        }
        public void SetMenusInSession(string? DashboardLangShortName)
        {
            string? lang = String.IsNullOrEmpty(DashboardLangShortName) ? "en" : DashboardLangShortName;

        }

        public void SetUserDataInSession(User model)
        {
            //set session
            var user = JsonConvert.SerializeObject(model);
        
        }
        public string EncryptPassword(string strId)
        {
            if (string.IsNullOrEmpty(strId))
            {
                return "";
            }
            else
            {
                try
                {
                    strId = secret + "_" + strId;
                    byte[] data = ASCIIEncoding.ASCII.GetBytes(strId);
                    string uId = Convert.ToBase64String(data);
                    return uId;
                }
                catch (Exception)
                {
                    
                    return "";
                }
               
            }
           
        }
        public string DecryptPassword(string strId)
        {
            if (string.IsNullOrEmpty(strId))
            {
                return "";
            }
            else
            {
                try
                {
                    byte[] data = Convert.FromBase64String(strId);
                    string veri = ASCIIEncoding.ASCII.GetString(data);
                    var start = secret.Length +1;
                    var end = veri.Length - start;

                    string uId = veri.Substring(start, end);
                    return uId;
                }
                catch (Exception)
                {
                    return "";
                }
                
            }
        }
    }

    public interface ISessionManager
    {
        public User? GetLoginAdminFromSession();
        public User? GetLoginAdminFromSessionAdmin();
        public User GetGostayUserFromSession();
        public User? GetLoginTeacherFromSession();
        public UserCall GetUserCall();
        // public students? GetLoginStudentFromSession();
        public void SetUserDataInSession(User model);
        public void SetMenusInSession(string? DashboardLangShortName);
        public string EncryptPassword(string strId);
        public string DecryptPassword(string strId);
    }
}