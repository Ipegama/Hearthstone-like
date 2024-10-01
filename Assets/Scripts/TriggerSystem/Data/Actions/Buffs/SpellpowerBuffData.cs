using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(menuName = "Buffs/Spellpower Buff Data")]
    public class SpellpowerBuffData : BuffData
    {
        public int spellpowerAmount;

        public override Buff Create()
        {
            return new SpellpowerBuff(this);
        }

        public override string GetDescription()
        {
            return $"Spellpower +{spellpowerAmount}.";
        }
    }
}

