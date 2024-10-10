using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(menuName = "Buffs/Charge Buff Data")]
    public class ChargeBuffData : BuffData
    {
        public override Buff Create()
        {
            return new ChargeBuff(this);
        }

        public override string GetDescription()
        {
            return "Charge.";
        }
    }
}

