using Gameplay;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName ="ApplyBuff",menuName = "Actions/Buff")]
    public class ApplyBuffData : ActionData
    {
        public BuffData buffData;
        public TargetType targetCardType;

        public override void Execute(ActionContext context)
        {
            var targetCard = context.Get(targetCardType);
            if(targetCard is Creature creature)
            {
                var buff = buffData.Create();
                creature.AddBuff(buff);
            }
        }

        public override string GetDescription()
        {
            return $"Apply a buff thats gives {buffData.GetDescription()} to {targetCardType}";
        }
    }
}
