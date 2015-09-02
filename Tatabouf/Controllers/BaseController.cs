using Microsoft.Practices.Unity;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tatabouf.DAL;

namespace Tatabouf.Controllers
{
    public abstract class BaseController : Controller
    {
        [Dependency]
        public TataboufRepository Repository { get; set; }

        protected static Logger logger = LogManager.GetCurrentClassLogger();

        protected static string GetIP(HttpRequestBase httpRequestBase)
        {
            string ip = httpRequestBase.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = httpRequestBase.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
    }
}