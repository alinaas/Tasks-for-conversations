using AppLogic.CarbagePoligon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IApplication
    {
        void Run();
    }

    public class Application : IApplication
    {
        private readonly ICarbagePolygon _service;

        public Application(ICarbagePolygon service)
        {
            _service = service;
        }

        public void Run()
        {
            int counter = 0;
            while (counter++ < 100)
            {
                Console.WriteLine("Enter weigth");

                var weight = int.Parse(Console.ReadLine());

                var result = _service.DisposeGarbage(weight);

                Console.WriteLine(JsonConvert.SerializeObject(result));
            }

            Console.ReadLine();
        }
    }
}
