using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(menuName = "Buffs/Freeze Buff Data")]
    public class FreezeBuffData : BuffData
    {
        public override Buff Create()
        {
            return new FreezeBuff(this);
        }

        public override string GetDescription()
        {
            return "Frozen.";
        }
    }
}