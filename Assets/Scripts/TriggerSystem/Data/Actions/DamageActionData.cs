using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Actions/Damage")]
    public class DamageActionData : ActionData
    {
        public ProjectileActionData projectileAction;
        public int amount;
        public TargetType type;
        public TargetFilter filter;

        public override void Execute(ActionContext context)
        {
            var source = context.Get(TargetType.This);
            var targets = context.GetAll(type,filter);

            foreach ( var target in targets )
            {
                if(target != null)
                {
                    if (projectileAction)
                    {
                        Events.Actions.Projectile?.Invoke(projectileAction,context.thisCard,target.GetTransform());
                    }

                    target.Damage(amount, true, source);
                }
            }
        }

        public override string GetDescription()
        {
            return $"Damage {amount} damage to {type}";
        }
    }
}
