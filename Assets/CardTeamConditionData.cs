using UnityEngine;

namespace TriggerSystem.Data.Conditions
{
    [CreateAssetMenu]
    public class CardTeamConditionData : ConditionData
    {
        public TargetCardType cardType;
        public TargetFilter targetFilter;

        public override bool Check(ActionContext context)
        {
            var card1 = context.Get(TargetCardType.ThisCard);
            var card2 = context.Get(cardType);

            return targetFilter.Match(card1, card2);
        }
    }
}
