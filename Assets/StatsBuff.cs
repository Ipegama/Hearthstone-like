using Gameplay;
using TriggerSystem.Data;
using UnityEngine;

namespace TriggerSystem
{
    [CreateAssetMenu(fileName ="Stats Buff",menuName ="Buffs/Stats Buff")]
    public class StatsBuff : Buff
    {
        private readonly StatsBuffData _buffData;

        public StatsBuff(StatsBuffData buffData)
        {
            _buffData = buffData;
        }

        public override void OnApply(Card card)
        {
            if (card is Creature creature)
            {
                creature.ChangedAttack(_buffData.attack);
                creature.ChangedMaxHealth(_buffData.maxHealth);
            }
        }

        public override void OnRemove(Card card)
        {
            if (card is Creature creature)
            {
                creature.ChangedAttack(-_buffData.attack);
                creature.ChangedMaxHealth(-_buffData.maxHealth);
            }
        }
    }
}
