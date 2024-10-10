using Gameplay.Data;
using System.Collections.Generic;
using TriggerSystem;

namespace Gameplay
{
    public class Weapon : Card
    {
        private WeaponData _weaponData;
        public override void SetData(CardData data)
        {
            base.SetData(data);
            CardType = CardType.Weapon;
            _weaponData = data as WeaponData;
        }

        public int GetWeaponAttack()
        {
            return _weaponData.attack;
        }
    }
}