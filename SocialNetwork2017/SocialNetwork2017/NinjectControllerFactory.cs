using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Ninject;
using SocialNetwork2017.BL.Interfaces;
using SocialNetwork2017.BL.Implementations;
using SocialNetwork2017.Domain;

namespace SocialNetwork2017
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        //Извлекаем экземпляр контроллера для заданного контекста запроса и типа контроллера
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController) ninjectKernel.Get(controllerType);
        }

        //Опрделяем все привязки
        private void AddBindings()
        {
            ninjectKernel.Bind<IUsersRepository>().To<EfUsersRepository>();
            ninjectKernel.Bind<IFriendsRepository>().To<EFFriendsRepository>();
            ninjectKernel.Bind<IFriendRequestsRepository>().To<EFFriendRequestsRepository>();
            ninjectKernel.Bind<IMessagesRepository>().To<EFMessagesRepository>();
            ninjectKernel.Bind<SocialNetworkContext>().ToSelf().WithConstructorArgument("connectionString",
                                                                               ConfigurationManager.ConnectionStrings[0]
                                                                                   .ConnectionString);
            ninjectKernel.Inject(Membership.Provider);
        }
    }
}