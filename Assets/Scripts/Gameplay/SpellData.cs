using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(menuName = "CardData/Spell")]
    public class SpellData : CardData
    {
        public override Card Create(Player owner)
        {
            var card = Instantiate(CardPrefab);
            card.name = cardName;
            card.transform.localRotation = Quaternion.Euler(90f, 0, 0);

            var spell = card.gameObject.AddComponent<Spell>();
            spell.SetData(this);
            spell.SetOwner(owner);
            return spell;
        }

        public override string GetDescription()
        {
            var desc = "";

            if (playActions.Length > 0)
            {
                desc += playActions[0].GetDescription();
            }

            return desc;
        }
    }
}
