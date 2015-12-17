using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Tatabouf.Business;
using Tatabouf.DAL;
using Unity.Mvc4;

namespace Tatabouf.Unity
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
            RegisterType<IFoodChoiceRepository, FoodChoiceRepository>(container);
            RegisterType<TataboufContext>(container);
            RegisterType<ValidationService>(container);
            RegisterType<FoodChoiceService>(container);

            return container;
        }

        private static void RegisterType<T>(IUnityContainer container)
        {
            container.RegisterType<T>(new HttpContextLifetimeManager(typeof(T).FullName));
        }

        private static void RegisterType<I, T>(IUnityContainer container) where T : I
        {
            container.RegisterType<I, T>(new HttpContextLifetimeManager(typeof(T).FullName));
        }
    }
}