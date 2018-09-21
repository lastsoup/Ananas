using System.Collections.Generic;
using System.Globalization;
using Ananas.Web.Mvc.Io.Implementation;
using Microsoft.AspNetCore.Http;

namespace Ananas.Web.Mvc.Models
{
    public class BaseList
    {
        public ViewModel GetCurrentModel(HttpContext context,UserInfo user)
        {
            return CommonModel(context,user);
        }


        public ViewModel CommonModel(HttpContext context,UserInfo user)
        {
            ViewModel model = new ViewModel();
            if (user != null)
            {
                model.CurrentUser = user;
            }
            else
            {
                model.CurrentUser = new UserInfo();
            }
            model.CurrentLang = GetCurrentLang(context);
            //获取登录名
            HttpCookie cookie = context.Cookies["COOKIE_DEFAULT_NAME"];
            model.LoginName = cookie == null ? "" : cookie["sName"];
            return model;
        }

        //获取当前的语言
        public Dictionary<string, string> GetCurrentLang(HttpContext context)
        {
            string querylang = null;
            querylang = context.Request.Query["l"];
            CultureInfo culture = CultureInfo.InstalledUICulture;
            string lang = CultureInfo.InstalledUICulture.ToString();
            if (querylang == null)
            {   //Get
                IRequestCookieCollection cookie = context.Request.Cookies["COOKIE_LANG_FOR_USER"];
                if (cookie != null)
                {
                    lang = cookie["Lang"];
                    culture = new CultureInfo(lang, true);
                }
            }
            else
            {
                //Set              
                lang = querylang;
                culture = new CultureInfo(lang, true);
            }
            LocalizationServiceFactory lsf = new LocalizationServiceFactory();
            Dictionary<string, string> langdictionary = lsf.CreateDictionary("PageResource", culture);
            return langdictionary;
        }

    }
}
