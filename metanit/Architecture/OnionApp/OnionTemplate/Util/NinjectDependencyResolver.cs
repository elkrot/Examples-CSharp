using Ninject;
using Ninject.Modules;
using OnionApp.Domain.Interfaces;
using OnionApp.Infrastructure.Business;
using OnionApp.Infrastructure.Data;
using OnionApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnionApp.Web.Util
{


    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IBookRepository>().To<BookRepository>();
            kernel.Bind<IOrder>().To<CacheOrder>();
        }
    }
}