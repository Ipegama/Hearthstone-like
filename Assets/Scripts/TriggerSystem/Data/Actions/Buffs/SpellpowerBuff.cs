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

        public override void OnApply(IBuffable entity)
        {
        }

        public override void OnRemove(IBuffable entity)
        {
        }

        public int GetSpellpower()
        {
            return _buffData.spellpowerAmount;
        }
    }
}
