using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Infrastructure
{
    public static class DomainEvents
    {
        [ThreadStatic]
        private static List<Delegate> actions;

        public static IServiceCollection _serviceCollection = new ServiceCollection();

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
                actions = new List<Delegate>();

            actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            actions = null;
        }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            foreach (var handler in _serviceCollection.RegisterAllTypes<IHandles<T>>())
                handler.Handle(args);

            if (actions != null)
                foreach (var action in actions)
                    if (action is Action<T>)
                        ((Action<T>)action)(args);
        }

        private static IEnumerable<T> RegisterAllTypes<T>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies();
            var typesFromAssemblies = assembly.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));

            return services.BuildServiceProvider().GetServices<T>();
        }
    }
}
