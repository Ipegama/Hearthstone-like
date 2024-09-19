using UnityEngine;

namespace TriggerSystem.Data.Actions
{
    [CreateAssetMenu(fileName = "Kill", menuName = "Actions/Kill")]
    public class KillActionData : ActionData
    {
        public TargetType targetType;
        public TargetFilter filter;
        public bool self;

        public override void Execute(ActionContext context)
        {
            var targets = context.GetAll(targetType, filter);

            foreach (var target in targets)
            {
                target.Kill();
            }
            if (self)
            {
                context.thisCard.Kill();
            }
        }

        public override string GetDescription()
        {
            return "";
        }
    }
}