using Gameplay;
using Gameplay.Interfaces;
using System;
using UnityEngine;

namespace TriggerSystem.Data.Conditions
{
    [CreateAssetMenu]
    public class CardConditionData : ConditionData
    {
        public TargetType cardType1;
        public TargetType cardType2;

        public override bool Check(ActionContext context)
        {
            var card1 = context.Get(cardType1);
            var card2 = context.Get(cardType2);

            return Evaluate(card1,card2);
        }

        private bool Evaluate(ITargetable card1, ITargetable card2)
        {
            switch (op) 
            {
                case ConditionOperator.Equals:
                    return card1 == card2;
                    case ConditionOperator.NotEquals:
                    return card1 != card2;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
