using DG.Tweening;
using Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Zone : MonoBehaviour 
    {
        //73
        private Vector3 GetPosition(List<Card> cards, Card card)
        {
            var count = cards.Count;
            var index = cards.IndexOf(card);

            var handSize = count * cardSize + (count - 1) * cardInterval;
            var positionX = (cardSize - handSize) / 2f;

            return new Vector3(positionX + (cardSize + cardInterval) * index,0f,0f);
        }

        public List<Creature> GetCreatures()
        {
            var creatures = new List<Creature>();
            foreach(var card in _cards)
            {
                if(card is Creature creature)
                {
                    creatures.Add(creature);
                }
            }
            return creatures;
        }

        public Transform GetTransform() => transform;
        public Card GetFirstCard() => _cards.Count > 0 ? _cards[0] : null;
        public bool IsDeck()=> zoneType == ZoneType.Deck;
        public bool IsBoard() => zoneType == ZoneType.Board;
        public void TurnStarted()
        {
            foreach(var card in _cards)
            {
                card.TurnStarted();
            }
            //
        }
    }
}
