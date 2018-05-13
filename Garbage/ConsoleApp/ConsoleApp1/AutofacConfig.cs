using AppLogic.CarbagePoligon;
using AppLogic.Plant;
using AppLogic.Truck;
using Autofac;

namespace ConsoleApp1
{
    public static class AutofacConfig
    {
        public static IContainer Container
        {
            get
            {
                var builder = new ContainerBuilder();

                builder.RegisterType<Plant>().As<IPlant>().InstancePerLifetimeScope();
                builder.RegisterType<Truck>().As<ITruck>().SingleInstance();

                builder.RegisterType<CarbagePolygon>().As<ICarbagePolygon>().SingleInstance();

                builder.RegisterType<Application>().As<IApplication>().SingleInstance();

                return builder.Build();
            }
        }
    }
}
