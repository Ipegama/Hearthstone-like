using Data;
using Extensions;
using Interface;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Deck : MonoBehaviour, ICardHandler
    {
        private List<Card> _cards = new List<Card>();
        private Player _owner;

        public void Initialize(Player owner,StartingDeckData startingDeckData)
        {
            _owner = owner;
            foreach (CardData card in startingDeckData.cards)
            {
                CreateCard(card);
            }
            Shuffle();
        }
        private void CreateCard(CardData cardData)
        {
            var card = cardData.Create();
            card.SetOwner(_owner);
            card.SetStatus(CardStatus.InDeck,this);
            _cards.Add(card);
        }

        private void Shuffle()
        {
            _cards.Shuffle();
        }

        public Vector3 GetPosition()
        {
            return Vector3.zero;
        }

        public Transform GetTransform()
        {
            return transform;
        }

    }
}