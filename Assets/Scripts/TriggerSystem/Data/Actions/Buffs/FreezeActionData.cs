using Gameplay;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "Freeze", menuName = "Actions/Freeze")]
    public class FreezeActionData : ActionData
    {
        public TargetType targetType;
        public TargetFilter filter;

        public override void Execute(ActionContext context)
        {
            var targets = context.GetAll(targetType, filter);

            foreach (var target in targets)
            {
                if (target is Creature creature)
                {
                    creature.Freeze();
                }
                if (target is Player player)
                {
                    player.Freeze();
                }
            }
        }

        public override string GetDescription()
        {
            return $"Freeze target.";
        }
    }
}
