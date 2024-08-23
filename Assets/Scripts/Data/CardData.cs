using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

namespace Data
{
    [CreateAssetMenu()]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public Sprite cardSprite;

        public int manaCost;

        public CardType cardType;
        public int attack;
        public int maxHealth;
        

        private Card _cardPrefab;
        public Card cardPrefab
        {
            get
            {
                if(_cardPrefab == null)
                {
                    _cardPrefab = Resources.Load<Card>("CardPrefab");
                }
                return _cardPrefab;
            }
        }
        public Card Create()
        {
            var card = Instantiate(cardPrefab);
            card.SetData(this);
            return card;
        }
    }
}

public enum CardType
{
    Creature,Spell
}
