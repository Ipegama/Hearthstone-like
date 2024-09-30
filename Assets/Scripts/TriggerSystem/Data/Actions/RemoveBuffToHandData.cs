using Gameplay;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "RemoveBuffToHand", menuName = "Actions/RemoveBuffToHand")]
    public class RemoveBuffToHandData : ActionData
    {
        public BuffData buffData;
        public bool includeSpells;
        public bool includeCreatures;

        public override void Execute(ActionContext context)
        {
            Debug.Log("Hi");
            var source = context.Get(TargetType.This);
            var targets = context.Get(TargetPlayerType.Owner).GetPlayer().hand.Cards;

            var buff = buffData.Create();
            foreach (var target in targets)
            {
                target.RemoveBuff(buff);
            }
        }

        public override string GetDescription()
        {
            return "";
        }
    }
}
