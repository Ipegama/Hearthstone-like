using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(menuName = "Buffs/Change Cost Buff Data")]
    public class ChangeCostBuffData : BuffData
    {
        public int amount;

        public override Buff Create()
        {
            return new ChangeCostBuff(this);
        }

        public override string GetDescription()
        {
            return "";
        }
    }
}
