using AppLogic.Garbage;
using AppLogic.Plant;
using AppLogic.Truck;
using System.Linq;

namespace AppLogic.CarbagePoligon
{
    public class CarbagePolygon : ICarbagePolygon
    {
        private readonly ITruck _truck;
        private readonly IPlant _plant;

        public CarbagePolygon(ITruck truck, IPlant plant)
        {
            _truck = truck;
            _plant = plant;
        }

        public Exhaust DisposeGarbage(int weight)
        {
            //A call to
            var currentCapacity = _truck.GetCapacity();

            if (weight > currentCapacity)
                return new Exhaust { IsSuccess = false };

            //Must have happened
            _truck.LoadGarbage(weight);

            var plant = _truck.GetAvaliablePlant();

            //Test Case
            if(plant != PlantType.Rublevka)
            {
                return new Exhaust { IsSuccess = false };
            }

            //Invoke
            _truck.DriveToPlant(new DriveInfo
            {
                Destination = plant,
                Weight = weight
            });

            //Throws
            _truck.UnloadTruck();

            var sortedGarbage = _plant.SortGarbage();

            //Same sequence
            var burnResult = _plant.BurnGarbage(sortedGarbage);

            //Assert collection
            return new Exhaust
            {
                IsSuccess = true,
                Components = burnResult.ToArray()
            };
        }
    }
}
