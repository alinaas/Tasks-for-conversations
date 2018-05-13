using System;

namespace AppLogic.Truck
{
    public class Truck : ITruck
    {
        private readonly Random _random;
        private int _loadedWeight;

        public Truck()
        {
            _random = new Random((int)DateTime.Now.Ticks);
        }

        public int GetCapacity()
        {
            return _random.Next() % 15;
        }

        public void LoadGarbage(int weight)
        {
            _loadedWeight = weight;
        }

        public PlantType GetAvaliablePlant()
        {
            return PlantType.Rublevka;
        }

        public void UnloadTruck()
        {
            if (_random.Next() % 4 == 1)
                throw new Exception("Plant has been exploded by garbage!!!");
        }

        public void DriveToPlant(DriveInfo info)
        {

        }
    }
}
