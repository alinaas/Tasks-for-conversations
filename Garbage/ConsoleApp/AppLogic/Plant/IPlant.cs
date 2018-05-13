using AppLogic.Garbage;
using System.Collections.Generic;

namespace AppLogic.Plant
{
    public interface IPlant
    {
        Garbage[] SortGarbage();

        IEnumerable<ExhaustComponent> BurnGarbage(Garbage[] garbage);
    }

    public class Garbage
    {
        public string Type { get; set; }

        public int Weight { get; set; }
    }
}
