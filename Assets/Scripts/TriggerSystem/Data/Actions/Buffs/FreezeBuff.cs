using Gameplay;
using TriggerSystem.Data;
namespace TriggerSystem
{
    public class FreezeBuff : Buff
    {
        public FreezeBuff(FreezeBuffData buffData)
        {

        }
        public override void OnApply(IBuffable entity)
        {
            if (entity is Creature creature)
            {
                
            }
        }

        public override void OnRemove(IBuffable entity)
        {
            if (entity is Creature creature)
            {
                
            }
        }
    }
}