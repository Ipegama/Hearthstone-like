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
        [SerializeField] private Board _board;

        private Card _highlightedCard;
        private Card _selectedCard;

        public event Action<int, int> ManaChanged;

        private int _mana;
        public int Mana
        {
            get => Mana;
            set
            {
                Mana = value;
                ManaChanged?.Invoke(_mana,_maximumMana);
            }
        }
        private int _maximumMana;
        public int MaximumMana
        {
            get=> _maximumMana;
            set
            {
                _maximumMana = value;
                ManaChanged?.Invoke(_mana, _maximumMana);
            }
        }

        public void Initialize()
        {
            _deck.Initialize(this,startingDeckData);
            _hand.Initialize(this);
            _board.Initialize(this);

            _maximumMana = 1;
            _mana = _maximumMana;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DrawCard();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Select(null);
            }
        }

        public void DrawCard()
        {
            var card = _deck.DrawCard();
            _hand.AddCard(card);
        }

        public void PlayCard(Card card)
        {
            _hand.RemoveCard(card);
            _board.AddCard(card);
        }

        public void PlaySelectedCard()
        {
            if (_selectedCard != null)
            {
                PlayCard(_selectedCard);
                Unhighlight(null);
                Select(null);
            }
        }

        public void Highlight(Card card)
        {
            if (_highlightedCard)
            {
                _highlightedCard.Unhighlight();
            }
            _highlightedCard = card;
            if (_highlightedCard)
            {
                _highlightedCard.Highlight();
            }
        }

        public void Unhighlight(Card card)
        {
            Highlight(null);
        }

        public void Select(Card card)
        {
            if (_selectedCard)
            {
                _selectedCard.Unselect();
            }

            _selectedCard = card;

            if (_selectedCard)
            {
                _selectedCard.Select();
            }
        }
    }
}