using System;
using System.Collections.Generic;
using System.Linq;

namespace IoC_Demystified_Demo
{
    public interface IContainer
    {
        void Register<TServiceKey, TServiceValue>()
            where TServiceValue : class, TServiceKey
            where TServiceKey : class;
        TService Resolve<TService>() where TService : class;
        object Resolve(Type type);
    }

    public class Container : IContainer
    {
        private readonly Dictionary<Type, Type> _registry;

        public Container()
        {
            _registry = new Dictionary<Type, Type>();
        }

        public void Register<TServiceKey, TServiceValue>()
            where TServiceValue : class, TServiceKey
            where TServiceKey : class
        {
            _registry.Add(typeof(TServiceKey), typeof(TServiceValue));
        }

        public TService Resolve<TService>() where TService : class
        {
            var type = typeof(TService);
            return Resolve(type) as TService;
        }

        public object Resolve(Type type)
        {
            var resolvedType =
                _registry.TryGetValue(type, out var registeredType)
                    ? registeredType
                    : type;

            var resolvedConstructorArgs = resolvedType
                .GetConstructors()
                .Select(ctorInfo =>
                    KeyValuePair.Create(
                        ctorInfo,
                        ctorInfo
                            .GetParameters()
                            .Select(parameter => Resolve(parameter.ParameterType))
                            .ToArray()))
                .Where(parameters => parameters.Value.Any())
                .ToList();

            if (resolvedConstructorArgs.Any())
            {
                var (constructor, constructorArgs) = resolvedConstructorArgs.FirstOrDefault();
                return constructor.Invoke(constructorArgs);
            }

            return resolvedType.GetConstructor(Array.Empty<Type>()).Invoke(Array.Empty<object>());
        }
    }
}
