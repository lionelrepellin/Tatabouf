using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tatabouf.Unity
{
    public class HttpContextDisposableLifetimeManager : LifetimeManager, IDisposable
    {
        private readonly string _key;

        public HttpContextDisposableLifetimeManager(string key)
        {
            _key = key;
        }

        public override object GetValue()
        {
            return HttpContext.Current.Items[_key];
        }

        public override void RemoveValue()
        {
            Dispose();
            HttpContext.Current.Items.Remove(_key);
        }

        public override void SetValue(object newValue)
        {
            if (HttpContext.Current.Items.Contains(_key))
            {
                throw new ArgumentException(_key + " already exists");
            }
            HttpContext.Current.Items[_key] = newValue;
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