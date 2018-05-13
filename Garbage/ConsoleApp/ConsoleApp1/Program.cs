using Autofac;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var scope = AutofacConfig.Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();

                app.Run();
            }
        }
    }
}
