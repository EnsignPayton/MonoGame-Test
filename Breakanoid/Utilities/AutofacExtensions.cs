using System;
using Autofac;

namespace Breakanoid.Utilities
{
    public static class AutofacExtensions
    {
        public static T Resolve<T>(this IContainer container, Action<T> initAction)
        {
            var result = container.Resolve<T>();

            initAction?.Invoke(result);

            return result;
        }
    }
}
