using DG.Tweening;
using Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Zone : MonoBehaviour 
    {
        public float cardSize;
        public float cardInterval;
        public ZoneData containerData;

        private List<Card> _cards = new List<Card>();
        private Player _owner;

        public void Initialize(Player owner)
        {
            _owner = owner;
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
            card.SetZone(this);
            //Do ListExtensions dodac .GetCopy()
            Events.Zones.CardAdded?.Invoke(this, new List<Card>(_cards), card);
            //new List<Card>(_cards) -> _cards.GetCopy()
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
            Events.Zones.CardRemoved?.Invoke(this,new List<Card>(_cards), card);
        }

        public void Shuffle()
        {
            _cards.Shuffle();
        }

        public void UpdateCardsPosition(List<Card> cards, bool animate)
        {
            foreach(var card in cards)
            {
                //UpdateCardPosition(card, this, GetPosition(cards, card), animate);
            }
        }

        private void UpdateCardPosition(Card card, Zone zone, Vector3 position, bool animate)
        {
            var tf = card.transform;
            //tf.SetParent(zone.GetTranform(), true);
            if (animate)
            {
                tf.DOComplete();
                tf.DOLocalMove(position,0.4f);
            }
            else
            {
                tf.localPosition = position;
            }
        }
        //
    }
}
