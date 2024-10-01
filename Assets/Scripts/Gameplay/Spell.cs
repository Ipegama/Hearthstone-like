
using Gameplay.Data;
using System.Collections.Generic;
using TriggerSystem;

namespace Gameplay {
    public class Spell : Card
    {
        private SpellData _spellData;
        public override void SetData(CardData data)
        {
            base.SetData(data);
            CardType = CardType.Spell;
            _spellData = data as SpellData;
        }
        public override void AddBuff(Buff buff)
        {
            _buffs.Add(buff);
            buff.OnApply(this);
        }
        public override void RemoveBuff(Buff buff)
        {
            _buffs.Remove(buff);
            buff.OnRemove(this);
        }

        public void ClearBuffs()
        {
            foreach (var buff in _buffs)
            {
                buff.OnRemove(this);
            }
            _buffs.Clear();
        }
    }
}
