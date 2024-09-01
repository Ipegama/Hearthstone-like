using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "Draw card", menuName = "Actions/DrawCard")]
    public class DrawCardActionData : ActionData
    {
        public int count;
        public TargetPlayerType targetPlayer;

        public override void Execute(ActionContext context)
        {
            var player = context.Get(targetPlayer);
            if(player != null) 
            { 
                for(int i = 0; i < count; i++)
                {
                    player.DrawCard();
                }
            }
        }

        public override string GetDescription()
        {
            return $"Player draw {count} cards.";
        }
    }
}
