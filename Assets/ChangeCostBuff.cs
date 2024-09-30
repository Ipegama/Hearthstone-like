using Gameplay;
using TriggerSystem.Data;
using UnityEngine;

namespace TriggerSystem
{
    [CreateAssetMenu(fileName = "Change Cost Buff", menuName = "Buffs/Change Cost Buff")]
    public class ChangeCostBuff : Buff
    {
        private readonly ChangeCostBuffData _buffData;

        public ChangeCostBuff(ChangeCostBuffData buffData)
        {
            _buffData = buffData;
        }

        public override void OnApply(Card card)
        {
            card.ChangeCost(_buffData.amount);
        }

        public override void OnRemove(Card card)
        {
            card.ChangeCost(-_buffData.amount);
        }
    }
}
