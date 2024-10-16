using UnityEngine;

namespace TriggerSystem.Data.Conditions
{
    [CreateAssetMenu(menuName = "Card Condition/Card Team")]
    public class CardTeamConditionData : ConditionData
    {
        public TargetType cardType;
        public TargetFilter targetFilter;

        public override bool Check(ActionContext context)
        {
            var card1 = context.Get(TargetType.This);
            var card2 = context.Get(cardType);

            return targetFilter.Match(card1, card2);
        }
    }
}
