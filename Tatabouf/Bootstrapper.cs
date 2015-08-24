using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Tatabouf.DAL;

namespace Tatabouf
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            RegisterType<TataboufRepository>(container);
            RegisterType<TataboufContext>(container);
            
            return container;
        }

        private static void RegisterType<T>(IUnityContainer container)
        {
            container.RegisterType<T>(new HttpContextDisposableLifetimeManager(typeof(T).FullName));
        }

        private static void RegisterType<I, T>(IUnityContainer container) where T : I
        {
            container.RegisterType<I, T>(new HttpContextDisposableLifetimeManager(typeof(I).FullName));
        }

        private static void RegisterType<I, T>(IUnityContainer container, string name) where T : I
        {
            container.RegisterType<I, T>(name, new HttpContextDisposableLifetimeManager(name));
        }
    }
}