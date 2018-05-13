using AppLogic.Garbage;
using System.Collections.Generic;

namespace AppLogic.Plant
{
    public class Plant : IPlant
    {
        public Garbage[] SortGarbage()
        {
            return new[]
            {
                new Garbage{ Type = "Glass", Weight = 1 },
                new Garbage{ Type = "Paper", Weight = 2 },
                new Garbage{ Type = "Metal", Weight = 1 },
            };
        }

        public IEnumerable<ExhaustComponent> BurnGarbage(Garbage[] garbage)
        {
            yield return new ExhaustComponent { Formula = "CO2", Volume = 4 };
            yield return new ExhaustComponent { Formula = "Fe2O3", Volume = 1 };
            yield return new ExhaustComponent { Formula = "Si2O4", Volume = 2 };
        }
    }
}
