using Microsoft.Practices.Unity;
using NLog;
using System.Web;
using System.Web.Mvc;
using Tatabouf.Business;

namespace Tatabouf.Controllers
{
    public abstract class BaseController : Controller
    {
        [Dependency]
        public FoodChoiceService FoodChoiceService { get; set; }
        
        [Dependency]
        public ValidationService ValidationService { get; set; }

        protected static Logger logger = LogManager.GetCurrentClassLogger();

        protected string GetIP(HttpRequestBase httpRequestBase)
        {
            //TODO for test purpose only
            if (httpRequestBase == null) return string.Empty;
            
            string ip = httpRequestBase.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = httpRequestBase.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
    }
}