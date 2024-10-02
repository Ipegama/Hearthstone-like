using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(menuName = "Buffs/Add Trigger Buff Data")]
    public class AddTriggerBuffData : BuffData
    {
        public GameTriggerData trigger;
        public override Buff Create()
        {
            return new AddTriggerBuff(this);
        }

        public override string GetDescription()
        {
            return "";
        }
    }
}
