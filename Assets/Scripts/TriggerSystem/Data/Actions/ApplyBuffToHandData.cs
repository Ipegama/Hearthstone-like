using Gameplay;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "ApplyBuffToHand", menuName = "Actions/ApplyBuffToHand")]
    public class ApplyBuffToHandData : ActionData
    {
        public BuffData buffData;
        public bool includeSpells;
        public bool includeCreatures;

        public override void Execute(ActionContext context)
        {
            Debug.Log("Hi");
            var source = context.Get(TargetType.This);
            var targets = context.Get(TargetPlayerType.Owner).GetPlayer().hand.Cards;

            foreach (var target in targets)
            {
                if (includeCreatures)
                {
                    if (target is Creature)
                    {
                        var buff = buffData.Create();
                        target.AddBuff(buff);
                    }
                }
                if (includeSpells)
                {
                    if (target is Spell)
                    {
                        var buff = buffData.Create();
                        target.AddBuff(buff);
                    }
                }
            }
        }

        public override string GetDescription()
        {
            return "";
        }
    }
}
