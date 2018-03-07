using Autofac;
using Prism.Events;
using Test.DataAccess;
using Test.UI.Services;
using Test.UI.Services.Lookups;
using Test.UI.Services.Repositories;
using Test.UI.View.Services;
using Test.UI.ViewModel;

namespace Test.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestDbDataContext>().AsSelf();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<TestDetailViewModel>().As<ITestDetailViewModel>();
            

            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<TestRepository>().As<ITestRepository>();


            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();
            return builder.Build();

        }
    }
}
