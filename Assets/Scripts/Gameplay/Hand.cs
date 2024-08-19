using Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Hand : MonoBehaviour, ICardHandler
    {
        public float cardSize;
        public float cardInterval;

        public List<Card> _cards = new List<Card>();

        private Player _owner;
        public void Initialize(Player owner)
        {
            _owner = owner;
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
            card.SetStatus(CardStatus.InHand, this);

            foreach(var c in _cards)
            {
                c.UpdatePosition();
            }
        }

        public Vector3 GetPosition(Card card)
        {
            var count = _cards.Count;
            var index = _cards.IndexOf(card);

            var handSize = count * cardSize + (count - 1) * cardInterval;
            var positionX = (cardSize - handSize) / 2f;

            return new Vector3(positionX + (cardSize+cardInterval) * index, 0f, 0f);
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}