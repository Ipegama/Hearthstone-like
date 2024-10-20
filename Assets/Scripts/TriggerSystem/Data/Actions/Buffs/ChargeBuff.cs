using Gameplay;
using TriggerSystem.Data;

namespace TriggerSystem
{
    public class ChargeBuff : Buff
    {
        public ChargeBuff(ChargeBuffData buffData) { }

        public override void OnApply(IBuffable entity)
        {
            /*if(entity is Creature creature)
            {
                if (!creature.HasAttackedThisTurn)
                {
                    creature.SetCanAttack(true);
                }
            }*/
        }

        public override void OnRemove(IBuffable entity)
        {
            
        }
    }
}
