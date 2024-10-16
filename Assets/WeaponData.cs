using TriggerSystem.Data;
using UnityEngine;

namespace Gameplay.Data {

    [CreateAssetMenu(menuName = "CardData/Weapon")]
    public class WeaponData : CardData
    {
        public int attack;
        public int durability;

        public GameTriggerData[] triggers;

        public override Card Create(Player owner)
        {
            var card = Instantiate(CardPrefab);
            card.name = cardName;
            card.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

            var weapon = card.gameObject.AddComponent<Weapon>();
            weapon.SetData(this);
            weapon.SetOwner(owner);
            return weapon;
        }
        public override string GetDescription()
        {
            var desc = "";
            return desc;
        }
    }
}