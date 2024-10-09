using DG.Tweening;
using Extensions;
using System.Collections.Generic;
using UnityEngine;
using static Events;

namespace Gameplay
{
    public enum ZoneType
    {
        Deck,
        Hand,
        Board,
        Graveyard
    }

    public class Zone : MonoBehaviour 
    {
        public float cardSize;
        public float cardInterval;

        public ZoneType zoneType;

        private List<Card> _cards = new List<Card>();
        public List<Card> Cards => _cards;

        private Player _owner;
        public Player Owner => _owner;

        public void Initialize(Player owner)
        {
            _owner = owner;
        }

        public void AddCard(Card card) 
        {
            _cards.Add(card);
            card.SetZone(this);
            Events.Zones.CardAdded?.Invoke(this, _cards.GetCopy(),card);
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
            Events.Zones.CardRemoved?.Invoke(this, _cards.GetCopy(), card);
        }

        public void Shuffle()
        {
            _cards.Shuffle();
        }

        public void UpdateCardsPosition(List<Card> cards, bool animate)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                bool isNewCard = (i == cards.Count - 1); 
                UpdateCardPosition(cards[i], this, GetPosition(cards, cards[i]), animate, isNewCard);
            }
        }


        private void UpdateCardPosition(Card card, Zone zone, Vector3 position, bool animate, bool applySpecialAnimation)
        {
            var tf = card.transform;
            var defaultScale = tf.localScale;

            tf.SetParent(zone.GetTransform(), true);
            if (animate)
            {
                tf.DOKill();

                if (applySpecialAnimation)
                {
                    PlaySpecialAnimation(tf, defaultScale, position);
                }
                else
                {
                    PlayStandardAnimation(tf, position);
                }
            }
            else
            {
                tf.localPosition = position;
            }
        }

        private void PlayStandardAnimation(Transform tf, Vector3 position)
        {
            tf.DOLocalMove(position, 0.4f);
        }

        private void PlaySpecialAnimation(Transform tf, Vector3 defaultScale, Vector3 position)
        {
            Vector3 displayPosition = tf.localPosition + new Vector3(-2, 0, 0);

            Sequence seq = DOTween.Sequence();
            seq.Append(tf.DOLocalMove(displayPosition, 0.2f));
            seq.Append(tf.DOScale(defaultScale * 3, 0.3f));
            seq.AppendInterval(1f);
            seq.Append(tf.DOScale(defaultScale, 0.3f));
            seq.Append(tf.DOLocalMove(position, 0.2f));
        }


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
        public bool Contains(Card card)=> Cards.Contains(card);
        public bool AllowsHighligt() => zoneType == ZoneType.Hand;
        public bool AllowsSelection() => zoneType == ZoneType.Hand || zoneType == ZoneType.Board;
        public bool IsDeck()=> zoneType == ZoneType.Deck;
        public bool IsBoard() => zoneType == ZoneType.Board;
        public void TurnStarted()
        {
            foreach(var card in _cards)
            {
                card.TurnStarted();
            }
        }
    }
}
