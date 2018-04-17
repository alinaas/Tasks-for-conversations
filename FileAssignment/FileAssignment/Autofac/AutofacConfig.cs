using Autofac;
using Autofac.Integration.Mvc;
using FileAssignment.Repository;

namespace FileAssignment.Autofac
{
	public class AutofacConfig
	{
		public IContainer BuildContainer()
		{
			var builder = new ContainerBuilder();
			
			builder
				.RegisterType<DataRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterControllers(typeof(MvcApplication).Assembly);				

			return builder.Build();
		}
	}
}
