using Gameplay.Data;
using System.Collections.Generic;
using System.Diagnostics;
using TriggerSystem;

namespace Gameplay
{
    public class Weapon : Card
    {
        private int _weaponAttack;
        private int _weaponDurability;

        private WeaponData _weaponData;
        public override void SetData(CardData data)
        {
            base.SetData(data);
            CardType = CardType.Weapon;
            _weaponData = data as WeaponData;

            _weaponAttack = _weaponData.attack;
            _weaponDurability = _weaponData.durability;
        }

        public int GetWeaponAttack()
        {
            return _weaponAttack;
        }

    }
}