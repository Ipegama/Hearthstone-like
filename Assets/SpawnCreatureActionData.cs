using TriggerSystem;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "SpawnCreature",menuName = "Actions/SpawnCreature")]
    public class SpawnCreatureActionData : ActionData
    {
        public CreatureData creatureData;
        public TargetPlayerType targetPlayer;

        public override void Execute(ActionContext context)
        {
            var creature = creatureData.Create(context.Get(targetPlayer));
            //creature.owner.board.AddCard(creature);
            Events.Cards.Created?.Invoke(creature);
        }

        public override string GetDescription()
        {
            var team = "you";
            return $"Spawn a {creatureData.cardName} for {team}";
        }
    }
}
