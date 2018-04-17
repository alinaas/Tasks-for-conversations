using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.Wcf;
using WcfAssignment.MatchBasicDataService;
using WcfAssignment.Repository.MatchBasicDataRepository;
using System.Configuration;

namespace WcfAssignment.Autofac
{
	public class AutofacConfig
	{
		public IContainer BuildContainer()
		{
			var builder = new ContainerBuilder();
			
			builder
			  .Register(c =>
			  {
				  var client = new MatchBasicDataServiceClient();
				  client.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationManager.AppSettings["MatchBasicDataServiceClient.Login"];
				  client.ClientCredentials.Windows.ClientCredential.Password = ConfigurationManager.AppSettings["MatchBasicDataServiceClient.Password"];

				  return client;
			  })
			  .As<IMatchBasicDataService>()
			  .UseWcfSafeRelease()
			  .InstancePerLifetimeScope();

			builder
				.RegisterType<MatchBasicDataRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			return builder.Build();
		}
	}
}
