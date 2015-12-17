using Microsoft.Practices.Unity;
using System;
using System.Web;

namespace Tatabouf.Unity
{
    public class HttpContextLifetimeManager : LifetimeManager, IDisposable
    {
        private readonly string key;

        public HttpContextLifetimeManager(string key)
        {
            this.key = key;
        }

        public override object GetValue()
        {
            return HttpContext.Current.Items[key];
        }

        public override void RemoveValue()
        {
            Dispose();
            HttpContext.Current.Items.Remove(key);
        }

        public override void SetValue(object newValue)
        {
            if (HttpContext.Current.Items.Contains(key))
                throw new ArgumentException(key + " already exists");

            HttpContext.Current.Items[key] = newValue;
        }

        public void Dispose()
        {
            // Dispose contained object, if there is a current HttpContext 
            // (not the case at Application_End)
            if (HttpContext.Current != null)
            {
                var obj = GetValue() as IDisposable;
                if (obj != null)
                {
                    obj.Dispose();
                }
            }
        }
    }
}