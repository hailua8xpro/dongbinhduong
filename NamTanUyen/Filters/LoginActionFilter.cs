
using System.Web;
using System.Web.Mvc;
using Library.Helper;
using Newtonsoft.Json;
using NamTanUyen.ViewModels;
using NamTanUyen.Models;
using System.Linq;

namespace NamTanUyen.Filters
{
    public class CustomAuthorizer : AuthorizeAttribute
    {
        private JewelryContext db = new JewelryContext();
        public bool CheckLogged()
        {
            bool islogged = false;
            var curr_session = HttpContext.Current.Session[ConfigHelper.SessionAdminUser];
            if (curr_session != null)
            {
                islogged = true;
            }
            else if (HttpContext.Current.Request.Cookies[ConfigHelper.CookieAdmin] != null)
            {
                var userck = JsonConvert.DeserializeObject<LoginViewModel>(HttpContext.Current.Request.Cookies[ConfigHelper.CookieAdmin].Value);
                if (userck != null && userck.Email != null)
                {
                    var db_user = db.AdminUsers.SingleOrDefault(x => x.Email == userck.Email && x.PassWord == userck.Password && x.IsActive == true);
                    if (db_user != null)
                    {
                        islogged = true;
                    }
                }
            }
            return islogged;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = CheckLogged();
            return authorize;
        }
    }
}
