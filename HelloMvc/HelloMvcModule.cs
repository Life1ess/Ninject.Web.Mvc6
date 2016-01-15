using System;
using Microsoft.AspNet.Antiforgery;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.OptionsModel;
using Ninject.Modules;

namespace HelloMvc
{
    public class HelloMvcModule : NinjectModule
    {
        public override void Load()
        {
            KernelInstance.Unbind<IServiceProvider>();
            KernelInstance.Bind<IOptions<MvcOptions>>().To<OptionsManager<MvcOptions>>();
            KernelInstance.Bind<IOptions<AntiforgeryOptions>>().To<OptionsManager<AntiforgeryOptions>>();
        }
    }
}
