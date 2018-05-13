namespace AppLogic.Truck
{
    public interface ITruck
    {
        int GetCapacity();

        void LoadGarbage(int weight);

        PlantType GetAvaliablePlant();

        void DriveToPlant(DriveInfo info);

        void UnloadTruck();
    }

    public enum PlantType
    {
        Solnechnogosk,
        Reutov,
        Rublevka
    }

    public class DriveInfo
    {
        public PlantType Destination { get; set; }

        public int Weight { get; set; }
    }
}
