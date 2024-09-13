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

        public void OnApply(Card card)
        {
            if (card is Creature creature)
            {
                creature.ChangedAttack(_buffData.attack);
                creature.ChangedMaxHealth(_buffData.maxHealth);
            }
        }

        public void OnRemove(Card card)
        {
            if (card is Creature creature)
            {
                creature.ChangedAttack(-_buffData.attack);
                creature.ChangedMaxHealth(-_buffData.maxHealth);
            }
        }
    }
}
