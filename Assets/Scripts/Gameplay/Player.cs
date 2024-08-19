using Data;
using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class Player : MonoBehaviour
    {
        public StartingDeckData startingDeckData;
        [SerializeField] private Hand _hand;
        [SerializeField] private Deck _deck;

        private int _mana;
        private int _maximumMana;
        public void Initialize()
        {
            _deck.Initialize(this,startingDeckData);
            _hand.Initialize(this);

            _maximumMana = 1;
            _mana = _maximumMana;
        }

        public void DrawCard()
        {
            var card = _deck.DrawCard();
            _hand.AddCard(card);
        }
    }
}