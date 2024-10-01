using System;
using UnityEngine;
using Utils.ExpressionEval;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "Heal", menuName = "Actions/Heal")]
    public class HealActionData : ActionData
    {
        public ProjectileActionData projectileAction;
        //public string amount;
        public int amount;
        public TargetType type;
        public TargetFilter filter;

        public override void Execute(ActionContext context)
        {
            var targets = context.GetAll(type, filter);
            //var intAmount = GetAmount(context);

            foreach(var target in targets)
            {
                if(target != null)
                {
                    if (projectileAction)
                    {
                        Events.Actions.Projectile?.Invoke(projectileAction, context.thisCard, context.thisCard.transform, target.GetTransform());
                    }
                    //target.Heal(intAmount, true);
                    target.Heal(amount, true);
                }
            }
        }

        //private int GetAmount(ActionContext context)=> ExpressionEval.Eval(amount, context.GetAllIntKeys());

        public override string GetDescription()
        {
            return $"Heals {amount} health to {type}";
        }
    }
}
