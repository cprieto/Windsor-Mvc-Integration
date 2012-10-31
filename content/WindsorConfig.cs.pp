using System;
using System.Linq;
using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.Windsor.Integration.Mvc;

[assembly: WebActivator.PostApplicationStartMethod(
    typeof($rootnamespace$.App_Start.WindsorConfig), "ConfigureContainer")]

namespace $rootnamespace$.App_Start
{
    public class WindsorConfig
    {
        public static void ConfigureContainer()
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.This());

            DependencyResolver.SetResolver(new WindsorDependencyResolver(container));
        }
    }
}

