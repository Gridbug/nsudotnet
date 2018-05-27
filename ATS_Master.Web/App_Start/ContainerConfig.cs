using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATS_Master.Data;
using Autofac;
using Autofac.Integration.Mvc;

namespace ATS_Master.Web.App_Start
{
    public static class ContainerConfig
    {
        public static void ConfigureContainer(ContainerBuilder cb)
        {
            cb.RegisterControllers(typeof(ContainerConfig).Assembly);

            cb.RegisterType<AtsContext>()
                .AsSelf()
                .InstancePerRequest();
        }
    }
}