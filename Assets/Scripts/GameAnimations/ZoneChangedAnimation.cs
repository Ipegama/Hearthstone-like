using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAnimations
{
    public class ZoneChangedAnimation : GameAnimation
    {
        private Zone _zone;
        private List<Card> _cards;
        private Card _card;

        private float _duration;

        public ZoneChangedAnimation(Zone zone, List<Card> cards, Card card, float duration)
        {
            _zone = zone;
            _cards = cards;
            _card = card;
            _duration = duration;
        }

        public override IEnumerator Execute()
        {
            _zone.UpdateCardsPosition(_cards, true);
            _card.UI.UpdateZoneUI(_zone);

            if(_zone.zoneType != ZoneType.Graveyard && _duration > 0f)
            {
                yield return new WaitForSeconds(_duration);
            }
        }

        public override void ExecuteWithoutAnimation()
        {
            _zone.UpdateCardsPosition(_cards,false);
            _card.UI.UpdateZoneUI(_zone);
        }
    }
}