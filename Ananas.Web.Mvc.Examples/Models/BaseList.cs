using System.Collections.Generic;
using System.Globalization;
using Ananas.Web.Mvc.Io.Implementation;
using Ananas.Web.Mvc.Models;
using Microsoft.AspNetCore.Http;

namespace Ananas.Web.Mvc.Examples.Models
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
            //model.CurrentLang = GetCurrentLang(context);
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
            {   //Get Cookies的lang
                
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
