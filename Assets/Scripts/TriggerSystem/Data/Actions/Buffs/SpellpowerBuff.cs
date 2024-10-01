using Gameplay;
using TriggerSystem.Data;
namespace TriggerSystem
{
    public class SpellpowerBuff : Buff
    {
        private readonly SpellpowerBuffData _buffData;

        public SpellpowerBuff(SpellpowerBuffData buffData)
        {
            _buffData = buffData;
        }

        public override void OnApply(Card card)
        {
        }

        public override void OnRemove(Card card)
        {
        }

        public int GetSpellpower()
        {
            return _buffData.spellpowerAmount;
        }
    }
}
