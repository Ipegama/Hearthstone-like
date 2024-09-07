using Data;
using TriggerSystem.Data;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu]
    public class CreatureData : CardData
    {
        public int attack;
        public int maxHealth;

        public GameTriggerData[] triggers;

        public override Card Create(Player owner)
        {
            var card = Instantiate(cardPrefab);
            card.name = cardName;
            card.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

            var creature = card.gameObject.AddComponent<Creature>();
            creature.SetData(this);
            creature.SetOwner(owner);
            return creature;
        }

        public override string GetDescription()
        {
            var desc = "";

            if (playActions.Length > 0)
            {
                desc += "<b>Battlecry:</b>" + playActions[0].GetDescription();
            }

            return desc;
        }
    }
}