// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;

namespace Microsoft.Framework.DependencyInjection.Ninject
{
    public static class NinjectRegistration
    {
        public static void Populate(this IKernel kernel, IServiceCollection descriptors)
        {
            kernel.Load(new ServiceProviderNinjectModule(descriptors));
        }

        public static IBindingNamedWithOrOnSyntax<T> InRequestScope<T>(
                this IBindingWhenInNamedWithOrOnSyntax<T> binding)
        {
            return binding.InScope(context => context.Parameters.GetScopeParameter(context.Kernel.Get<IHttpContextAccessor>()));
        }

        internal static ScopeParameter GetScopeParameter(this IEnumerable<IParameter> parameters, IHttpContextAccessor httpContextAccessor)
        {
            var scopeParameter = (ScopeParameter)(parameters
                .SingleOrDefault(p => p.Name == typeof(ScopeParameter).FullName));

            if (scopeParameter != null)
            {
                return scopeParameter;
            }

            HttpContext context = httpContextAccessor.HttpContext;

            if (context != null)
            {
                return (ScopeParameter)context.Items[typeof(ScopeParameter).FullName];
            }

            return null;
        }

        internal static IEnumerable<IParameter> AddOrReplaceScopeParameter(
                this IEnumerable<IParameter> parameters,
                ScopeParameter scopeParameter)
        {
            return parameters
                .Where(p => p.Name != typeof(ScopeParameter).FullName)
                .Concat(new[] { scopeParameter });
        }
    }
}
