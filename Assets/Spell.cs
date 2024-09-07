using Data;
using Gameplay.Data;

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
    }
}
