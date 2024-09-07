using Gameplay;
using System;
using UnityEngine;

namespace TriggerSystem.Data.Conditions
{
    [CreateAssetMenu]
    public class CardConditionData : ConditionData
    {
        public TargetCardType cardType1;
        public TargetCardType cardType2;

        public override bool Check(ActionContext context)
        {
            var card1 = context.Get(cardType1);
            var card2 = context.Get(cardType2);

            return Evaluate(card1,card2);
        }

        private bool Evaluate(Card card1, Card card2)
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
