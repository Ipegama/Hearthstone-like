using Extensions;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "DamageByOne", menuName = "Actions/DamageByOne")]
    public class DamageByOneActionData : ActionData
    {
        public ProjectileActionData projectileAction;
        public int shotsAmount;
        public int damageAmount;
        public TargetType targetType;
        public TargetFilter filter;

        public override void Execute(ActionContext context)
        {
            var source = context.Get(TargetType.This);
            var targets = context.GetAll(targetType, filter);
           
            for (int i = 0; i < shotsAmount; i++)
            {
                var target = targets.Random();

                    if (target != null)
                    {
                        if (projectileAction)
                        {
                            Events.Actions.Projectile?.Invoke(projectileAction, context.thisCard, source.GetOwner().GetTransform() ,target.GetTransform());
                        }

                        target.Damage(damageAmount, true, source);
                    }
                
            }
        }

        public override string GetDescription()
        {
            return $"Damage {damageAmount} damage to {targetType} {shotsAmount} times.";
        }
    }
}
