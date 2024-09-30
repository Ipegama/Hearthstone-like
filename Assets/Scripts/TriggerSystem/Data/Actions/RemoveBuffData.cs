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
            var buff = buffData.Create();

            foreach (var target in targets)
            {
                if (target is Creature creature)
                {

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