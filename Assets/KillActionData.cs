using UnityEngine;

namespace TriggerSystem.Data.Actions
{
    [CreateAssetMenu(fileName = "Kill", menuName ="Actions/Kill")]
    public class KillActionData : ActionData
    {
        public TargetType targetType;

        public override void Execute(ActionContext context)
        {
            var target = context.Get(targetType);
            target.Kill();
        }

        public override string GetDescription()
        {
            return "";
        }
    }
}
