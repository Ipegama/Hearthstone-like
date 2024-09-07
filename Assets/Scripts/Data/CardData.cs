using UnityEngine;
using TriggerSystem.Data;
using TriggerSystem;
using Gameplay;

namespace Data
{
    [CreateAssetMenu()]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public Sprite cardSprite;

        public int manaCost;

        public TargetFilter targetFilter;

        public ActionData[] playAction;

        private CardUI _cardPrefab;
        public CardUI CardPrefab
        {
            get
            {
                if (_cardPrefab == null)
                {
                    _cardPrefab = Resources.Load<CardUI>("CardPrefab");
                }
                return _cardPrefab;
            }
        }

        public virtual Card Create(Player owner)
        {
            return null;
        }
        public virtual string GetDescription()
        {
            return "";
        }
    }

    public enum CardType
    {
        Creature, Spell
    }
}
