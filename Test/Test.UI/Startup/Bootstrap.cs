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
            builder.RegisterType<TestDetailViewModel>()
                .Keyed<IDetailViewModel>(nameof(TestDetailViewModel));
            builder.RegisterType<MeetingDetailViewModel>()
                .Keyed<IDetailViewModel>(nameof(MeetingDetailViewModel));
            builder.RegisterType<ProgrammingLanguageDetailViewModel>()
            .Keyed<IDetailViewModel>(nameof(ProgrammingLanguageDetailViewModel));


            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<TestRepository>().As<ITestRepository>();


            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<MeetingRepository>().As<IMeetingRepository>();

            builder.RegisterType<ProgrammingLanguageRepository>().As<IProgrammingLanguageRepository>();

            return builder.Build();

        }
    }
}
