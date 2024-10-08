using Gameplay;
using UnityEngine;
using static Events;

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
                if (target != null)
                {
                    if (projectileAction)
                    {
                        Events.Actions.Projectile?.Invoke(projectileAction, context.thisCard, context.TriggerEntity.GetTransform(),target.GetTransform());
                    }

                    int totalDamage = amount;

                    if (usesSpellPower) 
                    { 
                        totalDamage += GetSpellPower(context.Get(TargetPlayerType.Owner));
                    }

                    target.Damage(totalDamage, true, source);
                }
            }
        }

        public int GetSpellPower(Player player)=> player.GetSpellpower();

        public override string GetDescription()
        {
            return $"Damage {amount} damage to {targetType}";
        }
    }
}
