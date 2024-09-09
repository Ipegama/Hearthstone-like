using UnityEngine;

namespace TriggerSystem.Data.Actions
{
    [CreateAssetMenu(fileName = "Mana", menuName = "Actions/Mana")]
    public class ManaActionData : ActionData
    {
        public int currentMana;
        public int maximumMana;
        public TargetPlayerType targetPlayer;

        public override void Execute(ActionContext context)
        {
            var player = context.Get(targetPlayer);
            player.ChangeMana(currentMana,maximumMana);
        }

        public override string GetDescription()
        {
            return "";
        }
    }
}
