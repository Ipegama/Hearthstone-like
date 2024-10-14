using Gameplay;
using TriggerSystem.Data;
namespace TriggerSystem
{
    public class DivineShieldBuff : Buff
    {
        public DivineShieldBuff(DivineShieldBuffData buffData)
        {

        }
        public override void OnApply(IBuffable entity)
        {
            if (entity is Creature creature)
            {
                creature.SetDivineShield();
            }
        }

        public override void OnRemove(IBuffable entity)
        {
            if (entity is Creature creature)
            {
                creature.SetDivineShield();
            }
        }
    }
}
