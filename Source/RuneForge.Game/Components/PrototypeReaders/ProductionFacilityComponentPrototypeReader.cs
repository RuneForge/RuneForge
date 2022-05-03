using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class ProductionFacilityComponentPrototypeReader : ComponentPrototypeReader<ProductionFacilityComponentPrototype>
    {
        public override ProductionFacilityComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            int unitCodesCount = contentReader.ReadInt32();
            List<string> unitCodes = new List<string>(unitCodesCount);
            for (int i = 0; i < unitCodesCount; i++)
            {
                string unitCode = contentReader.ReadString();
                unitCodes.Add(unitCode);
            }
            return new ProductionFacilityComponentPrototype(unitCodes);
        }
    }
}
