using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Http;
using TinyIoC;
using TryCatch.Core;
using TryCatch.Data;
using TryCatch.Interfaces;

namespace TryCatch.IoC
{
    public class IoCConfig
    {
        public static void Register()
        {
            // Container
            var container = Container();

            // MVC dependency resolver
            DependencyResolver.SetResolver(new TinyIoCDependencyResolver(container));
            // Web Api dependency resolver
            GlobalConfiguration.Configuration.DependencyResolver = new TinyIocWebApiDependencyResolver(container);
            //config.DependencyResolver = new TinyIocWebApiDependencyResolver(container);
        }
        

        public static TinyIoCContainer Container()
        {
            var container = TinyIoCContainer.Current;
            //container.Register<IRepository, Repository>();
            //container.Register<IRepository2, Repository2>();
            container.Register<IDbRepository, DatabaseDataProvider>();
            container.Register<IXmlRepository, XmlRepository>();

            container.Register<ICartComponent, CartComponent>();
            container.Register<IOrderComponent, OrderComponent>();
            container.Register<IArticleComponent, ArticleComponent>();
            container.Register<ICustomerComponent, CustomerComponent>();

            return container;
        }
    }
}
