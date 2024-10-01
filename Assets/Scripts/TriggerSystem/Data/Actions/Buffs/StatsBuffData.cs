using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(menuName = "Buffs/Stats Buff Data")]
    public class StatsBuffData : BuffData
    {
        public int attack;
        public int maxHealth;

        public override Buff Create()
        {
            return new StatsBuff(this);
        }

        public override string GetDescription()
        {
            return $"Increase Attack by {attack}, Max Health by {maxHealth}.";
        }
    }
}
