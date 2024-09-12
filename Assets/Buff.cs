using Gameplay;
using TriggerSystem.Data;

namespace TriggerSystem
{
    public class Buff
    {
        private BuffData _buffData;
        public Buff(BuffData buffData)
        {
            _buffData = buffData;
        }

        public void OnApply(Creature creature)
        {
            creature.ChangedAttack(_buffData.attack);
            creature.ChangedMaxHealth(_buffData.maxHealth);
        }

        public void OnRemove(Creature creature)
        {
            creature.ChangedAttack(-_buffData.attack);
        }
    }
}
