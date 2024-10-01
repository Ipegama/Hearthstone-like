using Gameplay;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName ="ApplyBuff",menuName = "Actions/ApplyBuff")]
    public class ApplyBuffData : ActionData
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
                    creature.AddBuff(buff);
                }
            }
        }

        public override string GetDescription()
        {
            return $"Apply a buff thats gives {buffData.GetDescription()} to {targetType}";
        }
    }
}
