using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(menuName = "Buffs/Taunt Buff Data")]
    public class TauntBuffData : BuffData
    {
        public override Buff Create()
        {
            return new TauntBuff(this);
        }

        public override string GetDescription()
        {
            return "Taunt.";
        }
    }
}

