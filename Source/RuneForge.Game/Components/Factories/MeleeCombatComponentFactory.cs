using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class MeleeCombatComponentFactory : ComponentFactory<MeleeCombatComponent, MeleeCombatComponentPrototype>
    {
        public override MeleeCombatComponent CreateComponentFromPrototype(MeleeCombatComponentPrototype componentPrototype, MeleeCombatComponentPrototype componentPrototypeOverride)
        {
            return new MeleeCombatComponent(componentPrototype.AttackPower, componentPrototype.CycleTime, componentPrototype.ActionTime);
        }
    }
}
