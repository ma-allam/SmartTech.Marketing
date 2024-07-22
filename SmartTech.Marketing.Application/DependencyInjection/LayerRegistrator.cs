using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SmartTech.Marketing.Application.DependencyInjection
{
    public static class LayerRegistrator
    {

        public static void AddHandlers<T>(this IServiceCollection services)
        {
            var x = typeof(T).Assembly.GetTypes()
                .Where(x => x.IsClass).ToList();
            var y = typeof(T).Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Any(y => IsServiceInterface<T>(y))).ToList();
            List<Type> handlerTypes = typeof(T).Assembly.GetTypes()
                .Where(x => x.IsClass)
                .Where(x => x.GetInterfaces().Any(y => IsServiceInterface<T>(y)))
                .ToList();


            foreach (Type type in handlerTypes)
            {
                AddHandler<T>(services, type);
            }
        }

        private static void AddHandler<T>(IServiceCollection services, Type type)
        {
            object[] attributes = type.GetCustomAttributes(false);

            List<Type> pipeline = new List<Type>() { type };

            Type interfaceType = type.GetInterfaces().Single(y => IsServiceNotCoreInterface<T>(y));

            Func<IServiceProvider, object> factory = BuildPipeline(pipeline, interfaceType);

            services.AddScoped(interfaceType, factory);
        }

        private static Func<IServiceProvider, object> BuildPipeline(List<Type> pipeline, Type interfaceType)
        {
            List<ConstructorInfo> ctors = pipeline
                .Select(x =>
                {
                    Type type = x.IsGenericType ? x.MakeGenericType(interfaceType.GenericTypeArguments) : x;
                    return type.GetConstructors().Single();
                })
                .ToList();

            Func<IServiceProvider, object> func = provider =>
            {
                object current = null;

                foreach (ConstructorInfo ctor in ctors)
                {
                    List<ParameterInfo> parameterInfos = ctor.GetParameters().ToList();

                    object[] parameters = GetParameters(parameterInfos, current, provider);

                    current = ctor.Invoke(parameters);
                }

                return current;
            };

            return func;
        }

        private static object[] GetParameters(List<ParameterInfo> parameterInfos, object current, IServiceProvider provider)
        {
            var result = new object[parameterInfos.Count];

            for (int i = 0; i < parameterInfos.Count; i++)
            {
                result[i] = GetParameter(parameterInfos[i], current, provider);
            }

            return result;
        }

        private static object GetParameter(ParameterInfo parameterInfo, object current, IServiceProvider provider)
        {
            Type parameterType = parameterInfo.ParameterType;

            object service = provider.GetService(parameterType);
            if (service != null)
                return service;

            throw new ArgumentException($"Type {parameterType} not found");
        }


        private static bool IsServiceInterface<T>(Type type)
        {
            //if (type.IsGenericType)
            //    return false;
            return typeof(T).IsAssignableFrom(type);
        }



        private static bool IsServiceNotCoreInterface<T>(Type type)
        {
            //if (type.IsGenericType)
            //    return false;
            var val = typeof(T).IsAssignableFrom(type);
            return val && type != typeof(T);
        }


    }
}
