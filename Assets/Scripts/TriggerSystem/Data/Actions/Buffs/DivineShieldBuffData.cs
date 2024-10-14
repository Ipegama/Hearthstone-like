using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(menuName = "Buffs/Divine Shield Buff Data")]
    public class DivineShieldBuffData : BuffData
    {
        public override Buff Create()
        {
            return new DivineShieldBuff(this);
        }

        public override string GetDescription()
        {
            return "Taunt.";
        }
    }
}

