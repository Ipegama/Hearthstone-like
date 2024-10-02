using Gameplay;
using Gameplay.Data;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "AddCard", menuName = "Actions/AddCard")]
    public class AddCardActionData : ActionData
    {
        public CardData cardData;
        public override void Execute(ActionContext context)
        {
            context.Get(TargetPlayerType.Owner).AddCardToHand(cardData);
        }

        public override string GetDescription()
        {
            return "";
        }
    }
}