using Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class Board : MonoBehaviour, ICardHandler, IPointerClickHandler
    {
        public float cardSize;
        public float cardInterval;

        private List<Card> _cards = new List<Card>();

        private Player _owner;

        public void Initialize(Player owner)
        {
            _owner = owner;
        }

        public Vector3 GetPosition(Card card)
        {
            var count = _cards.Count;
            var index = _cards.IndexOf(card);

            var handSize = count * cardSize + (count - 1) * cardInterval;
            var positionX = (cardSize - handSize) / 2f;

            return new Vector3(positionX + (cardSize + cardInterval) * index, 0f, 0f);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
            card.SetStatus(CardStatus.InGame, this);
            UpdateBoardPosition();
        }
        private void UpdateBoardPosition()
        {

            foreach (var c in _cards)
            {
                c.UpdatePosition();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _owner.PlaySelectedCard();
        }
    }
}