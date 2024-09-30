using Gameplay;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "RemoveBuff", menuName = "Actions/RemoveBuff")]
    public class RemoveBuffData : ActionData
    {
        public BuffData buffData;
        public TargetType targetType;
        public TargetFilter filter;

        public override void Execute(ActionContext context)
        {
            var source = context.Get(TargetType.This);
            var targets = context.GetAll(targetType, filter);
            foreach (var target in targets)
            {
                if (target is Creature creature)
                {
                    var buff = buffData.Create();
                    creature.RemoveBuff(buff);
                }
            }
        }

        public override string GetDescription()
        {
            return $"Remove buff";
        }
    }
}