using EagleRockService.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace EagleRockService.Infra
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
            .RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();

            var services = new ServiceCollection();
            builder.Populate(services);

            builder.RegisterAssemblyTypes(typeof(MediatorModule).Assembly)
                .Where(t => t.Namespace != null && t.Namespace.Contains("Features"))
                .Where(t => t.IsClosedTypeOf(typeof(IRequest<>)) || t.IsClosedTypeOf(typeof(IRequestHandler<,>)) ||
                            typeof(INotification).IsAssignableFrom(t) || t.IsClosedTypeOf(typeof(INotificationHandler<>)))
                .AsImplementedInterfaces();
            builder.RegisterType<TimeStampProvider>().As<ITimeStampProvider>().InstancePerLifetimeScope();
        }
    }
}
