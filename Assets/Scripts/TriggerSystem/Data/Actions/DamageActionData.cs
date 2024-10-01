using Gameplay;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Actions/Damage")]
    public class DamageActionData : ActionData
    {
        public ProjectileActionData projectileAction;
        public int amount;
        public TargetType targetType;
        public TargetFilter filter;

        public bool usesSpellPower;

        public override void Execute(ActionContext context)
        {
            var source = context.Get(TargetType.This);
            var targets = context.GetAll(targetType, filter);

            foreach (var target in targets)
            {
                if(target != null)
                {
                    if (projectileAction)
                    {
                        Events.Actions.Projectile?.Invoke(projectileAction,context.thisCard, context.thisCard.transform,target.GetTransform());
                    }

                    if (usesSpellPower) 
                    { 
                        target.Damage(amount + GetSpellPower(context.Get(TargetPlayerType.Owner)), true, source);
                    }
                    else
                    {
                        target.Damage(amount, true, source);
                    }
                }
            }
        }

        public int GetSpellPower(Player player)=> player.GetTotalSpellpower();

        public override string GetDescription()
        {
            return $"Damage {amount} damage to {targetType}";
        }
    }
}
