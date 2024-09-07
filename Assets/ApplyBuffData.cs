using UnityEngine;

namespace TriggerSystem.Data.Actions
{
    [CreateAssetMenu(fileName ="ApplyBuff",menuName = "Actions/Buff")]
    public class ApplyBuffData : ActionData
    {
        public BuffData buffData;
        public TargetType targetCardType;

        public override void Execute(ActionContext context)
        {
            var target = context.Get(targetCardType);
            if (target != null) 
            {
                var buff = buffData.Create();
                target.AddBuff(buff);
            }
        }

        public override string GetDescription()
        {
            return $"Apply a buff thats gives {buffData.GetDescription()} to {targetCardType}";
        }
    }
}
