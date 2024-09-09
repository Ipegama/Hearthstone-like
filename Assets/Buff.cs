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
            card.ChangeAttack(_buffData.attack);
            card.AddMaxHealth(_buffData.maxHealth);
        }

        public void OnRemove(Card card)
        {
            card.ChangeAttac(-_buffData.attack);
        }
    }
}
