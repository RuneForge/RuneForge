namespace RuneForge.Game.Components.Implementations
{
    public class UnitShelterComponentPrototype : ComponentPrototype
    {
        public int OccupantsLimit { get; }

        public UnitShelterComponentPrototype(int occupantsLimit)
        {
            OccupantsLimit = occupantsLimit;
        }
    }
}
